using AALife.BLL;
using System;
using System.Data;

public partial class JieHuanFenXi : BasePage
{
    private MonthBLL bll = new MonthBLL();
    private int userId = 0;
    public DateTime today = DateTime.Now;
    private string type = "year";

    protected void Page_Load(object sender, EventArgs e)
    {
        userId = Convert.ToInt32(Session["UserID"]);
        today = Utility.GetRequestDate(Request.QueryString["date"]);
        
        if (Request.QueryString["type"] != null && Request.QueryString["type"] != "")
        {
            type = Request.QueryString["type"];
        }

        if (!IsPostBack)
        {
            PopulateControls();
        }
    }

    //初始数据
    private void PopulateControls()
    {
        DataTable lists = bll.GetJieHuanFenXiList(userId, today, type);
        List.DataSource = lists;
        List.DataBind();
        
        double ZhiChuPrice = 0d;
        double ShouRuPrice = 0d;
        double JieRuPrice = 0d;
        double HuanChuPrice = 0d;
        double JieChuPrice = 0d;
        double HuanRuPrice = 0d;
        foreach (DataRow dr in lists.Rows)
        {
            ZhiChuPrice += Convert.ToDouble(dr["ZhiChuPrice"]);
            ShouRuPrice += Convert.ToDouble(dr["ShouRuPrice"]);
            JieChuPrice += Convert.ToDouble(dr["JieChuPrice"]);
            HuanRuPrice += Convert.ToDouble(dr["HuanRuPrice"]);
            JieRuPrice += Convert.ToDouble(dr["JieRuPrice"]);
            HuanChuPrice += Convert.ToDouble(dr["HuanChuPrice"]);
        }

        this.Label2.Text = ZhiChuPrice.ToString("0.0##");
        this.Label3.Text = ShouRuPrice.ToString("0.0##");
        this.Label4.Text = JieChuPrice.ToString("0.##");
        this.Label5.Text = HuanRuPrice.ToString("0.##");
        this.Label6.Text = JieRuPrice.ToString("0.##");
        this.Label7.Text = HuanChuPrice.ToString("0.##");

        this.Label8.Text = (ShouRuPrice - ZhiChuPrice).ToString("0.0##");
        this.Label9.Text = (JieChuPrice - HuanRuPrice).ToString("0.##");
        this.Label10.Text = (JieRuPrice - HuanChuPrice).ToString("0.##");
    }

    public string GetURL(string date)
    {
        if (type == "year")
        {
            return "JieHuanFenXi.aspx?date=" + date + "-01-01&type=month";
        }
        else
        {
            return "MonthList.aspx?date=" + date + "-01";
        }
    }

}