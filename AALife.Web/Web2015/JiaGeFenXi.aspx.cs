using AALife.BLL;
using System;
using System.Data;

public partial class JiaGeFenXi : BasePage
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
        decimal priceMax = 0m;

        DataTable lists = bll.GetJiaGeFenXiList(userId, pageNumber, pagePerNumber, out howManyItems, out priceMax);
        ItemList.DataSource = lists;
        ItemList.DataBind();

        this.hidChartData.Value = ItemHelper.GetChartData(lists, "PageUrl");
    }
}