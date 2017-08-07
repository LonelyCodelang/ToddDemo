using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadDemo
{
    public class MyTask
    {

        public async void Run1()
        {
            Task t1 = Task.Factory.StartNew(delegate { MyMethodA(); });
            Task t2 = Task.Factory.StartNew(delegate { MyMethodB(); });

            //t1.Wait();//单个执行
            //t2.Wait();//单个执行
            //上面的方法是一个一个的执行完毕


            Task.WaitAll(t1, t2);//执行全部
            //一起同时执行


            //Wait:等待方法执行完成才返回

            await Task.WaitAll(t1, t2);
        }

        public void Run2()
        {
            //创建Task有两种方式，一种是使用构造函数创建，另一种是使用 Task.Factory.StartNew 进行创建
            Task t1 = new Task(MyMethodA);
            t1.Start();

            //其实这两种方式都是一样的,Task.Factory 是对Task进行管理，调度管理这一类的
            Task t2 = Task.Factory.StartNew(MyMethodB);

            Task t3 = Task.Factory.StartNew(() => MyMethodC("测试"));
        }


        /// <summary>
        /// 执行方法A
        /// </summary>
        public void MyMethodA()
        {
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine("A++++++" + i);
                Thread.Sleep(1000);
            }
        }

        /// <summary>
        /// 执行方法B
        /// </summary>
        public void MyMethodB()
        {
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine("B++++++" + i);
                Thread.Sleep(1000);
            }
        }

        public void MyMethodC(string xx)
        {
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine(xx + "++++++" + i);
                Thread.Sleep(1000);
            }
        }



        //这是线程不安全，直接调用外部参数
        static void TestRun1(string Name, int Age)
        {
            Task.Factory.StartNew(() => Console.WriteLine("name:{0} age:{1}", Name, Age));
        }

        //如果你确定底层封装好了，可以像上面那样写，但建议写成下面这种
        public void TestRun(string Name, int Age)
        {
            Task.Factory.StartNew(obj =>
            {
                var o = (dynamic)obj;
                Console.WriteLine("name:{0} age:{1}", o.Name, o.Age);
            }, new { Name, Age });
        }

    }
}
