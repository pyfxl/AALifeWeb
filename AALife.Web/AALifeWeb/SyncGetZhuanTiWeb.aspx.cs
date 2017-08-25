using AALife.BLL;
using System;
using System.Data;

public partial class AALifeWeb_SyncGetZhuanTiWeb : System.Web.UI.Page
{
    private ZhuanTiTableBLL bll = new ZhuanTiTableBLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        int userId = Convert.ToInt32(Request.Form["userid"]);

        DataTable dt = bll.GetZhuanTiListWithSync(userId);

        string result = "{";
        if (dt.Rows.Count > 0)
        {
            result += "\"ztlist\":[";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                result += "{\"ztid\":\"" + dt.Rows[i]["ZTID"].ToString() + "\",";
                result += "\"ztname\":\"" + dt.Rows[i]["ZhuanTiName"].ToString() + "\",";
                result += "\"ztimage\":\"" + dt.Rows[i]["ZhuanTiImage"].ToString() + "\",";
                result += "\"ztlive\":\"" + dt.Rows[i]["ZhuanTiLive"].ToString() + "\"},";
            }
            result = result.Substring(0, result.Length - 1);
            result += "]";
        }
        else
        {
            result += "\"ztlist\":[]";
        }
        result += "}";

        Response.Write(result);
        Response.End();
    }
}