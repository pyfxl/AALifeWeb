using System;
using System.Web.UI.WebControls;
using System.Data;
using AALife.BLL;
using AALife.Model;

public partial class UserZhuanTiShow : BasePage
{
    private ItemTableBLL item_bll = new ItemTableBLL();
    private ZhuanTiTableBLL bll = new ZhuanTiTableBLL();
    private UserCategoryTableBLL cat_bll = new UserCategoryTableBLL();
    private CardTableBLL card_bll = new CardTableBLL();
    private int userId = 0;
    private int ztId = 0;
    private DataTable catTypeList = new DataTable();
    private DataTable itemTypeList = new DataTable();
    private DataTable cardList = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        userId = Convert.ToInt32(Session["UserID"]);
        ztId = Convert.ToInt32(Request.QueryString["ztid"]);
        catTypeList = cat_bll.GetUserCategoryList(userId);
        itemTypeList = item_bll.GetItemTypeList();
        cardList = card_bll.GetCardList(userId);

        if (!IsPostBack)
        {
            BindGrid();

            BindInsert();

            if (Session["TodayDate"] != null)
            {
                string today = Session["TodayDate"].ToString();
                this.ItemBuyDateEmpIns.Text = today;
            }
        }
    }

    //初始列表
    private void BindGrid()
    {
        DataTable dt = item_bll.GetItemListByZhuanTiId(userId, ztId);
        List.DataSource = dt;
        List.DataBind();

        DataTable zhuanTi = bll.GetZhuanTiDataTableByZhuanTiId(userId, ztId);
        this.ZhuanTiDate.Text = zhuanTi.Rows[0]["ZhuanTiDate"].ToString();
        this.ZhuanTiShouRu.Text = Convert.ToDouble(zhuanTi.Rows[0]["ZhuanTiShouRu"]).ToString("0.0##");
        this.ZhuanTiZhiChu.Text = Convert.ToDouble(zhuanTi.Rows[0]["ZhuanTiZhiChu"]).ToString("0.0##");
    }

    //初始添加
    private void BindInsert()
    {
        //商品分类
        this.CatTypeEmpIns.DataSource = catTypeList;
        this.CatTypeEmpIns.DataTextField = "CategoryTypeName";
        this.CatTypeEmpIns.DataValueField = "CategoryTypeID";
        this.CatTypeEmpIns.DataBind();
        if (Request.Cookies["CatTypeID"] != null)
        {
            this.CatTypeEmpIns.SelectedValue = Request.Cookies["CatTypeID"].Value;
        }

        //类别
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

        HiddenField itemTypeIdHid = (HiddenField)List.Rows[e.NewEditIndex].FindControl("ItemTypeIDHid");
        DropDownList itemTypeDropDown = (DropDownList)List.Rows[e.NewEditIndex].FindControl("ItemTypeDropDown");
        DropDownList cardDropDown = (DropDownList)List.Rows[e.NewEditIndex].FindControl("CardDropDown");
        HiddenField cardIdHid = (HiddenField)List.Rows[e.NewEditIndex].FindControl("CardIDHid");

        //类别
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
    }

    //删除操作
    protected void List_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int itemId = Convert.ToInt32(List.DataKeys[e.RowIndex].Value);
        int itemAppId = Convert.ToInt32(((HiddenField)List.Rows[e.RowIndex].FindControl("ItemAppIDHid")).Value);

        bool success = item_bll.DeleteItem(userId, itemId, itemAppId);
        if (success)
        {
            List.EditIndex = -1;
            BindGrid();
        }
        else
        {
            Utility.Alert(this, "删除失败！");
            return;
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
        string itemPrice = ((TextBox)List.Rows[e.RowIndex].FindControl("ItemPriceBox")).Text.Trim();
        DateTime itemBuyDate = Convert.ToDateTime(((TextBox)List.Rows[e.RowIndex].FindControl("ItemBuyDateBox")).Text.Trim() + " " + DateTime.Now.ToString("HH:mm:ss"));
        int cardId = Convert.ToInt32(((DropDownList)List.Rows[e.RowIndex].FindControl("CardDropDown")).SelectedValue);

        if (itemName == "")
        {
            Utility.Alert(this, "商品名称未填写！");
            return;
        }

        ItemInfo item = item_bll.GetItemByItemId(itemId);
        item.ItemID = itemId;
        item.ItemType = itemType;
        item.ItemName = itemName;
        item.ItemPrice = Convert.ToDecimal(itemPrice);
        item.ItemBuyDate = itemBuyDate;
        item.Synchronize = 1;
        item.ModifyDate = DateTime.Now;
        item.CardID = cardId;

        bool success = item_bll.UpdateItem(item);
        if (success)
        {
            Session["TodayDate"] = itemBuyDate.ToString("yyyy-MM-dd");

            List.EditIndex = -1;
            BindGrid();
        }
    }
    
    //快速添加消费
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        string itemName = this.ItemNameEmpIns.Text.Trim();
        string itemType = this.ItemTypeEmpIns.SelectedValue;
        int catTypeId = Convert.ToInt32(this.CatTypeEmpIns.SelectedValue);
        string itemPrice = this.ItemPriceEmpIns.Text.Trim();
        DateTime itemBuyDate = Convert.ToDateTime(this.ItemBuyDateEmpIns.Text.Trim() + " " + DateTime.Now.ToString("HH:mm:ss"));
        int cardId = Convert.ToInt32(this.CardEmpIns.SelectedValue);

        if (itemName == "")
        {
            Utility.Alert(this, "商品名称未填写！");
            return;
        }

        Response.Cookies["CatTypeID"].Value = catTypeId.ToString();
        Response.Cookies["CatTypeID"].Expires = DateTime.MaxValue;

        Response.Cookies["CardID"].Value = cardId.ToString();
        Response.Cookies["CardID"].Expires = DateTime.MaxValue; 

        if (!ValidHelper.CheckDouble(itemPrice))
        {
            Utility.Alert(this, "商品价格填写错误！");
            return;
        }

        ItemInfo item = new ItemInfo();
        item.ItemType = itemType;
        item.ItemName = itemName;
        item.CategoryTypeID = catTypeId;
        item.ItemPrice = Convert.ToDecimal(itemPrice);
        item.ItemBuyDate = itemBuyDate;
        item.UserID = userId;
        item.RegionID = 0;
        item.RegionType = "";
        item.Synchronize = 1;
        item.ZhuanTiID = ztId;
        item.ModifyDate = DateTime.Now;
        item.CardID = cardId;

        bool success = item_bll.InsertItem(item);
        if (success)
        {
            Session["TodayDate"] = itemBuyDate.ToString("yyyy-MM-dd");
            this.ItemNameEmpIns.Text = "";
            this.ItemPriceEmpIns.Text = "";

            List.EditIndex = -1;
            BindGrid();
        }
        else
        {
            Utility.Alert(this, "添加失败！");
            return;
        }
    }
}
