using AALife.BLL;
using System;

public partial class AALifeWeb_SyncZhuanTiWebBack : System.Web.UI.Page
{
    private ZhuanTiTableBLL bll = new ZhuanTiTableBLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        int userId = Convert.ToInt32(Request.Form["userid"]);

        bool success = bll.UpdateZhuanTiListWebBack(userId);

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