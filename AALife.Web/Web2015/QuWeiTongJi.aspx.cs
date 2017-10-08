using System;
using System.Web.UI.WebControls;
using System.Data;
using AALife.BLL;

public partial class QuWeiTongJi : FirstPage
{
    private MonthBLL bll = new MonthBLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PopulateControls();
        }
    }

    private void PopulateControls()
    {
        ItemNameCountList.DataSource = bll.GetTongJiWithItemNameCount();
        ItemNameCountList.DataBind();

        ItemPriceMaxList.DataSource = bll.GetTongJiWithItemPriceMax();
        ItemPriceMaxList.DataBind();

        ItemAddLastList.DataSource = bll.GetTongJiWithItemAddLast();
        ItemAddLastList.DataBind();

        UserItemCountList.DataSource = bll.GetTongJiWithUserItemCount();
        UserItemCountList.DataBind();

        UserItemLastList.DataSource = bll.GetTongJiWithUserItemLast();
        UserItemLastList.DataBind();

        UserAddLastList.DataSource = bll.GetTongJiWithUserAddLast();
        UserAddLastList.DataBind();
    }
}