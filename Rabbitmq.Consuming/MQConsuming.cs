using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Rabbitmq.Consuming
{
    /// <summary>
    /// MQ消息消费者
    /// </summary>
    public static class MQConsuming
    {
        public static void ReadMqMsg()
        {
            ConfigRabMq config = GetMqConfig();

            var factory = new ConnectionFactory();
            factory.HostName = config.HostName;//连接地址
            factory.UserName = config.UserName;//账号
            factory.Password = config.PassWord;//密码
            if (config.Port > 0)
            {
                factory.Port = config.Port;//端口
            }

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    //在MQ上定义一个持久化队列，如果名称相同不会重复创建
                    channel.QueueDeclare(config.QueueName, true, false, false, null);

                    //输入1，那如果接收一个消息，但是没有应答，则客户端不会收到下一个消息
                    channel.BasicQos(0, 1, false);

                    //在队列上定义一个消费者
                    var consumer = new QueueingBasicConsumer(channel);

                    //消费队列，并设置应答模式为程序主动应答
                    channel.BasicConsume(config.QueueName, true, consumer);

                    Console.WriteLine(" waiting for message.");
                    while (true)
                    {
                        //阻塞函数，获取队列中的消息
                        var ea = (BasicDeliverEventArgs)consumer.Queue.Dequeue();

                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);


                        Console.WriteLine("Received {0}", message);

                    }
                }
            }
        }

        /// <summary>
        /// 读取配置文件
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        private static ConfigRabMq GetMqConfig()
        {
            ConfigRabMq result = new ConfigRabMq();
            IDictionary dic = ConfigurationManager.GetSection("RabbitMQConfig") as IDictionary;
            result.HostName = dic["HostName"].ToString();
            if (dic["Port"] != null && dic["Port"].ToString().Length > 0)
            {
                result.Port = int.Parse(dic["Port"].ToString());
            }
            result.QueueName = dic["QueueName"].ToString();
            result.UserName = dic["UserName"].ToString();
            result.PassWord = dic["PassWord"].ToString();
            return result;
        }
    }

    /// <summary>
    /// config配置
    /// </summary>
    public class ConfigRabMq
    {
        public ConfigRabMq()
        {
            this.Port = 0;
        }

        /// <summary>
        /// 连接地址
        /// </summary>
        public string HostName { get; set; }

        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 队列名称
        /// </summary>
        public string QueueName { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        public string PassWord { get; set; }
    }
}
