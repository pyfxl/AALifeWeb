using AALife.BLL;
using NLog;
using System;

public partial class AALifeWeb_SyncItemListWebBack : System.Web.UI.Page
{
    public static Logger log = LogManager.GetCurrentClassLogger();
    private ItemTableBLL bll = new ItemTableBLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        int itemId = Convert.ToInt32(Request.Form["itemid"]);
        int itemAppId = Convert.ToInt32(Request.Form["itemappid"]);

        bool success = bll.UpdateItemListWebBack(itemId, itemAppId);

        //写日志
        //log.Info(string.Format(" Success:{0} | ItemID:{0} | ItemAppID:{1}", success, itemId, itemAppId));

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