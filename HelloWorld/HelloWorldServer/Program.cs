﻿using RabbitMQ.Client;
using System;
using System.Text;

namespace HelloWorld
{
    /// <summary>
    ///     RabbitMq test
    /// 
    ///     This console application will send messages typed into the console to the message queue.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Send();
        }

        static void Send()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "hello",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                Console.WriteLine("Type a message to RabbitMq. Type 'quit' to exit...");
                
                while(true)
                {
                    var message = Console.ReadLine();
                    if (message == "quit") return;

                    var body = Encoding.UTF8.GetBytes(message);
                    channel.BasicPublish(exchange: "",
                                         routingKey: "hello",
                                         basicProperties: null,
                                         body: body);
                    Console.WriteLine(" [x] Sent {0}", message);
                }
            }
        }
    }
}
