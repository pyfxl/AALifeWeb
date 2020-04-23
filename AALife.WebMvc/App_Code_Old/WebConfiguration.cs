using System.Configuration;
using System.Web;
using System;

public class WebConfiguration
{
    //private static string dbConnectionString;
    //private static string dbProviderName;
    //private static string webSite;
    //private static string siteName;
    //private static string siteAuthor;
    //private static string siteKeywords;
    //private static string siteDescription;
    //private static string pagePerNumber;
    //private static string siteTips;
    //private static string messageCode;
    //private static string siteMessage;
    //private static string phoneMessage;
    //private static string userWorkDay;
    //private static string categoryRate;

    static WebConfiguration()
    {
        DbConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnString"].ConnectionString;
        DbProviderName = ConfigurationManager.ConnectionStrings["DefaultConnString"].ProviderName;
        WebSite = ConfigurationManager.AppSettings["webSite"];
        GetConfig();
    }

    public static void GetConfig()
    {
        string strXmlFile = HttpContext.Current.Server.MapPath("/Site.Config");
        XmlControl xmlTool = new XmlControl(strXmlFile);

        SiteName = xmlTool.GetText("Root/SiteName");
        SiteAuthor = xmlTool.GetText("Root/SiteAuthor");
        SiteKeywords = xmlTool.GetText("Root/SiteKeywords");
        SiteDescription = xmlTool.GetText("Root/SiteDescription");
        PagePerNumber = xmlTool.GetText("Root/PagePerNumber");
        SiteTips = xmlTool.GetText("Root/SiteTips");
        MessageCode = xmlTool.GetText("Root/MessageCode");
        SiteMessage = xmlTool.GetText("Root/SiteMessage");
        PhoneMessage = xmlTool.GetText("Root/PhoneMessage");
        UserWorkDay = xmlTool.GetText("Root/UserWorkDay");
        CategoryRate = xmlTool.GetText("Root/CategoryRate");
        xmlTool.Dispose();
    }

    public static void SetConfig()
    {
        string strXmlFile = HttpContext.Current.Server.MapPath("~/Site.Config");
        XmlControl xmlTool = new XmlControl(strXmlFile);

        xmlTool.Update("Root/SiteName", SiteName);
        xmlTool.Update("Root/SiteAuthor", SiteAuthor);
        xmlTool.Update("Root/SiteKeywords", SiteKeywords);
        xmlTool.Update("Root/SiteDescription", SiteDescription);
        xmlTool.Update("Root/PagePerNumber", PagePerNumber);
        xmlTool.Update("Root/SiteTips", SiteTips);
        xmlTool.Update("Root/MessageCode", MessageCode);
        xmlTool.Update("Root/SiteMessage", SiteMessage);
        xmlTool.Update("Root/PhoneMessage", PhoneMessage);
        xmlTool.Update("Root/UserWorkDay", UserWorkDay);
        xmlTool.Update("Root/CategoryRate", CategoryRate);

        xmlTool.Save();
        xmlTool.Dispose();
    }

    public static string DbConnectionString { get; }

    public static string DbProviderName { get; }

    public static string WebSite { get; }

    public static string SiteName { get; set; }

    public static string SiteAuthor { get; set; }

    public static string SiteKeywords { get; set; }

    public static string SiteDescription { get; set; }

    public static string PagePerNumber { get; set; }

    public static string SiteTips { get; set; }

    public static string MessageCode { get; set; }

    public static string SiteMessage { get; set; }

    public static string PhoneMessage { get; set; }

    public static string UserWorkDay { get; set; }

    public static string CategoryRate { get; set; }

}
