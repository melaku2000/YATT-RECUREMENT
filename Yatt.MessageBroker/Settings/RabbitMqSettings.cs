namespace Yatt.MessageBroker.Settings
{
    public class RabbitMqSettings
    {
        public string HostName { get; init; } = "";
        public string UserName { get; init; } = "";
        public string Password { get; init; } = "";
        public string VertualHost { get; init; } = ""; 
    }
}
