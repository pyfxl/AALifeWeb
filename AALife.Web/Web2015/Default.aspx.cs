using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Web;
using AALife.BLL;
using AALife.Model;

public partial class _Default : BasePage
{
    private ItemTableBLL bll = new ItemTableBLL();
    private MonthBLL month_bll = new MonthBLL();
    private CardTableBLL card_bll = new CardTableBLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Calendar1.VisibleDate = Convert.ToDateTime(Session["TodayDate"]);
        this.Calendar1.SelectedDate = Convert.ToDateTime(Session["TodayDate"]);

        if (!IsPostBack)
        {
            BindGrid();
        }
    }

    //初始数据
    private void BindGrid()
    {
        int userId = Convert.ToInt32(Session["UserID"]);
        DateTime today = Utility.GetRequestDate(Request.QueryString["date"]);
        
        //图表点击所需数据
        decimal priceMax = 0m;
        DataTable dt = month_bll.GetItemDateTopList(userId, today, "chart", out priceMax);
        this.hidChartData.Value = ItemHelper.GetChartData(dt, "ItemBuyDate");

        #region 每日消费代码
        DataTable total = month_bll.GetShouZhiJieHuanList(userId, today);
        this.Label2.Text = Convert.ToDouble(total.Rows[0]["ShouRuPrice"]).ToString("0.0##");
        this.Label4.Text = Convert.ToDouble(total.Rows[0]["ZhiChuPrice"]).ToString("0.0##");
        this.Label1.Text = Convert.ToDouble(total.Rows[0]["ShouRuPriceMonth"]).ToString("0.0##");
        this.Label3.Text = Convert.ToDouble(total.Rows[0]["ZhiChuPriceMonth"]).ToString("0.0##");
        this.Label11.Text = Convert.ToDouble(total.Rows[0]["ShouRuPriceYear"]).ToString("0.0##");
        this.Label12.Text = Convert.ToDouble(total.Rows[0]["ZhiChuPriceYear"]).ToString("0.0##");

        double JieChuPrice = Convert.ToDouble(total.Rows[0]["JieChuPriceYear"]);
        double HuanRuPrice = Convert.ToDouble(total.Rows[0]["HuanRuPriceYear"]);
        double JieRuPrice = Convert.ToDouble(total.Rows[0]["JieRuPriceYear"]);
        double HuanChuPrice = Convert.ToDouble(total.Rows[0]["HuanChuPriceYear"]);
        this.Label5.Text = JieChuPrice.ToString("0.##");
        this.Label6.Text = HuanRuPrice.ToString("0.##");
        this.Label7.Text = JieRuPrice.ToString("0.##");
        this.Label8.Text = HuanChuPrice.ToString("0.##");
        this.Label9.Text = (JieChuPrice - HuanRuPrice).ToString("0.##");
        this.Label10.Text = (JieRuPrice - HuanChuPrice).ToString("0.##");
        #endregion

        //首页功能链接
        string userFunction = "";
        if(Session["UserFunction"] != null) 
        {
            userFunction = Session["UserFunction"].ToString();
        }
        this.Label13.Text = UserHelper.GetUserFunctionText(userFunction);

        //钱包列表
        this.CardList.DataSource = card_bll.GetCardList(userId);
        this.CardList.DataTextField = "CardName";
        this.CardList.DataValueField = "CDID";
        this.CardList.DataBind();
        if (Request.Cookies["CardID"] != null)
        {
            string cardId = Request.Cookies["CardID"].Value;
            this.CardList.SelectedValue = cardId;
        }
        CardList_SelectionChanged(this.CardList, null);
    }

    //钱包更改
    protected void CardList_SelectionChanged(object sender, EventArgs e)
    {
        int userId = Convert.ToInt32(Session["UserID"]);
        int cardId = Convert.ToInt32(this.CardList.SelectedValue);

        Response.Cookies["CardID"].Value = cardId.ToString();
        Response.Cookies["CardID"].Expires = DateTime.MaxValue; 

        DataTable card = card_bll.GetCardDataTableByCardId(userId, cardId);
        this.Label14.Text = "￥ " + Convert.ToDecimal(card.Rows[0]["CardBalance"]).ToString("0.0##");
    }

    //选择日期
    protected void Calendar1_SelectionChanged(object sender, EventArgs e)
    {
        this.Calendar1.VisibleDate = this.Calendar1.SelectedDate;
        Session["TodayDate"] = this.Calendar1.SelectedDate.ToString("yyyy-MM-dd");
        Response.Redirect("Default.aspx");
    }

    //选择月份
    protected void Calendar1_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
    {
        this.Calendar1.SelectedDate = new DateTime(e.NewDate.Year, e.NewDate.Month, e.PreviousDate.Day);
        Session["TodayDate"] = this.Calendar1.SelectedDate.ToString("yyyy-MM-dd");
        Response.Redirect("Default.aspx");
    }

}