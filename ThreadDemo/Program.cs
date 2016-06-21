using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //new MyTask().Run1();
            new MyTask().Run2();
             Console.WriteLine("完成");
            for (int i = 0; i < 1000; i++)
            {
                Thread.Sleep(1000);
            }
        }
    }
}
