using System;
using System.Text;
using System.Data;
using AALife.BLL;

public partial class JiaGeFXMXChartJson : BasePage
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
        string itemType = Request.QueryString["itemType"] ?? "";
        string itemName = Request.QueryString["itemName"] ?? "";
        int pageNumber = 1;
        int pagePerNumber = Convert.ToInt32(WebConfiguration.PagePerNumber);
        int howManyItems = 0;
        decimal priceMax = 0m;

        MonthBLL bll = new MonthBLL();
        DataTable dt = bll.GetJiaGeFenXiMingXiList(userId, itemType, itemName, pageNumber, pagePerNumber, out howManyItems, out priceMax);
 
        string max = "1";
        string step = "1";
        string itemBuyDate = "";
        string itemPrice = "";

        if (dt.Rows.Count > 0)
        {
            max = Math.Floor(priceMax).ToString();
            step = Math.Floor(Convert.ToDouble(max) / 10).ToString();
            
            int i = 0;
            foreach (DataRow dr in dt.Rows)
            {
                if (i == 15) break;
                string dot = (i < 15 - 1 && i < dt.Rows.Count - 1 ? "," : "");
                itemBuyDate += "{\"text\":\"" + Convert.ToDateTime(dr["ItemBuyDate"]).ToString("yyyy-MM-dd") + "\",\"rotate\":90}" + dot;
                itemPrice += dr["ItemPrice"].ToString() + dot;
                i++;
            }
        }

        Response.Write(GetChartJsonString(itemBuyDate, itemPrice, max, step));
        Response.End();
    }

    private string GetChartJsonString(string itemBuyDate, string itemPrice, string max, string step)
    {
        StringBuilder items = new StringBuilder();
        items.Append("{\"title\":{\"text\":\"消费价格明细\",\"style\":\"font-size:14px;font-family:Microsoft YaHei;text-align:center;\"},");
        items.Append("\"x_axis\":{\"labels\":{\"labels\":[" + itemBuyDate + "]}},");
        items.Append("\"y_axis\":{\"steps\":" + step + ", \"min\":0, \"max\":" + max + "},");
        items.Append("\"bg_colour\":\"#ffffff\",\"elements\":[");
        items.Append("{\"type\":\"area\",\"dot-style\":{\"type\":\"solid-dot\",\"tip\":\"￥#val# 元<br>#x_label#\",\"on-click\":\"chart_click\",\"dot-size\":4,\"halo-size\":1},\"values\":[" + itemPrice + "],\"colour\":\"#0000ff\",\"fill-alpha\":0.3,\"fill\":\"#0000ff\"}]}");

        return items.ToString();
    }
}