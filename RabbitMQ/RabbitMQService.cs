using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMQ
{
    public class RabbitMQService
    {
        // RabbitMQ服务器连接
        public static ConnectionFactory Connection;
        static RabbitMQService()
        {
            Connection = new ConnectionFactory()
            {
                HostName = "127.0.0.1",
                Port = 5672,
                //UserName = "admin",
                //Password = "admin123",
            };
        }
    }
}
