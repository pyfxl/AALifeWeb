using System;
using System.Text;
using System.Data;
using AALife.BLL;

public partial class FLZJMXChartJson : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PopulateControls();
        }
    }

    private void PopulateControls()
    {
        int userId = Convert.ToInt32(Session["UserID"]);
        DateTime today = Convert.ToDateTime(Session["TodayDate"]);
        int catTypeId = Convert.ToInt32(Request.QueryString["catTypeId"]);
        decimal priceMax = 0m;

        MonthBLL bll = new MonthBLL();
        DataTable dt = bll.GetFenLeiZongJiMingXiList(userId, today, catTypeId, out priceMax);

        string max = "1";
        string max2 = "1";
        string step = "1";
        string step2 = "1";
        string itemName = "";
        string itemPrice = "";
        string countNum = "";

        if (dt.Rows.Count > 0)
        {
            max = Math.Ceiling(priceMax).ToString();
            max2 = dt.Rows[0]["CountNum"].ToString();
            step = Math.Floor(Convert.ToDouble(max) / 10).ToString();
            step2 = Math.Floor(Convert.ToDouble(max2) / 10).ToString();

            int i = 0;
            foreach (DataRow dr in dt.Rows)
            {
                if (i == 15) break;
                string dot = (i < 15-1 && i < dt.Rows.Count - 1 ? "," : "");
                itemName += "{\"text\":\"" + dr["ItemName"].ToString() + "\",\"rotate\":90}" + dot;
                itemPrice += dr["ItemPrice"].ToString() + dot;
                countNum += dr["CountNum"].ToString() + dot;
                i++;
            }
        }

        Response.Write(GetChartJsonString(itemName, itemPrice, countNum, max, step, max2, step2));
        Response.End();
    }

    private string GetChartJsonString(string itemName, string itemPrice, string countNum, string max, string step, string max2, string step2)
    {
        StringBuilder items = new StringBuilder(); 
        items.Append("{\"title\":{\"text\":\"消费类别明细\",\"style\":\"font-size:14px;font-family:Microsoft YaHei;text-align:center;\"},");
        items.Append("\"x_axis\":{\"colour\":\"#909090\",\"3d\":5,\"labels\":{\"size\":12,\"labels\":[" + itemName + "]}},");
        items.Append("\"y_axis\":{\"steps\":" + step + ",\"max\":" + max + "},\"y_axis_right\":{\"grid-colour\":\"#000000\",\"steps\":" + step2 + ",\"max\":" + max2 + "},");
        items.Append("\"bg_colour\":\"#ffffff\",\"elements\":[");
        items.Append("{\"type\":\"bar_3d\",\"tip\":\"￥#val# 元<br>#x_label#\",\"values\":[" + itemPrice + "],\"on-click\":\"chart_click\",\"colour\":\"#ff8800\"},");
        items.Append("{\"type\":\"line\",\"values\":[" + countNum + "],\"dot-style\":{\"type\":\"solid-dot\",\"on-click\":\"chart_click\",\"tip\":\"#val# 次<br>#x_label#\",\"dot-size\":5},\"colour\":\"#000000\",\"axis\":\"right\"}]}");

        return items.ToString();
    }
}