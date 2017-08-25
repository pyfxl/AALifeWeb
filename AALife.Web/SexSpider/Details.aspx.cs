using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SexSpider_Details : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        var lists = new List<Business.Models.ListModel>();
        
        string url = this.siteLinkHid.Value;
        string encoding = this.pageEncodeHid.Value;
        string listStart = this.listDivHid.Value;
        string listFilter = this.listFilterHid.Value;
        string domain = this.domainHid.Value;

        try
        {
            lists = Business.SiteHelper.GetSiteList(url, encoding, listStart, listFilter, domain).ToList();
        }
        catch(Exception ex)
        {
        }

        this.Repeater1.DataSource = lists;
        this.Repeater1.DataBind();
    }

    protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        var lists = new List<Business.Models.ImageModel>();

        string url = e.CommandArgument.ToString();
        string encoding = this.pageEncodeHid.Value;
        string imageStart = this.imageDivHid.Value;
        string imageFilter = this.imageFilterHid.Value;
        string pageStart = this.pageDivHid.Value;
        string pageFilter = this.pageFilterHid.Value;
        string domain = this.domainHid.Value;
        string pageLevel = this.pageLevelHid.Value;

        try
        {
            if(pageLevel == "0")
            {
                lists = Business.SiteHelper.GetListImage(url, encoding, imageStart, imageFilter, domain).ToList();
            }
            else
            {
                lists = Business.SiteHelper.GetListImagePage(url, encoding, imageStart, imageFilter, domain, pageStart, pageFilter, pageLevel).ToList();
            }          
        }
        catch (Exception ex)
        {
        }

        this.Repeater2.DataSource = lists;
        this.Repeater2.DataBind();

        this.UpdatePanel2.Update();
    }
}