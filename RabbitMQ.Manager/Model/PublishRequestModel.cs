using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Manager.Model
{
    public class PublishRequestModel
    {
        [JsonProperty("queue")]
        public string Queue { get; set; }
        
        [JsonProperty("message")]
        public object Message { get; set; }
    }
}
