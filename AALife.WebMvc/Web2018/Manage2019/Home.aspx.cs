using AALife.Service.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AALife.WebMvc.Web2018.Manage2019
{
    public partial class Manage_Home : AdminPage
    {
        public readonly string UserTheme = "[{\"value\":\"main\",\"text\":\"低调红\"},{\"value\":\"blue\",\"text\":\"屌丝蓝\"},{\"value\":\"gold\",\"text\":\"土豪金 \"}]";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public string GetUserFrom()
        {
            UserFromBLL bll = new UserFromBLL();

            var lists = bll.GetUserFrom();

            var results = lists.Select(x => new
            {
                value = x.UserFrom,
                text = x.UserFromName
            });

            return Newtonsoft.Json.JsonConvert.SerializeObject(results);
        }

        public string GetUserWorkDay()
        {
            WorkDayBLL bll = new WorkDayBLL();

            var lists = bll.GetWorkDay();

            var results = lists.Select(x => new
            {
                value = x.WorkDay,
                text = x.WorkDayName
            });

            return Newtonsoft.Json.JsonConvert.SerializeObject(results);
        }
    }
}