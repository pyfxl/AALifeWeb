using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

/// <summary>
/// SyncBase 的摘要说明
/// </summary>
public class SyncBase : System.Web.UI.Page
{
    public static Logger log = LogManager.GetCurrentClassLogger();

    public static bool useMsmq = ConfigurationManager.AppSettings["useMsmq"] == "1";

    public SyncBase()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //

        HttpContext.Current.Response.AddHeader("P3P", "CP= CURa ADMa DEVa PSAo PSDo OUR BUSUNI PUR INT DEM STA PRE COM NAV OTC NOI DSP COR ");
        
        this.Load += new EventHandler(SyncBase_Load);
    }

    void SyncBase_Load(object sender, EventArgs e)
    {
        //if (object.Equals(Session["UserID"], null))
        //{
        //    Response.Write("{\"result\": \"error\"}");
        //    Response.End();
        //}
    }
}