using AALife.WebMvc.JobWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AALife.WebMvc.JobBase
{
    public class JobManager
    {
        public static void Start()
        {
            //开启调度
            JobBase.Scheduler.Start();

            //第一个参数是你要执行的工作(job)  第二个参数是这个工作所对应的触发器(Trigger)(例如:几秒或几分钟执行一次)

            JobBase.AddSchedule(new JobServer<KeepAliveJob>(),
                new JobTriggerServer().KeepAliveTrigger(), "保持在线", "作业触发器");

            JobBase.AddSchedule(new JobServer<SendToDingJob>(),
                new JobTriggerServer().SendToDingTrigger(), "推送消息", "作业触发器");

        }
    }
}