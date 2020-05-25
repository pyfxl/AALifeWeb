using AALife.Service;
using AALife.Service.Models;
using NLog;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AALife.WebMvc.JobWork
{
    public class SendToDingJob : IJob
    {
        public static Logger log = LogManager.GetCurrentClassLogger();

        public void Execute(IJobExecutionContext context)
        {
            using (var db = new AALifeDbContext())
            {
                var startDate = DateTime.Now.AddDays(-1).Date;
                var endDate = DateTime.Now.Date;
                var result = db.ItemTable.Where(a => a.ModifyDate >= startDate && a.ModifyDate < endDate);
                string msg = string.Format("每日统计\n\n{0} 日，共记录消费 {1} 条。", startDate.ToString("yyyy/MM/dd"), result.Count());

                //log
                log.Info(msg);

                var dd = new DingClient();
                string accessToken = dd.GetToken(dd.AppKey, dd.AppSecret);

                //ding
                DingHelper.SendMessage(dd.AgentId, accessToken, "统计消息", msg, dd.AdminUser);
            }
        }
    }
}