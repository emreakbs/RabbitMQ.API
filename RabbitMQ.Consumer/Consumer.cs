using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Consumer.Abstract;
using RabbitMQ.Utils.Helper;
using RabbitMQ.Utils.Helper.Abstract;
using RabbitMQ.Utils.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Consumer
{
    public class Consumer : IConsumer
    {
        #region Property

        /// <summary>
        /// Rabbitmq bağlantısı getirmek için oluşturulan class
        /// </summary>
        private readonly IConnectionHelper _connectionHelper;

        /// <summary>
        /// Modeli dinlemek için kullanıclan event
        /// </summary>
        private EventingBasicConsumer eventingBasicConsumer;

        private readonly ILog _log;

        /// <summary>
        /// Rabbitmq bağlantısı almak için
        /// </summary>

        #endregion  
        public Consumer(ILog log, IConnectionHelper connectionHelper)
        {
            _connectionHelper = connectionHelper;
            _log = log;
        }

        public void Consume(params string[] queue)
        {
            try
            {
                for (int i = 0; i < queue.Length; i++)
                {
                    if (string.IsNullOrEmpty(queue[i]))
                    {
                        _log.Info("Consumer QueueName was null");
                    }
                    IModel channel = _connectionHelper.GetChannel(queue[i].ToLower());
                    eventingBasicConsumer = new EventingBasicConsumer(channel);
                    eventingBasicConsumer.Received += EventingBasicConsumerOnReceived;
                    channel.BasicConsume(queue[i].ToLower(), true, eventingBasicConsumer);
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex, "RabbitMQ.Consumer/Consumer/Consumer");
            }
        }

        #region Private Method

        /// <summary>
        ///kuruktaki mesaj düştükten sonra çalışan metot
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EventingBasicConsumerOnReceived(object sender, BasicDeliverEventArgs e)
        {
            try
            {
                ReadMessageModel model = JsonConvert.DeserializeObject<ReadMessageModel>(Encoding.UTF8.GetString(e.Body.ToArray()));
                _log.Info($"Consumer/EventingBasicConsumerOnReceived | Date: {model.CreatedDateTime} | Queue: {model.QueueName}  | Message: {model.Message.ToString()}  ");
            }
            catch (Exception exception)
            {
                _log.Error(exception, "RabbitMQ.Consumer/Consumer/EventingBasicConsumerOnReceived");
            }

        }

        #endregion
    }
}
