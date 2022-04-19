using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Producer.Abstarct;
using RabbitMQ.Utils.Helper;
using RabbitMQ.Utils.Helper.Abstract;
using RabbitMQ.Utils.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Producer
{
    public class Producer : IProducer
    {
        #region Properties

        /// <summary>
        /// Rabbitmq bağlantısı getirmek için oluşturulan class
        /// </summary>
        private readonly IConnectionHelper _connectionHelper;
        private readonly ILog _log;
        #endregion
        public Producer(ILog log, IConnectionHelper connectionHelper)
        {
            _log = log;
            _connectionHelper = connectionHelper;
        }
        #region Methods

        /// <summary>
        /// Aldığı mesajı aldığı kuyruğa yazar
        /// </summary>
        /// <param name="queueName">kuyruk adı</param>
        /// <param name="message">mesaj</param>
        public bool Publish(string queueName, object message)
        {
            bool result = false;
            try
            {
                if (string.IsNullOrEmpty(queueName))
                {
                    _log.Info("QueueName was null");
                }
                using IModel channel = _connectionHelper.GetChannel(queueName);

                channel.BasicPublish(string.Empty, queueName, null, Encoding.UTF8.GetBytes(
                    JsonConvert.SerializeObject(new ReadMessageModel
                    {
                        CreatedDateTime = DateTime.Now,
                        Message = message.ToString(),
                        QueueName = queueName.ToString()
                    })));
                _log.Info($"Publish | Date{DateTime.Now} | Queue: {queueName} | Message: {message}");
                result = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                _log.Error(ex, "RabbitMQ.Producer/Publish/Publish");
            }
            return result;
        }

        #endregion        
    }
}
