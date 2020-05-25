using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AALife.WebMvc.JobBase
{
    public class JobServer<T> where T : IJob
    {
        public string JobName { get; set; }
        public string JobGroup { get; set; }

        public IJobDetail CreateJob()
        {
            IJobDetail job1 = JobBuilder.Create<T>() //创建一个作业
                .WithIdentity(JobName, JobGroup) //JobName  代表你要执行的这个任务的名称。 JobGroup任务分组
                .Build();

            return job1;
        }
    }
}