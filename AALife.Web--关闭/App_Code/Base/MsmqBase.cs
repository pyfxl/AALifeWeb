using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// MsmqBase 的摘要说明
/// </summary>
public class MsmqBase : System.Web.UI.Page
{
    public static Logger log = LogManager.GetCurrentClassLogger();

    public MsmqBase()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //

        HttpContext.Current.Response.AddHeader("P3P", "CP= CURa ADMa DEVa PSAo PSDo OUR BUSUNI PUR INT DEM STA PRE COM NAV OTC NOI DSP COR ");
        
        this.Load += new EventHandler(MsmqBase_Load);
    }

    void MsmqBase_Load(object sender, EventArgs e)
    {
        
    }
}