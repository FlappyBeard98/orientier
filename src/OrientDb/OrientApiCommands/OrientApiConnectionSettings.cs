namespace OrientDb.OrientApiCommands
{
    public class OrientApiConnectionSettings
    {
        public string Server { get; }
        public string Database { get; }
        public string Username { get; }
        public string Password { get; }
        public int Port { get; }

        public OrientApiConnectionSettings(string server, string database, int port, string username, string password)
        {
            Server = server;
            Database = database;
            Username = username;
            Password = password;
            Port = port;
        }

        public OrientApiConnectionSettings(GraphSettings graphSettings) 
            : this(graphSettings.Server,graphSettings.Database,graphSettings.Port,graphSettings.Username,graphSettings.Password)
        {
        }

        public override string ToString() => $"{Server} | {Database} | {Port} | {Username} | {Password}";
    }
}