using AALife.BLL;
using AALife.Model;
using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Transactions;
using NLog;

public partial class AALifeWeb_SyncLoginQQNew : System.Web.UI.Page
{
    public static Logger log = LogManager.GetCurrentClassLogger();
    private ItemTableBLL bll = new ItemTableBLL();
    private UserTableBLL user_bll = new UserTableBLL();
    private OAuthTableBLL oauth_bll = new OAuthTableBLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        string userName = Request.Form["username"] ?? "";
        string openId = Request.Form["openid"].ToString();
        string accessToken = Request.Form["accesstoken"].ToString();
        string oAuthFrom = "sjqq";
        string nickName = Request.Form["nickname"].ToString();
        string userImage = Request.Form["userimage"].ToString();
        string userFrom = Request.Form["userfrom"].ToString() ?? Request.Form["oauthfrom"].ToString();
        int type = Convert.ToInt32(Request.Form["type"]);
        string isUpdate = Request.Form["isupdate"] ?? "0";

        if (userFrom.Length > 5)
        {
            userFrom = userFrom.Replace("_", "");
            userFrom = userFrom.Insert(5, "_");
        }

        UserInfo user = user_bll.GetUserByUserName(userName);
        if (userName == "") user.UserName = UserHelper.GetUserName(oAuthFrom);
        if (userName == "") user.UserPassword = "aalife";
        user.UserNickName = nickName;
        user.UserImage = (userImage=="" ? "none.gif" : userImage);
        user.UserFrom = userFrom;
        user.CreateDate = DateTime.Now;
        user.ModifyDate = DateTime.Now;
        user.IsUpdate = Convert.ToByte(isUpdate);

        //写日志
        log.Info(string.Format(" UserInfo -> {0}", user.ToString()));
            
        OAuthInfo oauth = new OAuthInfo();
        oauth.OpenID = openId;
        oauth.AccessToken = accessToken;
        oauth.OAuthFrom = oAuthFrom;
        oauth.OAuthBound = 1;
        oauth.ModifyDate = DateTime.Now;

        //写日志
        log.Info(string.Format(" OAuthInfo -> {0}", oauth.ToString()));
            
        bool success = oauth_bll.OAuthLoginByOpenId(oauth.OpenID);
        if (!success)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                if (user.UserID > 0)
                {
                    success = user_bll.UpdateUser(user);
                }
                else
                {
                    success = user_bll.InsertUser(user);
                    user = user_bll.GetUserByUserPassword(user.UserName, user.UserPassword);
                }
                
                oauth.UserID = user.UserID;
                oauth.OldUserID = user.UserID;
                success = oauth_bll.InsertOAuth(oauth);

                ts.Complete();
            }
            if (!success)
            {
                Response.Write("{\"result\":\"userid\":\"0\"}");
                Response.End();
            }
        }

        string result = "{";

        if (success)
        {
            oauth = oauth_bll.GetOAuthByOpenId(openId);
            user = user_bll.GetUserByUserId(oauth.UserID);
            
            decimal userMoney = user.UserMoney;
            if (isUpdate == "1")
            {
                userMoney = user.MoneyStart;
            }

            result += "\"userid\":\"" + user.UserID + "\",";
            result += "\"username\":\"" + user.UserName + "\",";
            result += "\"userpass\":\"" + user.UserPassword + "\",";
            result += "\"usernickname\":\"" + user.UserNickName + "\",";
            result += "\"createdate\":\"" + user.CreateDate.ToString("yyyy-MM-dd") + "\",";
            result += "\"useremail\":\"" + user.UserEmail + "\",";
            result += "\"userphone\":\"" + user.UserPhone + "\",";
            result += "\"userimage\":\"" + user.UserImage + "\",";
            result += "\"userworkday\":\"" + user.UserWorkDay + "\",";
            result += "\"usermoney\":\"" + userMoney + "\",";

            if (type == 1)
            {
                user_bll.UpdateSyncByUserId(user.UserID);
            }

            DataTable dt = bll.GetItemListWithSync(user.UserID);
            if (dt.Rows.Count > 0)
            {
                result += "\"hassync\":\"1\",";
            }
            else
            {
                result += "\"hassync\":\"0\",";
            } 
            
            result += "\"userbound\":\"1\"";
        }
        else
        {
            result += "\"userid\":\"0\",";
            result += "\"username\":\"\",";
            result += "\"userpass\":\"\",";
            result += "\"usernickname\":\"\",";
            result += "\"createdate\":\"\",";
            result += "\"useremail\":\"\",";
            result += "\"userphone\":\"\",";
            result += "\"userimage\":\"\",";
            result += "\"userworkday\":\"5\",";
            result += "\"usermoney\":\"0\",";
            result += "\"categoryrate\":\"90\",";
            result += "\"hassync\":\"0\",";
            result += "\"userbound\":\"0\"";
        }

        result += "}";

        Response.Write(result);
        Response.End();
    }
}