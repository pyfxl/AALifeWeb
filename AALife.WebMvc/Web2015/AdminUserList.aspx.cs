using System;
using System.Web.UI.WebControls;
using System.Data;
using AALife.BLL;
using System.Collections.Generic;
using AALife.Model;

public partial class AdminUserList : AdminPage
{
    private UserTableBLL bll = new UserTableBLL();
    private DateTime curDate = DateTime.Now;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["date"] != null && Request.QueryString["date"] != "")
        {
            curDate = Convert.ToDateTime(Request.QueryString["date"]);
        }

        if (!IsPostBack)
        {
            PopulateControls();
        }
    }

    private void PopulateControls()
    {
        this.TypeHid.Value = "0";

        DateTime beginDate = curDate.Date;
        DateTime endDate = curDate.AddDays(1).Date;
                
        DataTable lists = bll.GetUserListByDate(beginDate, endDate);
        AdminList.DataSource = lists;
        AdminList.DataBind();
        this.Label1.Text = "记录：" + lists.Rows.Count;
    }

    protected void BindGrid()
    {
        this.TypeHid.Value = "2";

        DataTable lists = bll.GetUserList();
        AdminList.DataSource = lists;
        AdminList.DataBind();
        this.Label1.Text = "记录：" + lists.Rows.Count;
    }

    protected void List_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int userId = Convert.ToInt32(AdminList.DataKeys[e.RowIndex].Value); 
        string userImage = ((HiddenField)AdminList.Rows[e.RowIndex].FindControl("UserImageHid")).Value;

        bool success = bll.DeleteUser(userId);
        if (success)
        {
            Utility.Alert(this, "删除成功。");
        }
        else
        {
            Utility.Alert(this, "删除失败！");
            return;
        }

        //删除头像
        if (ImageHelper.CanDelete(userImage))
        {
            try
            {
            
                    ImageHelper.DeleteUserImage(userImage);
            }
            catch { }
        }

        AdminList.EditIndex = -1;
        SwitchData();
    }

    protected void List_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        bool success = false;

        int userId = Convert.ToInt32(AdminList.DataKeys[e.RowIndex].Value);
        string oldUserName = ((HiddenField)AdminList.Rows[e.RowIndex].FindControl("UserNameHid")).Value;
        string userName = ((TextBox)AdminList.Rows[e.RowIndex].FindControl("UserNameBox")).Text.Trim();
        string userPassword = ((TextBox)AdminList.Rows[e.RowIndex].FindControl("UserPassBox")).Text.Trim();
        string userNickName = ((TextBox)AdminList.Rows[e.RowIndex].FindControl("UserNickBox")).Text.Trim();

        if (userName == "")
        {
            Utility.Alert(this, "用户名未填写！");
            return;
        }
        if (oldUserName != userName)
        {
            success = bll.UserExists(userName);
            if (success)
            {
                Utility.Alert(this, "用户名重复！");
                return;
            }
        }

        if (userPassword == "")
        {
            Utility.Alert(this, "密码未填写！");
            return;
        }

        string userImage = ((HiddenField)AdminList.Rows[e.RowIndex].FindControl("UserImageHid")).Value;
        FileUpload userImageUpload = (FileUpload)AdminList.Rows[e.RowIndex].FindControl("UserImageUpload");
        if (userImageUpload.HasFile)
        {
            string fileName = userImageUpload.FileName;
            userImage = ImageHelper.GetUserImageName(userId, fileName);
            string imagePath = ImageHelper.GetUserImagePath(userImage);
            try
            {
                userImageUpload.SaveAs(imagePath);
                ImageHelper.SaveImage(imagePath, 100, 100);
            }
            catch
            {
                Utility.Alert(this, "头像上传失败！");
                return;
            }
        }
        
        string userEmail = ((TextBox)AdminList.Rows[e.RowIndex].FindControl("UserEmailBox")).Text.Trim();
        string userTheme = ((TextBox)AdminList.Rows[e.RowIndex].FindControl("UserThemeBox")).Text.Trim();
        byte userLevel = Convert.ToByte(((TextBox)AdminList.Rows[e.RowIndex].FindControl("UserLevelBox")).Text.Trim());
        string userFrom = ((TextBox)AdminList.Rows[e.RowIndex].FindControl("UserFromBox")).Text.Trim();
        string categoryRate = ((TextBox)AdminList.Rows[e.RowIndex].FindControl("CategoryRateBox")).Text.Trim();
        string userMoney = ((TextBox)AdminList.Rows[e.RowIndex].FindControl("UserMoneyBox")).Text.Trim();

        UserInfo user = bll.GetUserByUserId(userId);
        user.UserName = userName;
        user.UserPassword = userPassword;
        user.UserNickName = userNickName;
        user.UserImage = userImage;
        user.UserEmail = userEmail;
        user.UserTheme = userTheme;
        user.UserLevel = userLevel;
        user.UserFrom = userFrom;
        user.CategoryRate = Convert.ToInt32(categoryRate);
        user.UserMoney = (userMoney == "" ? 0 : Convert.ToDecimal(userMoney));
        user.Synchronize = 1;
                
        success = bll.UpdateUser(user);
        if (success)
        {
            Utility.Alert(this, "更新成功。");
        }
        else
        {
            Utility.Alert(this, "更新失败！");
            return;
        }

        AdminList.EditIndex = -1;
        SwitchData();
    }

    protected void List_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        AdminList.EditIndex = -1;
        SwitchData();
    }

    protected void List_RowEditing(object sender, GridViewEditEventArgs e)
    {
        AdminList.EditIndex = e.NewEditIndex;
        SwitchData();
    }

    //所有用户
    protected void Button1_Click(object sender, EventArgs e)
    {
        AdminList.AllowPaging = true;
        AdminList.PageSize = 36;
        BindGrid();
    }

    //昨天按钮
    protected void Button2_Click(object sender, EventArgs e)
    {
        DateTime date = curDate.AddDays(-1);
        Response.Redirect("AdminUserList.aspx?date=" + date.ToString("yyyy-MM-dd"));
    }

    //明天按钮
    protected void Button3_Click(object sender, EventArgs e)
    {
        DateTime date = curDate.AddDays(1);
        Response.Redirect("AdminUserList.aspx?date=" + date.ToString("yyyy-MM-dd"));
    }

    protected void List_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        AdminList.PageIndex = e.NewPageIndex;
        BindGrid();
    }

    //搜索按钮
    protected void Button4_Click(object sender, EventArgs e)
    {
        string keywords = this.KeyBox.Text.Trim().Replace("%", "");
        if (keywords == "")
        {
            Utility.Alert(this, "关键字不能为空！");
            return;
        } 
        
        this.TypeHid.Value = "1";

        DataTable lists = bll.GetUserListByKeywords(keywords);
        AdminList.AllowPaging = false;
        AdminList.PageIndex = 0;
        AdminList.DataSource = lists;
        AdminList.DataBind();
        this.Label1.Text = "记录：" + lists.Rows.Count;
    }

    protected void SwitchData()
    {
        if (this.TypeHid.Value == "0")
        {
            PopulateControls();
        }
        else if (this.TypeHid.Value == "1")
        {
            Button4_Click(null, null);
        }
        else
        {
            Button1_Click(null, null);
        }
    }
}