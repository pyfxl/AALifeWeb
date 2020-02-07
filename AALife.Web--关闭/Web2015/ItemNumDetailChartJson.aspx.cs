using System;
using System.Text;
using System.Data;
using AALife.BLL;

public partial class ItemNumDetailChartJson : BasePage
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
        string title = Request.QueryString["title"];

        int userId = Convert.ToInt32(Session["UserID"]);
        DateTime today = Utility.GetRequestDate(Request.QueryString["date"]);

        string catTypeId = "0";
        if (Request.QueryString["catTypeId"] != null && Request.QueryString["catTypeId"] != "")
        {
            catTypeId = Request.QueryString["catTypeId"];
        }

        string itemType = Request.QueryString["itemType"] ?? "";
        string itemName = Request.QueryString["itemName"] ?? "";
          
        MonthBLL bll = new MonthBLL();      
        DataTable dt = bll.GetItemNumDetailList(userId, today, Convert.ToInt32(catTypeId), itemType, itemName, "chart");

        decimal priceMax = 0m;
        string max = "1";
        string step = "1";
        string itemNameValue = "";
        string itemPrice = "";

        if (dt.Rows.Count > 0)
        {
            priceMax = Convert.ToDecimal(dt.Rows[0]["ItemPrice"]);
            max = Math.Ceiling(priceMax).ToString();
            step = Math.Floor(Convert.ToDouble(max) / 10).ToString();

            int i = 0;
            foreach (DataRow dr in dt.Rows)
            {
                if (i == 15) break;
                string dot = (i < 15 - 1 && i < dt.Rows.Count - 1 ? "," : "");
                itemNameValue += "{\"text\":\"" + Convert.ToDateTime(dr["ItemBuyDate"]).ToString("yyyy-MM-dd") + "\",\"rotate\":90}" + dot;
                itemPrice += dr["ItemPrice"].ToString() + dot;
                i++;
            }
        }
        else
        {
            itemNameValue = "{\"text\":\"0\"},{\"text\":\"1\"}";
        }

        Response.Write(GetChartJsonString(itemNameValue, itemPrice, title, max, step));
        Response.End();
    }

    private string GetChartJsonString(string itemNameValue, string itemPrice, string title, string max, string step)
    {
        StringBuilder items = new StringBuilder();
        items.Append("{\"title\":{\"text\":\"" + title + "\",\"style\":\"font-size:14px;font-family:Microsoft YaHei;text-align:center;\"},");
        items.Append("\"x_axis\":{\"labels\":{\"labels\":[" + itemNameValue + "]}},");
        items.Append("\"y_axis\":{\"steps\":" + step + ", \"min\":0, \"max\":" + max + "},");
        items.Append("\"bg_colour\":\"#ffffff\",\"elements\":[");
        items.Append("{\"type\":\"area\",\"dot-style\":{\"type\":\"solid-dot\",\"dot-size\":4,\"halo-size\":1,\"tip\":\"￥#val# 元<br>#x_label#\",\"on-click\":\"chart_click\"},\"values\":[" + itemPrice + "],\"colour\":\"#0000ff\",\"fill-alpha\":0.3,\"fill\":\"#0000ff\"}]}");

        return items.ToString();
    }
}