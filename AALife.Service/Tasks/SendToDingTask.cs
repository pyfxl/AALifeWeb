using AALife.Service.Models;
using System;
using System.Linq;
using System.Net;

namespace AALife.Service.Tasks
{
    /// <summary>
    /// Represents a task for keeping the site alive
    /// </summary>
    public partial class SendToDingTask : ITask
    {
        /// <summary>
        /// Executes a task
        /// </summary>
        public void Execute()
        {
            using(var db = new AALifeDbContext())
            {
                var startDate = DateTime.Now.AddDays(-1).Date;
                var endDate = DateTime.Now.Date;
                var result = db.ItemTable.Where(a => a.ModifyDate >= startDate && a.ModifyDate < endDate);
                string msg = string.Format("每日统计\n\n{0} 日，共记录消费 {1} 条。", startDate.ToString("yyyy/MM/dd"), result.Count());

                var dd = new DingClient();
                string accessToken = dd.GetToken(dd.AppKey, dd.AppSecret);

                //ding
                DingHelper.SendMessage(dd.AgentId, accessToken, "统计消息", msg, dd.AdminUser);
            }
        }
    }
}
