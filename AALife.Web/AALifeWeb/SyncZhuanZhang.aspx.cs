using AALife.BLL;
using AALife.Model;
using System;
using System.Data;

public partial class AALifeWeb_SyncZhuanZhang : System.Web.UI.Page
{
    private ZhuanZhangTableBLL bll = new ZhuanZhangTableBLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        int zzId = Convert.ToInt32(Request.Form["zzid"]);
        string zhangFrom = Request.Form["zhangfrom"].ToString();
        string zhangTo = Request.Form["zhangto"].ToString();
        string zhangMoney = Request.Form["zhangmoney"].ToString();
        string zhangDate = Request.Form["zhangdate"].ToString();
        string zhangNote = Request.Form["zhangnote"].ToString();
        byte zhangLive = Convert.ToByte(Request.Form["zhanglive"]);
        int userId = Convert.ToInt32(Request.Form["userid"]);

        ZhuanZhangInfo zhang = bll.GetZhuanZhangByZZID(userId, zzId);
        zhang.ZZID = zzId;
        zhang.ZhuanZhangFrom = Convert.ToInt32(zhangFrom);
        zhang.ZhuanZhangTo = Convert.ToInt32(zhangTo);
        zhang.ZhuanZhangMoney = Convert.ToDecimal(zhangMoney);
        zhang.ZhuanZhangDate = Convert.ToDateTime(zhangDate);
        zhang.ZhuanZhangNote = zhangNote;
        zhang.UserID = userId;
        zhang.ZhuanZhangLive = zhangLive;
        zhang.Synchronize = 0;
        zhang.ModifyDate = DateTime.Now;

        bool success = false;
        if (zhang.ZhuanZhangID == 0)
        {
            success = bll.InsertZhuanZhang(zhang);
        }
        else
        {
            success = bll.UpdateZhuanZhang(zhang);
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