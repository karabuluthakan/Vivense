namespace Library.Utilities.AppSettings
{
    public class RedisSettings 
    {
        public string ConnectionString { get; }
        public string InstanceName { get; }

        public const string ConnectionStringKey = nameof(ConnectionString);
        public const string InstanceNameKey = nameof(InstanceName);
    }
}