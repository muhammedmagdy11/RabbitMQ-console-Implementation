using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consumer
{
    static class DirectExchangeConsumer
    {
        public static void Consume(IModel channel)
        {
            channel.ExchangeDeclare("my-direct-exchange", ExchangeType.Direct);
            channel.QueueDeclare("my-direct-queue", durable: true, exclusive: false, autoDelete: false, arguments: null);
            channel.QueueBind("my-direct-queue", "my-direct-exchange", "account.init");
            channel.BasicQos(0, 10, false);
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
            };
            channel.BasicConsume("my-direct-queue", true, consumer);
            Console.WriteLine("Consumer Started");
        }
    }
}
