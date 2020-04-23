using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AALife.WebMvc
{
    public class DingClient
    {
        public string AgentId = System.Configuration.ConfigurationManager.AppSettings["AgentId"];//"540445656";
        public string CorpId = System.Configuration.ConfigurationManager.AppSettings["CorpId"];//"ding4aa776f5b292f33335c2f4657eb6378f";
        public string AppKey = System.Configuration.ConfigurationManager.AppSettings["AppKey"];//"dingcsaeusqene9ajnlb";
        public string AppSecret = System.Configuration.ConfigurationManager.AppSettings["AppSecret"];//"_pKS2ehfeDHh95T3vRT7SKWxrh0elfnSxU9UzET8R2jj6mQV3Hf2B4QzLPb-tW1O";
        public string AdminUser = System.Configuration.ConfigurationManager.AppSettings["AdminUser"];//"manager8822";

        public string GetTicket(string accessToken)
        {
            return DingHelper.GetJSTicket(accessToken);
        }

        public string GetToken(string appKey, string appSecret)
        {
            return DingHelper.GetAccessToken(appKey, appSecret);
        }

        public string GetTimestamp()
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); // 当地时区
            long timeStamp = (long)(DateTime.Now - startTime).TotalMilliseconds; // 相差毫秒数
            return timeStamp.ToString();
        }

        public string GetNoncestr()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }

        public string GetSignature(string ticket, string noncestr, string timestamp, string url)
        {
            string string1 = string.Format("jsapi_ticket={0}&noncestr={1}&timestamp={2}&url={3}", ticket, noncestr, timestamp, url);
            string signature = SHA1(string1);
            return signature;
        }

        public string GetUrl()
        {
            return HttpContext.Current.Request.Url.ToString();
        }

        public string SHA1(string s)
        {
            s = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(s, "SHA1").ToString();
            return s.ToLower();
        }

    }
}