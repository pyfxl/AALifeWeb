using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SexSpider_List : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            
        }
    }

    [WebMethod(EnableSession = true)]
    public static string CheckSiteLink(string siteLink, string encoding)
    {
        string content = Business.SiteHelper.GetHtmlContent(siteLink, encoding);

        return "OK";
    }
}