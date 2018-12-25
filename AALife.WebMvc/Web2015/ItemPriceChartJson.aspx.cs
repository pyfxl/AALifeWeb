using System;
using System.Text;
using System.Data;
using AALife.BLL;

public partial class ItemPriceChartJson : BasePage
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
        DataTable dt = bll.GetItemPriceTopList(userId, today);

        string max = "1";
        string step = "1";
        string itemName = "";
        string itemPrice = "";

        if (dt.Rows.Count > 0)
        {
            max = Math.Ceiling(Convert.ToDouble(dt.Rows[0]["ItemPrice"])).ToString();
            step = Math.Floor(Convert.ToDouble(max) / 10).ToString();

            int i = 0;
            foreach (DataRow dr in dt.Rows)
            {
                if (i == 15) break;
                string dot = (i < 15 - 1 && i < dt.Rows.Count - 1 ? "," : "");
                itemName += "{\"text\":\"" + dr["ItemName"].ToString() + "\",\"rotate\":90}" + dot;
                itemPrice += dr["ItemPrice"].ToString() + dot;
                i++;
            }

            
        }

        Response.Write(GetChartJsonString(itemName, itemPrice, max, step));
        Response.End();
    }

    private string GetChartJsonString(string itemName, string itemPrice, string max, string step)
    {
        StringBuilder items = new StringBuilder();
        items.Append("{\"title\":{\"text\":\"消费单价排行\",\"style\":\"font-size:14px;font-family:Microsoft YaHei;text-align:center;\"},");
        items.Append("\"x_axis\":{\"labels\":{\"size\":12,\"labels\":[" + itemName + "]}},");
        items.Append("\"y_axis\":{\"steps\":" + step + ",\"max\":" + max + "},");
        items.Append("\"bg_colour\":\"#ffffff\",\"elements\":[");
        items.Append("{\"type\":\"bar_cylinder\",\"tip\":\"￥#val# 元<br>#x_label#\",\"values\":[" + itemPrice + "],\"on-click\":\"chart_click\",\"colour\":\"#ff0000\"}]}");

        return items.ToString();
    }
}