using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using OrientDb.CodeGen;
using OrientDb.Common;
using OrientDb.Mutable;
using OrientDb.OrientApiCommands;
using OrientDb.Queries;
using OrientDb.Readonly;
using RestSharp;
namespace OrientDb
{
    public abstract class Graph : IGraph
    {
        private  ReadonlyGraph _graph;
        private readonly GraphSettings _settings;
        private readonly IRestClient _restClient;
        private OrientConnection _orientConnection;
        private bool _setuped;

        public async Task<OrientConnection> GetConnection()
        {
            if (_orientConnection == null)
                await Connect();
            return _orientConnection;
        }
        
       

        protected Graph(GraphSettings settings)
        {
            _settings = settings;
            _restClient = new RestClient().UseSerializer<RestSerializer>();
        }

        public async Task Connect()
        {
            var connectionSettings = new OrientApiConnectionSettings(_settings);
            var connectCommand =  new Connect(connectionSettings,_restClient);
            var batchResult = await connectCommand.Execute();
            _orientConnection = batchResult switch
            {
                Result<OrientConnection, Exception>.OkCase ok => ok.Payload,
                Result<OrientConnection, Exception>.ErrCase err => throw err.Payload,
                _ => throw new InvalidOperationException()
            };
        }

        public async Task Setup(bool force = false)
        {
            var connection = await GetConnection();
            if (!_setuped)
            {
                var setup = new GraphSetup(connection, _restClient);
                Setup(setup);
                _graph = new ReadonlyGraph(setup);
            }

            if (!_setuped || force)
                if (_settings.Mode == Mode.WriteThrough)
                    await _graph.Prepare();

            _setuped = true;
        }

        protected virtual void Setup(GraphSetup setup)
        {
            
        }

        public async Task<IEnumerable<OrientTypeScheme>> ReadScheme()
        {
            var query = new ReadSchemeQuery();
            var batchQuery = new OrientQueryBatch(await GetConnection(),query.Query);
            var batch = new Batch<List<OrientTypeScheme>>(batchQuery,_restClient);
            var batchResult = await batch.Execute();
            var result = batchResult switch
            {
                Result<List<OrientTypeScheme>, Exception>.OkCase ok => ok.Payload,
                Result<List<OrientTypeScheme>, Exception>.ErrCase err => throw err.Payload,
                _ => throw new InvalidOperationException()
            };
            return result;
        }

        public IEnumerable<T> V<T>() where T : V => _graph.V<T>();
        public IEnumerable<T> E<T>() where T : E => _graph.E<T>();

        public IMutableGraph Mutate() => new MutableGraph(_graph);
    }
}