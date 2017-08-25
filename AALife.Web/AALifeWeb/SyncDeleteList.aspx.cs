using AALife.BLL;
using System;

public partial class AALifeWeb_SyncDeleteList : System.Web.UI.Page
{
    private ItemTableBLL bll = new ItemTableBLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        int itemId = Convert.ToInt32(Request.Form["itemid"]);
        int itemWebId = Convert.ToInt32(Request.Form["itemwebid"]);
        int userId = Convert.ToInt32(Request.Form["userid"]);
        
        string result = "{";

        bool success = bll.ItemExistsWithSync(userId, itemId, itemWebId);
        if (success)
        {
            success = bll.DeleteItemWithSync(userId, itemId, itemWebId);
        }
        else
        {
            success = true;
        }

        if (success)
        {
            result += "\"result\":\"ok\"";
        }
        else
        {
            result += "\"result\":\"error\"";
        }

        result += "}";

        Response.Write(result);
        Response.End();
    }
}