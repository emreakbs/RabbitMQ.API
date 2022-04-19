using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Utils.Helper.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Utils.Helper
{
    public class ConnectionHelper : IConnectionHelper
    {
        public IConnection Connection { get; set; }
        public bool IsConnected { get; set; } = false;
        private static readonly object _lockObj = new();
        private readonly IConfiguration _configuration;

        private readonly ILog _log;
        public ConnectionHelper(IConfiguration configuration, ILog log)
        {
            _configuration = configuration;
            _log = log;
        }

        /// <summary>
        /// Rabbitmq bağlantısı oluşturup döner
        /// </summary>
        /// <returns></returns>
        public IConnection GetConnection()
        {
            if (IsConnected)
                return Connection;
            try
            {
                lock (_lockObj)
                {
                    if (IsConnected)
                        return Connection;
                    ConnectionFactory connectionFactory = new ConnectionFactory
                    {
                        Uri = new Uri(_configuration.GetSection("RabbitMQ:HostName").Value)
                    };
                    Connection = connectionFactory.CreateConnection();
                    IsConnected = true;
                    return Connection;
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex, "RabbitMQ.Manager/MessageService/GetConnection");
                return null;
            }
        }

        /// <summary>
        /// Channel oluşturup döner(IModel Threadsafety çalıştiği için böyle düzenlendi)
        /// </summary>
        /// <param name="queueName"></param>
        /// <returns></returns>
        public IModel GetChannel(string queueName)
        {
            lock (_lockObj)
            {
                if (!IsConnected)
                {
                    ConnectionFactory connectionFactory = new ConnectionFactory
                    {
                        Uri = new Uri($"amqp://{_configuration.GetSection("RabbitMQ:UserName").Value}:{_configuration.GetSection("RabbitMQ:Password").Value}@{_configuration.GetSection("RabbitMQ:HostName").Value}:{_configuration.GetSection("RabbitMQ:Port").Value}")
                    };
                    Connection = connectionFactory.CreateConnection();
                    IsConnected = true;
                }
            }
            IModel channel = Connection.CreateModel();
            channel.QueueDeclare(queueName, false, false, true, null);

            return channel;
        }
    }
}
