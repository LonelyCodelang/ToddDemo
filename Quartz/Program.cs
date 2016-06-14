using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quartz
{
    class Program
    {
        static void Main(string[] args)
        {
            Start();
            Start1();
        }

        static void Start()
        {
            Console.WriteLine(DateTime.Now.ToString("r"));
            //1.首先创建一个作业调度池
            ISchedulerFactory schedf = new StdSchedulerFactory();
            IScheduler sched = schedf.GetScheduler();
            //2.创建出来一个具体的作业
            IJobDetail job = JobBuilder.Create<JobDemo>().Build();
            //3.创建并配置一个触发器(没3秒执行一次)
            ISimpleTrigger trigger = (ISimpleTrigger)TriggerBuilder.Create().WithSimpleSchedule(x => x.WithIntervalInSeconds(3).WithRepeatCount(int.MaxValue)).Build();
            //4.加入作业调度池中
            sched.ScheduleJob(job, trigger);
            //5.开始运行
            sched.Start();
            Console.ReadKey();
        }

        static void Start1()
        {
            Console.WriteLine(DateTime.Now.ToString("r"));
            //1.首先创建一个作业调度池
            ISchedulerFactory schedf = new StdSchedulerFactory();
            IScheduler sched = schedf.GetScheduler();
            //2.创建出来一个具体的作业
            IJobDetail job = JobBuilder.Create<JobDemo1>().Build();
            //3.创建并配置一个触发器(没3秒执行一次)
            ISimpleTrigger trigger = (ISimpleTrigger)TriggerBuilder.Create().WithSimpleSchedule(x => x.WithIntervalInSeconds(1).WithRepeatCount(int.MaxValue)).Build();
            //4.加入作业调度池中
            sched.ScheduleJob(job, trigger);
            //5.开始运行
            sched.Start();
            Console.ReadKey();
        }
    }

    public class JobDemo : IJob
    {
        /// <summary>
        /// 这里是作业调度每次定时执行方法
        /// </summary>
        /// <param name="context"></param>
        public void Execute(IJobExecutionContext context)
        {
            Console.WriteLine(DateTime.Now.ToString("r"));
        }
    }

    public class JobDemo1 : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            Console.WriteLine("xxxx:" + DateTime.Now.ToString("r"));
        }
    }
}
