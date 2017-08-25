using AALife.BLL;
using AALife.Model;
using System;
using System.Data;
using System.Text.RegularExpressions;

public partial class AALifeWeb_SyncBoundNew : System.Web.UI.Page
{
    private OAuthTableBLL bll = new OAuthTableBLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        string openId = Request.Form["openid"].ToString();
        string accessToken = Request.Form["accesstoken"].ToString();
        string oAuthFrom = Request.Form["oauthfrom"].ToString();
        int userId = Convert.ToInt32(Request.Form["userid"]);

        string result = "{";

        bool success = false;

        OAuthInfo oauth = bll.GetOAuthByUserId(userId);
        oauth.OpenID = openId;
        oauth.AccessToken = accessToken;
        oauth.UserID = userId;
        oauth.OAuthFrom = oAuthFrom;
        oauth.OAuthBound = 1;
        oauth.ModifyDate = DateTime.Now;

        if (oauth.OAuthID == 0)
        {
            success = bll.InsertOAuth(oauth);
            if (success)
            {
                result += "\"result\":\"1\"";
            }
            else
            {
                result += "\"result\":\"0\"";
            }
        }
        else if (oauth.OAuthBound == 0)
        {
            success = bll.UpdateOAuth(oauth);
            if (success)
            {
                result += "\"result\":\"1\"";
            }
            else
            {
                result += "\"result\":\"0\"";
            }
        }
        else
        {
            result += "\"result\":\"2\"";
        }
        
        result += "}";

        Response.Write(result);
        Response.End();
    }
}