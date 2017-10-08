using System;

public partial class UserLogout : FirstPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["TodayDate"] = null;

        Session["UserID"] = null;
        Session["UserName"] = null;
        Session["UserNickName"] = null;
        Session["UserTheme"] = null;
        Session["UserLevel"] = null;
        Session["UserFrom"] = null;
        Session["UserWorkDay"] = null;
        Session["UserFunction"] = null;
        Session["CategoryRate"] = null;
        Session["IsUpdate"] = null;

        Response.Redirect("Default.aspx");
    }
}
