using AALife.BLL;
using System;
using System.Data;

public partial class ItemNumDetail : BasePage
{
    private MonthBLL bll = new MonthBLL();
    private int userId = 0;
    public DateTime today = DateTime.Now;
    public string catTypeId = "0";
    public string itemType = "";
    public string itemName = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        //比较分析明细进入使用
        userId = Convert.ToInt32(Session["UserID"]);
        today = Utility.GetRequestDate(Request.QueryString["date"]);
        
        if (Request.QueryString["catTypeId"] != null && Request.QueryString["catTypeId"] != "")
        {
            catTypeId = Request.QueryString["catTypeId"];
        }

        itemType = Request.QueryString["itemType"] ?? "";
        itemName = Request.QueryString["itemName"] ?? "";
        
        if (!IsPostBack)
        {
            PopulateControls();
        }
    }

    //初始数据
    private void PopulateControls()
    {
        DataTable lists = bll.GetItemNumDetailList(userId, today, Convert.ToInt32(catTypeId), itemType, itemName, "list");
        List.DataSource = lists;
        List.DataBind();

        this.hidChartData.Value = ItemHelper.GetChartData(lists, "ItemBuyDate");
    }
}