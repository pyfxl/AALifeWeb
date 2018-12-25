using System;
using System.Text;
using System.Data;
using AALife.BLL;

public partial class FLZJChartJson : BasePage
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

        MonthBLL bll = new MonthBLL();
        DataTable dt = bll.GetFenLeiZongJiList(userId, today);

        string value = "";

        if (dt.Rows.Count > 0 && Convert.ToDouble(dt.Rows[0]["ZhiPriceTotal"]) > 0)
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (Convert.ToDouble(dr["ZhiPriceTotal"]) > 0)
                {
                    value += "{\"value\":" + dr["ZhiPriceTotal"].ToString() + ",\"label\":\"" + dr["CategoryTypeName"].ToString() + "\",\"text\":\"" + dr["CategoryTypeName"].ToString() + "\"},";
                }
            }

            value = value.Remove(value.Length - 1);
        }
        else
        {
            value = "{\"value\":100,\"label\":\"空记录\",\"text\":\"空记录\"}";
        }

        Response.Write(GetChartJsonString(value));
        Response.End();
    }

    private string GetChartJsonString(string value)
    {
        StringBuilder items = new StringBuilder();
        items.Append("{\"title\":{\"text\":\"消费类别排行\",\"style\":\"font-size:14px;font-family:Microsoft YaHei;text-align:center;\"},");
        items.Append("\"legend\":{\"visible\":true,\"bg_colour\":\"#fefefe\",\"position\":\"right\",\"border\":true,\"shadow\":true},");
        items.Append("\"bg_colour\":\"#ffffff\",\"elements\":[");
        items.Append("{\"type\":\"pie\",\"tip\":\"￥#val#<br>#label# #percent#\",\"values\":[" + value + "],\"on-click\":\"chart_click\",\"start-angle\":35,\"animate\":[{\"type\":\"fade\"},{\"type\":\"bounce\",\"distance\":4}],\"gradient-fill\":true,\"alpha\":0.5,");
        items.Append("\"colours\":[\"#ff0000\",\"#00ff00\",\"#0000ff\",\"#ff9900\",\"#ff00ff\",\"#FFFF00\",\"#6699FF\",\"#339933\",\"#00ff00\"]}]}");
        
        return items.ToString();
    }
}