using AALife.BLL;
using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class AdminUserDetail : AdminPage
{
    private ItemTableBLL bll = new ItemTableBLL();
    private UserCategoryTableBLL cat_bll = new UserCategoryTableBLL();
    private CardTableBLL card_bll = new CardTableBLL();
    private UserTableBLL user_bll = new UserTableBLL();
    private OAuthTableBLL oauth_bll = new OAuthTableBLL();
    private ZhuanTiTableBLL zt_bll = new ZhuanTiTableBLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PupolateControls();
        }
    }

    private void PupolateControls()
    {
        int userId = 0;
        if (Request.QueryString["userId"] != "" && Request.QueryString["userId"] != null)
        {
            userId = Convert.ToInt32(Request.QueryString["userId"]);
        }

        DataTable user = user_bll.GetUserDataTableByUserId(userId);
        UserList.DataSource = user;
        UserList.DataBind();

        OAuthList.DataSource = oauth_bll.GetOAuthListDataTableByUserId(userId);
        OAuthList.DataBind();

        int categoryRate = Convert.ToInt32(user.Rows[0]["CategoryRate"]);
        UserCategoryList.DataSource = cat_bll.GetUserCategoryList(userId, categoryRate);
        UserCategoryList.DataBind();

        ZhuanTiList.DataSource = zt_bll.GetZhuanTiList(userId);
        ZhuanTiList.DataBind();

        CardList.DataSource = card_bll.GetCardList(userId);
        CardList.DataBind();

        List.DataSource = bll.GetItemListByUserId(userId);
        List.DataBind();
    }

    protected void List_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        List.PageIndex = e.NewPageIndex;
        PupolateControls();
    }
}