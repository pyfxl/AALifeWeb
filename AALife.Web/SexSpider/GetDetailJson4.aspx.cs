using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SexSpider_GetDetailJson4 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string result = "";
        
        int siteId = Convert.ToInt32(Request["siteId"] ?? "0");

        if (siteId > 0)
        {
            Business.SiteService service = new Business.SiteService();

            var sexSpider = service.GetSexSpider(siteId);

            var lists = GetLists(sexSpider);

            result = Newtonsoft.Json.JsonConvert.SerializeObject(lists);
        }
        else
        {
            result = "SiteId 无效！";
        }

        Response.Write(result.ToString());
        Response.End();
    }

    private List<Business.Models.ListModel> GetLists(Repository.SexSpider sexSpider)
    {
        var lists = new List<Business.Models.ListModel>();

        try
        {
            lists = Business.SiteHelper.GetSiteList(sexSpider.SiteLink, sexSpider.PageEncode, sexSpider.ListDiv, sexSpider.ListFilter, sexSpider.Domain).ToList();
        }
        catch (Exception ex)
        {
        }

        return lists;
    }
}