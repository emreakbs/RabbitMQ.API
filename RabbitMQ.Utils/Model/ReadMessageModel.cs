using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Utils.Model
{
    public class ReadMessageModel
    {
        [JsonProperty]
        public string Message { get; set; }
        [JsonProperty]
        public string QueueName { get; set; }

        [JsonProperty]
        public DateTime CreatedDateTime { get; set; }
    }
}
