using RabbitMQ.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Rabbitmq.Producing
{

    public static class MQProducing
    {
        public static void SendMqMsg(string msg)
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
                    //我们需要确认RabbitMQ永远不会丢失我们的队列。为了这样，我们需要声明它为持久化的。
                    //durable = true;
                    //在MQ上定义一个持久化队列，如果名称相同不会重复创建
                    //注意此处参数的设置，必须保证接收方设置也是一样
                    channel.QueueDeclare(config.QueueName, true, false, false, null);
                    var body = Encoding.UTF8.GetBytes(msg);
                    channel.BasicPublish("", config.QueueName, null, body);
                   // Console.WriteLine(" set {0}", message);
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
