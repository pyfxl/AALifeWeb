using AALife.BLL;
using AALife.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ItemGroup : WebPage
{
    private ItemTableBLL bll = new ItemTableBLL();
    private UserQueryTableBLL query_bll = new UserQueryTableBLL();
    private DataTable lists = new DataTable();

    private int userId = 0;
    protected string curDate = "";
    protected string showType = "";
    protected string groupType = "";
    protected string subGroup = "";
    protected string keywords = "";
    protected DateTime beginDate = DateTime.Now.Date;
    protected DateTime endDate = DateTime.Now.Date;
    protected string sort = "";
    protected string by = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        userId = Convert.ToInt32(Session["UserID"]);
        curDate = Utility.GetRequestDate(Request.QueryString["date"]).ToString("yyyy-MM-dd");
        showType = Request.QueryString["showType"] ?? "m";
        groupType = Request.QueryString["groupType"] ?? "CategoryTypeName";
        subGroup = Request.QueryString["subGroup"] ?? "";
        keywords = Request.QueryString["keywords"] ?? "";
        sort = Request.QueryString["sort"] ?? "CountNum";
        by = Request.QueryString["by"] ?? "desc";
        this.sortHid.Value = (by == "desc" ? "asc" : "desc");

        if (!IsPostBack)
        {
            BindShowTypeDropDown();

            BindGroupTypeDropDown();

            BindSubGroupDropDown();
            
            BindItemGrid();
            
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

    //分组类别下拉
    private void BindGroupTypeDropDown()
    {
        this.GroupTypeDropDown.Items.Add(new ListItem("按商品类别分组", "CategoryTypeName"));
        this.GroupTypeDropDown.Items.Add(new ListItem("按分类分组", "ItemTypeName"));
        this.GroupTypeDropDown.Items.Add(new ListItem("按商品名称分组", "ItemName"));
        this.GroupTypeDropDown.Items.Add(new ListItem("按专题分组", "ZhuanTiName"));
        this.GroupTypeDropDown.Items.Add(new ListItem("按钱包分组", "CardName"));
        this.GroupTypeDropDown.Items.Add(new ListItem("按日期分组", "ItemBuyDate"));

        if (groupType != "")
        {
            this.GroupTypeDropDown.Items.FindByValue(groupType).Selected = true;
        }
    }

    //子分组类别下拉
    private void BindSubGroupDropDown()
    {
        this.SubGroupDropDown.Items.Add(new ListItem("请选择子分组", ""));
        this.SubGroupDropDown.Items.Add(new ListItem("按商品类别分子组", "CategoryTypeName"));
        this.SubGroupDropDown.Items.Add(new ListItem("按分类分子组", "ItemTypeName"));
        this.SubGroupDropDown.Items.Add(new ListItem("按商品名称分子组", "ItemName"));
        this.SubGroupDropDown.Items.Add(new ListItem("按专题分子组", "ZhuanTiName"));
        this.SubGroupDropDown.Items.Add(new ListItem("按钱包分子组", "CardName"));
        this.SubGroupDropDown.Items.Add(new ListItem("按日期分子组", "ItemBuyDate"));

        if (subGroup != "")
        {
            this.SubGroupDropDown.Items.FindByValue(subGroup).Selected = true;
        }
    }

    //初始列表
    private void BindItemGrid()
    {
        DateTime now = Convert.ToDateTime(curDate).Date;

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

        string query = "1=1";
        if (keywords != "")
        {
            query = "ItemName like '%" + keywords + "%'";
        }

        DataTable newlists = new DataTable();
        lists = bll.GetItemListByGroup(userId, beginDate, endDate, query, groupType, "asc");
        if (lists.Rows.Count > 0)
        {
            lists.DefaultView.Sort = string.Format("{0} {1}", sort, by);
        }

        this.GroupList.DataSource = lists;
        this.GroupList.DataBind();

        //更新总价
        UpdateTotal(lists);
    }

    //更新总价
    private void UpdateTotal(DataTable dt)
    {
        decimal zhiTotal = 0m;
        decimal shouTotal = 0m;
        decimal jiechuTotal = 0m;
        decimal huanruTotal = 0m;
        decimal huanchuTotal = 0m;
        decimal jieruTotal = 0m;
        int countTotal = 0;

        foreach (DataRow row in dt.Rows)
        {
            decimal shouPrice = Convert.IsDBNull(row["ShouRuPrice"]) ? 0 : Convert.ToDecimal(row["ShouRuPrice"]);
            decimal zhiPrice = Convert.IsDBNull(row["ZhiChuPrice"]) ? 0 : Convert.ToDecimal(row["ZhiChuPrice"]);
            decimal jiechuPrice = Convert.IsDBNull(row["JieChuPrice"]) ? 0 : Convert.ToDecimal(row["JieChuPrice"]);
            decimal huanruPrice = Convert.IsDBNull(row["HuanRuPrice"]) ? 0 : Convert.ToDecimal(row["HuanRuPrice"]);
            decimal huanchuPrice = Convert.IsDBNull(row["HuanChuPrice"]) ? 0 : Convert.ToDecimal(row["HuanChuPrice"]);
            decimal jieruPrice = Convert.IsDBNull(row["JieRuPrice"]) ? 0 : Convert.ToDecimal(row["JieRuPrice"]);
            int countNum = Convert.ToInt32(row["CountNum"]);

            zhiTotal += zhiPrice;
            jiechuTotal += jiechuPrice;
            huanchuTotal += huanchuPrice;

            shouTotal += shouPrice;
            jieruTotal += jieruPrice;
            huanruTotal += huanruPrice;

            countTotal += countNum;
        }

        this.ZhiTotalLab.Text = "- " + zhiTotal.ToString("0.0##");
        this.ShouTotalLab.Text = "+ " + shouTotal.ToString("0.0##");
        this.JieChuTotalLab.Text = jiechuTotal.ToString("0.###");
        this.HuanRuTotalLab.Text = huanruTotal.ToString("0.###");
        this.HuanChuTotalLab.Text = huanchuTotal.ToString("0.###");
        this.JieRuTotalLab.Text = jieruTotal.ToString("0.###");
        this.CountNumTotalLab.Text = countTotal.ToString();

        this.JieCunTotalLab.Text = (shouTotal - zhiTotal).ToString("0.0##");
        this.WeiHuanTotalLab.Text = (jieruTotal - huanchuTotal).ToString("0.###");
        this.QianHuanTotalLab.Text = (huanruTotal - jiechuTotal).ToString("0.###");

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
            this.titleHid.Value = "消费统计";
        }

        this.QueryTitle.Text = this.titleHid.Value;
    }

    //行数据绑定
    protected void GroupList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string style = "";
            if (e.Row.RowIndex % 2 == 0)
            {
                style = "trcolor1";
            }
            else
            {
                style = "trcolor2";
            }
            e.Row.CssClass = style;
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

}