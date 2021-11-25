using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RabbitMQPublisher
{
    static class DirectExchangePublisher
    {
        public static void Publish(IModel channel)
        {
            var ttl = new Dictionary<string, object>() { { "ttl-message", 30000 } };
            channel.ExchangeDeclare("my-direct-exchange",ExchangeType.Direct,arguments:ttl);
          

            var count = 0;
            while (true)
            {
                var message = new { Name = "Magdy", Message = $"Hello There count:{count}" };
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
                channel.BasicPublish("my-direct-exchange", "account.init", null, body);
                count++;
                Thread.Sleep(1000);
            }
        }
    }
}
