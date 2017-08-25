using AALife.BLL;
using AALife.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Caching;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ItemQuery : WebPage
{
    private ItemTableBLL bll = new ItemTableBLL();
    private UserCategoryTableBLL cat_bll = new UserCategoryTableBLL();
    private CardTableBLL card_bll = new CardTableBLL();
    private ZhuanTiTableBLL zt_bll = new ZhuanTiTableBLL();
    private UserQueryTableBLL query_bll = new UserQueryTableBLL();
    private DataTable lists = new DataTable();
    private DataTable catTypeList = new DataTable();
    private DataTable itemTypeList = new DataTable();
    private DataTable cardList = new DataTable();
    private DataTable ztList = new DataTable();

    private int userId = 0;
    protected string curDate = "";
    protected string showType = "";
    protected string itemType = "";
    protected string regionType = "";
    protected string catId = "";
    protected string ztId = "";
    protected string cardId = "";
    protected string recommend = "";
    protected string keywords = "";
    protected string sort = "";
    protected string by = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        userId = Convert.ToInt32(Session["UserID"]);
        curDate = Utility.GetRequestDate(Request.QueryString["date"]).ToString("yyyy-MM-dd");
        showType = Request.QueryString["showType"] ?? "m";
        itemType = Request.QueryString["itemType"] ?? "";
        regionType = Request.QueryString["regionType"] ?? "null";
        catId = Request.QueryString["catId"] ?? "";
        ztId = Request.QueryString["ztId"] ?? "null";
        cardId = Request.QueryString["cardId"] ?? "";
        recommend = Request.QueryString["recommend"] ?? "null";
        keywords = Request.QueryString["keywords"] ?? "";
        sort = Request.QueryString["sort"] ?? "ItemBuyDate";
        by = Request.QueryString["by"] ?? "desc";
        this.sortHid.Value = (by == "desc" ? "asc" : "desc");
        
        catTypeList = CacheHelper.GetFromCache("cattypeedit", cat_bll.GetUserCategoryList(userId));
        itemTypeList = CacheHelper.GetFromCache("itemtypeedit", bll.GetItemTypeList());
        cardList = CacheHelper.GetFromCache("cardlistedit", card_bll.GetCardList(userId));
        ztList = CacheHelper.GetFromCache("ztlistedit", zt_bll.GetZhuanTiList(userId));

        if (!IsPostBack)
        {
            BindShowTypeDropDown();

            BindItemGrid();

            BindItemTypeListBox();

            BindCategoryTypeListBox();

            BindRegionTypeListBox();

            BindZhuanTiListBox();

            BindCardListBox();

            BindRecommendListBox();

            GetQueryInfo();
        }

    }

    //显示下拉
    private void BindShowTypeDropDown()
    {
        this.ShowTypeDropDown.Items.Add(new ListItem("全部", "a"));
        this.ShowTypeDropDown.Items.Add(new ListItem("本日", "d"));
        this.ShowTypeDropDown.Items.Add(new ListItem("本周", "w"));
        this.ShowTypeDropDown.Items.Add(new ListItem("本月", "m"));
        this.ShowTypeDropDown.Items.Add(new ListItem("本年", "y"));

        if (showType != "")
        {
            this.ShowTypeDropDown.Items.FindByValue(showType).Selected = true;
        }
    }
    
    //初始列表
    private void BindItemGrid()
    {
        DateTime now = Convert.ToDateTime(curDate).Date;
        DateTime beginDate = DateTime.Now.Date;
        DateTime endDate = DateTime.Now.Date;
        DateTime cachedate = DateTime.Now.Date;

        switch (showType)
        {
            case "a":
                beginDate = DateHelper.GetSqlMinDate();
                endDate = DateHelper.GetSqlMaxDate();
                break;
            case "d":
                beginDate = DateHelper.GetCurDate(now);
                endDate = DateHelper.GetCurDate(now);
                break;
            case "w":
                beginDate = DateHelper.GetWeekFirst(now);
                endDate = DateHelper.GetWeekLast(now);
                break;
            case "m":
                beginDate = DateHelper.GetMonthFirst(now);
                endDate = DateHelper.GetMonthLast(now);
                break;
            case "y":
                beginDate = DateHelper.GetYearFirst(now);
                endDate = DateHelper.GetYearLast(now);
                break;
        }

        DataTable newlists = new DataTable();
        lists = bll.GetItemList(userId, beginDate, endDate);
        if (lists.Rows.Count > 0)
        {
            DataRow[] rows = lists.Select(GetRowFilter(), string.Format("{0} {1}", sort, by));
            if (rows.Length > 0)
            {
                newlists = rows.CopyToDataTable();
            }
            //lists.DefaultView.RowFilter = GetRowFilter();
            //lists.DefaultView.Sort = string.Format("{0} {1}", sort, by);
        }

        this.ItemGrid.DataSource = newlists;
        this.ItemGrid.DataBind();

        //更新总价
        UpdateTotal(newlists);
    }

    //更新总价
    private void UpdateTotal(DataTable dt)
    {
        //DataTable dt = lists.DefaultView.ToTable();

        decimal zhiTotal = 0m;
        decimal shouTotal = 0m;
        decimal jiechuTotal = 0m;
        decimal huanruTotal = 0m;
        decimal huanchuTotal = 0m;
        decimal jieruTotal = 0m;

        int zhiCount = 0;
        int shouCount = 0;
        int jiechuCount = 0;
        int huanruCount = 0;
        int huanchuCount = 0;
        int jieruCount = 0;

        foreach (DataRow row in dt.Rows)
        {
            string itemType = row["ItemType"].ToString();
            decimal itemPrice = Convert.ToDecimal(row["ItemPrice"]);
            switch (itemType)
            {
                case "zc":
                    zhiTotal += itemPrice;
                    zhiCount++;
                    break;
                case "jc":
                    jiechuTotal += itemPrice;
                    jiechuCount++;
                    break;
                case "hc":
                    huanchuTotal += itemPrice;
                    huanchuCount++;
                    break;
                case "sr":
                    shouTotal += itemPrice;
                    shouCount++;
                    break;
                case "jr":
                    jieruTotal += itemPrice;
                    jieruCount++;
                    break;
                case "hr":
                    huanruTotal += itemPrice;
                    huanruCount++;
                    break;
            }
        }

        UpdateTotalLab(zhiTotal, shouTotal, jiechuTotal, huanruTotal, huanchuTotal, jieruTotal, zhiCount, shouCount, jiechuCount, huanruCount, huanchuCount, jieruCount);
    }

    //更新总价文本
    private void UpdateTotalLab(decimal zhiTotal, decimal shouTotal, decimal jiechuTotal, decimal huanruTotal, decimal huanchuTotal, decimal jieruTotal,
        int zhiCount, int shouCount, int jiechuCount, int huanruCount, int huanchuCount, int jieruCount)
    {
        this.ZhiTotalLab.Text = zhiTotal.ToString("0.0##");
        this.ShouTotalLab.Text = shouTotal.ToString("0.0##");
        this.JieChuTotalLab.Text = jiechuTotal.ToString("0.###");
        this.HuanRuTotalLab.Text = huanruTotal.ToString("0.###");
        this.HuanChuTotalLab.Text = huanchuTotal.ToString("0.###");
        this.JieRuTotalLab.Text = jieruTotal.ToString("0.###");

        this.JieCunTotalLab.Text = (shouTotal - zhiTotal).ToString("0.0##");
        this.WeiHuanTotalLab.Text = (jieruTotal - huanchuTotal).ToString("0.###");
        this.QianHuanTotalLab.Text = (huanruTotal - jiechuTotal).ToString("0.###");

        decimal money = (shouTotal - zhiTotal) + (jieruTotal - huanchuTotal) + (huanruTotal - jiechuTotal);
        this.MyMoney.Text = money.ToString("0.0##");
        this.MyMoney.CssClass = (money >= 0 ? "mymoney shoucolor" : "mymoney zhicolor");

        this.ZhiCountLab.Text = zhiCount.ToString();
        this.ShouCountLab.Text = shouCount.ToString();
        this.JieChuCountLab.Text = jiechuCount.ToString();
        this.HuanRuCountLab.Text = huanruCount.ToString();
        this.HuanChuCountLab.Text = huanchuCount.ToString();
        this.JieRuCountLab.Text = jieruCount.ToString();
    }

    //取列表过滤条件
    private string GetRowFilter()
    {
        string filter = "1=1";
        if (itemType != "")
        {
            filter += " and ItemType in ('" + itemType.Replace(",", "','") + "')";
        }
        if (catId != "")
        {
            filter += " and CategoryTypeID in (" + catId + ")";
        }
        if (regionType != "" && regionType != "null")
        {
            filter += " and RegionType in ('" + regionType.Replace(",", "','") + "')";
        }
        if (ztId != "" && ztId != "null")
        {
            filter += " and ZhuanTiID in (" + ztId + ")";
        }
        if (cardId != "")
        {
            filter += " and CardID in (" + cardId + ")";
        }
        if (recommend != "" && recommend != "null")
        {
            filter += " and Recommend in (" + recommend + ")";
        }
        if (keywords != "")
        {
            filter += " and ItemName like '%" + keywords + "%'";
        }

        return filter;
    }

    //分类下拉
    private void BindItemTypeListBox()
    {
        DataTable dt = new DataTable();
        if (Cache["itemtype"] != null)
        {
            dt = (DataTable)Cache["itemtype"];
        }
        else
        {
            dt = bll.GetListBoxData(userId, "ItemTypeName", "ItemType", "DESC");
            CacheHelper.AddCache("itemtype", dt, CacheItemPriority.Normal);
        }
        this.ItemTypeListBox.DataSource = dt;
        this.ItemTypeListBox.DataTextField = "ItemTypeName";
        this.ItemTypeListBox.DataValueField = "ItemType";
        this.ItemTypeListBox.DataBind();

        string[] arr = itemType.Split(',');
        foreach (ListItem item in this.ItemTypeListBox.Items)
        {
            if(itemType == "")
            {
                item.Selected = true;
            }
            foreach (string str in arr)
            {
                if (item.Value == str)
                {
                    item.Selected = true;
                }
            }
        }
    }
    
    //商品类别下拉
    private void BindCategoryTypeListBox()
    {
        DataTable dt = new DataTable();
        if (Cache["cattype"] != null)
        {
            dt = (DataTable)Cache["cattype"];
        }
        else
        {
            dt = bll.GetListBoxData(userId, "CategoryTypeName", "CategoryTypeID", "ASC");
            CacheHelper.AddCache("cattype", dt, CacheItemPriority.Normal);
        }
        this.CategoryTypeListBox.DataSource = dt;
        this.CategoryTypeListBox.DataTextField = "CategoryTypeName";
        this.CategoryTypeListBox.DataValueField = "CategoryTypeID";
        this.CategoryTypeListBox.DataBind();

        string[] arr = catId.Split(',');
        foreach (ListItem item in this.CategoryTypeListBox.Items)
        {
            if (catId == "")
            {
                item.Selected = true;
            }
            foreach (string str in arr)
            {
                if (item.Value == str)
                {
                    item.Selected = true;
                }
            }
        }
    }

    //区间下拉
    private void BindRegionTypeListBox()
    {
        DataTable dt = new DataTable();
        if (Cache["regiontype"] != null)
        {
            dt = (DataTable)Cache["regiontype"];
        }
        else
        {
            dt = bll.GetListBoxData(userId, "RegionTypeFull", "RegionType", "ASC");
            CacheHelper.AddCache("regiontype", dt, CacheItemPriority.Normal);
        }
        this.RegionTypeListBox.DataSource = dt;
        this.RegionTypeListBox.DataTextField = "RegionTypeFull";
        this.RegionTypeListBox.DataValueField = "RegionType";
        this.RegionTypeListBox.DataBind();

        string[] arr = regionType.Split(',');
        foreach (ListItem item in this.RegionTypeListBox.Items)
        {
            foreach (string str in arr)
            {
                if (item.Value == str)
                {
                    item.Selected = true;
                }
            }
        }
    }

    //专题下拉
    private void BindZhuanTiListBox()
    {
        DataTable dt = new DataTable();
        if (Cache["zttype"] != null)
        {
            dt = (DataTable)Cache["zttype"];
        }
        else
        {
            dt = bll.GetListBoxData(userId, "ZhuanTiName", "ZhuanTiID", "ASC");
            CacheHelper.AddCache("zttype", dt, CacheItemPriority.Normal);
        }
        this.ZhuanTiListBox.DataSource = dt;
        this.ZhuanTiListBox.DataTextField = "ZhuanTiName";
        this.ZhuanTiListBox.DataValueField = "ZhuanTiID";
        this.ZhuanTiListBox.DataBind();

        string[] arr = ztId.Split(',');
        foreach (ListItem item in this.ZhuanTiListBox.Items)
        {
            foreach (string str in arr)
            {
                if (item.Value == str)
                {
                    item.Selected = true;
                }
            }
        }
    }

    //钱包下拉
    private void BindCardListBox()
    {
        DataTable dt = new DataTable();
        if (Cache["cardtype"] != null)
        {
            dt = (DataTable)Cache["cardtype"];
        }
        else
        {
            dt = bll.GetListBoxData(userId, "CardName", "CardID", "ASC");
            CacheHelper.AddCache("cardtype", dt, CacheItemPriority.Normal);
        }
        this.CardListBox.DataSource = dt;
        this.CardListBox.DataTextField = "CardName";
        this.CardListBox.DataValueField = "CardID";
        this.CardListBox.DataBind();

        string[] arr = cardId.Split(',');
        foreach (ListItem item in this.CardListBox.Items)
        {
            if (cardId == "")
            {
                item.Selected = true;
            }
            foreach (string str in arr)
            {
                if (item.Value == str)
                {
                    item.Selected = true;
                }
            }
        }
    }

    //推荐下拉
    private void BindRecommendListBox()
    {
        this.RecommendListBox.Items.Add(new ListItem("是", "1"));
        this.RecommendListBox.Items.Add(new ListItem("否", "0"));

        string[] arr = recommend.Split(',');
        foreach (ListItem item in this.RecommendListBox.Items)
        {
            foreach (string str in arr)
            {
                if (item.Value == str)
                {
                    item.Selected = true;
                }
            }
        }
    }

    //获取查询信息
    private void GetQueryInfo()
    {
        string param = Request.Url.Query;

        this.titleHid.Value = "你可以保存这个查询";

        if (param != "")
        {
            string url = Request.Url.PathAndQuery;
            string value = url.Substring(url.IndexOf('&'));
            UserQueryInfo query = query_bll.GetUserQueryByValue(userId, value);
            if (query.UserQueryID > 0)
            {
                this.urlHid.Value = query.UserQueryURL;
                this.titleHid.Value = query.UserQueryName;
                Page.Title = query.UserQueryName;
            }
        }
        else
        {
            this.titleHid.Value = "消费明细";
        }

        this.QueryTitle.Text = this.titleHid.Value;
    }

    //进入编辑操作
    protected void ItemGrid_RowEditing(object sender, GridViewEditEventArgs e)
    {
        ItemGrid.EditIndex = e.NewEditIndex;
        BindItemGrid();

        HiddenField catTypeIdHid = (HiddenField)ItemGrid.Rows[e.NewEditIndex].FindControl("CatTypeIDHid");
        DropDownList catTypeDropDown = (DropDownList)ItemGrid.Rows[e.NewEditIndex].FindControl("CatTypeDropDown");
        HiddenField itemTypeHid = (HiddenField)ItemGrid.Rows[e.NewEditIndex].FindControl("ItemTypeHid");
        DropDownList itemTypeDropDown = (DropDownList)ItemGrid.Rows[e.NewEditIndex].FindControl("ItemTypeDropDown");
        TextBox regionType = (TextBox)ItemGrid.Rows[e.NewEditIndex].FindControl("RegionTypeBox");
        TextBox itemBuyDate = (TextBox)ItemGrid.Rows[e.NewEditIndex].FindControl("ItemBuyDateBox");
        int regionId = Convert.ToInt32(((HiddenField)ItemGrid.Rows[e.NewEditIndex].FindControl("RegionIDHid")).Value);
        DropDownList cardDropDown = (DropDownList)ItemGrid.Rows[e.NewEditIndex].FindControl("CardDropDown");
        HiddenField cardIdHid = (HiddenField)ItemGrid.Rows[e.NewEditIndex].FindControl("CardIDHid");
        DropDownList zhuanTiDropDown = (DropDownList)ItemGrid.Rows[e.NewEditIndex].FindControl("ZhuanTiDropDown");
        HiddenField zhuanTiIdHid = (HiddenField)ItemGrid.Rows[e.NewEditIndex].FindControl("ZhuanTiIDHid");

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
        if (itemTypeHid.Value != "")
        {
            itemTypeDropDown.Items.FindByValue(itemTypeHid.Value).Selected = true;
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

        //专题
        zhuanTiDropDown.DataSource = ztList;
        zhuanTiDropDown.DataTextField = "ZhuanTiName";
        zhuanTiDropDown.DataValueField = "ZTID";
        zhuanTiDropDown.DataBind();
        zhuanTiDropDown.Items.Insert(0, new ListItem("", "0"));
        if (zhuanTiIdHid.Value != "")
        {
            zhuanTiDropDown.Items.FindByValue(zhuanTiIdHid.Value).Selected = true;
        }
        else
        {
            zhuanTiDropDown.SelectedIndex = 0;
        }

        //如果是区间消费，则购买日期不可编辑
        if (regionId > 0)
        {
            itemBuyDate.Attributes.Add("disabled", "disabled");
            itemBuyDate.CssClass = "celldisable";
        }

    }

    //删除操作
    protected void ItemGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int itemId = Convert.ToInt32(this.ItemGrid.DataKeys[e.RowIndex].Value);
        int regionId = Convert.ToInt32(((HiddenField)this.ItemGrid.Rows[e.RowIndex].FindControl("RegionIDHid")).Value);
        int itemAppId = Convert.ToInt32(((HiddenField)this.ItemGrid.Rows[e.RowIndex].FindControl("ItemAppIDHid")).Value);

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
            this.ItemGrid.EditIndex = -1;
            BindItemGrid();
        }
        else
        {
            //Utility.Alert(this, "删除失败！");
        }
    }

    //取消操作
    protected void ItemGrid_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        ItemGrid.EditIndex = -1;
        BindItemGrid();
    }

    //更新操作
    protected void ItemGrid_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int itemId = Convert.ToInt32(ItemGrid.DataKeys[e.RowIndex].Value);
        string itemName = ((TextBox)ItemGrid.Rows[e.RowIndex].FindControl("ItemNameBox")).Text.Trim();
        string itemType = ((DropDownList)ItemGrid.Rows[e.RowIndex].FindControl("ItemTypeDropDown")).SelectedValue.ToString();
        int catTypeId = Convert.ToInt32(((DropDownList)ItemGrid.Rows[e.RowIndex].FindControl("CatTypeDropDown")).SelectedValue);
        string itemPrice = ((TextBox)ItemGrid.Rows[e.RowIndex].FindControl("ItemPriceBox")).Text.Trim();
        DateTime itemBuyDate = Convert.ToDateTime(((TextBox)ItemGrid.Rows[e.RowIndex].FindControl("ItemBuyDateBox")).Text.Trim() + " " + DateTime.Now.ToString("HH:mm:ss"));
        int regionId = Convert.ToInt32(((HiddenField)ItemGrid.Rows[e.RowIndex].FindControl("RegionIDHid")).Value);
        int cardId = Convert.ToInt32(((DropDownList)ItemGrid.Rows[e.RowIndex].FindControl("CardDropDown")).SelectedValue);
        int ztId = Convert.ToInt32(((DropDownList)ItemGrid.Rows[e.RowIndex].FindControl("ZhuanTiDropDown")).SelectedValue);
        byte recommend = Convert.ToByte(((CheckBox)ItemGrid.Rows[e.RowIndex].FindControl("RecommendBox")).Checked);
        int itemAppId = 0;

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
        item.ZhuanTiID = ztId;
        item.ModifyDate = DateTime.Now;
        item.Recommend = recommend;

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
            ItemGrid.EditIndex = -1;
            BindItemGrid();
        }
        else
        {
            Utility.Alert(this, "修改失败！");
        }
    }

    //点击CheckBox统计总价
    protected void ItemCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        decimal zhiTotal = Convert.ToDecimal(this.ZhiTotalLab.Text);
        decimal shouTotal = Convert.ToDecimal(this.ShouTotalLab.Text);
        decimal jiechuTotal = Convert.ToDecimal(this.JieChuTotalLab.Text);
        decimal huanruTotal = Convert.ToDecimal(this.HuanRuTotalLab.Text);
        decimal huanchuTotal = Convert.ToDecimal(this.HuanChuTotalLab.Text);
        decimal jieruTotal = Convert.ToDecimal(this.JieRuTotalLab.Text);

        int zhiCount = Convert.ToInt32(this.ZhiCountLab.Text);
        int shouCount = Convert.ToInt32(this.ShouCountLab.Text);
        int jiechuCount = Convert.ToInt32(this.JieChuCountLab.Text);
        int huanruCount = Convert.ToInt32(this.HuanRuCountLab.Text);
        int huanchuCount = Convert.ToInt32(this.HuanChuCountLab.Text);
        int jieruCount = Convert.ToInt32(this.JieRuCountLab.Text);

        try
        {
            CheckBox cb = (CheckBox)sender;
            decimal itemPrice = Convert.ToDecimal(((HiddenField)cb.Parent.FindControl("ItemPriceHid")).Value);
            string itemType = ((HiddenField)cb.Parent.FindControl("ItemTypeHid")).Value;
            if (cb.Checked)
            {
                switch (itemType)
                {
                    case "zc":
                        zhiTotal += itemPrice;
                        zhiCount++;
                        break;
                    case "jc":
                        jiechuTotal += itemPrice;
                        jiechuCount++;
                        break;
                    case "hc":
                        huanchuTotal += itemPrice;
                        huanchuCount++;
                        break;
                    case "sr":
                        shouTotal += itemPrice;
                        shouCount++;
                        break;
                    case "jr":
                        jieruTotal += itemPrice;
                        jieruCount++;
                        break;
                    case "hr":
                        huanruTotal += itemPrice;
                        huanruCount++;
                        break;
                }
            }
            else
            {
                switch (itemType)
                {
                    case "zc":
                        zhiTotal -= itemPrice;
                        zhiCount--;
                        break;
                    case "jc":
                        jiechuTotal -= itemPrice;
                        jiechuCount--;
                        break;
                    case "hc":
                        huanchuTotal -= itemPrice;
                        huanchuCount--;
                        break;
                    case "sr":
                        shouTotal -= itemPrice;
                        shouCount--;
                        break;
                    case "jr":
                        jieruTotal -= itemPrice;
                        jieruCount--;
                        break;
                    case "hr":
                        huanruTotal -= itemPrice;
                        huanruCount--;
                        break;
                }
            }
        }
        catch
        {
            throw;
        }

        UpdateTotalLab(zhiTotal, shouTotal, jiechuTotal, huanruTotal, huanchuTotal, jieruTotal, zhiCount, shouCount, jiechuCount, huanruCount, huanchuCount, jieruCount);
    }

    //行数据绑定
    protected void ItemGrid_RowDataBound(object sender, GridViewRowEventArgs e)
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

    //查询重命名
    protected void ButtonRename_Click(object sender, EventArgs e)
    {
        this.QueryTitle.Visible = false;
        this.QueryTitleBox.Visible = true;

        this.ButtonBack.Visible = true;
        this.ButtonRename.Visible = false;
    }

    //查询重命名返回
    protected void ButtonBack_Click(object sender, EventArgs e)
    {
        this.QueryTitle.Visible = true;
        this.QueryTitleBox.Visible = false;

        this.ButtonBack.Visible = false;
        this.ButtonRename.Visible = true;
    }

    //查询删除
    protected void ButtonDelete_Click(object sender, EventArgs e)
    {
        UserQueryInfo info = query_bll.GetUserQueryByName(userId, this.titleHid.Value);
        info.UserQueryLive = 0;
        info.ModifyDate = DateTime.Now;

        bool success = false;
        if (info.UserQueryID > 0)
        {
            success = query_bll.UpdateUserQuery(info);
        }

        if (success)
        {
            Utility.Alert(this, "查询删除成功。", "Default.aspx");
        }
        else
        {
            Utility.Alert(this, "查询删除失败！");
        }
    }

    //查询保存
    protected void ButtonSave_Click(object sender, EventArgs e)
    {
        if (!this.QueryTitleBox.Visible || this.QueryTitleBox.Text.Trim() == "消费明细")
        {
            Utility.Alert(this, "请重命名查询名称再保存。");
            return;
        }

        string queryURL = QueryHelper.GetQueryURL(Request.Url.PathAndQuery);
        string queryValue = queryURL.Substring(queryURL.IndexOf('&'));
        if (queryURL == "")
        {
            Utility.Alert(this, "没有要保存的查询。");
            return;
        }

        if (this.QueryTitleBox.Visible && this.QueryTitleBox.Text.Trim() == "")
        {
            Utility.Alert(this, "查询名称不能为空。");
            return;
        }

        UserQueryInfo info = query_bll.GetUserQueryByName(userId, this.QueryTitleBox.Text.Trim());
        if (info.UserQueryID <= 0)
        {
            info = query_bll.GetUserQueryByValue(userId, queryValue);
        }
        info.UserQueryName = this.QueryTitleBox.Text.Trim();
        info.UserQueryURL = queryURL;
        info.UserQueryValue = queryValue;
        info.UserQueryLive = 1;
        info.ModifyDate = DateTime.Now;
        info.UserID = userId;

        bool success = false;
        if (info.UserQueryID > 0)
        {
            success = query_bll.UpdateUserQuery(info);
        }
        else
        {
            success = query_bll.InsertUserQuery(info);
        }

        if (success)
        {
            Utility.Alert(this, "查询保存成功。", "Default.aspx");
        }
        else
        {
            Utility.Alert(this, "查询保存失败！");
        }
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

}