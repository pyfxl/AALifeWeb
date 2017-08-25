using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SexSpider_Test : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        //string htmlJson = GetHtmlJson();

        //var jsonObject = Newtonsoft.Json.JsonConvert.DeserializeObject<SexSpiderViewModel>(htmlJson);

        //List<SexSpider> sexSpiders = new List<SexSpider>();

        //jsonObject.site_list.ForEach(s => {
        //    sexSpiders.Add(new SexSpider {
        //        SiteId = Convert.ToInt32(s.siteid),
        //        SiteRank = s.siterank,
        //        VipLevel = Convert.ToByte(s.viplevel),
        //        IsHided = Convert.ToByte(s.ishided),
        //        SiteName = s.sitename,
        //        ListPage = s.listpage,
        //        PageEncode = s.pageencode,
        //        Domain = s.domain,
        //        SiteLink = s.sitelink,
        //        ListDiv = s.listdiv,
        //        ImageDiv = s.imagediv,
        //        PageDiv = s.pagediv,
        //        PageLevel = Convert.ToByte(s.pagelevel),
        //        ListFilter = s.listfilter,
        //        ImageFilter = s.imagefilter,
        //        PageFilter = s.pagefilter
        //    });
        //});

        //using(var db = new AALifeDbContext())
        //{
        //    db.SexSpider.AddRange(sexSpiders);
        //    db.SaveChanges();
        //}
    }

    private string GetHtmlJson()
    {
        WebClient client = new WebClient();
        client.Encoding = Encoding.GetEncoding("utf-8");
        client.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:46.0) Gecko/20100101 Firefox/46.0");
        return client.DownloadString("http://localhost:81/SexSpider/GetListData4.aspx");
    }
        
}