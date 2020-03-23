using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OrientDb.Common;
using OrientDb.OrientApiCommands;
using OrientDb.Queries;
using OrientDb.Readonly;

namespace OrientDb.Mutable
{
    internal class MutableGraph : IMutableGraph
    {
        private readonly ReadonlyGraph _graph;
        private readonly List<IGraphMutation> _mutations;
        private readonly IdentifierFactory _factory;

        public MutableGraph(ReadonlyGraph graph)
        {
            _graph = graph;
            _mutations = new List<IGraphMutation>();
            _factory = new IdentifierFactory();
        }

        public IEnumerable<T> V<T>() where T : V => _graph.V<T>();
        public IEnumerable<T> E<T>() where T : E => _graph.E<T>();

        public void Dispose()
        {
            Commit();
        }

        private class ReturnResult
        {
            [JsonProperty("value")] public List<List<string>> Value { get; set; }
        }

        public void Commit()
        {
            CommitInternal().ConfigureAwait(false).GetAwaiter().GetResult();
        }
        public async Task CommitInternal()
        {
            if(!_mutations.Any())
                return;
            
            var queries = _mutations.Select(p => p.Query).ToList();
            var ret = new Return(queries.OfType<Let>().ToArray());
            queries.Add(ret);
            var batch = new OrientQueryBatch(_graph.Setup.Connection, queries.ToArray());
            var command = new Batch<List<ReturnResult>>(batch, _graph.Setup.Client);
            var result = (await command.Execute()) switch
            {
                Result<List<ReturnResult>, Exception>.OkCase o => o
                                                                  .Payload.SelectMany(p => p.Value)
                                                                  .ToDictionary(p => p[0], p => p[1]),
                Result<List<ReturnResult>, Exception>.ErrCase e => throw e.Payload,
                _ => throw new InvalidCastException()
            };
            Debug.WriteLine("database -> ok");
            foreach (var mutation in _mutations.OrderBy(p => p.Idx))
            {
                await mutation.RefreshItem(result, _graph.Setup.Connection, _graph.Setup.Client);
                mutation.Apply(_graph);
            }

            Debug.WriteLine("graph -> ok");
            _mutations.Clear();
        }

        public void CreateVertex<T>(T item) where T : V
        {
            var mutation = new CreateVertexMutation<T>(item, _factory);
            _mutations.Add(mutation);
        }

        public ISetter<T> UpdateEdge<T>(T item) where T : E
        {
            var mutation = new UpdateEdgeMutation<T>(item, _factory);
            _mutations.Add(mutation);
            return new Setter<T>(mutation);
        }

        public ISetter<T> UpdateVertex<T>(T item) where T : V
        {
            var mutation = new UpdateVertexMutation<T>(item, _factory);
            _mutations.Add(mutation);
            return new Setter<T>(mutation);
        }

        public void DeleteEdge<T>(T item) where T : E
        {
            if (item == null)
                return;
            var mutation = new DeleteEdgeMutation<T>(item, _factory);
            _mutations.Add(mutation);
        }

        public void DeleteVertex<T>(T item) where T : V
        {
            if (item == null)
                return;
            var mutation = new DeleteVertexMutation<T>(item, _factory);
            _mutations.Add(mutation);
        }

        public void CreateEdge<TE, TOut, TIn>(TOut @from, TE edge, TIn @to)
            where TOut : V where TIn : V where TE : E<TOut, TIn>
        {
            var mutation = new CreateEdgeMutation<TE>(edge, @from, @to, _mutations, _factory);
            _mutations.Add(mutation);
        }
        

    }
}