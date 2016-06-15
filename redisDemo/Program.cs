using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace redisDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            string Ip = "127.0.0.1";
            int Port = 6379;
            RedisClient redisClient = new RedisClient(Ip, Port);
            string key = "test-aliyun";
            string value = "test-aliyun-value";
            redisClient.Set(key, value);
            string listKey = "test-aliyun-list";
            System.Console.WriteLine("set key " + key + " value " + value);
            string getValue = System.Text.Encoding.Default.GetString(redisClient.Get(key));
            System.Console.WriteLine("get key " + getValue);
            System.Console.Read();

            //RedisClient Client = new RedisClient(Ip, Port);
            //string str = Client.Get<string>("test");
            //Console.WriteLine("之前通过客户端进行设置的test键值对:{0}", str);
            Console.ReadLine();
        }

        /// <summary>
        /// 连接池模式
        /// </summary>
        public static void RedisPoolClientTest()
        {
            string[] testReadWriteHosts = new[] {
                "redis://:fb92bf2e0abf11e5:1234561178a1A@127.0.0.1:6379"/*redis://:实例id:密码@访问地址:端口*/
                };
            RedisConfig.VerifyMasterConnections = false;//需要设置
            PooledRedisClientManager redisPoolManager = new PooledRedisClientManager(10/*连接池个数*/, 10/*连接池超时时间*/, testReadWriteHosts);
            for (int i = 0; i < 100; i++)
            {
                IRedisClient redisClient = redisPoolManager.GetClient();//获取连接
                RedisNativeClient redisNativeClient = (RedisNativeClient)redisClient;
                redisNativeClient.Client = null;//ApsaraDB for Redis不支持client setname所以这里需要显示的把client对象置为null
                try
                {
                    string key = "test-aliyun1111";
                    string value = "test-aliyun-value1111";
                    redisClient.Set(key, value);
                    string listKey = "test-aliyun-list";
                    redisClient.AddItemToList(listKey, value);
                    System.Console.WriteLine("set key " + key + " value " + value);
                    string getValue = redisClient.GetValue(key);
                    System.Console.WriteLine("get key " + getValue);
                    redisClient.Dispose();//
                }
                catch (Exception e)
                {
                    System.Console.WriteLine(e.Message);
                }
            }
            System.Console.Read();
        }


        public class RedisHelper
        {
            RedisClient client = null;
            public RedisHelper()
            {
                client = new RedisClient();
            }

            public void Add(string Key, Object Value)
            {
                // client.GeoAdd
            }
        }
    }
}
