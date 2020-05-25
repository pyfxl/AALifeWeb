using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading;
using System.Web;

namespace AALife.WebMvc.JobBase
{
    public class JobBase
    {

        public static IScheduler Scheduler
        {
            get
            {
                var properties = new NameValueCollection();

                // 设置线程池
                properties["quartz.threadPool.type"] = "Quartz.Simpl.SimpleThreadPool, Quartz";
                //设置线程池的最大线程数量
                properties["quartz.threadPool.threadCount"] = "5";
                //设置作业中每个线程的优先级
                properties["quartz.threadPool.threadPriority"] = ThreadPriority.Normal.ToString();

                // 远程输出配置
                properties["quartz.scheduler.exporter.type"] = "Quartz.Simpl.RemotingSchedulerExporter, Quartz";
                properties["quartz.scheduler.exporter.port"] = "1996";  //配置端口号
                properties["quartz.scheduler.exporter.bindName"] = "QuartzScheduler";
                properties["quartz.scheduler.exporter.channelType"] = "tcp"; //协议类型

                //创建一个工厂
                var schedulerFactory = new StdSchedulerFactory(properties);
                //启动
                var scheduler = schedulerFactory.GetScheduler();

                return scheduler;
            }

        }

        public static void AddSchedule<T>(JobServer<T> jobServer, ITrigger trigger, string jobName, string jobGroup) where T : IJob
        {
            jobServer.JobName = jobName;
            jobServer.JobGroup = jobGroup;
            Scheduler.ScheduleJob(jobServer.CreateJob(), trigger);
        }
    }
}