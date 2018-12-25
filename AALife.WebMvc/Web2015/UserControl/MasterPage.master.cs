using AALife.BLL;
using AALife.Model;
using System;
using System.Web.UI.WebControls;

public partial class UserControl_MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

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
