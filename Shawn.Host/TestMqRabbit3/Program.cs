using System;
using System.Text;
using System.Threading;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace TestMqRabbit3
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "115.159.155.126", UserName = "admin", Password = "admin", VirtualHost = "my_vhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                //channel.ExchangeDeclare(exchange: "TestMqExchange", type: "direct");

                //channel.QueueDeclare(queue: "TestMq",
                //    durable: false,
                //    exclusive: false,
                //    autoDelete: false,
                //    arguments: null);

                channel.QueueBind("TestMq", "TestMqExchange", "TestMq1");

                Console.WriteLine(" [*] Waiting for logs.");



                var consumer = new EventingBasicConsumer(channel);


                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Thread.Sleep(1000);
                    Console.WriteLine(" [x] {0}", message);

                      channel.BasicAck(deliveryTag:ea.DeliveryTag, multiple: false);

                
                };

                

                channel.BasicConsume(queue: "TestMq",
                    autoAck: false,
                    
                    consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }

        private static void Channel_BasicAcks(object sender, BasicAckEventArgs e)
        {
            var DeliveryTag = e.DeliveryTag;

            var ismuldia = e.Multiple;

            Console.WriteLine(DeliveryTag);
        }
    }
}
