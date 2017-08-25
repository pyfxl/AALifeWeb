using AALife.BLL;
using AALife.Model;
using System;
using System.Data;
using System.Transactions;
using System.Web.UI.WebControls;

public partial class ItemAddSmart : WebPage
{
    private ItemTableBLL bll = new ItemTableBLL();
    private UserCategoryTableBLL cat_bll = new UserCategoryTableBLL();
    private ZhuanTiTableBLL zt_bll = new ZhuanTiTableBLL();
    private CardTableBLL card_bll = new CardTableBLL();
    protected int RegionCount = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int userId = Convert.ToInt32(Session["UserID"]);

            DataTable catTypeList = cat_bll.GetUserCategoryList(userId);
            this.CatTypeList.DataSource = catTypeList;
            this.CatTypeList.DataBind();

            //商品类别
            this.CategoryTypeDown.DataSource = catTypeList;
            this.CategoryTypeDown.DataTextField = "CategoryTypeName";
            this.CategoryTypeDown.DataValueField = "CategoryTypeID";
            this.CategoryTypeDown.DataBind();
            this.CategoryTypeDown.Items.Insert(0, new ListItem("请选择", "0"));

            //专题
            this.ZhuanTiDown.DataSource = zt_bll.GetZhuanTiList(userId);
            this.ZhuanTiDown.DataTextField = "ZhuanTiName";
            this.ZhuanTiDown.DataValueField = "ZTID";
            this.ZhuanTiDown.DataBind();
            this.ZhuanTiDown.Items.Insert(0, new ListItem("请选择", "0"));

            //设置只读
            this.ItemType.Attributes.Add("readonly", "readonly");
            this.CategoryTypeDown.Attributes.Add("readonly", "readonly");
            this.ItemBuyDate.Attributes.Add("readonly", "readonly");

            if (Session["TodayDate"] != null)
            {
                string today = Session["TodayDate"].ToString();
                string today2 = Convert.ToDateTime(today).AddMonths(11).ToString("yyyy-MM-dd");

                this.ItemBuyDate.Text = today + " " + ItemHelper.GetWeekString(today, 0);
                this.ItemBuyDateHid.Value = today;

                this.ItemBuyDate1.Text = today;
                this.ItemBuyDate2.Text = today2;
            }
            
