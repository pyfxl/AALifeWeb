using System;
using System.Text;
using System.Data;
using AALife.BLL;

public partial class Web2016_AutoItemGroupJson : WebPage
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

        string begin = Request.QueryString["begin"];
        string end = Request.QueryString["end"];

        string query = Request.QueryString["query"];
        string sub = Request.QueryString["sub"];

        string sort = Request.QueryString["sort"];
        string by = Request.QueryString["by"];

        ItemTableBLL bll = new ItemTableBLL();
        DataTable newlists = new DataTable();
        DataTable lists = bll.GetItemListByGroup(userId, Convert.ToDateTime(begin), Convert.ToDateTime(end), query, sub, "asc");
        if (lists.Rows.Count > 0)
        {
            DataRow[] rows = lists.Select("1=1", string.Format("{0} {1}", sort, by));
            if (rows.Length > 0)
            {
                newlists = rows.CopyToDataTable();
            }
        }

        StringBuilder items = new StringBuilder();

        items.Append("<table border=\"0\" class=\"tablelist\">");
        if (lists.Rows.Count > 0)
        {
            foreach (DataRow dr in newlists.Rows)
            {
                items.Append("<tr>");
                items.Append("<td style=\"width:140px;\">" + dr["MyLabel"].ToString() + "</td>");
                items.Append("<td style=\"width:100px;\" class=\"countcolor\">" + dr["CountNum"].ToString() + "</td>");
                items.Append("<td style=\"width:100px;\" class=\"shoucolor\">" + QueryHelper.GetPriceDot(dr["ShouRuPrice"], "sr") + "</td>");
                items.Append("<td style=\"width:100px;\" class=\"zhicolor\">" + QueryHelper.GetPriceDot(dr["ZhiChuPrice"], "zc") + "</td>");
                items.Append("<td style=\"width:100px;\" class=\"shoucolor\">" + QueryHelper.GetPriceDot(dr["JieRuPrice"], "jr") + "</td>");
                items.Append("<td style=\"width:100px;\" class=\"zhicolor\">" + QueryHelper.GetPriceDot(dr["HuanChuPrice"], "hc") + "</td>");
                items.Append("<td style=\"width:100px;\" class=\"shoucolor\">" + QueryHelper.GetPriceDot(dr["HuanRuPrice"], "hr") + "</td>");
                items.Append("<td style=\"width:100px;\" class=\"zhicolor\">" + QueryHelper.GetPriceDot(dr["JieChuPrice"], "jc") + "</td>");
                if (sub == "ItemBuyDate")
                {
                    items.Append("<td class=\"cellleft\"><a href=\"ItemQuery.aspx?date=" + dr["MyLabel"].ToString() + "&showType=d\" class=\"baselink\">查看</a></td>");
                }
                else if (sub == "ItemName")
                {
                    items.Append("<td class=\"cellleft\"><a href=\"javascript:void(0);\" class=\"itemdown baselink\" ref=\"" + dr["MyLabel"].ToString() + "\" query=\"" + query + "\">展开</a></td>");
                }
                else
                {
                    items.Append("<td class=\"cellleft\"><a href=\"javascript:void(0);\" class=\"detaildown baselink\" ref=\"" + dr["MyLabel"].ToString() + "\" query=\"" + query + "\">展开</a></td>");
                }
                items.Append("</tr>");
            }
        }
        else
        {
            items.Append("<tr>");
            items.Append("<td colspan=\"9\">没有消费记录。</td>");
            items.Append("</tr>");
        }
        items.Append("</table>");

        Response.Write(items.ToString());
        Response.End();
    }
}