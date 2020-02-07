using AALife.BLL;
using System;
using System.Data;

public partial class MonthList : BasePage
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
        DataTable lists = bll.GetMonthList(userId, today);
        List.DataSource = lists;
        List.DataBind();

        double ZhiChuPrice = 0d;
        double ShouRuPrice = 0d;
        double JiePrice = 0d;
        double HuanPrice = 0d;
        foreach (DataRow dr in lists.Rows)
        {
            ZhiChuPrice += Convert.ToDouble(dr["ZhiChuPrice"]);
            ShouRuPrice += Convert.ToDouble(dr["ShouRuPrice"]);
            JiePrice -= Convert.ToDouble(dr["JiePrice"]);
            HuanPrice -= Convert.ToDouble(dr["HuanPrice"]);
        }

        this.Label2.Text = ZhiChuPrice.ToString("0.0##");
        this.Label3.Text = ShouRuPrice.ToString("0.0##");
        this.Label4.Text = JiePrice.ToString("0.##");
        this.Label5.Text = HuanPrice.ToString("0.##");
    }
}
