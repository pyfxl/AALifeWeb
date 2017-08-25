using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SexSpider_GetImageJson4 : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            string url = Request["url"] ?? "";

            int siteId = Convert.ToInt32(Request["siteId"] ?? "0");

            if (siteId > 0 && url != "")
            {
                Business.SiteService service = new Business.SiteService();

                var sexSpider = service.GetSexSpider(siteId);

                var lists = GetImages(sexSpider, Server.UrlDecode(url));

                this.Repeater1.DataSource = lists;
                this.Repeater1.DataBind();
            }
        }
    }

    private List<Business.Models.ImageModel> GetImages(Repository.SexSpider sexSpider, string url)
    {
        var lists = new List<Business.Models.ImageModel>();

        try
        {
            if (sexSpider.PageLevel == 0)
            {
                lists = Business.SiteHelper.GetListImage(url, sexSpider.PageEncode, sexSpider.ImageDiv, sexSpider.ImageFilter, sexSpider.Domain).ToList();
            }
            else
            {
                lists = Business.SiteHelper.GetListImagePage(url, sexSpider.PageEncode, sexSpider.ImageDiv, sexSpider.ImageFilter, sexSpider.Domain, sexSpider.PageDiv, sexSpider.PageFilter, sexSpider.PageLevel).ToList();
            }
        }
        catch (Exception ex)
        {
        }

        return lists;
    }

}