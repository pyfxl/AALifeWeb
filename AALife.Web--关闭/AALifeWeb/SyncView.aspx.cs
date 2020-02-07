using AALife.BLL;
using AALife.Model;
using System;
using System.Data;

public partial class AALifeWeb_SyncView : SyncBase
{
    private ViewTableBLL bll = new ViewTableBLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        int pageId = Convert.ToInt32(Request.Form["pageid"]);
        DateTime dateStart = Convert.ToDateTime(Request.Form["datestart"]);
        DateTime dateEnd = Convert.ToDateTime(Request.Form["dateend"]);
        string portal = Request.Form["portal"].ToString();
        string version = Request.Form["version"].ToString();
        string browser = Request.Form["browser"].ToString();
        int width = Convert.ToInt32(Request.Form["width"] ?? "0");
        int height = Convert.ToInt32(Request.Form["height"] ?? "0");
        string remark = Request.Form["remark"].ToString();
        int userId = Convert.ToInt32(Request.Form["userid"]);
        string network = Request.Form["network"] ?? "";

        ViewInfo info = new ViewInfo();
        info.PageID = pageId;
        info.DateStart = dateStart;
        info.DateEnd = dateEnd;
        info.Portal = portal;
        info.Version = version;
        info.Browser = browser;
        info.Width = width;
        info.Height = height;
        info.Remark = remark;
        info.UserID = userId;
        info.Network = network;
        
        bool success = false;
        if (info.ViewID == 0)
        {
            try
            {
                success = bll.InsertView(info);
            }
            catch (Exception ex) { }
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