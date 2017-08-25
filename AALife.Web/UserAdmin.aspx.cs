using System;
using System.Text.RegularExpressions;
using AALife.BLL;
using AALife.Model;
using System.Data;

public partial class Web2016_UserAdmin : WebPage
{
    private UserTableBLL bll = new UserTableBLL();
    private OAuthTableBLL oauth_bll = new OAuthTableBLL();
    private ItemTableBLL item_bll = new ItemTableBLL();
    private int userId = 0;
    private UserInfo user = new UserInfo();

    protected void Page_Load(object sender, EventArgs e)
    {
        userId = Convert.ToInt32(Session["UserID"]);
        user = bll.GetUserByUserId(userId);

        //未绑定帐号跳转
        OAuthInfo oauth = oauth_bll.GetOAuthByUserId(userId);
        if (oauth.OAuthID > 0 && oauth.OAuthBound == 0)
        {
            Response.Redirect("UserBoundAdmin.aspx");
        }

        if (!IsPostBack)
        {
            BindWorkDay();

            PopulateControls();
        }
    }
    
    //绑定工作日
    private void BindWorkDay()
    {
        this.UserWorkDay.DataSource = bll.GetUserWorkDay();
        this.UserWorkDay.DataTextField = "WorkDayName";
        this.UserWorkDay.DataValueField = "WorkDay";
        this.UserWorkDay.DataBind();
    }

    //加载数据
    private void PopulateControls()
    {
        this.UserImage.ImageUrl = ImageHelper.GetUserImage(user.UserImage);
        this.UserNickName.Text = user.UserNickName;
        this.UserEmail.Text = user.UserEmail;
        this.UserPhone.Text = user.UserPhone;
        this.UserWorkDay.Items.FindByValue(user.UserWorkDay).Selected = true;
        this.CategoryRate.Text = user.CategoryRate.ToString();

        this.JoinDay.Text = UserHelper.JoinDay(user.CreateDate).ToString();
        DataTable items = item_bll.GetItemListByUserId(userId);
        this.ItemCount.Text = items.Rows.Count.ToString();
    }

    //修改密码
    protected void Button2_Click(object sender, EventArgs e)
    {
        string userPassword = user.UserPassword;//旧版此处用了try，不明原因
        string oldPassword = this.UserPassword.Text.Trim();
        string newPassword = this.NewPassword.Text.Trim();
        string newPassword2 = this.NewPassword2.Text.Trim();

        if (oldPassword == "")
        {
            this.Label2.Text = "旧密码未填写！";
            return;
        }
        if (oldPassword != userPassword)
        {
            this.Label2.Text = "旧密码不正确！";
            return;
        }

        if (!ValidHelper.CheckLength(newPassword, 2))
        {
            this.Label1.Text = "新密码填写错误！";
            return;
        }
        if (newPassword != newPassword2)
        {
            this.Label4.Text = "两次新密码不一致！";
            return;
        }

        user.UserPassword = newPassword;
        user.Synchronize = 1;
        user.ModifyDate = DateTime.Now;

        bool success = bll.UpdateUser(user);
        if (success)
        {
            Utility.Alert(this, "修改成功。", "UserLogout.aspx");
        }
        else
        {
            Utility.Alert(this, "修改失败！");
        }
    }

    //修改资料
    protected void SubmitButton_Click(object sender, EventArgs e)
    {
        string userNickName = this.UserNickName.Text.Trim();
        string userEmail = this.UserEmail.Text.Trim();
        string userPhone = this.UserPhone.Text.Trim();
        string userWorkDay = this.UserWorkDay.SelectedValue;
        string categoryRate = this.CategoryRate.Text.Trim();

        this.Label3.Text = "";
        if (userEmail != "")
        {
            if (!ValidHelper.CheckEmail(userEmail))
            {
                this.Label3.Text = "邮箱格式填写错误！";
                return;
            }
        }

        if (userPhone != "")
        {
            if (!ValidHelper.CheckPhone(userPhone))
            {
                this.Label6.Text = "手机号码填写错误！";
                return;
            }
        }

        if (!ValidHelper.CheckNumber(categoryRate))
        {
            Utility.Alert(this, "预算率填写错误！");
            return;
        } 
        
        int rateValue = Convert.ToInt32(categoryRate);
        if (rateValue > 100)
        {
            Utility.Alert(this, "预算率不能大于100！");
            return;
        }

        user.UserNickName = userNickName;
        user.UserEmail = userEmail;
        user.UserPhone = userPhone;
        user.UserWorkDay = userWorkDay;
        user.CategoryRate = rateValue;
        user.Synchronize = 1;
        user.ModifyDate = DateTime.Now;

        bool success = bll.UpdateUser(user);
        if (success)
        {
            Session["UserNickName"] = user.UserNickName;
            Session["UserWorkDay"] = user.UserWorkDay;
            Session["CategoryRate"] = user.CategoryRate;
            Utility.Alert(this, "修改成功。", "UserAdmin.aspx#info");
        }
        else
        {
            Utility.Alert(this, "修改失败！");
        }
    }

    //修改头像
    protected void Button1_Click(object sender, EventArgs e)
    {
        string userImage = "";

        if (this.UserImageUpload.HasFile)
        {
            this.Label5.Text = "";
            string fileName = this.UserImageUpload.FileName;
            if (!ImageHelper.CanUpload(fileName))
            {
                this.Label5.Text = "头像文件格式不支持！";
                return;
            }
            userImage = ImageHelper.GetUserImageName(userId, fileName);
            string imagePath = ImageHelper.GetUserImagePath(userImage);
            try
            {
                this.UserImageUpload.SaveAs(imagePath);
                ImageHelper.SaveImage(imagePath, 100, 100);
            }
            catch
            {
                this.Label5.Text = "头像上传失败！";
                return;
            }
        }
        else
        {
            this.Label5.Text = "头像文件未选择！";
            return;
        }

        user.UserImage = userImage;

        bool success = bll.UpdateUser(user);
        if (success)
        {
            Utility.Alert(this, "修改成功。", "UserAdmin.aspx");
        }
        else
        {
            Utility.Alert(this, "修改失败！");
        }
    }

}
