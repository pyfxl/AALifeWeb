using System;
using System.Text;
using System.Data;
using AALife.BLL;

public partial class BiJiaoFXChartJson : BasePage
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
        decimal priceMax = 0m;

        MonthBLL bll = new MonthBLL();
        DataTable dt = bll.GetBiJiaoFenXiList(userId, today, "chart", out priceMax);

        string max = "1";
        string step = "1";
        string catTypeName = "";
        string itemPriceCur = "";
        string itemPricePrev = "";
        
        if (dt.Rows.Count > 0)
        {
            max = (priceMax > 0 ? Math.Floor(priceMax).ToString() : "1");
            step = Math.Floor(Convert.ToDouble(max) / 10).ToString();

            int i = 0;
            foreach (DataRow dr in dt.Rows)
            {
                string dot = (i < dt.Rows.Count - 1 ? "," : "");
                catTypeName += "{\"text\":\"" + dr["CategoryTypeName"].ToString() + "\",\"rotate\":0}" + dot;
                itemPriceCur += dr["ZhiPriceCur"].ToString() + dot;
                itemPricePrev += dr["ZhiPricePrev"].ToString() + dot;
                i++;
            }
        }

        Response.Write(GetChartJsonString(catTypeName, itemPriceCur, itemPricePrev, max, step));
        Response.End();
    }

    private string GetChartJsonString(string catTypeName, string itemPriceCur, string itemPricePrev, string max, string step)
    {
        StringBuilder items = new StringBuilder();
        items.Append("{\"title\":{\"text\":\"消费比较分析\",\"style\":\"font-size:14px;font-family:Microsoft YaHei;text-align:center;\"},");
        items.Append("\"x_axis\":{\"labels\":{\"labels\":[" + catTypeName + "]}},");
        items.Append("\"y_axis\":{\"steps\":" + step + ",\"min\":0,\"max\":" + max + "},");
        items.Append("\"bg_colour\":\"#ffffff\",\"elements\":[");
        items.Append("{\"type\":\"area\",\"dot-style\":{\"on-click\":\"chart_click\",\"type\":\"solid-dot\",\"dot-size\":4,\"halo-size\":1,\"tip\":\"本月 ￥#val# 元<br>#x_label#\"},\"values\":[" + itemPriceCur + "],\"colour\":\"#ff0000\",\"fill-alpha\":0.3,\"fill\":\"#ff0000\"},");
        items.Append("{\"type\":\"area\",\"dot-style\":{\"on-click\":\"chart_click\",\"type\":\"solid-dot\",\"dot-size\":4,\"halo-size\":1,\"tip\":\"上月 ￥#val# 元<br>#x_label#\"},\"values\":[" + itemPricePrev + "],\"colour\":\"#00aa00\",\"fill-alpha\":0.3,\"fill\":\"#00aa00\"}]}");

        return items.ToString();
    }
}