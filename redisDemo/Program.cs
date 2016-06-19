using Newtonsoft.Json;
using ServiceStack.Redis;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace redisDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            IDatabase db = null;
            string connstr = "127.0.0.1:6379,allowadmin=true";
            ConnectionMultiplexer conn = ConnectionMultiplexer.Connect(connstr);
            //  ConfigurationOptions options = ConfigurationOptions.Parse(connstr);
            //  string parsestr = options.ToString();
            db = conn.GetDatabase(0);


            List<Customer> list = new List<Customer>();
            list.Add(new Customer() { Name="张三",age=18 });
            list.Add(new Customer() { Name = "李四", age = 19,Email="1212@qq.com" });
            list.Add(new Customer() { Name = "王五", age = 22,Email="xxxxx@qq.com" });

            List<HashEntry> listHashEntry = new List<HashEntry>();
            foreach (var item in list)
            {
                string json = JsonConvert.SerializeObject(list);
                listHashEntry.Add(new HashEntry("",""));
            }
        }

        public class Customer
        {
            public string Name { get; set; }

            public int age { get; set; }

            public string Email { get; set; }
        }
    }
}
