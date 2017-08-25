using AALife.BLL;
using System;

public partial class AALifeWeb_SyncDeleteListWebBack : System.Web.UI.Page
{
    private DeleteTableBLL bll = new DeleteTableBLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        int userId = Convert.ToInt32(Request.Form["userid"]);

        bool success = bll.UpdateDeleteListWebBack(userId);

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