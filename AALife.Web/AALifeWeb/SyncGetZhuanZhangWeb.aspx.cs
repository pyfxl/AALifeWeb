using AALife.BLL;
using System;
using System.Data;

public partial class AALifeWeb_SyncGetZhuanZhangWeb : System.Web.UI.Page
{
    private ZhuanZhangTableBLL bll = new ZhuanZhangTableBLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        int userId = Convert.ToInt32(Request.Form["userid"]);

        DataTable dt = bll.GetZhuanZhangListWithSync(userId);

        string result = "{";
        if (dt.Rows.Count > 0)
        {
            result += "\"zzlist\":[";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                result += "{\"zzid\":\"" + dt.Rows[i]["ZZID"].ToString() + "\",";
                result += "\"zhangfrom\":\"" + dt.Rows[i]["ZhuanZhangFrom"].ToString() + "\",";
                result += "\"zhangto\":\"" + dt.Rows[i]["ZhuanZhangTo"].ToString() + "\",";
                result += "\"zhangmoney\":\"" + dt.Rows[i]["ZhuanZhangMoney"].ToString() + "\",";
                result += "\"zhangdate\":\"" + Convert.ToDateTime(dt.Rows[i]["ZhuanZhangDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "\",";
                result += "\"zhangnote\":\"" + dt.Rows[i]["ZhuanZhangNote"].ToString() + "\",";
                result += "\"zhanglive\":\"" + dt.Rows[i]["ZhuanZhangLive"].ToString() + "\"},";
            }
            result = result.Substring(0, result.Length - 1);
            result += "]";
        }
        else
        {
            result += "\"zzlist\":[]";
        }
        result += "}";

        Response.Write(result);
        Response.End();
    }
}