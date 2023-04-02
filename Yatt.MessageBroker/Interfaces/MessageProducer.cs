using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
using System.Threading.Channels;

namespace Yatt.MessageBroker.Interfaces
{
    public class MessageProducer : IMessageProducer
    {
        //private readonly IModel _chanel;
        //public MessageProducer(IModel chanel)
        //{
        //    //_chanel= chanel;
        //}
        public async Task SendMessage<T>(T message)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare("orders");

            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);
            //_chanel.BasicPublish(exchange: "", routingKey: "orders", body: body);
            channel.BasicPublish(exchange: "", routingKey: "identity", body: body);
        }
    }
}