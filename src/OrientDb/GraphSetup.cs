using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrientDb.Common;
using OrientDb.OrientApiCommands;
using OrientDb.Queries;
using OrientDb.Readonly;
using RestSharp;

namespace OrientDb
{
    public class GraphSetup
    {
        public OrientConnection Connection { get; }
        public IRestClient Client { get; }

        public GraphSetup(OrientConnection connection, IRestClient restClient)
        {
            Connection = connection;
            Client = restClient;
        }

        internal Dictionary<Type,Task<List<GraphItem>>> Downloads { get; } = 
            new Dictionary<Type,Task<List<GraphItem>>>();
        internal List<Action<IGraph>> AfterSetupActions { get; }=
            new List<Action<IGraph>>();

        public GraphSetup Add<T>() where T : GraphItem
        {
            async Task<List<GraphItem>> Download()
            {
                var query = new ReadAllQuery<T>();
                var batchQuery = new OrientQueryBatch(Connection,query.Query);
                var batch = new Batch<List<T>>(batchQuery,Client);
                var batchResult = await batch.Execute();
                var result =  batchResult switch
                {
                    Result<List<T>, Exception>.OkCase ok => ok.Payload,
                    Result<List<T>, Exception>.ErrCase err => throw err.Payload,
                    _ => throw new InvalidOperationException()
                };
                return result.Cast<GraphItem>().ToList();
            }
            Downloads.Add(typeof(T),Download());
            return this;
        }

        public GraphSetup AfterSetup(Action<IGraph> action) 
        {
            AfterSetupActions.Add(action);
            return this;
        }
    }
}