using System;
using System.Text;
using System.Data;
using AALife.BLL;

public partial class BJMXChartJson : BasePage
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
        int countMax = 0;

        MonthBLL bll = new MonthBLL();
        DataTable dt = bll.GetBiJiaoMingXiList(userId, today, catTypeId, out priceMax, out countMax);

        string max = "1";
        string step = "1";
        string max2 = "1";
        string step2 = "1";
        string itemName = "";
        string itemPriceCur = "";
        string itemPricePrev = "";
        string countNumCur = "";
        string countNumPrev = "";

        if (dt.Rows.Count > 0)
        {
            max = Math.Floor(priceMax).ToString();
            step = Math.Floor(Convert.ToDouble(max) / 10).ToString();
            max2 = countMax.ToString();
            step2 = Math.Floor(Convert.ToDouble(max2) / 10).ToString();

            int i = 0;
            foreach (DataRow dr in dt.Rows)
            {
                if (i == 15) break;
                string dot = (i < 15-1 && i < dt.Rows.Count-1 ? "," : "");
                itemName += "{\"text\":\"" + dr["ItemName"].ToString() + "\",\"rotate\":90}" + dot;
                itemPriceCur += dr["ItemPriceCur"].ToString() + dot;
                itemPricePrev += dr["ItemPricePrev"].ToString() + dot;
                countNumCur += dr["CountNumCur"].ToString() + dot;
                countNumPrev += dr["CountNumPrev"].ToString() + dot;
                i++;
            }
        }

        Response.Write(GetChartJsonString(itemName, itemPriceCur, itemPricePrev, countNumCur, countNumPrev, max, step, max2, step2));
        Response.End();
    }

    private string GetChartJsonString(string itemName, string itemPriceCur, string itemPricePrev, string countNumCur, string countNumPrev, string max, string step, string max2, string step2)
    {
        StringBuilder items = new StringBuilder();
        items.Append("{\"title\":{\"text\":\"消费比较明细\",\"style\":\"font-size:14px;font-family:Microsoft YaHei;text-align:center;\"},");
        items.Append("\"x_axis\":{\"labels\":{\"size\":12,\"labels\":[" + itemName + "]}},");
        items.Append("\"y_axis\":{\"steps\":" + step + ",\"max\":" + max + "},\"y_axis_right\":{\"grid-colour\":\"#000000\",\"steps\":" + step2 + ",\"max\":" + max2 + "},");
        items.Append("\"bg_colour\":\"#ffffff\",\"elements\":[");
        items.Append("{\"type\":\"bar\",\"tip\":\"￥#val# 元<br>本月\",\"values\":[" + itemPriceCur + "],\"on-click\":\"chart_click\",\"colour\":\"#ff0000\"},");
        items.Append("{\"type\":\"bar\",\"tip\":\"￥#val# 元<br>上月\",\"values\":[" + itemPricePrev + "],\"on-click\":\"chart_click2\",\"colour\":\"#00aa00\"},");
        items.Append("{\"type\":\"line\",\"values\":[" + countNumCur + "],\"dot-style\":{\"type\":\"solid-dot\",\"on-click\":\"chart_click\",\"tip\":\"#val# 次<br>#x_label#\",\"dot-size\":4,\"halo-size\":3},\"colour\":\"#ff0000\",\"axis\":\"right\"},");
        items.Append("{\"type\":\"line\",\"values\":[" + countNumPrev + "],\"dot-style\":{\"type\":\"solid-dot\",\"on-click\":\"chart_click2\",\"tip\":\"#val# 次<br>#x_label#\",\"dot-size\":4,\"halo-size\":3},\"colour\":\"#00aa00\",\"axis\":\"right\"}]}");

        return items.ToString();
    }
}