using System;
using System.Text;
using System.Data;
using AALife.BLL;

public partial class ItemNumChartJson : BasePage
{
    private string curDate = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        curDate = Request.QueryString["date"] ?? "";

        if (!IsPostBack)
        {
            PopulateControls();
        }
    }

    private void PopulateControls()
    {
        int userId = Convert.ToInt32(Session["UserID"]);
        DateTime today = Convert.ToDateTime(Session["TodayDate"]);
        if (curDate != "")
        {
            today = Convert.ToDateTime(curDate);
        }

        MonthBLL bll = new MonthBLL();
        DataTable dt = bll.GetItemNumTopList(userId, today);

        int countMax = 0;
        string max = "1";
        string step = "1";
        string itemName = "";
        string countNum = "";

        if (dt.Rows.Count > 0)
        {
            countMax = Convert.ToInt32(dt.Rows[0]["CountNum"]);
            max = countMax.ToString();
            step = Math.Floor(Convert.ToDouble(max) / 10).ToString();

            int i = 0;
            foreach (DataRow dr in dt.Rows)
            {
                if (i == 15) break;
                string dot = (i < 15-1 && i < dt.Rows.Count - 1 ? "," : "");
                itemName += "{\"text\":\"" + dr["ItemName"].ToString() + "\",\"rotate\":90}" + dot;
                countNum += dr["CountNum"].ToString() + dot;
                i++;
            }

        }

        Response.Write(GetChartJsonString(itemName, countNum, max, step));
        Response.End();
    }

    private string GetChartJsonString(string itemName, string countNum, string max, string step)
    {
        StringBuilder items = new StringBuilder();
        items.Append("{\"title\":{\"text\":\"消费次数排行\",\"style\":\"font-size:14px;font-family:Microsoft YaHei;text-align:center;\"},");
        items.Append("\"x_axis\":{\"labels\":{\"size\":12,\"labels\":[" + itemName + "]}},");
        items.Append("\"y_axis\":{\"steps\":" + step + ",\"max\":" + max + "},");
        items.Append("\"bg_colour\":\"#ffffff\",\"elements\":[");
        items.Append("{\"type\":\"bar_glass\",\"tip\":\"#val# 次<br>#x_label#\",\"values\":[" + countNum + "],\"on-click\":\"chart_open\",\"colour\":\"#0000ff\"}]}");

        return items.ToString();
    }
}