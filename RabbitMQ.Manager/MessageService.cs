using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Consumer.Abstract;
using RabbitMQ.Manager.Abstract;
using RabbitMQ.Producer.Abstarct;
using RabbitMQ.Utils.Helper.Abstract;

namespace RabbitMQ.Manager
{
    public class MessageService : IMessageService
    {
        private readonly IConsumer _consumer;
        private readonly IProducer _producer;
        public MessageService(IConsumer consumer, IProducer producer)
        {
            _consumer = consumer;
            _producer = producer;
            _consumer.Consume("Queue1", "Queue2", "Queue3");
        }

        #region Method



        public bool PublishMessage(string queueName, object message)
        {
            return _producer.Publish(queueName.ToLower(), message);
        }
        #endregion 
    }
}
