using AALife.BLL;
using AALife.Model;
using System;
using System.Data;

public partial class AALifeWeb_SyncZhuanTi : System.Web.UI.Page
{
    private ZhuanTiTableBLL bll = new ZhuanTiTableBLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        int ztId = Convert.ToInt32(Request.Form["ztid"]);
        string ztName = Request.Form["ztname"].ToString();
        string ztImage = Request.Form["ztimage"].ToString();
        byte ztLive = Convert.ToByte(Request.Form["ztlive"]);
        int userId = Convert.ToInt32(Request.Form["userid"]);

        ZhuanTiInfo zhuanTi = bll.GetZhuanTiByZhuanTiId(userId, ztId);
        zhuanTi.ZTID = ztId;
        zhuanTi.ZhuanTiName = ztName;
        zhuanTi.ZhuanTiImage = ztImage;
        zhuanTi.UserID = userId;
        zhuanTi.ZhuanTiLive = ztLive;
        zhuanTi.Synchronize = 0;
        zhuanTi.ModifyDate = DateTime.Now;

        bool success = false;
        if (zhuanTi.ZhuanTiID == 0)
        {
            success = bll.InsertZhuanTi(zhuanTi);
        }
        else
        {
            success = bll.UpdateZhuanTi(zhuanTi);
        }
                        
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