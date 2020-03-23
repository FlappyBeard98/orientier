using System.Linq;
using OrientDb.Queries;

namespace OrientDb.OrientApiCommands
{
    public class OrientQueryBatch
    {
        private readonly OrientSqlExpr[] _queries;

        public OrientQueryBatch(OrientConnection orientConnection,params OrientSqlExpr[] queries)
        {
            _queries = queries;
            Server = orientConnection.Server;
            Database = orientConnection.Database;
            Port = orientConnection.Port;
            SessionId = orientConnection.SessionId;
        }
        

        public string Server { get; }
        public string Database { get; }
        public int Port { get; }
        public string SessionId { get; }
        public override string ToString() => $"{Server} | {Database} | {Port} | {SessionId} | {string.Join(";",GetSqlStatements())}";
        
        public string[] GetSqlStatements() => _queries.Select(p=>p.Sql).ToArray();
    }
}