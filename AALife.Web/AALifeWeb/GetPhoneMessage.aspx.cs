using System;

public partial class AALifeWeb_GetPhoneMessage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        String result = "{ \"messagecode\":\"" + WebConfiguration.MessageCode + "\", \"message\":\"" + Utility.UnReplaceString(WebConfiguration.PhoneMessage) + "\" }";
        
        Response.Write(result);
        Response.End();
    }
}