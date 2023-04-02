namespace Yatt.MessageBroker.Interfaces
{
    //public interface IMessageProducer<T> where T : class
    //{
    //    Task SendMessage(T message, string routKey);
    //}
    public interface IMessageProducer
    {
        Task SendMessage<T>(T message);
    }
}