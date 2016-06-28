using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AsyncProject
{
    public class AsyncT1
    {
        /**
         * 现代的异步.net程序使用两个关键字:async和await。async关键字加上方法声明上，
         * 它主要的目的是使方法内的await关键字生效(为了保持向后兼容，同时引入了这两个关键字)。
         * 如果async方法有返回值，应返回Task<T>；如果没有返回值，应返回task。这些task类型相当于future，用来在异步方法结束的时候通知主程序。
         * */

        public async Task DoSomethingAsync()
        {
            int val = 13;

            //异步方式等待5秒
            await Task.Delay(TimeSpan.FromSeconds(3));
            Console.WriteLine(val);
            val += 2;

            await Task.Delay(TimeSpan.FromSeconds(2));
            Console.WriteLine(val);
        }
    }
}
