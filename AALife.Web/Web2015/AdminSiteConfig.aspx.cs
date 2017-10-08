using System;

public partial class AdminSiteConfig : AdminPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
        }
    }

    protected void BindData()
    {
        this.SiteNameBox.Text = WebConfiguration.SiteName;
        this.SiteAuthorBox.Text = WebConfiguration.SiteAuthor;
        this.SiteKeywordsBox.Text = WebConfiguration.SiteKeywords;
        this.SiteDescriptionBox.Text = WebConfiguration.SiteDescription;
        this.PagePerNumberBox.Text = WebConfiguration.PagePerNumber;
        this.SiteTipsBox.Text = Utility.UnReplaceString(WebConfiguration.SiteTips);
        this.MessageCodeBox.Text = WebConfiguration.MessageCode;
        this.SiteMessageBox.Text = Utility.UnReplaceString(WebConfiguration.SiteMessage);
        this.PhoneMessageBox.Text = Utility.UnReplaceString(WebConfiguration.PhoneMessage);
        this.UserWorkDayBox.Text = WebConfiguration.UserWorkDay;
        this.CategoryRateBox.Text = WebConfiguration.CategoryRate;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string siteName = this.SiteNameBox.Text.Trim();
        string siteAuthor = this.SiteAuthorBox.Text.Trim();
        string siteKeywords = this.SiteKeywordsBox.Text.Trim();
        string siteDescription = this.SiteDescriptionBox.Text.Trim();
        string pagePerNumber = this.PagePerNumberBox.Text.Trim();
        string siteTips = Utility.ReplaceString(this.SiteTipsBox.Text.Trim());
        string messageCode = this.MessageCodeBox.Text.Trim();
        string siteMessage = Utility.ReplaceString(this.SiteMessageBox.Text.Trim());
        string phoneMessage = Utility.ReplaceString(this.PhoneMessageBox.Text.Trim());
        string userWorkDay = this.UserWorkDayBox.Text.Trim();
        string categoryRate = this.CategoryRateBox.Text.Trim();

        string strXmlFile = Server.MapPath("~/Site.Config");
        XmlControl xmlTool = new XmlControl(strXmlFile);
        xmlTool.Update("Root/SiteName", siteName);
        xmlTool.Update("Root/SiteAuthor", siteAuthor);
        xmlTool.Update("Root/SiteKeywords", siteKeywords);
        xmlTool.Update("Root/SiteDescription", siteDescription);
        xmlTool.Update("Root/PagePerNumber", pagePerNumber);
        xmlTool.Update("Root/SiteTips", siteTips);
        xmlTool.Update("Root/MessageCode", messageCode);
        xmlTool.Update("Root/SiteMessage", siteMessage);
        xmlTool.Update("Root/PhoneMessage", phoneMessage);
        xmlTool.Update("Root/UserWorkDay", userWorkDay);
        xmlTool.Update("Root/CategoryRate", categoryRate);
        
        bool success = xmlTool.Save();
        xmlTool.Dispose();
        if (success)
        {
            WebConfiguration.GetConfig();
            Utility.Alert(this, "修改成功。", "AdminSiteConfig.aspx");
        }
        else
        {
            Utility.Alert(this, "修改失败！");
        }
    }
}