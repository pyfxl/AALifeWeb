using System;
using System.Text;
using System.Data;
using AALife.BLL;

public partial class AutoSmartItemPriceJson : BasePage
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
        string itemName = "";
        if (Request.QueryString["term"] != null && Request.QueryString["term"] != "")
        {
            itemName = Request.QueryString["term"];
        }

        ItemTableBLL bll = new ItemTableBLL();
        DataTable dt = bll.GetItemPriceListByItemName(userId, itemName);

        StringBuilder items = new StringBuilder();
        items.Append("[");

        if (dt.Rows.Count > 0)
        {
            foreach (DataRow dr in dt.Rows)
            {
                items.Append("{\"label\":" + "\"" + Convert.ToDouble(dr["ItemPrice"]).ToString("0.0##") + "\"},");
            }
            items.Remove(items.ToString().LastIndexOf(','), 1);
        }

        items.Append("]");

        Response.Write(items.ToString());
        Response.End();
    }
}