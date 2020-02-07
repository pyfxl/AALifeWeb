using System;
using System.Text;
using System.Data;
using AALife.BLL;

public partial class AutoItemListJson : BasePage
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

        DateTime today = DateTime.Now;
        if (Request.QueryString["term"] != null && Request.QueryString["term"] != "")
        {
            today = Convert.ToDateTime(Request.QueryString["term"]);
        }

        MonthBLL bll = new MonthBLL();
        DataTable lists = bll.GetMonthListByItemBuyDate(userId, today);

        StringBuilder items = new StringBuilder();

        if (lists.Rows.Count > 0)
        {
            items.Append("<table cellspacing=\"0\" border=\"1\" style=\"width:100%;\" class=\"tablelist\">");
            foreach (DataRow dr in lists.Rows)
            {
                double ShouRuPrice = Convert.ToDouble(dr["ShouRuPrice"]);
                double ZhiChuPrice = Convert.ToDouble(dr["ZhiChuPrice"]);
                double JiePrice = Convert.ToDouble(dr["JiePrice"]);
                double HuanPrice = Convert.ToDouble(dr["HuanPrice"]);

                items.Append("<tr>");
                items.Append("<td style=\"width:16%;\">" + dr["ItemName"].ToString() + "</td>");
                items.Append("<td style=\"width:17%;\" class=\"cellprice\">" + (ShouRuPrice == 0 ? "" : "￥" + ShouRuPrice.ToString("0.0##")) + "</td>");
                items.Append("<td style=\"width:17%;\" class=\"cellprice\">" + (ZhiChuPrice == 0 ? "" : "￥" + ZhiChuPrice.ToString("0.0##")) + "</td>");
                items.Append("<td style=\"width:17%;\" class=\"cellprice\">" + (JiePrice == 0 ? "" : "￥" + JiePrice.ToString("0.##")) + "</td>");
                items.Append("<td style=\"width:17%;\" class=\"cellprice\">" + (HuanPrice == 0 ? "" : "￥" + HuanPrice.ToString("0.##")) + "</td>");
                items.Append("<td style=\"width:16%;\"><a href=\"ItemList.aspx?date=" + Convert.ToDateTime(dr["ItemBuyDate"]).ToString("yyyy-MM-dd") + "\">查看</a></td>");
                items.Append("</tr>");
            }
            items.Append("</table>");
        }

        Response.Write(items.ToString());
        Response.End();
    }
}