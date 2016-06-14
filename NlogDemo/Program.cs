using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NlogDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            for (int i = 0; i < 100000; i++)
            {
                LogHelper.WriteInfo("xxxx" + i);
                LogHelper.WriteDebug("ddddddddddd" + i);
            }

            try
            {
                throw new Exception("xxxxxxxxx");
            }
            catch (Exception ex)
            {
                //  Loggerxxx.Default.Error("xxx", ex);
                LogHelper.WriteException("xxx", ex);
            }
            string time = watch.ElapsedMilliseconds.ToString();
            Console.WriteLine(time);
        }
    }
}
