using System;
using System.Web;
using System.Text.RegularExpressions;
using NLog;

public class WebPage : System.Web.UI.Page
{
    public static Logger log = LogManager.GetCurrentClassLogger();
    private static string url = "";

    public WebPage()
    {
        HttpContext.Current.Response.AddHeader("P3P", "CP= CURa ADMa DEVa PSAo PSDo OUR BUSUNI PUR INT DEM STA PRE COM NAV OTC NOI DSP COR ");

        url = HttpContext.Current.Request.Url.ToString();

        this.Load += new EventHandler(WebPage_Load);
    }

    void WebPage_Load(object sender, EventArgs e)
    {
        if (object.Equals(Session["UserID"], null))
        {
            Response.Write("<script>window.location.href='/UserLogin.aspx?url=" + HttpUtility.UrlEncode(url) + "';</script>");
            Response.End();
        }
    }
}