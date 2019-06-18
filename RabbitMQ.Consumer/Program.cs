using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            //string type = "simple"; // 简单队列
            string type = "work"; // 工作队列

            var rabbitMQService = RabbitMQFactory.CreateRabbitMQService(type);
            rabbitMQService.Receive();

            Console.ReadKey();
        }
    }
}
