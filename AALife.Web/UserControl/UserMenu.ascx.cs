using System;
using System.Data;

public partial class UserControl_UserMenu : System.Web.UI.UserControl
{
    private MenuHelper menuHelper = new MenuHelper();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindMenuList();
        }
    }

    //显示菜单
    private void BindMenuList()
    {
        DataTable menus = menuHelper.GetMenuData();

        menus.DefaultView.RowFilter = "MenuType='user'";
        this.UserMenu.DataSource = menus;
        this.UserMenu.DataBind();
    }

}