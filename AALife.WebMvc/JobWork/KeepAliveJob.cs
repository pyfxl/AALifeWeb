using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace AALife.WebMvc.JobWork
{
    public class KeepAliveJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            string url = "http://www.fxlweb.com/Default.aspx";
            using (var wc = new WebClient())
            {
                wc.DownloadString(url);
            }
        }
    }
}