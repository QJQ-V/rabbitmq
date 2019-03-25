using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ
{
    public interface IRabbitMQ
    {
        void Publish(string message);

        void Receive();
    }
}
