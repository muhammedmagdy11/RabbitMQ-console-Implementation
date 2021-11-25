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
   static class QueueProducer
    {
        public static void publish(IModel channel)
        {
            channel.QueueDeclare("myqueue", durable: true, exclusive: false, autoDelete: false, arguments: null);
            var count = 0;
            while(true)
            {
                var message = new { Name = "Magdy", Message = $"Hello There count:{count}" };
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
                channel.BasicPublish("", "myqueue", null, body);
                count++;
                Thread.Sleep(1000);
            }
           
        }
    }
}