            //钱包
            this.CardDown.DataSource = card_bll.GetCardList(userId);
            this.CardDown.DataTextField = "CardName";
            this.CardDown.DataValueField = "CDID";
            this.CardDown.DataBind(); 
            if (Request.Cookies["CardID"] != null)
            {
                string cardId = Request.Cookies["CardID"].Value;
                this.CardDown.SelectedValue = cardId;
            }
        }
    }

    //添加
    protected void SubmitButtom_Click(object sender, EventArgs e)
    {
        if (!CheckSave())
        {
            return;
        }

        if (this.RegionID.Checked)
        {
            string itemTypeText = this.ItemType.Text.Trim();
            DateTime itemBuyDate1 = Convert.ToDateTime(this.ItemBuyDate1.Text.Trim());
            DateTime itemBuyDate2 = Convert.ToDateTime(this.ItemBuyDate2.Text.Trim());

            ItemInfo item = new ItemInfo();
            item.ItemName = this.ItemName.Text.Trim();
            item.ItemPrice = Convert.ToDecimal(this.ItemPrice.Text.Trim());
            item.RegionType = this.RegionTypeHid.Value;

            try
            {
                bll.CheckRegion(item.RegionType, itemBuyDate1, itemBuyDate2);
            }
            catch(Exception ex)
            {
                Utility.Alert(this, ex.Message);
                return;
            }

            DataTable list  = bll.GetRegionList(item, itemTypeText, itemBuyDate1, itemBuyDate2);
            this.RegionList.DataSource = list;
            this.RegionList.DataBind();

            RegionCount = list.Rows.Count;

            ClientScript.RegisterStartupScript(this.GetType(), "show", "$(function(){$('#dialog').dialog('open')})", true);            
        }
        else
        {
            Buttom2_Click(sender, e);
        }
    }
    
    //固定消费列表添加
    protected void Buttom2_Click(object sender, EventArgs e)
    {
        int result = SaveItem();
        if (result == 1)
        {
            Utility.Confirm(this, "添加成功，是否继续添加？", "ItemAddSmart.aspx", "ItemQuery.aspx?date=&showType=d");
        }
        else
        {
            Utility.Alert(this, "添加失败！");
        }
    }

    //X2添加
    protected void Buttom1_Click(object sender, EventArgs e)
    {
        if (!CheckSave())
        {
            return;
        }

        if (this.RegionID.Checked)
        {
            Utility.Alert(this, "选择了固定消费，不能进行X2添加！");
            return;
        }

        int result = SaveItem();
        if (result == 1)
        {
            Utility.Alert(this, "添加成功。");
        }
        else
        {
            Utility.Alert(this, "添加失败！");
        }
    }

    //保存方法
    private int SaveItem()
    {
        string itemType = this.ItemTypeHid.Value;
        int catTypeId = Convert.ToInt32(this.CategoryTypeDown.SelectedValue);
        string itemName = this.ItemName.Text.Trim();
        int userId = Convert.ToInt32(Session["UserID"]);
        string itemPrice = this.ItemPrice.Text.Trim();
        int zhuanTiId = Convert.ToInt32(this.ZhuanTiDown.SelectedValue);
        int cardId = Convert.ToInt32(this.CardDown.SelectedValue);
        int regionId = 0;
        string regionType = "";
        DateTime itemBuyDate = DateTime.Now;
        string itemBuyTime = DateTime.Now.ToString("HH:mm:ss");
        
        Response.Cookies["CatTypeID"].Value = catTypeId.ToString();
        Response.Cookies["CatTypeID"].Expires = DateTime.MaxValue;

        Response.Cookies["CardID"].Value = cardId.ToString();
        Response.Cookies["CardID"].Expires = DateTime.MaxValue; 
        
        if (this.RegionID.Checked)
        {
            itemBuyDate = Convert.ToDateTime(this.ItemBuyDate1.Text.Trim() + " " + itemBuyTime);
        }
        else
        {
            itemBuyDate = Convert.ToDateTime(this.ItemBuyDateHid.Value + " " + itemBuyTime);
        }

        ItemInfo item = new ItemInfo();
        item.ItemType = itemType;
        item.ItemName = itemName;
        item.CategoryTypeID = catTypeId;
        item.ItemPrice = Convert.ToDecimal(itemPrice);
        item.ItemBuyDate = itemBuyDate;
        item.UserID = userId;
        item.Recommend = 0;
        item.ModifyDate = DateTime.Now;
        item.RegionID = regionId;
        item.RegionType = regionType;
        item.Synchronize = 1;
        item.ZhuanTiID = zhuanTiId;
        item.CardID = cardId;

        bool success = false;
        if (this.RegionID.Checked)
        {
            DateTime itemBuyDate1 = Convert.ToDateTime(this.ItemBuyDate1.Text.Trim() + " " + itemBuyTime);
            DateTime itemBuyDate2 = Convert.ToDateTime(this.ItemBuyDate2.Text.Trim() + " " + itemBuyTime);
            item.RegionType = this.RegionTypeHid.Value;

            try
            {
                success = bll.InsertItem(item, itemBuyDate1, itemBuyDate2);
            }
            catch (Exception ex)
            {
                Utility.Alert(this, ex.Message);
                return 0;
            }
        }
        else
        {
            success = bll.InsertItem(item);
        }

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

    //检查内容
    private bool CheckSave()
    {
        string itemType = this.ItemTypeHid.Value;
        int catTypeId = Convert.ToInt32(this.CategoryTypeDown.SelectedValue);
        string itemName = this.ItemName.Text.Trim();
        string itemPrice = this.ItemPrice.Text.Trim();

        if (itemType == "")
        {
            Utility.Alert(this, "消费分类填写错误！");
            return false;
        }

        if (catTypeId == 0)
        {
            Utility.Alert(this, "商品类别填写错误！");
            return false;
        }

        if (itemName == "")
        {
            Utility.Alert(this, "商品名称填写错误！");
            return false;
        }

        if (!ValidHelper.CheckDouble(itemPrice))
        {
            Utility.Alert(this, "商品价格填写错误！");
            return false;
        }

        if (this.RegionID.Checked)
        {
            if (!ValidHelper.CheckDate(this.ItemBuyDate1.Text.Trim()) || !ValidHelper.CheckDate(this.ItemBuyDate2.Text.Trim()))
            {
                Utility.Alert(this, "购买日期填写错误！");
                return false;
            }
        }
        else
        {
            if (!ValidHelper.CheckDate(this.ItemBuyDateHid.Value))
            {
                Utility.Alert(this, "购买日期填写错误！");
                return false;
            }
        }

        return true;
    }
}
