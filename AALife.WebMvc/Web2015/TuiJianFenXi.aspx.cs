using AALife.BLL;
using System;
using System.Data;

public partial class TuiJianFenXi : BasePage
{
    private MonthBLL bll = new MonthBLL();
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
        DataTable lists = bll.GetTuiJianFenXiList(userId);
        ItemList.DataSource = lists;
        ItemList.DataBind();

        this.hidChartData.Value = ItemHelper.GetChartData(lists, "ItemBuyDate");
    }
}