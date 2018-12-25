using System;
using System.Text;
using System.Data;
using AALife.BLL;

public partial class AutoItemNameJson : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int userId = Convert.ToInt32(Session["UserID"]);
        string keywords = "";

        if (Request.QueryString["term"] != "" && Request.QueryString["term"] != null)
        {
            keywords = Request.QueryString["term"].Replace("%", "");

            StringBuilder items = new StringBuilder();
            items.Append("[");

            ItemTableBLL bll = new ItemTableBLL();
            DataTable dt = bll.GetItemNameListByKeywords(userId, keywords);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    items.Append("{");
                    items.Append("\"id\":" + "\"" + dr["ItemName"].ToString() + "\",");
                    items.Append("\"label\":" + "\"" + dr["ItemName"].ToString() + "\"");
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