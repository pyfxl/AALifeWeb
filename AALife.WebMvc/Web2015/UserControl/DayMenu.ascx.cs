using System;

public partial class UserControl_DayMenu : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DateTime date = new DateTime();
            if (Session["TodayDate"] != null && Session["TodayDate"].ToString() != "")
            {
                try
                {
                    date = Convert.ToDateTime(Session["TodayDate"]);
                }
                catch { }
            }

            this.Label1.Text = date.ToString("yyyy-MM-dd") + " " + Utility.GetWeekStr(Convert.ToInt32(date.DayOfWeek));
            this.HyperLink2.Text = date.AddDays(-1).ToString("yyyy-MM-dd") + " " + Utility.GetWeekStr(Convert.ToInt32(date.AddDays(-1).DayOfWeek));
            this.HyperLink3.Text = date.AddDays(-2).ToString("yyyy-MM-dd") + " " + Utility.GetWeekStr(Convert.ToInt32(date.AddDays(-2).DayOfWeek));
            this.HyperLink4.Text = date.AddDays(+1).ToString("yyyy-MM-dd") + " " + Utility.GetWeekStr(Convert.ToInt32(date.AddDays(+1).DayOfWeek));
            this.HyperLink5.Text = date.AddDays(+2).ToString("yyyy-MM-dd") + " " + Utility.GetWeekStr(Convert.ToInt32(date.AddDays(+2).DayOfWeek));

            this.HyperLink2.NavigateUrl = "../ItemList.aspx?date=" + date.AddDays(-1).ToString("yyyy-MM-dd");
            this.HyperLink3.NavigateUrl = "../ItemList.aspx?date=" + date.AddDays(-2).ToString("yyyy-MM-dd");
            this.HyperLink4.NavigateUrl = "../ItemList.aspx?date=" + date.AddDays(+1).ToString("yyyy-MM-dd");
            this.HyperLink5.NavigateUrl = "../ItemList.aspx?date=" + date.AddDays(+2).ToString("yyyy-MM-dd");
        }
    }
}