using System;
using System.Web.UI.WebControls;
using System.Data;
using AALife.BLL;
using AALife.Model;
using System.Transactions;

public partial class UserCardAdmin : WebPage
{
    private CardTableBLL bll = new CardTableBLL();
    private ItemTableBLL item_bll = new ItemTableBLL();
    private UserTableBLL user_bll = new UserTableBLL();
    private ZhuanZhangTableBLL zhuan_bll = new ZhuanZhangTableBLL();
    private int userId = 0;
    public double TotalMoney = 0d;

    protected void Page_Load(object sender, EventArgs e)
    {
        userId = Convert.ToInt32(Session["UserID"]);
        
        BindTotal();

        if (!IsPostBack)
        {
            BindGrid();
            
            //转账
            this.CardDownEdit.DataSource = bll.GetCardList(userId);
            this.CardDownEdit.DataTextField = "CardName";
            this.CardDownEdit.DataValueField = "CDID";
            this.CardDownEdit.DataBind();

            //转账日期
            this.CardDateEdit.Text = DateTime.Now.ToString("yyyy-MM-dd");
            this.CardDateEdit.Attributes.Add("ReadOnly", "readonly");
        }
    }

    //初始列表
    private void BindGrid()
    {
        DataTable lists = bll.GetCardList(userId);
        this.CardList.DataSource = lists;
        this.CardList.DataBind();

        this.CardIdEmpIns.Text = (lists.Rows.Count + 1).ToString();
    }

    //计算总计
    private void BindTotal()
    {
        DataTable lists = bll.GetCardList(userId);
        foreach (DataRow dr in lists.Rows)
        {
            TotalMoney += Convert.ToDouble(dr["CardBalance"]);
        }
    }

    //钱包更新操作
    protected void CardList_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int cardId = Convert.ToInt32(CardList.DataKeys[e.RowIndex].Value);
        string cardName = ((TextBox)CardList.Rows[e.RowIndex].FindControl("CardNameBox")).Text.Trim();
        string cardNameHid = ((HiddenField)CardList.Rows[e.RowIndex].FindControl("CardNameHid")).Value;
        string cardMoneyBox = ((TextBox)CardList.Rows[e.RowIndex].FindControl("CardMoneyBox")).Text.Trim();
        decimal cardMoneyHid = Convert.ToDecimal(((HiddenField)CardList.Rows[e.RowIndex].FindControl("CardMoneyHid")).Value);
        decimal moneyStartHid = Convert.ToDecimal(((HiddenField)CardList.Rows[e.RowIndex].FindControl("MoneyStartHid")).Value);
        decimal cardBalanceHid = Convert.ToDecimal(((HiddenField)CardList.Rows[e.RowIndex].FindControl("CardBalanceHid")).Value);

        if (cardName == "")
        {
            Utility.Alert(this, "钱包名称未填写！");
            return;
        }

        if (!ValidHelper.CheckDouble(cardMoneyBox))
        {
            Utility.Alert(this, "余额填写错误！");
            return;
        }

        decimal cardMoney = 0m;
        if (moneyStartHid == 0)
        {
            cardMoney = Convert.ToDecimal(cardMoneyBox) - cardBalanceHid;
        }
        else
        {
            cardMoney = Convert.ToDecimal(cardMoneyBox) + moneyStartHid - cardBalanceHid;
        }

        bool IsUpdate = Session["IsUpdate"].ToString() == "1";
        if (!IsUpdate)
        {
            cardMoney = Convert.ToDecimal(cardMoneyBox);
        }

        bool success = false;
        if (cardId == 0)
        {
            UserInfo user = user_bll.GetUserByUserId(userId);
            if (!IsUpdate) user.UserMoney = cardMoney;
            if (IsUpdate) user.MoneyStart = cardMoney;
            user.ModifyDate = DateTime.Now;
            user.Synchronize = 1;

            success = user_bll.UpdateUser(user);
        }
        else
        {
            CardInfo card = new CardInfo();

            if (cardName != cardNameHid)
            {
                card = bll.GetCardByCardName(userId, cardName);
                if (card.CardID > 0)
                {
                    Utility.Alert(this, "钱包已存在，不能重复添加！");
                    return;
                }
            }

            card.CDID = cardId;
            card.CardName = cardName;
            if (!IsUpdate) card.CardMoney = cardMoney;
            if (IsUpdate) card.MoneyStart = cardMoney;
            card.UserID = userId;
            card.CardLive = 1;
            card.Synchronize = 1;
            card.ModifyDate = DateTime.Now;

            success = bll.UpdateCard(card);
        }

