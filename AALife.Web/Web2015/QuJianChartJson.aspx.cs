using System;
using System.Text;
using System.Data;
using AALife.BLL;

public partial class QuJianChartJson : BasePage
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
        DataTable dt = bll.GetQuJianTongJiList(userId);

        string value = "";
        string itemName = "";

        if (dt.Rows.Count > 0)
        {
            int i = 0;
            foreach (DataRow dr in dt.Rows)
            {
                DateTime d1 = Convert.ToDateTime(dr["ItemBuyDate"]);
                DateTime d2 = Convert.ToDateTime(dr["ItemBuyDateMax"]);
                string dot = (i < dt.Rows.Count - 1 ? "," : "");
                value += "{\"left\":\"" + (Convert.ToInt32(d1.ToString("MM")) - 1) + "\",\"right\":" + (GetRegionDate(d1, d2) - 1) + ",\"tip\":\"区间 " + d1.ToString("yyyy-MM-dd") + "~" + d2.ToString("yyyy-MM-dd") + "\"}" + dot;
                itemName = "\"" + dr["ItemName"].ToString() + "\"," + itemName;
                i++;
            }
            itemName = itemName.Remove(itemName.Length - 1);
        }
        else
        {
            itemName = "\"0\"";
        }

        Response.Write(GetChartJsonString(value, itemName));
        Response.End();
    }

    private string GetChartJsonString(string value, string itemName)
    {
        StringBuilder items = new StringBuilder();
        items.Append("{\"title\":{\"text\":\"消费区间统计\",\"style\":\"font-size:14px;font-family:Microsoft YaHei;text-align:center;\"},");
        items.Append("\"x_axis\":{\"labels\":{\"labels\":[\"一月\",\"二月\",\"三月\",\"四月\",\"五月\",\"六月\",\"七月\",\"八月\",\"九月\",\"十月\",\"十一月\",\"十二月\"]}},");
        items.Append("\"y_axis\":{\"offset\":1,\"labels\":[" + itemName + "]},");
        items.Append("\"bg_colour\":\"#ffffff\",\"elements\":[");
        items.Append("{\"type\":\"hbar\",\"values\":[" + value + "],\"colour\":\"#742894\"}]}");

        return items.ToString();
    }

    private int GetRegionDate(DateTime d1, DateTime d2)
    {
        if (d2.Year == d1.Year && d2.Month == d1.Month && d2.Day > d1.Day)
        {
            return d2.Month + 1;
        }
        else
        {
            return d2.Month;
        }
    }
}