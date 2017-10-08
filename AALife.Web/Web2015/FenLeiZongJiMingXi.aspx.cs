using AALife.BLL;
using System;
using System.Data;

public partial class FenLeiZongJiMingXi : BasePage
{
    private MonthBLL bll = new MonthBLL();
    private int userId = 0;
    private DateTime today = DateTime.Now;
    public int catTypeId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        userId = Convert.ToInt32(Session["UserID"]); 
        today = Convert.ToDateTime(Session["TodayDate"]);

        if (Request.QueryString["catTypeId"] != null && Request.QueryString["catTypeId"] != "")
        {
            if (!ValidHelper.CheckNumber(Request.QueryString["catTypeId"]))
            {
                Response.Redirect("FenLeiZongJi.aspx");
            }
            else
            {
                catTypeId = Convert.ToInt32(Request.QueryString["catTypeId"]);
            }
        }

        if (!IsPostBack)
        {
            PopulateControls();
        }
    }

    //初始数据
    private void PopulateControls()
    {
        decimal priceMax = 0m;
        DataTable lists = bll.GetFenLeiZongJiMingXiList(userId, today, catTypeId, out priceMax);
        List.DataSource = lists;
        List.DataBind();

        this.hidChartData.Value = ItemHelper.GetChartData(lists, "PageUrl");
    }
}