using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public partial class SexSpider_GetListJson4 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Business.SiteService service = new Business.SiteService();

        StringBuilder result = new StringBuilder();
        
        //具体对象
        result.Append(service.GetSiteList());
        
        Response.Write(result.ToString());
        Response.End();
    }
    
}