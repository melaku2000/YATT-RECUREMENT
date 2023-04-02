namespace YattCommon.Settings
{
    public class MongoDbSetting
    {
        public string? Host { get; set; }
        public string? Port { get; set; }
        public string ConnectionName => $"mongodb://{Host}:27017/{Port}";
    }
}
