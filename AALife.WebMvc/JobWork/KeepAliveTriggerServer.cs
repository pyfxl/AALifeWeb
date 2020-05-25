using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AALife.WebMvc.JobWork
{
    public class KeepAliveTriggerServer
    {
        public ITrigger KeepAliveTrigger()
        {
            var trigger = TriggerBuilder.Create()
               .WithIdentity("保持在线", "作业触发器")
               .WithSimpleSchedule(x => x
                   //.WithIntervalInSeconds(5)
                   .WithIntervalInMinutes(1) //每五分钟执行一次
                   .RepeatForever())
               .Build();

            return trigger;
        }
    }
}