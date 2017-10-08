using System;
using System.Web.UI.WebControls;
using AALife.BLL;
using AALife.Model;
using System.Transactions;

public partial class UserBoundAdmin : BasePage
{
    private OAuthTableBLL bll = new OAuthTableBLL();
    private UserTableBLL user_bll = new UserTableBLL();
    private ItemTableBLL item_bll = new ItemTableBLL();
    private int userId = 0;
    public bool IsBound = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        userId = Convert.ToInt32(Session["UserID"]);
        IsBound = GetIsBound();

        if (!IsPostBack)
        {
            BindGrid();
        }
    }

    //列表数据
    private void BindGrid()
    {
        this.BoundList.DataSource = bll.GetOAuthList(userId);
        this.BoundList.DataBind();
    }

    //是否绑定
    private bool GetIsBound()
    {
        OAuthInfo oauth = bll.GetOAuthByUserId(userId);
        if (oauth.OAuthID == 0)
        {
            return true;
        }
        else
        {
            return oauth.OAuthBound == 1;
        }
    }

    //绑定新帐号
    protected void NewButton_Click(object sender, EventArgs e)
    {
        string userName = this.UserNameNew.Text.Trim();
        string userPassword = this.UserPasswordNew.Text.Trim();

        if (!ValidHelper.CheckLength(userName, 2))
        {
            Utility.Alert(this, "用户名填写错误！");
            return;
        }

        bool success = user_bll.UserExists(userName);
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

        UserInfo user = user_bll.GetUserByUserId(userId);
        user.UserName = userName;
        user.UserPassword = userPassword;

        using (TransactionScope ts = new TransactionScope())
        {
            success = user_bll.UpdateUser(user);
            success = bll.OAuthBoundNewUser(userId);

            ts.Complete();
        }

        if (success)
        {
            Utility.Alert(this, "绑定成功。", "UserBoundAdmin.aspx");
        }
        else
        {
            Utility.Alert(this, "绑定失败！");
        }
    }

    //绑定已有帐号
    protected void BoundButton_Click(object sender, EventArgs e)
    {
        string userName = this.UserNameBound.Text.Trim();
        string userPassword = this.UserPasswordBound.Text.Trim();

        if (!ValidHelper.CheckLength(userName, 2))
        {
            Utility.Alert(this, "用户名填写错误！");
            return;
        }

        if (!ValidHelper.CheckLength(userPassword, 2))
        {
            Utility.Alert(this, "密码填写错误！");
            return;
        }

        bool success = user_bll.UserLogin(userName, userPassword);
        if (!success)
        {
            Utility.Alert(this, "登录失败！");
            return;
        }

        UserInfo user = user_bll.GetUserByUserPassword(userName, userPassword);
        using (TransactionScope ts = new TransactionScope())
        {
            success = bll.OAuthBoundOldUser(userId, user.UserID);
            bool succ = item_bll.UpdateItemToUser(userId, user.UserID);

            ts.Complete();
        }

        if (success)
        {
            UserHelper.SaveSession(user);

            Response.Cookies["ThemeCookie"].Value = user.UserTheme;
            Response.Cookies["ThemeCookie"].Expires = DateTime.MaxValue;

            Utility.Alert(this, "绑定成功。", "UserBoundAdmin.aspx");
        }
        else
        {
            Utility.Alert(this, "绑定失败！");
        }
        
    }

    //解除绑定
    protected void Button3_Command(object sender, CommandEventArgs e)
    {
        int oauthId = Convert.ToInt32(e.CommandArgument);

        bool success = bll.OAuthBoundCancel(oauthId);
        if (success)
        {
            Utility.Alert(this, "解除成功。", "UserBoundAdmin.aspx");
        }
        else
        {
            Utility.Alert(this, "解除失败！");
        }
    }
}
