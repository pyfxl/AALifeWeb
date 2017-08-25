using AALife.BLL;
using AALife.Model;
using System;
using System.Transactions;

public partial class AuthorLogin_OAuth : FirstPage
{
    private UserTableBLL bll = new UserTableBLL();
    private OAuthTableBLL oauth_bll = new OAuthTableBLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
        }
    }

    private void BindData()
    {
        string u = Request.QueryString["u"];
        string openId = Request.QueryString["openId"];

        UserInfo user = new UserInfo();
        user.UserName = UserHelper.GetUserName(u);
        user.UserPassword = "aalife";
        user.UserNickName = Request.QueryString["name"] ?? "";
        user.UserImage = Request.QueryString["image"] == "" ? "none.gif" : Request.QueryString["image"];
        user.UserFrom = u;
        user.ModifyDate = DateTime.Now;
        user.IsUpdate = 1;

        OAuthInfo oauth = new OAuthInfo();
        oauth.OpenID = openId;
        oauth.AccessToken = Request.QueryString["accessToken"];
        oauth.OAuthFrom = u;
        oauth.OAuthBound = 0;
        oauth.ModifyDate = DateTime.Now;

        bool success = oauth_bll.OAuthLoginByOpenId(oauth.OpenID);
        if (!success)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                success = bll.InsertUser(user);
                user = bll.GetUserByUserPassword(user.UserName, user.UserPassword);
                oauth.UserID = user.UserID;
                oauth.OldUserID = user.UserID;
                success = oauth_bll.InsertOAuth(oauth);

                ts.Complete();
            }
            if (!success)
            {
                Response.Write("自动登录错误！");
                Response.End();
            }
        }
        else
        {
            oauth = oauth_bll.GetOAuthByOpenId(openId);
            user = bll.GetUserByUserId(oauth.UserID);
        }

        UserHelper.SaveSession(user);

        Response.Redirect("/Default.aspx");
    }

}