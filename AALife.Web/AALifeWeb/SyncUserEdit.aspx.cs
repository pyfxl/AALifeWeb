using AALife.BLL;
using AALife.Model;
using NLog;
using System;

public partial class AALifeWeb_SyncUserEdit : System.Web.UI.Page
{
    public static Logger log = LogManager.GetCurrentClassLogger();
    private UserTableBLL bll = new UserTableBLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        int userId = Convert.ToInt32(Request.Form["userid"]);
        string userName = Request.Form["username"].ToString();
        string userPassword = Request.Form["userpass"].ToString();
        string userNickName = Request.Form["nickname"].ToString();
        string userEmail = Request.Form["useremail"].ToString();
        string userImage = Request.Form["userimage"].ToString();
        string userFrom = Request.Form["userfrom"].ToString();
        string userWorkDay = Request.Form["userworkday"].ToString();
        string categoryRate = Request.Form["categoryRate"] ?? "";

        if (userFrom.Length > 5)
        {
            userFrom = userFrom.Replace("_", "");
            userFrom = userFrom.Insert(5, "_");
        }

        UserInfo user = bll.GetUserByUserId(userId);
        if(!user.UserName.Equals(userName)) user.UserName = userName;
        user.UserPassword = userPassword;
        user.UserNickName = userNickName;
        user.UserImage = userImage;
        user.UserEmail = userEmail;
        user.UserFrom = userFrom;
        user.UserWorkDay = userWorkDay;
        user.ModifyDate = DateTime.Now;
        if (categoryRate != "") user.CategoryRate = Convert.ToInt32(categoryRate);

        //写日志
        log.Info(string.Format(" UserInfo -> {0}", user.ToString()));
          
        string result = "{";

        bool success = false;
        if (!user.UserName.Equals(userName))
        {
            success = bll.UserExists(user.UserName);
        }
        if (success)
        {
            result += "\"result\":\"2\"";
        }
        else
        {
            result += UpdateUserInfo(user);
        }

        result += "}";

        Response.Write(result);
        Response.End();
    }

    private string UpdateUserInfo(UserInfo user)
    {
        bool success = bll.UpdateUser(user);
        if (success)
        {
            return "\"result\":\"1\"";
        }
        else
        {
            return "\"result\":\"0\"";
        }
    }

}