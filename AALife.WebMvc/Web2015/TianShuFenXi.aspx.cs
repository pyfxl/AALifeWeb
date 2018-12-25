using AALife.BLL;
using System;
using System.Data;

public partial class TianShuFenXi : BasePage
{
    private MonthBLL bll = new MonthBLL();
    private int userId = 0;
    public int howManyItems = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        userId = Convert.ToInt32(Session["UserID"]);

        if (!IsPostBack)
        {
            PopulateControls();
        }
    }

    private void PopulateControls()
    {
        int pageNumber = 1; 
        int pagePerNumber = Convert.ToInt32(WebConfiguration.PagePerNumber);
        int notBuyMax = 0;

        DataTable lists = bll.GetTianShuFenXiList(userId, pageNumber, pagePerNumber, out howManyItems, out notBuyMax);
        List.DataSource = lists;
        List.DataBind();
        
        this.hidChartData.Value = ItemHelper.GetChartData(lists, "ItemBuyDate");
    }
}