using AALife.BLL;
using System;
using System.Data;

public partial class JiaGeFenXiMingXi : BasePage
{
    private MonthBLL bll = new MonthBLL();
    int userId = 0;
    public string itemType = ""; 
    public string itemName = "";
    public int howManyItems = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        userId = Convert.ToInt32(Session["UserID"]);
        itemType = Request.QueryString["itemType"] ?? "";
        itemName = Request.QueryString["itemName"] ?? "";

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

        DataTable lists = bll.GetJiaGeFenXiMingXiList(userId, itemType, itemName, pageNumber, pagePerNumber, out howManyItems, out priceMax);
        List.DataSource = lists;
        List.DataBind();

        this.hidChartData.Value = ItemHelper.GetChartData(lists, "ItemBuyDate");
    }
}