using AALife.BLL;
using System;
using System.Data;

public partial class UserCardDetail : BasePage
{
    private ItemTableBLL bll = new ItemTableBLL();
    private int userId = 0;
    private int cardId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        userId = Convert.ToInt32(Session["UserID"]);
        cardId = Convert.ToInt32(Request.QueryString["cardId"]);

        if (!IsPostBack)
        {
            PopulateControls();
        }
    }

    //初始数据
    private void PopulateControls()
    {
        DataTable lists = bll.GetItemListByCardId(userId, cardId);
        PriceTop.DataSource = lists;
        PriceTop.DataBind();

        UpdateTotalPrice(lists);
    }

    //更新总价
    private void UpdateTotalPrice(DataTable dt)
    {
        double ZhiChuPrice = 0d;
        double ShouRuPrice = 0d;
        foreach (DataRow dr in dt.Rows)
        {
            string itemType = dr["ItemType"].ToString();
            double itemPrice = Convert.ToDouble(dr["ItemPrice"]);
            if (itemType == "zc" || itemType == "jc" || itemType == "hc")
            {
                ZhiChuPrice += itemPrice;
            }
            else
            {
                ShouRuPrice += itemPrice;
            }
        }

        this.Label2.Text = ShouRuPrice.ToString("0.00#");
        this.Label3.Text = ZhiChuPrice.ToString("0.00#");
    }

}