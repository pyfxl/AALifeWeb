using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AALife.WebMvc.JobWork
{
    public class JobTriggerServer
    {
        public ITrigger KeepAliveTrigger()
        {
            var trigger = TriggerBuilder.Create()
               .WithIdentity("保持在线", "作业触发器")
               .WithSimpleSchedule(x => x
                   //.WithIntervalInSeconds(5)
                   .WithIntervalInMinutes(30) //每五分钟执行一次
                   .RepeatForever())
               .Build();

            return trigger;
        }

        public ITrigger SendToDingTrigger()
        {
            var trigger = TriggerBuilder.Create()
               .WithIdentity("推送消息", "作业触发器")
               .WithCronSchedule("0 0 0 * * ?")
               .Build();

            return trigger;
        }
    }
}