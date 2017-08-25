using AALife.BLL;
using System;
using System.Data;

public partial class AALifeWeb_SyncGetDeleteListWeb : System.Web.UI.Page
{
    private DeleteTableBLL bll = new DeleteTableBLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        int userId = Convert.ToInt32(Request.Form["userid"]);

        DataTable dt = bll.GetDeleteListByUserId(userId);

        string result = "{";
        if (dt.Rows.Count > 0)
        {
            result += "\"deletelist\":[";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                result += "{\"itemid\":\"" + dt.Rows[i]["ItemID"].ToString() + "\",";
                result += "\"itemappid\":\"" + dt.Rows[i]["ItemAppID"].ToString() + "\"},";
            }
            result = result.Substring(0, result.Length - 1);
            result += "]";
        }
        else
        {
            result += "\"deletelist\":[]";
        }
        result += "}";

        Response.Write(result);
        Response.End();
    }
}