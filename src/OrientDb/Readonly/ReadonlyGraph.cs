using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace OrientDb.Readonly
{
    internal class ReadonlyGraph : IGraph
    {
        private bool _preparing;
        internal readonly GraphSetup Setup;


        internal Dictionary<Type,List<GraphItem>> GraphItems = new Dictionary<Type, List<GraphItem>>(); 
        
        public ReadonlyGraph(GraphSetup setup)
        {
            Setup = setup;
        }

        private IEnumerable<T> Get<T>() where T : GraphItem
        {
            var result =
                GraphItems.TryGetValue(typeof(T), out var items)
                    ? items
                    : GraphItems.Values.SelectMany(p => p);
            return result.OfType<T>();  
        }

        public IEnumerable<T> V<T>() where T : V => Get<T>();

        public IEnumerable<T> E<T>() where T : E => Get<T>();

        public GraphItem FindById(string rid)
        {
            var result = GraphItems
                         .Select(p => p.Value.FirstOrDefault(q => q.Rid == rid))
                         .FirstOrDefault(p=>p!=null);

            return result;
        }


        public async Task Prepare()
        {
            if(_preparing)
                return;
            
            lock (typeof(ReadonlyGraph))
                _preparing = true;
            
            await Download();
            Link();
            ExecuteActions();

            _preparing = false;
        }

        private void ExecuteActions()
        {
            Setup.AfterSetupActions.ForEach(p=>p(this));
        }

        private async Task Download()
        {
            var items = new Dictionary<Type, List<GraphItem>>();
            foreach (var (type,download) in Setup.Downloads)
            {
                var data = await download;
                items.Add(type,data);
            }

            GraphItems = items;
        }


        private void Link()
        {
            Dictionary<string, IGrouping<string, E>> GetEdges(Func<E, string> direction) =>
                GraphItems.Values
                           .AsParallel()
                           .SelectMany(p => p.OfType<E>())
                           .GroupBy(direction)
                           .ToDictionary(p => p.Key);
            
            var edgesIn = GetEdges(p=>p.ToRid);
            var edgesOut = GetEdges(p=>p.FromRid);
            
            
            void Link(V vertex)
            {
                vertex.InE = edgesIn.TryGetValue(vertex.Rid,out var lin)? lin.ToList() : new List<E>();
                vertex.InE.ForEach(p=>p.ToV = vertex); 
                vertex.OutE =  edgesOut.TryGetValue(vertex.Rid,out var lout)? lout.ToList() : new List<E>();
                vertex.OutE.ForEach(p=>p.FromV = vertex);
            }

            GraphItems.Values
                       .AsParallel()
                       .WithExecutionMode(ParallelExecutionMode.ForceParallelism)
                       .SelectMany(p => p.OfType<V>())
                       .ForAll(Link);
            
        }
    }
}