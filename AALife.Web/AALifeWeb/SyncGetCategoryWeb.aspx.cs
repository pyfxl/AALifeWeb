using AALife.BLL;
using System;
using System.Data;

public partial class AALifeWeb_SyncGetCategoryWeb : System.Web.UI.Page
{
    private UserCategoryTableBLL bll = new UserCategoryTableBLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        int userId = Convert.ToInt32(Request.Form["userid"]);

        DataTable dt = bll.GetUserCategoryListWithSync(userId);

        string result = "{";
        if (dt.Rows.Count > 0)
        {
            result += "\"catlist\":[";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                result += "{\"catid\":\"" + dt.Rows[i]["CategoryTypeID"].ToString() + "\",";
                result += "\"catname\":\"" + dt.Rows[i]["CategoryTypeName"].ToString() + "\",";
                result += "\"catprice\":\"" + dt.Rows[i]["CategoryTypePrice"].ToString() + "\",";
                result += "\"catlive\":\"" + dt.Rows[i]["CategoryTypeLive"].ToString() + "\"},";
            }
            result = result.Substring(0, result.Length - 1);
            result += "]";
        }
        else
        {
            result += "\"catlist\":[]";
        }
        result += "}";

        Response.Write(result);
        Response.End();
    }
}