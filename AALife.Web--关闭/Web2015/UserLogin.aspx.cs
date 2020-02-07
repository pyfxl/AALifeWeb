using AALife.BLL;
using AALife.Model;
using System;

public partial class UserLogin : FirstPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Cookies["UserCookie"] != null)
            {
                this.UserName.Text = Request.Cookies["UserCookie"].Value; 
            }
        }
    }

    //登录按钮
    protected void SubmitButtom_Click(object sender, EventArgs e)
    {
        string userName = this.UserName.Text.Trim();
        string userPassword = this.UserPassword.Text.Trim();

        if (userName == "")
        {
            Utility.Alert(this, "用户名未填写！");
            return;
        }

        if (userPassword == "")
        {
            Utility.Alert(this, "密码未填写！");
            return;
        }

        //保留用户名Cookie
        Response.Cookies["UserCookie"].Value = userName;
        Response.Cookies["UserCookie"].Expires = DateTime.MaxValue;

        UserTableBLL bll = new UserTableBLL();
        bool success = bll.UserLogin(userName, userPassword);
        if (success)
        {
            Session["TodayDate"] = DateTime.Now.ToString("yyyy-MM-dd");

            UserInfo user = bll.GetUserByUserPassword(userName, userPassword);
            UserHelper.SaveSession(user);

            Response.Cookies["ThemeCookie"].Value = user.UserTheme;
            Response.Cookies["ThemeCookie"].Expires = DateTime.MaxValue;
            
            string url = Request.QueryString["url"];
            if (url == "" || url == null)
            {
                url = "Default.aspx";
            }

            Response.Redirect(url);
        }
        else
        {
            Utility.Alert(this, "登录失败！");
        }
    }
}
