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

            List<int> source = new List<int>();
            for (int i = 0; i < 1000; i++)
            {
                source.Add(i);
            }

            //var result = from x in source.AsParallel().WithDegreeOfParallelism(50)
            //             select proc(x);
            for (int i = 0; i < 1000; i++)
            {
                Thread.Sleep(1000);
            }
        }

        static void proc(int x)
        {
            Console.WriteLine(x);
        }
    }
}
