using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_MonthMenu : System.Web.UI.UserControl
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

            this.Label1.Text = date.ToString("yyyy年MM月");
            this.HyperLink2.Text = date.AddMonths(-1).ToString("yyyy年MM月");
            this.HyperLink3.Text = date.AddMonths(-2).ToString("yyyy年MM月");
            this.HyperLink4.Text = date.AddMonths(+1).ToString("yyyy年MM月");
            this.HyperLink5.Text = date.AddMonths(+2).ToString("yyyy年MM月");

            this.HyperLink2.NavigateUrl = "../MonthList.aspx?date=" + date.AddMonths(-1).ToString("yyyy-MM-dd");
            this.HyperLink3.NavigateUrl = "../MonthList.aspx?date=" + date.AddMonths(-2).ToString("yyyy-MM-dd");
            this.HyperLink4.NavigateUrl = "../MonthList.aspx?date=" + date.AddMonths(+1).ToString("yyyy-MM-dd");
            this.HyperLink5.NavigateUrl = "../MonthList.aspx?date=" + date.AddMonths(+2).ToString("yyyy-MM-dd");
        }
    }
}