using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ
{
    /// <summary>
    /// Publish-Fanout队列
    /// </summary>
    public class RabbitMQFanoutService : IRabbitMQ
    {
        public void Publish(string message)
        {
            using (var connection = RabbitMQService.Connection.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    // 声明交换机
                    channel.ExchangeDeclare("logs", "fanout");

                    // 发送消息到队列
                    var body = Encoding.UTF8.GetBytes(message);
                    channel.BasicPublish("logs", "", null, body);

                    Console.WriteLine("x Sent {0}", message);
                }
            }
        }

        public void Receive()
        {
            using (var connection = RabbitMQService.Connection.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    // 声明交换机 （交换机名需要和生产者交换机名保持一致）
                    channel.ExchangeDeclare("logs", "fanout");

                    var queueName = channel.QueueDeclare().QueueName;
                    channel.QueueBind(queueName, "logs", "");

                    Console.WriteLine(" [*] Waiting for logs.");

                    // 接收消息
                    var consumer = new EventingBasicConsumer(channel);

                    consumer.Received += (model, eq) =>
                    {
                        var body = eq.Body;
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine("y Received {0}", message);
                    };

                    channel.BasicConsume(queueName, true, consumer);
                }
            }
        }
    }
}
