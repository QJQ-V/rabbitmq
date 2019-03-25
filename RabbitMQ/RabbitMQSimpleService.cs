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
    /// 简单队列
    /// </summary>
    public class RabbitMQSimpleService : IRabbitMQ
    {
        private static string _queueName = "queue_simple";
        public void Publish(string message)
        {
            using (var connection = RabbitMQService.Connection.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    // 声明队列
                    channel.QueueDeclare(_queueName, false, false, false, null);

                    // 发送消息到队列
                    var body = Encoding.UTF8.GetBytes(message);
                    channel.BasicPublish("", _queueName, null, body);

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
                    // 声明队列
                    channel.QueueDeclare(_queueName, false, false, false, null);

                    // 定义队列中的消费者
                    var consumer = new EventingBasicConsumer(channel);

                    consumer.Received += (model, eq) =>
                    {
                        var body = eq.Body;
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine("y Received {0}", message);
                    };

                    // 监听队列
                    channel.BasicConsume(_queueName, true, consumer);
                }
            }
        }
    }
}
