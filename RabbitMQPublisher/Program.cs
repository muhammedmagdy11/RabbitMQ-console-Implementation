using RabbitMQ.Client;
using System;
using System.Text;


using Newtonsoft.Json;

namespace RabbitMQPublisher
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory {Uri=new Uri("AMQP://guest:guest@localhost:5672") };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            DirectExchangePublisher.Publish(channel);
            Console.ReadLine();
        }
    }
}
