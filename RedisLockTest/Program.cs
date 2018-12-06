using RedLockNet.SERedis;
using RedLockNet.SERedis.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RedisLockTest
{
    class Program
    {
        public static RedLockFactory redLock;
        static void Main(string[] args)
        {
            for (int i = 0; i < 20; i++)
            {
                var t1 = new Thread(() => TestLockMenth1(i));
                t1.Start();
            }
            // TestLockMenth(1);
            Console.ReadKey();
            // the lock is automatically released at the end of the using block
        }

        public static RedLockFactory getRedLock()
        {
            if (redLock == null)
            {
                var endPoints = new List<RedLockEndPoint>
                {
                    //new DnsEndPoint("redis1", 6379),
                    //new DnsEndPoint("redis2", 6379),
                    new DnsEndPoint("192.168.3.39", 6379)
                };
                redLock = RedLockFactory.Create(endPoints);
            }
            return redLock;
        }

        public static void TestLockMenth(int num)
        {
            var resource = "the-thing-we-are-locking-on";//资源
            var expiry = TimeSpan.FromSeconds(300);//等待到期时间

            //当您想要锁定资源时（如果锁定不可用则立即放弃）：
            using (var redLock = getRedLock().CreateLock(resource, expiry)) // there are also non async Create() methods
            {
                // make sure we got the lock
                if (redLock.IsAcquired)
                {
                    Thread.Sleep(100);
                    Console.WriteLine(string.Format("数字输出:{0}",num));
                    // do stuff
                }
                else
                {
                    Console.WriteLine("获取锁失败！");
                }
            }
        }

        //当您想要锁定资源时（每隔retry几秒阻塞并重试一次，直到锁定可用或wait已经过了几秒钟）
        public static void TestLockMenth1(int num)
        {
            var resource = "the-thing-we-are-locking-on";
            var expiry = TimeSpan.FromSeconds(30);//超时时间
            var wait = TimeSpan.FromSeconds(10);//等待时间
            var retry = TimeSpan.FromSeconds(1);//

            using (var redLock = getRedLock().CreateLock(resource, expiry, wait, retry)) // there are also non async Create() methods
            {
                // make sure we got the lock
                if (redLock.IsAcquired)
                {
                    Thread.Sleep(500);
                    Console.WriteLine(string.Format("数字输出:{0}", num));
                    // do stuff
                }
                else
                {
                    Console.WriteLine("获取锁失败！");
                }
            }
        }
    }
}
