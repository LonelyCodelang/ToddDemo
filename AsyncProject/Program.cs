using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncProject
{
    class Program
    {
        static void Main(string[] args)
        {
            new AsyncT1().DoSomethingAsync();
            // Task.Delay(TimeSpan.FromDays(1));
            Thread.Sleep(1000 * 60 * 60);
        }
    }
}
