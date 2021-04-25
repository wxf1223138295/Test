using System;
using System.Text;
using RabbitMQ.Client;

namespace TestMqRabbit
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "115.159.155.126",UserName = "admin",Password = "admin",VirtualHost = "my_vhost",Port = 30011};
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(exchange: "TestMqExchange", type: "direct");

                    channel.QueueDeclare(queue: "TestMq",
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);


                    channel.QueueBind("TestMq", "TestMqExchange", "TestMq1");
                    Console.WriteLine("\nRabbitMQ连接成功，请输入消息，输入exit退出！");

                    channel.ConfirmSelect();
                    channel.BasicAcks += Channel_BasicAcks;
                    channel.BasicNacks += Channel_BasicNacks;


                    string input;
                    do
                    {
                        input = Console.ReadLine();
                        var sendBytes = Encoding.UTF8.GetBytes(input);
                        var properties = channel.CreateBasicProperties();
                        properties.DeliveryMode = 2;

                        channel.BasicPublish(exchange: "TestMqExchange",
                            routingKey: "TestMq1",
                            basicProperties: null,
                            body: sendBytes);


                        Console.WriteLine(" [x] Sent {0}", input);
                    } while (input.Trim().ToLower() != "exit");


                }
            }
        }

        private static void Channel_BasicNacks(object sender, RabbitMQ.Client.Events.BasicNackEventArgs e)
        {
            Console.WriteLine($"发送失败{e.DeliveryTag}----{sender.ToString()}");
        }

        private static void Channel_BasicAcks(object sender, RabbitMQ.Client.Events.BasicAckEventArgs e)
        {
            Console.WriteLine($"发送成功{e.DeliveryTag}----{sender.ToString()}");
        }
    }
}
