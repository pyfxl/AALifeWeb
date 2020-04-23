using AALife.BLL;
using AALife.Model;
using NLog;
using System;
using System.Data;
using System.Web;

public partial class AALifeWeb_SyncNewUser : SyncBase
{
    private UserTableBLL bll = new UserTableBLL();
    private CardTableBLL card_bll = new CardTableBLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        string userName = Request.Form["username"].ToString();
        string userPassword = Request.Form["userpass"].ToString();
        string userNickName = Request.Form["usernickname"].ToString();
        string userImage = "user.gif";
        string userEmail = Request.Form["useremail"].ToString();
        string userFrom = Request.Form["userfrom"].ToString();
        string userWorkDay = Request.Form["userworkday"].ToString();
        string userMoney = Request.Form["usermoney"] ?? "0";
        string categoryRate = Request.Form["categoryrate"] ?? "90";
        string isUpdate = Request.Form["isupdate"] ?? "0";

        if (userFrom.Length > 5)
        {
            userFrom = userFrom.Replace("_", "");
            userFrom = userFrom.Substring(5);
        }

        UserInfo user = new UserInfo();
        user.UserName = userName;
        user.UserPassword = userPassword;
        user.UserNickName = userNickName;
        user.UserImage = userImage;
        user.UserPhone = "";
        user.UserEmail = userEmail;
        user.UserTheme = "main";
        user.UserFrom = userFrom;
        user.UserWorkDay = userWorkDay;
        user.UserMoney = Convert.ToDecimal(userMoney);
        user.CategoryRate = Convert.ToInt32(categoryRate);
        user.CreateDate = DateTime.Now;
        user.ModifyDate = DateTime.Now;
        if (isUpdate == "1")
        {
            user.UserMoney = 0;
            user.MoneyStart = Convert.ToDecimal(userMoney);
            user.IsUpdate = 1;
        }
        
        //写日志
        log.Info(string.Format(" UserInfo -> {0}", user.ToString()));
                        
        string result = "{";

        bool success = bll.UserExists(userName);
        if (success)
        {
            result += "\"result\":\"2\"";
        }
        else
        {
            success = bll.InsertUser(user);
            if (success)
            {
                AALife.WebMvc.MsgHelper.DingMessage(string.Format("新用户注册成功消息\n\n姓名：{0}\n\n昵称：{1}\n\n来自：{2}\n\n日期：{3}", user.UserName, user.UserNickName, user.UserFrom, user.CreateDate));
                result += "\"result\":\"1\"";
            }
            else
            {
                result += "\"result\":\"0\"";
            }
        }

        result += "}";

        Response.Write(result);
        Response.End();
    }
}