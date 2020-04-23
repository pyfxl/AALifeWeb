using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AALife.WebMvc
{
    public static class MsgHelper
    {
        public static Logger log = LogManager.GetCurrentClassLogger();

        public static void DingMessage(string msg)
        {
            var dd = new DingClient();
            string accessToken = dd.GetToken(dd.AppKey, dd.AppSecret);

            //log
            log.Info(msg);

            //ding
            DingHelper.SendMessage(dd.AgentId, accessToken, "收到新的消息", msg, dd.AdminUser);
        }
    }
}