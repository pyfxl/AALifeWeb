using AALife.BLL;
using System;

public partial class AALifeWeb_SyncCardWebBack : System.Web.UI.Page
{
    private CardTableBLL bll = new CardTableBLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        int userId = Convert.ToInt32(Request.Form["userid"]);

        bool success = bll.UpdateCardListWebBack(userId);

        string result = "{";
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