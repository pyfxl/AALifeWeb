using AALife.BLL;
using System;
using System.Data;
using System.Transactions;
using System.Web.UI.WebControls;

public partial class QuJianTongJi : BasePage
{
    private MonthBLL bll = new MonthBLL();
    private ItemTableBLL item_bll = new ItemTableBLL();
    private int userId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        userId = Convert.ToInt32(Session["UserID"]);

        if (!IsPostBack)
        {
            PopulateControls();
        }
    }

    //初始数据
    private void PopulateControls()
    {
        DataTable lists = bll.GetQuJianTongJiList(userId);
        List.DataSource = lists;
        List.DataBind();

        this.hidChartData.Value = ItemHelper.GetChartData(lists, "ItemBuyDate");
    }

    //删除区间
    protected void Button2_Command(object sender, CommandEventArgs e)
    {
        int regionId = Convert.ToInt32(e.CommandArgument);

        bool success = false;
        using (TransactionScope ts = new TransactionScope())
        {
            DataTable lists = item_bll.GetItemListByRegionId(userId, regionId);
            foreach (DataRow dr in lists.Rows)
            {
                int itemId = Convert.ToInt32(dr["ItemID"]);
                int itemAppId = Convert.ToInt32(dr["ItemAppID"]);

                success = item_bll.DeleteItem(userId, itemId, itemAppId);
                if (!success)
                {
                    break;
                }
            }

            ts.Complete();
        }
        
        if (success)
        {
            Utility.Alert(this, "删除成功。", "QuJianTongJi.aspx");
        }
        else
        {
            Utility.Alert(this, "删除失败！");
        }
    }

}