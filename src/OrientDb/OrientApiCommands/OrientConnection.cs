namespace OrientDb.OrientApiCommands
{
    public class OrientConnection
    {
        public OrientConnection(string server, string database, int port, string sessionId)
        {
            Server = server;
            Database = database;
            Port = port;
            SessionId = sessionId;
        }

        public string Server { get; }
        public string Database { get; }
        public int Port { get; }
        public string SessionId { get; }
        public override string ToString() => $"{Server} | {Database} | {Port} | {SessionId}";
    }
}