using System.Configuration;
using System.Web;
using System;

public class WebConfiguration
{
    private static string dbConnectionString;
    private static string dbProviderName;
    private static string siteName;
    private static string siteAuthor;
    private static string siteKeywords;
    private static string siteDescription;
    private static string pagePerNumber;
    private static string siteTips;
    private static string messageCode;
    private static string siteMessage;
    private static string phoneMessage;
    private static string userWorkDay;
    private static string categoryRate;

    static WebConfiguration()
    {
        dbConnectionString = ConfigurationManager.ConnectionStrings["AALifeWebForApp"].ConnectionString;
        dbProviderName = ConfigurationManager.ConnectionStrings["AALifeWebForApp"].ProviderName;
        GetConfig();
    }

    public static void GetConfig()
    {
        string strXmlFile = HttpContext.Current.Server.MapPath("/Site.Config");
        XmlControl xmlTool = new XmlControl(strXmlFile);

        siteName = xmlTool.GetText("Root/SiteName");
        siteAuthor = xmlTool.GetText("Root/SiteAuthor");
        siteKeywords = xmlTool.GetText("Root/SiteKeywords");
        siteDescription = xmlTool.GetText("Root/SiteDescription");
        pagePerNumber = xmlTool.GetText("Root/PagePerNumber");
        siteTips = xmlTool.GetText("Root/SiteTips");
        messageCode = xmlTool.GetText("Root/MessageCode");
        siteMessage = xmlTool.GetText("Root/SiteMessage");
        phoneMessage = xmlTool.GetText("Root/PhoneMessage");
        userWorkDay = xmlTool.GetText("Root/UserWorkDay");
        categoryRate = xmlTool.GetText("Root/CategoryRate");
        xmlTool.Dispose();
    }

    public static string DbConnectionString { get { return dbConnectionString; } }

    public static string DbProviderName { get { return dbProviderName; } }

    public static string SiteName { get { return siteName; } }

    public static string SiteAuthor { get { return siteAuthor; } }

    public static string SiteKeywords { get { return siteKeywords; } }

    public static string SiteDescription { get { return siteDescription; } }

    public static string PagePerNumber { get { return pagePerNumber; } }

    public static string SiteTips { get { return siteTips; } }

    public static string MessageCode { get { return messageCode; } }

    public static string SiteMessage { get { return siteMessage; } }

    public static string PhoneMessage { get { return phoneMessage; } }

    public static string UserWorkDay { get { return userWorkDay; } }

    public static string CategoryRate { get { return categoryRate; } }
    
}
