using System;
using System.Text;
using System.Data;
using AALife.BLL;

public partial class ItemDateChartJson : BasePage
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
        string orderBy = Request.QueryString["orderBy"] ?? "chart";
        decimal priceMax = 0m;
        
        MonthBLL bll = new MonthBLL();
        DataTable dt = bll.GetItemDateTopList(userId, today, orderBy, out priceMax);

        string max = "1";
        string step = "1";
        string itemBuyDate = "";
        string shouruPrice = "";
        string zhichuPrice = "";

        string tip = (orderBy == "list" ? "#x_label#" : today.ToString("yyyy-MM-") + "#x_label#");

        if (dt.Rows.Count > 0)
        {
            max = Math.Ceiling(priceMax).ToString();
            step = Math.Floor(Convert.ToDouble(max) / 10).ToString();

            int i = 0;
            foreach (DataRow dr in dt.Rows)
            {
                string dot = (i < dt.Rows.Count - 1 ? "," : "");
                string date = (orderBy == "list" ? Convert.ToDateTime(dr["ItemBuyDate"]).ToString("yyyy-MM-dd") : Convert.ToDateTime(dr["ItemBuyDate"]).ToString("dd"));
                int rotate = (orderBy == "list" ? 90 : 0);
                itemBuyDate += "{\"text\":\"" + date + "\",\"rotate\":" + rotate + "}" + dot;
                shouruPrice += dr["ShouRuPrice"].ToString() + dot;
                zhichuPrice += dr["ZhiChuPrice"].ToString() + dot;
                i++;
            }
        }
        else
        {
            itemBuyDate = "{\"text\":\"0\"},{\"text\":\"1\"}";
        }

        Response.Write(GetChartJsonString(itemBuyDate, shouruPrice, zhichuPrice, max, step, tip));
        Response.End();
    }

    private string GetChartJsonString(string itemBuyDate, string shouruPrice, string zhichuPrice, string max, string step, string tip)
    {
        StringBuilder items = new StringBuilder();
        items.Append("{\"title\":{\"text\":\"消费日期排行\",\"style\":\"font-size:14px;font-family:Microsoft YaHei;text-align:center;\"},");
        items.Append("\"x_axis\":{\"labels\":{\"labels\":[" + itemBuyDate + "]}},");
        items.Append("\"y_axis\":{\"steps\":" + step + ",\"min\":0,\"max\":" + max + "},");
        items.Append("\"bg_colour\":\"#ffffff\",\"elements\":[");
        items.Append("{\"type\":\"area\",\"dot-style\":{\"on-click\":\"chart_click\",\"type\":\"solid-dot\",\"dot-size\":4,\"halo-size\":1,\"tip\":\"￥#val# 元<br>" + tip + "\"},\"values\":[" + shouruPrice + "],\"colour\":\"#ff0000\",\"fill-alpha\":0.3,\"fill\":\"#ff0000\"},");
        items.Append("{\"type\":\"area\",\"dot-style\":{\"on-click\":\"chart_click\",\"type\":\"solid-dot\",\"dot-size\":4,\"halo-size\":1,\"tip\":\"￥#val# 元<br>" + tip + "\"},\"values\":[" + zhichuPrice + "],\"colour\":\"#00aa00\",\"fill-alpha\":0.3,\"fill\":\"#00aa00\"}]}");

        return items.ToString();
    }
}