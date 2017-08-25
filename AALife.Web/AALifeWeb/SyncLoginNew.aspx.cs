using AALife.BLL;
using AALife.Model;
using NLog;
using System;
using System.Data;
using System.Text.RegularExpressions;

public partial class AALifeWeb_SyncLoginNew : System.Web.UI.Page
{
    public static Logger log = LogManager.GetCurrentClassLogger();
    private ItemTableBLL bll = new ItemTableBLL();
    private UserTableBLL user_bll = new UserTableBLL();
    private OAuthTableBLL oauth_bll = new OAuthTableBLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        string userName = Request.Form["username"].ToString();
        string userPassword = Request.Form["userpass"].ToString();
        int type = Convert.ToInt32(Request.Form["type"]);
        string isUpdate = Request.Form["isupdate"] ?? "0";

        //写日志
        log.Info(string.Format(" UserName:{0} | Type:{1} | IsUpdate:{2}", userName, type, isUpdate));
          
        string result = "{";

        bool success = user_bll.UserLogin(userName, userPassword);
        if (success)
        {
            UserInfo user = user_bll.GetUserByUserPassword(userName, userPassword);

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
            result += "\"categoryrate\":\"" + user.CategoryRate + "\",";

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

            OAuthInfo oauth = oauth_bll.GetOAuthByUserId(user.UserID);
            if (oauth.OAuthBound == 0)
            {
                result += "\"userbound\":\"0\"";
            }
            else
            {
                result += "\"userbound\":\"1\"";
            }
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