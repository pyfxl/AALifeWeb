using System;
using AALife.BLL;
using AALife.Model;
using System.Data;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Web.UI;

public partial class UserFunctionSetting : WebPage
{
    private UserTableBLL bll = new UserTableBLL();
    private int userId = 0;
    private MenuHelper menuHelper = new MenuHelper();

    protected void Page_Load(object sender, EventArgs e)
    {
        userId = Convert.ToInt32(Session["UserID"]);
        
        AddCheckBox();

        if (!IsPostBack)
        {
            BindMenuList();

            string menu = Session["UserFunction"].ToString();
            menuHelper.PopulateControls(menu);
        }
    }

    //显示菜单
    private void BindMenuList()
    {
        UserQueryTableBLL query_bll = new UserQueryTableBLL();
        DataTable dt = query_bll.GetUserQueryList(userId);
        menuHelper.SetQueryData(dt);

        DataTable menus = menuHelper.GetMenuData();

        menus.DefaultView.RowFilter = "MenuType='system'";
        this.SystemMenu.DataSource = menus;
        this.SystemMenu.DataBind();

        menus.DefaultView.RowFilter = "MenuType='query'";
        this.QueryList.DataSource = menus;
        this.QueryList.DataBind();

        menus.DefaultView.RowFilter = "MenuType='user'";
        this.UserMenu.DataSource = menus;
        this.UserMenu.DataBind();

        AddCheckBox();
    }

    //添加checkbox
    private void AddCheckBox()
    {
        menuHelper.AddCheckBox(this.SystemMenu);
        menuHelper.AddCheckBox(this.QueryList);
        menuHelper.AddCheckBox(this.UserMenu);
    }
    
    //checkbox事件
    protected void Button1_Click(object sender, EventArgs e)
    {
        string value = "";

        try
        {
            value = menuHelper.GetSaveMenu();
        } catch (Exception ex)
        {
            Utility.Alert(this, ex.Message, "UserFunctionSetting.aspx");
            return;
        }

        UserInfo user = bll.GetUserByUserId(userId);
        user.Synchronize = 1;
        user.UserFunction = value;
        user.ModifyDate = DateTime.Now;

        bool success = bll.UpdateUser(user);
        if (success)
        {
            Session["UserFunction"] = value;
            Response.Redirect("UserFunctionSetting.aspx");
        }
        else
        {
            Utility.Alert(this, "修改失败！");
        }
    }

}
