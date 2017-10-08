using System;
using System.Text;
using System.Data;
using AALife.BLL;

public partial class AutoSmartItemNameJson : BasePage
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
        int catTypeId = 0;
        if (Request.QueryString["term"] != null && Request.QueryString["term"] != "")
        {
            catTypeId = Convert.ToInt32(Request.QueryString["term"]);
        }

        ItemTableBLL bll = new ItemTableBLL();
        DataTable dt = bll.GetItemNameListByCategoryId(userId, catTypeId);

        StringBuilder items = new StringBuilder();
        items.Append("[");

        if (dt.Rows.Count > 0)
        {
            foreach (DataRow dr in dt.Rows)
            {
                items.Append("{\"label\":" + "\"" + dr["ItemName"].ToString() + "\"},");
            }
            items.Remove(items.ToString().LastIndexOf(','), 1);
        }

        items.Append("]");

        Response.Write(items.ToString());
        Response.End();
    }
}