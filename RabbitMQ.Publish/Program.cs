using RabbitMQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mq
{
    class Program
    {
        static void Main(string[] args)
        {
            //string type = "simple"; // 简单队列
            string type = "work"; // 工作队列

            string message = "a.b.c.d.e.f!";

            var rabbitMQService = RabbitMQFactory.CreateRabbitMQService(type);
            rabbitMQService.Publish(message);

            Console.ReadLine();
        }
    }
}
