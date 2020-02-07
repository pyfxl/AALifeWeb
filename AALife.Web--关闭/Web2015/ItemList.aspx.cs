using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using AALife.BLL;
using AALife.Model;
using System.Transactions;

public partial class ItemList : BasePage
{
    private ItemTableBLL bll = new ItemTableBLL();
    private MonthBLL month_bll = new MonthBLL();
    private UserCategoryTableBLL cat_bll = new UserCategoryTableBLL();
    private CardTableBLL card_bll = new CardTableBLL();
    private ZhuanTiTableBLL zt_bll = new ZhuanTiTableBLL();
    private UserTableBLL user_bll = new UserTableBLL();
    private int userId = 0;
    private DateTime today = DateTime.Now;
    private DataTable catTypeList = new DataTable();
    private DataTable itemTypeList = new DataTable();
    private DataTable cardList = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        userId = Convert.ToInt32(Session["UserID"]);
        today = Utility.GetRequestDate(Request.QueryString["date"]);
        catTypeList = cat_bll.GetUserCategoryList(userId);
        itemTypeList = bll.GetItemTypeList();
        cardList = card_bll.GetCardList(userId);

        if (!IsPostBack)
        {
            BindGrid();

            BindTotal();

            BindInsert();

            //专题
            this.ZhuanTiDown.DataSource = zt_bll.GetZhuanTiList(userId);
            this.ZhuanTiDown.DataTextField = "ZhuanTiName";
            this.ZhuanTiDown.DataValueField = "ZTID";
            this.ZhuanTiDown.DataBind();
            this.ZhuanTiDown.Items.Insert(0, new ListItem("请选择", "0"));
        }
    }

    //初始列表
    private void BindGrid()
    {
        DataTable lists = bll.GetItemList(userId, today);
        List.DataSource = lists;
        List.DataBind();
    }

    //初始总计
    private void BindTotal()
    {
        DataTable total = month_bll.GetShouZhiJieHuanList(userId, today);

        //收支
        this.Label2.Text = Convert.ToDouble(total.Rows[0]["ShouRuPrice"]).ToString("0.0##");
        this.Label4.Text = Convert.ToDouble(total.Rows[0]["ZhiChuPrice"]).ToString("0.0##");
        this.Label1.Text = Convert.ToDouble(total.Rows[0]["ShouRuPriceMonth"]).ToString("0.0##");
        this.Label3.Text = Convert.ToDouble(total.Rows[0]["ZhiChuPriceMonth"]).ToString("0.0##");
        this.Label11.Text = Convert.ToDouble(total.Rows[0]["ShouRuPriceYear"]).ToString("0.0##");
        this.Label12.Text = Convert.ToDouble(total.Rows[0]["ZhiChuPriceYear"]).ToString("0.0##");

        //借还
        double JieChuPrice = Convert.ToDouble(total.Rows[0]["JieChuPrice"]);
        double HuanRuPrice = Convert.ToDouble(total.Rows[0]["HuanRuPrice"]);
        double JieRuPrice = Convert.ToDouble(total.Rows[0]["JieRuPrice"]);
        double HuanChuPrice = Convert.ToDouble(total.Rows[0]["HuanChuPrice"]);
        this.Label5.Text = JieChuPrice.ToString("0.##");
        this.Label6.Text = HuanRuPrice.ToString("0.##");
        this.Label7.Text = JieRuPrice.ToString("0.##");
        this.Label8.Text = HuanChuPrice.ToString("0.##");
        this.Label9.Text = (JieChuPrice - HuanRuPrice).ToString("0.##");
        this.Label10.Text = (JieRuPrice - HuanChuPrice).ToString("0.##");
    }

    //初始添加
    private void BindInsert()
    {
        //商品类别
        this.CatTypeEmpIns.DataSource = catTypeList;
        this.CatTypeEmpIns.DataTextField = "CategoryTypeName";
        this.CatTypeEmpIns.DataValueField = "CategoryTypeID";
        this.CatTypeEmpIns.DataBind();
        if (Request.Cookies["CatTypeID"] != null)
        {
            this.CatTypeEmpIns.SelectedValue = Request.Cookies["CatTypeID"].Value;
        }

        //分类
        this.ItemTypeEmpIns.DataSource = itemTypeList;
        this.ItemTypeEmpIns.DataTextField = "ItemTypeName";
        this.ItemTypeEmpIns.DataValueField = "ItemType";
        this.ItemTypeEmpIns.DataBind();

        //钱包
        this.CardEmpIns.DataSource = cardList;
        this.CardEmpIns.DataTextField = "CardName";
        this.CardEmpIns.DataValueField = "CDID";
        this.CardEmpIns.DataBind();
        if (Request.Cookies["CardID"] != null)
        {
            this.CardEmpIns.SelectedValue = Request.Cookies["CardID"].Value;
        }
    }

    //进入编辑操作
    protected void List_RowEditing(object sender, GridViewEditEventArgs e)
    {
        List.EditIndex = e.NewEditIndex;
        BindGrid();

        HiddenField catTypeIdHid = (HiddenField)List.Rows[e.NewEditIndex].FindControl("CatTypeIDHid");
        HiddenField itemTypeIdHid = (HiddenField)List.Rows[e.NewEditIndex].FindControl("ItemTypeIDHid");
        DropDownList catTypeDropDown = (DropDownList)List.Rows[e.NewEditIndex].FindControl("CatTypeDropDown");
        DropDownList itemTypeDropDown = (DropDownList)List.Rows[e.NewEditIndex].FindControl("ItemTypeDropDown");
        TextBox itemBuyDate = (TextBox)List.Rows[e.NewEditIndex].FindControl("ItemBuyDateBox");
        int regionId = Convert.ToInt32(((HiddenField)List.Rows[e.NewEditIndex].FindControl("RegionIDHid")).Value);
        DropDownList cardDropDown = (DropDownList)List.Rows[e.NewEditIndex].FindControl("CardDropDown");
        HiddenField cardIdHid = (HiddenField)List.Rows[e.NewEditIndex].FindControl("CardIDHid");

        //设置下拉字段样式
        TableCell catCell = (TableCell)catTypeDropDown.Parent;
        catCell.CssClass = "typeselect";
        TableCell itemCell = (TableCell)itemTypeDropDown.Parent;
        itemCell.CssClass = "typeselect";
        TableCell cardCell = (TableCell)cardDropDown.Parent;
        cardCell.CssClass = "typeselect";

        //商品类别
        catTypeDropDown.DataSource = catTypeList;
        catTypeDropDown.DataTextField = "CategoryTypeName";
        catTypeDropDown.DataValueField = "CategoryTypeID";
        catTypeDropDown.DataBind();
        if (catTypeIdHid.Value != "")
        {
            catTypeDropDown.Items.FindByValue(catTypeIdHid.Value).Selected = true;
        }
        else
        {
            catTypeDropDown.SelectedIndex = 0;
        }

        //分类
        itemTypeDropDown.DataSource = itemTypeList;
        itemTypeDropDown.DataTextField = "ItemTypeName";
        itemTypeDropDown.DataValueField = "ItemType";
        itemTypeDropDown.DataBind();
        if (itemTypeIdHid.Value != "")
        {
            itemTypeDropDown.Items.FindByValue(itemTypeIdHid.Value).Selected = true;
        }
        else
        {
            itemTypeDropDown.SelectedIndex = 0;
        }

        //钱包
        cardDropDown.DataSource = cardList;
        cardDropDown.DataTextField = "CardName";
        cardDropDown.DataValueField = "CDID";
        cardDropDown.DataBind();
        if (cardIdHid.Value != "")
        {
            cardDropDown.Items.FindByValue(cardIdHid.Value).Selected = true;
        }
        else
        {
            cardDropDown.SelectedIndex = 0;
        }

        //如果是区间消费，则购买日期不可编辑
        if (regionId > 0)
        {
            itemBuyDate.Attributes.Add("disabled", "disabled");
            itemBuyDate.Style.Add("background", "#F4F4F4");
        }
    }

    //删除操作
    protected void List_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int itemId = Convert.ToInt32(List.DataKeys[e.RowIndex].Value);
        int regionId = Convert.ToInt32(((HiddenField)List.Rows[e.RowIndex].FindControl("RegionIDHid")).Value);
        int itemAppId = Convert.ToInt32(((HiddenField)List.Rows[e.RowIndex].FindControl("ItemAppIDHid")).Value);

        bool success = false;
        using (TransactionScope ts = new TransactionScope())
        {
            if (regionId > 0)
            {
                DataTable items = bll.GetItemListByRegionId(userId, regionId);
                foreach (DataRow dr in items.Rows)
                {
                    itemId = Convert.ToInt32(dr["ItemID"]);
                    itemAppId = Convert.ToInt32(dr["ItemAppID"]);

                    success = bll.DeleteItem(userId, itemId, itemAppId);
                }
            }
            else
            {
                success = bll.DeleteItem(userId, itemId, itemAppId);
            }

            ts.Complete();
        }
        
        if (success)
        {
            List.EditIndex = -1;
            BindGrid();
        }
        else 
        {
            Utility.Alert(this, "删除失败！");
        }
    }

    //取消操作
    protected void List_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        List.EditIndex = -1;
        BindGrid();
    }

    //更新操作
    protected void List_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int itemId = Convert.ToInt32(List.DataKeys[e.RowIndex].Value);
        string itemName = ((TextBox)List.Rows[e.RowIndex].FindControl("ItemNameBox")).Text.Trim();
        string itemType = ((DropDownList)List.Rows[e.RowIndex].FindControl("ItemTypeDropDown")).SelectedValue.ToString();
        int catTypeId = Convert.ToInt32(((DropDownList)List.Rows[e.RowIndex].FindControl("CatTypeDropDown")).SelectedValue);
        string itemPrice = ((TextBox)List.Rows[e.RowIndex].FindControl("ItemPriceBox")).Text.Trim();
        DateTime itemBuyDate = Convert.ToDateTime(((TextBox)List.Rows[e.RowIndex].FindControl("ItemBuyDateBox")).Text.Trim() + " " + DateTime.Now.ToString("HH:mm:ss"));
        int regionId = Convert.ToInt32(((HiddenField)List.Rows[e.RowIndex].FindControl("RegionIDHid")).Value);
        int cardId = Convert.ToInt32(((DropDownList)List.Rows[e.RowIndex].FindControl("CardDropDown")).SelectedValue);
        int itemAppId = 0;
        byte recommend = 0;

        if (itemName == "")
        {
            Utility.Alert(this, "商品名称未填写！");
            return;
        }

        ItemInfo item = bll.GetItemByItemId(itemId);
        item.ItemID = itemId;
        item.ItemType = itemType;
        item.ItemName = itemName;
        item.CategoryTypeID = catTypeId;
        item.ItemPrice = Convert.ToDecimal(itemPrice);
        item.ItemBuyDate = itemBuyDate;
        item.Synchronize = 1;
        item.CardID = cardId;
        item.ModifyDate = DateTime.Now;

        bool success = false;
        using (TransactionScope ts = new TransactionScope())
        {
            if (regionId > 0)
            {
                DataTable items = bll.GetItemListByRegionId(userId, regionId);
                foreach (DataRow dr in items.Rows)
                {
                    itemId = Convert.ToInt32(dr["ItemID"]);
                    itemAppId = Convert.ToInt32(dr["ItemAppID"]);
                    itemBuyDate = Convert.ToDateTime(dr["ItemBuyDate"]);
                    recommend = Convert.ToByte(dr["Recommend"]);

                    item.ItemID = itemId;
                    item.ItemAppID = itemAppId;
                    item.ItemBuyDate = itemBuyDate;
                    item.Recommend = recommend;

                    success = bll.UpdateItem(item);
                }
            }
            else
            {
                success = bll.UpdateItem(item);
            }

            ts.Complete();
        }
        
        if (success)
        {
            List.EditIndex = -1;
            BindGrid();
        }
        else
        {
            Utility.Alert(this, "修改失败！");
        }
    }

    //列表为空时快速添加消费
    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        int result = SaveItem();
        if (result == 1)
        {
            Response.Redirect("ItemList.aspx");
        }
        else
        {
            Utility.Alert(this, "添加失败！");
        }
    }
    
    //列表为空时X2添加消费
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        int result = SaveItem();
        if (result == 1)
        {
            List.EditIndex = -1;
            BindGrid();
        }
        else
        {
            Utility.Alert(this, "添加失败！");
        }
    }

    //保存方法
    protected int SaveItem()
    {
        string itemName = this.ItemNameEmpIns.Text.Trim();
        string itemType = this.ItemTypeEmpIns.SelectedValue;
        int catTypeId = Convert.ToInt32(this.CatTypeEmpIns.SelectedValue);
        string itemPrice = this.ItemPriceEmpIns.Text.Trim();
        DateTime itemBuyDate = Convert.ToDateTime(today.ToString("yyyy-MM-dd") + " " + DateTime.Now.ToString("HH:mm:ss"));
        int cardId = Convert.ToInt32(this.CardEmpIns.SelectedValue);

        if (itemName == "")
        {
            Utility.Alert(this, "商品名称未填写！");
            return 2;
        }

        if (!ValidHelper.CheckDouble(itemPrice))
        {
            Utility.Alert(this, "商品价格填写错误！");
            return 2;
        }
        
        Response.Cookies["CatTypeID"].Value = catTypeId.ToString();
        Response.Cookies["CatTypeID"].Expires = DateTime.MaxValue;

        Response.Cookies["CardID"].Value = cardId.ToString();
        Response.Cookies["CardID"].Expires = DateTime.MaxValue;

        ItemInfo item = new ItemInfo();
        item.ItemType = itemType;
        item.ItemName = itemName;
        item.CategoryTypeID = catTypeId;
        item.ItemPrice = Convert.ToDecimal(itemPrice);
        item.ItemBuyDate = itemBuyDate;
        item.UserID = userId;
        item.ModifyDate = DateTime.Now;
        item.Recommend = 0;
        item.RegionID = 0;
        item.RegionType = "";
        item.Synchronize = 1;
        item.CardID = cardId;

        bool success = bll.InsertItem(item); 
        if (success)
        {
            Session["TodayDate"] = itemBuyDate.ToString("yyyy-MM-dd"); 
            return 1;
        }
        else
        {
            return 0;
        }
    }

    //点击CheckBox统计总价
    protected void ItemCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        double ShouRuPrice = Convert.ToDouble(this.Label2.Text);
        double ZhiChuPrice = Convert.ToDouble(this.Label4.Text);
        double ShouRuPriceMonth = Convert.ToDouble(this.Label1.Text);
        double ZhiChuPriceMonth = Convert.ToDouble(this.Label3.Text);
        double ShouRuPriceYear = Convert.ToDouble(this.Label11.Text);
        double ZhiChuPriceYear = Convert.ToDouble(this.Label12.Text);

        double JieChuPrice = Convert.ToDouble(this.Label5.Text);
        double HuanRuPrice = Convert.ToDouble(this.Label6.Text);
        double JieRuPrice = Convert.ToDouble(this.Label7.Text);
        double HuanChuPrice = Convert.ToDouble(this.Label8.Text);

        try
        {
            CheckBox cb = (CheckBox)sender;
            Label price = (Label)cb.Parent.FindControl("ItemPriceLab");
            Label type = (Label)cb.Parent.FindControl("ItemTypeLab");
            if (cb.Checked)
            {
                switch (type.Text)
                {
                    case "收入":
                        ShouRuPrice += Convert.ToDouble(price.Text);
                        ShouRuPriceMonth += Convert.ToDouble(price.Text);
                        ShouRuPriceYear += Convert.ToDouble(price.Text);
                        break;
                    case "支出":
                        ZhiChuPrice += Convert.ToDouble(price.Text);
                        ZhiChuPriceMonth += Convert.ToDouble(price.Text);
                        ZhiChuPriceYear += Convert.ToDouble(price.Text);
                        break;
                    case "借出":
                        JieChuPrice += Convert.ToDouble(price.Text);
                        break;
                    case "还入":
                        HuanRuPrice += Convert.ToDouble(price.Text);
                        break;
                    case "借入":
                        JieRuPrice += Convert.ToDouble(price.Text);
                        break;
                    case "还出":
                        HuanChuPrice += Convert.ToDouble(price.Text);
                        break;
                }
            }
            else
            {
                switch (type.Text)
                {
                    case "收入":
                        ShouRuPrice -= Convert.ToDouble(price.Text);
                        ShouRuPriceMonth -= Convert.ToDouble(price.Text);
                        ShouRuPriceYear -= Convert.ToDouble(price.Text);
                        break;
                    case "支出":
                        ZhiChuPrice -= Convert.ToDouble(price.Text);
                        ZhiChuPriceMonth -= Convert.ToDouble(price.Text);
                        ZhiChuPriceYear -= Convert.ToDouble(price.Text);
                        break;
                    case "借出":
                        JieChuPrice -= Convert.ToDouble(price.Text);
                        break;
                    case "还入":
                        HuanRuPrice -= Convert.ToDouble(price.Text);
                        break;
                    case "借入":
                        JieRuPrice -= Convert.ToDouble(price.Text);
                        break;
                    case "还出":
                        HuanChuPrice -= Convert.ToDouble(price.Text);
                        break;
                }
            }
        }
        catch 
        { 
        }

        this.Label2.Text = ShouRuPrice.ToString("0.0##");
        this.Label4.Text = ZhiChuPrice.ToString("0.0##");
        this.Label1.Text = ShouRuPriceMonth.ToString("0.0##");
        this.Label3.Text = ZhiChuPriceMonth.ToString("0.0##");
        this.Label11.Text = ShouRuPriceYear.ToString("0.0##");
        this.Label12.Text = ZhiChuPriceYear.ToString("0.0##");

        this.Label5.Text = JieChuPrice.ToString("0.##");
        this.Label6.Text = HuanRuPrice.ToString("0.##");
        this.Label7.Text = JieRuPrice.ToString("0.##");
        this.Label8.Text = HuanChuPrice.ToString("0.##");
        this.Label9.Text = (JieChuPrice - HuanRuPrice).ToString("0.##");
        this.Label10.Text = (JieRuPrice - HuanChuPrice).ToString("0.##");
    }

    //点击推荐CheckBox
    protected void RecommendBox_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox cb = (CheckBox)sender;
        int itemId = Convert.ToInt32(((HiddenField)cb.Parent.FindControl("ItemIDHid")).Value);

        ItemInfo item = bll.GetItemByItemId(itemId);
        item.Synchronize = 1;
        item.ModifyDate = DateTime.Now;

        if (cb.Checked)
        {
            item.Recommend = 1;
        }
        else
        {
            item.Recommend = 0;
        }

        bool success = bll.UpdateItem(item);
        if (!success)
        {
            Utility.Alert(this, "更新失败！");
        }
    }

    //修改专题
    protected void SubmitButtom_Click(object sender, EventArgs e)
    {
        int itemId = Convert.ToInt32(this.ItemIDHid.Value);
        int ztId = Convert.ToInt32(this.ZhuanTiDown.SelectedValue);

        ItemInfo item = bll.GetItemByItemId(itemId);
        item.ZhuanTiID = ztId;
        item.Synchronize = 1;
        item.ModifyDate = DateTime.Now;

        bool success = bll.UpdateItem(item);        
        if (success)
        {
            List.EditIndex = -1;
            BindGrid();
        }
        else
        {
            Utility.Alert(this, "更新失败！");
        }
    }
}
