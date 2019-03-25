using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ
{
    public class RabbitMQFactory
    {
        public static IRabbitMQ CreateRabbitMQService(string rabbitType)
        {
            if (String.IsNullOrEmpty(rabbitType))
            {
                return new RabbitMQSimpleService();
            }
            switch (rabbitType.ToLower())
            {
                case "simple":
                    return new RabbitMQSimpleService();
                case "work":
                    return new RabbitMQWorkService();
                default:
                    return new RabbitMQSimpleService();
            }
        }
    }
}
