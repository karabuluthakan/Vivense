namespace Library.Utilities.AppSettings
{
    public class MongoDbSettings
    {
        public string ConnectionString { get; set; }
        public string DefaultDatabase { get; set; }

        public const string ConnectionStringKey = nameof(ConnectionString);
        public const string DefaultDatabaseKey = nameof(DefaultDatabase);
    }
}