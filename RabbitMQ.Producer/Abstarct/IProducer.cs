using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Producer.Abstarct
{
    public interface IProducer
    {
        bool Publish(string queueName, object message);
    }
}
