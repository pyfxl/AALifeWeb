using AALife.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : WebPage
{
    private MonthBLL month_bll = new MonthBLL();
    private CardTableBLL card_bll = new CardTableBLL();
    private int userId = 0;
    protected string curDate = "";
    protected string chartType = "";
    protected string chartUrl = "";
    protected string chartDate = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        userId = Convert.ToInt32(Session["UserID"]);
        curDate = Utility.GetRequestDate(Request.QueryString["date"]).ToString("yyyy-MM-dd");
        chartType = Request.QueryString["chartType"] ?? "datechart";

        if (!IsPostBack)
        {
            BindHome();

            BindCardList();

            BindChartTypeDropDown();

            GetChartInfo();
        }

        //加上使hyperlink可以使用eval，去掉是因为alert不能用
        //Page.DataBind();
    }
    
    //初始数据
    private void BindHome()
    {
        DateTime now = Convert.ToDateTime(curDate).Date;

        DataTable total = month_bll.GetShouZhiJieHuanListV6(userId, now);

        //收入支出
        decimal shouRuPrice = Convert.ToDecimal(total.Rows[0]["ShouRuPrice"]);
        decimal zhiChuPrice = Convert.ToDecimal(total.Rows[0]["ZhiChuPrice"]);
        this.ShouRuLab.Text = shouRuPrice.ToString("0.0##");
        this.ZhiChuLab.Text = zhiChuPrice.ToString("0.0##");
        this.JieCunLab.Text = (shouRuPrice - zhiChuPrice).ToString("0.0##");

        //周收入支出
        decimal shouRuPriceWeek = Convert.ToDecimal(total.Rows[0]["ShouRuPriceWeek"]);
        decimal zhiChuPriceWeek = Convert.ToDecimal(total.Rows[0]["ZhiChuPriceWeek"]);
        this.ShouRuWeekLab.Text = shouRuPriceWeek.ToString("0.0##");
        this.ZhiChuWeekLab.Text = zhiChuPriceWeek.ToString("0.0##");
        this.JieCunWeekLab.Text = (shouRuPriceWeek - zhiChuPriceWeek).ToString("0.0##");

        //月收入支出
        decimal shouRuPriceMonth = Convert.ToDecimal(total.Rows[0]["ShouRuPriceMonth"]);
        decimal zhiChuPriceMonth = Convert.ToDecimal(total.Rows[0]["ZhiChuPriceMonth"]);
        this.ShouRuMonthLab.Text = shouRuPriceMonth.ToString("0.0##");
        this.ZhiChuMonthLab.Text = zhiChuPriceMonth.ToString("0.0##");
        this.JieCunMonthLab.Text = (shouRuPriceMonth - zhiChuPriceMonth).ToString("0.0##");

        //年收入支出
        decimal shouRuPriceYear = Convert.ToDecimal(total.Rows[0]["ShouRuPriceYear"]);
        decimal zhiChuPriceYear = Convert.ToDecimal(total.Rows[0]["ZhiChuPriceYear"]);
        this.ShouRuYearLab.Text = shouRuPriceYear.ToString("0.0##");
        this.ZhiChuYearLab.Text = zhiChuPriceYear.ToString("0.0##");
        this.JieCunYearLab.Text = (shouRuPriceYear - zhiChuPriceYear).ToString("0.0##");

        //全部收入支出
        decimal shouRuPriceAll = Convert.ToDecimal(total.Rows[0]["ShouRuPriceAll"]);
        decimal zhiChuPriceAll = Convert.ToDecimal(total.Rows[0]["ZhiChuPriceAll"]);
        this.ShouRuAllLab.Text = shouRuPriceAll.ToString("0.0##");
        this.ZhiChuAllLab.Text = zhiChuPriceAll.ToString("0.0##");
        this.JieCunAllLab.Text = (shouRuPriceAll - zhiChuPriceAll).ToString("0.0##");

        //借入还出
        decimal jieRuPriceAll = Convert.ToDecimal(total.Rows[0]["JieRuPriceAll"]);
        decimal huanChuPriceAll = Convert.ToDecimal(total.Rows[0]["HuanChuPriceAll"]);
        this.JieRuAllLab.Text = jieRuPriceAll.ToString("0.###");
        this.HuanChuAllLab.Text = huanChuPriceAll.ToString("0.###");
        this.WeiHuanAllLab.Text = (jieRuPriceAll - huanChuPriceAll).ToString("0.###");

        //借出还入
        decimal jieChuPriceAll = Convert.ToDecimal(total.Rows[0]["JieChuPriceAll"]);
        decimal huanRuPriceAll = Convert.ToDecimal(total.Rows[0]["HuanRuPriceAll"]);
        this.JieChuAllLab.Text = jieChuPriceAll.ToString("0.###");
        this.HuanRuAllLab.Text = huanRuPriceAll.ToString("0.###");
        this.QianHuanAllLab.Text = (huanRuPriceAll - jieChuPriceAll).ToString("0.###");

    }

    //取钱包下拉
    private void BindCardList()
    {
        DataTable lists = card_bll.GetCardListWithHome(userId);
        lists.DefaultView.RowFilter = "RowNumber < 3";
        this.CardList.DataSource = lists;
        this.CardList.DataBind();
    }

    //显示下拉
    private void BindChartTypeDropDown()
    {
        this.ChartTypeDropDown.Items.Add(new ListItem("消费日期排行图表", "datechart"));
        this.ChartTypeDropDown.Items.Add(new ListItem("消费次数排行图表", "numchart"));
        this.ChartTypeDropDown.Items.Add(new ListItem("消费单价排行图表", "pricechart"));
        
        if (chartType != "")
        {
            this.ChartTypeDropDown.Items.FindByValue(chartType).Selected = true;
        }
    }

    //设置图表
    private void GetChartInfo()
    {
        DataTable dt = new DataTable();
        DateTime now = DateTime.Now.Date;
        if (curDate != "")
        {
            now = Convert.ToDateTime(curDate).Date;
        }

        switch (chartType)
        {
            case "datechart": 
                decimal priceMax = 0m;
                dt = month_bll.GetItemDateTopList(userId, now, "chart", out priceMax);
                chartDate = ItemHelper.GetChartData(dt, "ItemBuyDate");
                chartUrl = "/Web2015/ItemDateChartJson.aspx?date=" + curDate;
                break;
            case "numchart":
                dt = month_bll.GetItemNumTopList(userId, now);
                chartDate = ItemHelper.GetChartData(dt, "ChartUrl");
                chartUrl = "/Web2015/ItemNumChartJson.aspx?date=" + curDate;
                break;
            case "pricechart":
                dt = month_bll.GetItemPriceTopList(userId, now);
                chartDate = ItemHelper.GetChartData(dt, "ItemBuyDate");
                chartUrl = "/Web2015/ItemPriceChartJson.aspx?date=" + curDate;
                break;
        }
    }
}