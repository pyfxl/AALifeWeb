using AALife.BLL;
using System;
using System.Data;

public partial class BiJiaoMingXi : BasePage
{
    private int userId = 0;
    private DateTime today = DateTime.Now;
    private int catTypeId = 0;
    private MonthBLL bll = new MonthBLL();

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

    private void PopulateControls()
    {
        decimal priceMax = 0m;
        int countMax = 0;
        DataTable dt = bll.GetBiJiaoMingXiList(userId, today, catTypeId, out priceMax, out countMax);
        CatTypeList.DataSource = dt;
        CatTypeList.DataBind();

        this.hidChartData.Value = ItemHelper.GetChartData(dt, "PageUrlCur");
        this.hidChartData2.Value = ItemHelper.GetChartData(dt, "PageUrlPrev");
    }
}