using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Manager.Abstract
{
    public interface IMessageService
    {

        bool PublishMessage(string queueName, object message);
    }
}
