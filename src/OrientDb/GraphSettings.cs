namespace OrientDb
{
    public class GraphSettings
    {
        public string Server { get; }
        public string Database { get; }
        public string Username { get; }
        public string Password { get; }
        public int Port { get; }
        public Mode Mode { get; }

        public GraphSettings( Mode mode,string server, string database, int port, string username, string password)
        {
            Server = server;
            Database = database;
            Port = port;
            Username = username;
            Password = password;
            Mode = mode;
        }
    }
}