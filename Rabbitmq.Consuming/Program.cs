using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rabbitmq.Consuming
{
    /// <summary>
    /// 消息的消费者
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            MQConsuming.ReadMqMsg();
        }
    }
}
