using AALife.BLL;
using System;
using System.Data;

public partial class ItemNumTop : BasePage
{
    private MonthBLL bll = new MonthBLL();
    private int userId = 0;
    private DateTime today = DateTime.Now;

    protected void Page_Load(object sender, EventArgs e)
    {
        userId = Convert.ToInt32(Session["UserID"]);
        today = Utility.GetRequestDate(Request.QueryString["date"]);

        if (!IsPostBack)
        {
            PopulateControls();
        }
    }

    //初始数据
    private void PopulateControls()
    {
        DataTable lists = bll.GetItemNumTopList(userId, today);
        NumTop.DataSource = lists;
        NumTop.DataBind();

        this.hidChartData.Value = ItemHelper.GetChartData(lists, "PageUrl");
    }
}