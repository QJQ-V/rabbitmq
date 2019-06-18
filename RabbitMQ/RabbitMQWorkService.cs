using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RabbitMQ
{
    /// <summary>
    /// 工作队列
    /// </summary>
    public class RabbitMQWorkService : IRabbitMQ
    {
        private static string _queueName = "queue_work";
        public void Publish(string message)
        {
            using (var connection = RabbitMQService.Connection.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    // 声明队列（设置durable=true 来开启数据持久化到本地防止丢失）
                    channel.QueueDeclare(_queueName, true, false, false, null);

                    // 也是标识数据持久化？
                    var properties = channel.CreateBasicProperties();
                    properties.Persistent = true;

                    // 发送消息到队列
                    var body = Encoding.UTF8.GetBytes(message);
                    channel.BasicPublish("", _queueName, properties, body);

                    Console.WriteLine("x Sent {0}", message);
                    Console.ReadLine();
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
                    channel.QueueDeclare(_queueName, true, false, false, null);

                    // 一个消费者同时只消费一个，防止不合理分配浪费资源
                    channel.BasicQos(0, 1, false);

                    // 定义队列中的消费者
                    var consumer = new EventingBasicConsumer(channel);

                    consumer.Received += (model, eq) =>
                    {
                        var body = eq.Body;
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine("y Received {0}", message);

                        int dots = message.Split('.').Length - 1;
                        Thread.Sleep(dots * 1000);

                        Console.WriteLine("y Done");

                        // 消费完之后反馈给消息队列中心
                        channel.BasicAck(eq.DeliveryTag, false);
                    };

                    // 监听队列
                    channel.BasicConsume(_queueName, true, consumer);

                    Console.WriteLine("持续消费中...");
                    Console.ReadLine();
                }
            }
        }
    }
}
