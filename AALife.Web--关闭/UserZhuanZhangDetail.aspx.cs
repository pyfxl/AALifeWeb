using AALife.BLL;
using AALife.Model;
using System;
using System.Data;
using System.Transactions;
using System.Web.UI.WebControls;

public partial class UserZhuanZhangDetail : WebPage
{
    private ZhuanZhangTableBLL bll = new ZhuanZhangTableBLL();
    private UserTableBLL user_bll = new UserTableBLL();
    private CardTableBLL card_bll = new CardTableBLL();
    private int userId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        userId = Convert.ToInt32(Session["UserID"]);

        if (!IsPostBack)
        {
            PopulateControls();
        }
    }

    //初始数据
    private void PopulateControls()
    {
        DataTable lists = bll.GetZhuanZhangList(userId);
        ItemList.DataSource = lists;
        ItemList.DataBind();

    }

    //删除按钮
    protected void List_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        using (TransactionScope ts = new TransactionScope())
        {
            CardInfo card = new CardInfo();
            UserInfo user = user_bll.GetUserByUserId(userId);

            int zzId = Convert.ToInt32(e.CommandArgument);
            decimal newMoney = Convert.ToDecimal(((HiddenField)e.Item.FindControl("ZhangMoneyHid")).Value);
            int toCardId = Convert.ToInt32(((HiddenField)e.Item.FindControl("ZhangFromHid")).Value);
            int cardId = Convert.ToInt32(((HiddenField)e.Item.FindControl("ZhangToHid")).Value);

            if (cardId == 0)
            {
                if (user.IsUpdate == 1)
                {
                    user.MoneyStart = user.MoneyStart - newMoney;
                }
                else
                {
                    user.UserMoney = user.UserMoney - newMoney;
                }
                user.ModifyDate = DateTime.Now;
                user.Synchronize = 1;

                user_bll.UpdateUser(user);
            }
            else
            {
                card = card_bll.GetCardByCardId(userId, cardId);
                if (user.IsUpdate == 1)
                {
                    card.MoneyStart = card.MoneyStart - newMoney;
                }
                else
                {
                    card.CardMoney = card.CardMoney - newMoney;
                }
                card.ModifyDate = DateTime.Now;
                card.Synchronize = 1;

                card_bll.UpdateCard(card);
            }

            if (toCardId == 0)
            {
                if (user.IsUpdate == 1)
                {
                    user.MoneyStart = user.MoneyStart + newMoney;
                }
                else
                {
                    user.UserMoney = user.UserMoney + newMoney;
                }
                user.ModifyDate = DateTime.Now;
                user.Synchronize = 1;

                user_bll.UpdateUser(user);
            }
            else
            {
                card = card_bll.GetCardByCardId(userId, toCardId);
                if (user.IsUpdate == 1)
                {
                    card.MoneyStart = card.MoneyStart + newMoney;
                }
                else
                {
                    card.CardMoney = card.CardMoney + newMoney;
                }
                card.ModifyDate = DateTime.Now;
                card.Synchronize = 1;

                card_bll.UpdateCard(card);
            }

            ZhuanZhangInfo zzInfo = bll.GetZhuanZhangByZZID(userId, zzId);
            zzInfo.Synchronize = 1;
            zzInfo.ZhuanZhangLive = 0;
            zzInfo.ModifyDate = DateTime.Now;

            bool success = bll.UpdateZhuanZhang(zzInfo);
            if (!success)
            {
                Utility.Alert(this, "转账删除失败！");
            }

            ts.Complete();
        }

        Response.Redirect("UserZhuanZhangDetail.aspx");

    }

}