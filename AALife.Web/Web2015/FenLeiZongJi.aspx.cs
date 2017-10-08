using AALife.BLL;
using System;
using System.Data;

public partial class FenLeiZongJi : BasePage
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
        DataTable lists = bll.GetFenLeiZongJiList(userId, today);
        List.DataSource = lists;
        List.DataBind();

        double zhiPriceTotal = 0d;
        double shouPriceTotal = 0d;
        foreach (DataRow dr in lists.Rows)
        {
            zhiPriceTotal += Convert.ToDouble(dr["ZhiPriceTotal"]);
            shouPriceTotal += Convert.ToDouble(dr["ShouPriceTotal"]);
        }
        this.Label3.Text = zhiPriceTotal.ToString("0.0##");
        this.Label2.Text = shouPriceTotal.ToString("0.0##");

        this.hidChartData.Value = ItemHelper.GetChartData(lists, "PageUrl");
    }

    //前台设置预警灯
    public string GetCategoryDen(double catPrice, double price) {
        double catRate = Convert.ToDouble(Session["CategoryRate"]);
        double[] denArr = GetDenArray(catPrice, catRate);
        string result = "";
        if (price > 0 && catPrice > 0)
        {
            if (price >= denArr[0] && price < denArr[1])
            {
                result = "<img src='/Images/Others/Bullet-Yellow.png' height='29px' title='临界 " + denArr[0] + " ~ " + denArr[1] + "' />";
            }
            else if (price > denArr[1])
            {
                result = "<img src='/Images/Others/Bullet-Red.png' height='29px' title='超出 " + denArr[0] + " ~ " + denArr[1] + "' />";
            }
            else
            {
                result = "<img src='/Images/Others/Bullet-Blue.png' height='29px' title='正常 " + denArr[0] + " ~ " + denArr[1] + "' />";
            }
        }
        else
        {
            result = "<img src='/Images/Others/Bullet-White.png' height='29px' title='空值' />";
        }

        return result;
    }

    //取预警范围
    private double[] GetDenArray(double catPrice, double catRate)
    {
        double num = catPrice - catPrice * (catRate / 100);
        double[] result = new double[2];
        result[0] = catPrice - num;
        result[1] = catPrice + num;

        return result;
    }

}