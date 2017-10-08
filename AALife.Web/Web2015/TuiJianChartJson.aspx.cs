using System;
using System.Text;
using System.Data;
using System.Web;
using AALife.BLL;

public partial class TuiJianChartJson : BasePage
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

        MonthBLL bll = new MonthBLL();
        DataTable dt = bll.GetTuiJianFenXiList(userId);

        string max_x = "12";
        string max_y = "2";
        string value = "";
        string label = "";
        string data = "";

        if (dt.Rows.Count > 0)
        {
            max_y = dt.Rows.Count.ToString();
            data = "\"1\",\"2\",\"3\",\"4\",\"5\",\"6\",\"7\",\"8\",\"9\",\"10\",\"11\",\"12\"";

            int i = 0;
            foreach (DataRow dr in dt.Rows)
            {
                i++;
                value += (Convert.ToDateTime(dr["ItemBuyDate"]).ToString("MM") + ",");
                label += ("\"" + dr["ItemName"].ToString() + "\",");
            }

            value = value.Remove(value.Length - 1);
            label = label.Remove(label.Length - 1);
        }
        else
        {
            data = "\"空记录\"";
        }

        Response.Write(GetChartJsonString(value, label, data, max_x, max_y));
        Response.End();
    }

    private string GetChartJsonString(string value, string label, string data, string max_x, string max_y)
    {
        StringBuilder items = new StringBuilder();
        items.Append("{\"title\":{\"text\":\"商品推荐统计\",\"style\":\"font-size:14px;font-family:Microsoft YaHei;text-align:center;\"},");
        items.Append("\"radar_axis\":{\"max\":12,\"colour\":\"#A1D4B5\",\"grid-colour\":\"#C0DEBF\",\"labels\":{\"labels\":[" + data + "]},");
        items.Append("\"spoke-labels\":{\"labels\":[" + label + "]}},");
        items.Append("\"bg_colour\":\"#ffffff\",\"elements\":[");
        items.Append("{\"type\":\"area\",\"dot-style\":{\"type\":\"star\",\"colour\":\"#ff0000\",\"dot-size\":8,\"hollow\":false,\"on-click\":\"chart_click\",\"tip\":\"#val# 月\"},\"colour\":\"#ff0000\",\"fill\":\"#ff0000\",\"fill-alpha\":0.3,\"loop\":true,\"values\":[" + value + "]}]}");

        return items.ToString();
    }

    private string GetChartJsonStringScatter(string value, string max_x, string max_y)
    {
        StringBuilder items = new StringBuilder();
        items.Append("{\"title\":{\"text\":\"商品推荐统计\",\"style\":\"font-size:14px;font-family:Microsoft YaHei;text-align:center;\"},");
        items.Append("\"x_axis\":{\"steps\":1,\"min\":1,\"max\":" + max_x + ",\"labels\":[\"1\",\"1\",\"1\",\"1\",\"1\",\"1\",\"1\",\"1\",\"1\",\"1\",\"1\",\"1\"]}");
        items.Append("\"y_axis\":{\"steps\":1,\"min\":0,\"max\":" + max_y + "},");
        items.Append("\"bg_colour\":\"#ffffff\",\"elements\":[");
        items.Append("{\"type\":\"scatter\",\"dot-style\":{\"type\":\"star\",\"colour\":\"#ff0000\",\"dot-size\":8,\"on-click\":\"chart_click\"},\"values\":[" + value + "]}]}");

        return items.ToString();
    }
}