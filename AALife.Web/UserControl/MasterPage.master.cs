using AALife.BLL;
using AALife.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Web2016_UserControl_MasterPage : System.Web.UI.MasterPage
{
    private MenuHelper menuHelper = new MenuHelper();
    private int userId = 0;
    private string function = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        userId = Convert.ToInt32(Session["UserID"]);
        if (Session["UserFunction"] != null)
        {
            function = Session["UserFunction"].ToString();
        }
        
        AddCheckBox();
        
        if (!IsPostBack)
        {
            BindMenuList();

            menuHelper.PopulateControls(function);

            BindFunctionList();
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
        }
        catch (Exception ex)
        {
            Utility.Alert(this.Page, ex.Message, "UserFunctionSetting.aspx");
            return;
        }

        UserTableBLL bll = new UserTableBLL();
        UserInfo user = bll.GetUserByUserId(userId);
        user.Synchronize = 1;
        user.UserFunction = value;
        user.ModifyDate = DateTime.Now;

        bool success = bll.UpdateUser(user);
        if (success)
        {
            Session["UserFunction"] = value;
            Response.Redirect(Request.Url.ToString());
        }
        else
        {
            Utility.Alert(this.Page, "修改失败！");
        }
    }

    //显示自定义菜单
    private void BindFunctionList()
    {
        //this.UserFunctionLab.Text = UserHelper.GetUserFunctionText(userFunction, true);
        
        UserQueryTableBLL query_bll = new UserQueryTableBLL();
        DataTable dt = query_bll.GetUserQueryList(userId);
        menuHelper.SetQueryData(dt);

        this.MenuList.DataSource = menuHelper.GetUserFunction(function);
        this.MenuList.DataBind();
    }

    //切换主题
    protected void ImageButton_Command(object sender, CommandEventArgs e)
    {
        int userId = 0;
        string theme = e.CommandArgument.ToString();

        Response.Cookies["ThemeCookie"].Value = theme;
        Response.Cookies["ThemeCookie"].Expires = DateTime.MaxValue;

        if (Session["UserID"] != null && Session["UserID"].ToString() != "")
        {
            userId = Convert.ToInt32(Session["UserID"]);

            UserTableBLL bll = new UserTableBLL();

            UserInfo user = bll.GetUserByUserId(userId);
            user.UserTheme = theme;
            user.ModifyDate = DateTime.Now;

            bll.UpdateUser(user);
        }

        Response.Redirect(Request.Url.ToString());
    }
    
}