        if (success)
        {
            Utility.Alert(this, "更新成功。");

            CardList.EditIndex = -1;
            BindGrid();
        }
        else
        {
            Utility.Alert(this, "更新失败！");
        }
    }

    //钱包取消操作
    protected void CardList_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        CardList.EditIndex = -1;
        BindGrid();
    }

    //钱包更新进入操作
    protected void CardList_RowEditing(object sender, GridViewEditEventArgs e)
    {
        CardList.EditIndex = e.NewEditIndex;
        BindGrid();

        int cardId = Convert.ToInt32(CardList.DataKeys[e.NewEditIndex].Value);
        TextBox cardNameBox = (TextBox)CardList.Rows[e.NewEditIndex].FindControl("CardNameBox");
        if (cardId == 0)
        {
            cardNameBox.Attributes.Add("readonly", "readonly");
            cardNameBox.CssClass = "celldisable";
        }
    }

    //行数据绑定
    protected void CardList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowIndex % 2 == 0)
            {
                e.Row.CssClass = "trcolor1";
            }
            else
            {
                e.Row.CssClass = "trcolor2";
            }
        }
    }

    //添加钱包操作
    protected void Button1_Click(object sender, EventArgs e)
    {
        string cardName = this.CardNameEmpIns.Text.Trim();
        string cardMoney = this.CardMoneyEmpIns.Text.Trim();

        if (cardName == "")
        {
            Utility.Alert(this, "钱包名称未填写！");
            return;
        }

        if (cardMoney != "")
        {
            if (!ValidHelper.CheckDouble(cardMoney))
            {
                Utility.Alert(this, "余额填写错误！");
                return;
            }
        }
        else
        {
            cardMoney = "0";
        }

        CardInfo card = bll.GetCardByCardName(userId, cardName);
        card.CardName = cardName;
        card.MoneyStart = Convert.ToDecimal(cardMoney);
        card.UserID = userId;
        card.CardLive = 1;
        card.Synchronize = 1;
        card.CDID = bll.GetMaxCardId(userId);
        card.ModifyDate = DateTime.Now;

        if (card.CardID > 0 || cardName == "我的钱包")
        {
            Utility.Alert(this, "钱包已存在，不能重复添加！");
            return;
        }

        bool success = bll.InsertCard(card);
        if (success)
        {
            Utility.Alert(this, "添加成功。", "UserCardAdmin.aspx");
        }
        else
        {
            Utility.Alert(this, "添加失败！");
        }
    }

    //钱包删除操作
    protected void CardList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int cardId = Convert.ToInt32(CardList.DataKeys[e.RowIndex].Value);

        if (cardId == 0)
        {
            Utility.Alert(this, "不能删除我的钱包！");
            return;
        }

        DataTable items = item_bll.GetItemListByCardId(userId, cardId);
        if (items.Rows.Count > 0)
        {
            Utility.Alert(this, "不能删除已使用的钱包！");
            return;
        }

        CardInfo card = bll.GetCardByCardId(userId, cardId);
        card.CardLive = 0;
        card.Synchronize = 1;
        card.ModifyDate = DateTime.Now;

        bool success = bll.UpdateCard(card);
        if (success)
        {
            Utility.Alert(this, "删除成功。");

            CardList.EditIndex = -1;
            BindGrid();
        }
        else
        {
            Utility.Alert(this, "删除失败！");
        }

    }

    //转账
    protected void SubmitButtom_Click(object sender, EventArgs e)
    {
        int cardId = Convert.ToInt32(this.CardIDEditHid.Value);
        int toCardId = Convert.ToInt32(this.CardDownEdit.SelectedValue);
        string cardMoney = this.CardMoneyEdit.Text.Trim();
        string cardDate = this.CardDateEdit.Text;

        if (!ValidHelper.CheckDouble(cardMoney))
        {
            Utility.Alert(this, "金额填写错误！");
            return;
        }

        if (cardId == toCardId)
        {
            Utility.Alert(this, "不允许转给相同钱包！");
            return;
        }

        using (TransactionScope ts = new TransactionScope())
        {
            CardInfo card = new CardInfo();
            UserInfo user = user_bll.GetUserByUserId(userId);
            decimal newMoney = Convert.ToDecimal(cardMoney);

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
                card = bll.GetCardByCardId(userId, cardId);
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

                bll.UpdateCard(card);
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
                card = bll.GetCardByCardId(userId, toCardId);
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

                bll.UpdateCard(card);
            }

            ZhuanZhangInfo zhuan = new ZhuanZhangInfo();
            zhuan.ZhuanZhangFrom = cardId;
            zhuan.ZhuanZhangTo = toCardId;
            zhuan.ZhuanZhangDate = Convert.ToDateTime(cardDate);
            zhuan.ZhuanZhangMoney = Convert.ToDecimal(cardMoney);
            zhuan.ZhuanZhangLive = 1;
            zhuan.Synchronize = 1;
            zhuan.ModifyDate = DateTime.Now;
            zhuan.UserID = userId;
            zhuan.ZZID = zhuan_bll.GetMaxZhuanZhangId(userId);

            bool success = zhuan_bll.InsertZhuanZhang(zhuan);
            if (!success)
            {
                Utility.Alert(this, "保存转账记录失败！");
            }

            ts.Complete();
        }

        Response.Redirect("UserCardAdmin.aspx");
    }

    //首页显示CheckBox
    protected void CardShowBox_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox cb = (CheckBox)sender;
        int cardId = Convert.ToInt32(((HiddenField)cb.Parent.FindControl("CardIDHid")).Value);
        
        CardInfo card = bll.GetCardByCardId(userId, cardId);
        card.Synchronize = 1;
        card.ModifyDate = DateTime.Now;

        if (cb.Checked)
        {
            card.CardShow = 1;
        }
        else
        {
            card.CardShow = 0;
        }

        bool success = bll.UpdateCard(card);
        if (!success)
        {
            Utility.Alert(this, "更新失败！");
        }
    }

}
