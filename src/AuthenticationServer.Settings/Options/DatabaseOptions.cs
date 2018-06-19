namespace AuthenticationServer.Settings.Options
{
    public class DatabaseOptions
    {
        public string ConnectionString { get; set; }
        public bool InMemory { get; set; }
        public string Database { get; set; }
        public string Type { get; set; }
    }
}