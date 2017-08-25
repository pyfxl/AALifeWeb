using AALife.BLL;
using System;
using System.Data;

public partial class AALifeWeb_SyncCheckWebData : System.Web.UI.Page
{
    private ItemTableBLL bll = new ItemTableBLL();
    private UserCategoryTableBLL cat_bll = new UserCategoryTableBLL();
    private CardTableBLL card_bll = new CardTableBLL();
    private UserTableBLL user_bll = new UserTableBLL();
    private ZhuanTiTableBLL zt_bll = new ZhuanTiTableBLL();
    private ZhuanZhangTableBLL zz_bll = new ZhuanZhangTableBLL();
    private DeleteTableBLL del_bll = new DeleteTableBLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        int userId = Convert.ToInt32(Request.Form["userid"]);

        string result = "{";

        DataTable dt = bll.GetItemListWithSync(userId);
        if (dt.Rows.Count == 0)
        {
            dt = del_bll.GetDeleteListByUserId(userId);
        }
        if (dt.Rows.Count == 0)
        {
            dt = cat_bll.GetUserCategoryListWithSync(userId);
        }
        if (dt.Rows.Count == 0)
        {
            dt = zt_bll.GetZhuanTiListWithSync(userId);
        }
        if (dt.Rows.Count == 0)
        {
            dt = zz_bll.GetZhuanZhangListWithSync(userId);
        }
        if (dt.Rows.Count == 0)
        {
            dt = card_bll.GetCardListWithSync(userId);
        }
        if (dt.Rows.Count == 0)
        {
            dt = user_bll.GetUserListWithSync(userId);
        }
        
        if (dt.Rows.Count > 0)
        {
            result += "\"result\":\"ok\"";
        }
        else
        {
            result += "\"result\":\"error\"";
        } 

        result += "}";

        Response.Write(result);
        Response.End();
    }
}