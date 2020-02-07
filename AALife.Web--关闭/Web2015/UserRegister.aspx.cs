using AALife.BLL;
using AALife.Model;
using System;

public partial class UserRegister : FirstPage
{
    private UserTableBLL bll = new UserTableBLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.UserWorkDay.DataSource = bll.GetUserWorkDay();
            this.UserWorkDay.DataTextField = "WorkDayName";
            this.UserWorkDay.DataValueField = "WorkDay";
            this.UserWorkDay.DataBind();
            this.UserWorkDay.Items.FindByValue("5").Selected = true;
        }
    }

    //注册按钮
    protected void SubmitButtom_Click(object sender, EventArgs e)
    {
        string userName = this.UserName.Text.Trim();
        string userPassword = this.UserPassword.Text.Trim();
        string userPassword2 = this.UserPassword2.Text.Trim();
        string userNickName = this.UserNickName.Text.Trim();
        string userEmail = this.UserEmail.Text.Trim();
        string userTheme = "main"; 
        string userWorkDay = this.UserWorkDay.SelectedValue;

        if (!ValidHelper.CheckLength(userName, 2))
        {
            Utility.Alert(this, "用户名填写错误！");
            return;
        }

        bool success = bll.UserExists(userName);
        if (success)
        {
            Utility.Alert(this, "用户名重复！");
            return;
        }

        if (!ValidHelper.CheckLength(userPassword, 2))
        {
            Utility.Alert(this, "密码填写错误！");
            return;
        }

        if (userPassword2 == "")
        {
            Utility.Alert(this, "重复密码未填写！");
            return;
        }

        if (userPassword != userPassword2)
        {
            Utility.Alert(this, "两次密码不一致！");
            return;
        }

        if (userEmail != "")
        {
            if (!ValidHelper.CheckEmail(userEmail))
            {
                Utility.Alert(this, "邮箱格式填写错误！");
                return;
            }
        }
        
        if (Request.Cookies["ThemeCookie"] != null)
        {
            userTheme = Request.Cookies["ThemeCookie"].Value;
        }

        UserInfo user = new UserInfo();
        user.UserName = userName;
        user.UserPassword = userPassword;
        user.UserNickName = userNickName;
        user.UserEmail = userEmail;
        user.UserWorkDay = userWorkDay;
        user.UserTheme = userTheme;
        user.IsUpdate = 1;

        success = bll.InsertUser(user);
        if (success)
        {
            Session["TodayDate"] = DateTime.Now.ToString("yyyy-MM-dd");

            UserInfo newUser = bll.GetUserByUserPassword(userName, userPassword);
            UserHelper.SaveSession(newUser);
            
            Utility.Alert(this, "注册成功。", "Default.aspx");
        }
        else
        {
            Utility.Alert(this, "注册失败！");
        }
    }
}
