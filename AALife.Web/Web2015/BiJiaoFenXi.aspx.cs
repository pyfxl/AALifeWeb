using AALife.BLL;
using System;
using System.Data;

public partial class BiJiaoFenXi : BasePage
{
    private MonthBLL bll = new MonthBLL();
    private DateTime today = DateTime.Now;
    private int userId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        userId = Convert.ToInt32(Session["UserID"]);
        today = Utility.GetRequestDate(Request.QueryString["date"]);

        if (!IsPostBack)
        {
            PopulateControls();
        }
    }

    private void PopulateControls()
    {
        decimal priceMax = 0m;
        DataTable lists = bll.GetBiJiaoFenXiList(userId, today, "list", out priceMax);
        List.DataSource = lists;
        List.DataBind();

        double zhiTotalCur = 0d;
        double shouTotalCur = 0d;
        double zhiTotalPrev = 0d;
        double shouTotalPrev = 0d;
        foreach (DataRow dr in lists.Rows)
        {
            zhiTotalCur += Convert.ToDouble(dr["ZhiPriceCur"]);
            shouTotalCur += Convert.ToDouble(dr["ShouPriceCur"]);
            zhiTotalPrev += Convert.ToDouble(dr["ZhiPricePrev"]);
            shouTotalPrev += Convert.ToDouble(dr["ShouPricePrev"]);
        }

        this.Label4.Text = zhiTotalCur.ToString("0.0##");
        this.Label2.Text = shouTotalCur.ToString("0.0##");
        this.Label5.Text = zhiTotalPrev.ToString("0.0##");
        this.Label3.Text = shouTotalPrev.ToString("0.0##");

        DataTable dt = bll.GetBiJiaoFenXiList(userId, today, "chart", out priceMax);
        this.hidChartData.Value = ItemHelper.GetChartData(dt, "PageUrl");
    }
}