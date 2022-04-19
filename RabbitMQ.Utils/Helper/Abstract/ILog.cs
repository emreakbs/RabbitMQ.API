using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Utils.Helper.Abstract
{
    public interface ILog
    {
        void Error(Exception ex, string messageTemplate);
        void Info(string messageTemplate);
    }
}
