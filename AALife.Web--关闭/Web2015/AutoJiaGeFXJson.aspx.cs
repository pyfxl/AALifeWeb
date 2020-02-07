using System;
using System.Text;
using System.Data;
using AALife.BLL;

public partial class AutoJiaGeFXJson : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["page"] != "" && Request.QueryString["page"] != null)
        {
            int userId = Convert.ToInt32(Session["UserID"]);
            int pageNumber = Convert.ToInt32(Request.QueryString["page"]);
            int pagePerNumber = Convert.ToInt32(WebConfiguration.PagePerNumber);
            int howManyItems = 0;
            decimal priceMax = 0m;

            StringBuilder items = new StringBuilder();
            items.Append("[");

            MonthBLL bll = new MonthBLL();
            DataTable dt = bll.GetJiaGeFenXiList(userId, pageNumber, pagePerNumber, out howManyItems, out priceMax);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    items.Append("{");
                    items.Append("\"RowNumber\":" + "\"" + dr["RowNumber"].ToString() + "\",");
                    items.Append("\"ItemType\":" + "\"" + dr["ItemType"].ToString() + "\",");
                    items.Append("\"ItemTypeName\":" + "\"" + dr["ItemTypeName"].ToString() + "\",");
                    items.Append("\"ItemName\":" + "\"" + dr["ItemName"].ToString() + "\",");
                    items.Append("\"ItemBuyDate\":" + "\"" + Convert.ToDateTime(dr["ItemBuyDate"]).ToString("yyyy-MM-dd") + "\",");
                    items.Append("\"ItemPrice\":" + "\"" + Convert.ToDouble(dr["ItemPrice"]).ToString("0.0##") + "\"");
                    items.Append("},");
                }
                items.Remove(items.ToString().LastIndexOf(','), 1);
            }

            items.Append("]");

            Response.Write(items.ToString());
            Response.End();
        }
    }
}