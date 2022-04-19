using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Utils.Helper.Abstract
{
    public interface IConnectionHelper
    {
        IConnection GetConnection();
        IModel GetChannel(string queueName);
    }
}
