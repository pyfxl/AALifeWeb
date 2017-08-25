using System;
using System.Web;

public partial class AALifeWeb_SyncSendEmail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string userName = Request.Form["username"].ToString();
        userName = (userName == "" ? "匿名" : userName);
        string userImage = Request.Form["userimage"].ToString();
        userImage = (userImage == "" ? "user.gif" : userImage);
        string content = Request.Form["content"].ToString();
        string userEmail = Request.Form["useremail"].ToString();

        //Response.Write("{ \"result\":\"ok\" }");
        //Response.End();

        //userName = HttpUtility.UrlEncode(userName);
        //content = HttpUtility.UrlEncode(content);
        //userEmail = HttpUtility.UrlEncode(userEmail);

        string mSubject = "来自用户 - " + userName;
        string mBody = Message.GetBody(userName, userImage, content, userEmail);
        string sUrl = "SyncSendEmail.asp?subject=" + HttpUtility.UrlEncode(mSubject) + "&body=" + HttpUtility.UrlEncode(mBody) + "&email=" + HttpUtility.UrlEncode(userEmail);
        Response.Redirect(sUrl);
    }
}