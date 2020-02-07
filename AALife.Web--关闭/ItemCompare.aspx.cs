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

public partial class ItemCompare : WebPage
{
    private ItemTableBLL bll = new ItemTableBLL();
    private UserQueryTableBLL query_bll = new UserQueryTableBLL();
    private DataTable lists = new DataTable();

    private int userId = 0;
    protected string curDate = "";
    protected string curDate2 = "";
    protected string showType = "";
    protected string groupType = "";
    protected string subGroup = "";
    protected string keywords = "";
    protected DateTime beginDate = DateTime.Now.Date;
    protected DateTime endDate = DateTime.Now.Date;
    protected DateTime beginDate2 = DateTime.Now.Date;
    protected DateTime endDate2 = DateTime.Now.Date;
    protected string sort = "";
    protected string by = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        userId = Convert.ToInt32(Session["UserID"]);
        curDate = Utility.GetRequestDate(Request.QueryString["date"]).ToString("yyyy-MM-dd");
        curDate2 = Utility.GetRequestDate2(Request.QueryString["date2"]).ToString("yyyy-MM-dd");
        showType = Request.QueryString["showType"] ?? "m";
        groupType = Request.QueryString["groupType"] ?? "CategoryTypeName";
        subGroup = Request.QueryString["subGroup"] ?? "";
        keywords = Request.QueryString["keywords"] ?? "";
        sort = Request.QueryString["sort"] ?? "CountNumCur";
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
        //this.ShowTypeDropDown.Items.Add(new ListItem("全部", "a"));
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
        DateTime now2 = Convert.ToDateTime(curDate2).Date;

        switch (showType)
        {
            case "d":
                beginDate = DateHelper.GetCurDate(now);
                endDate = DateHelper.GetCurDate(now);
                beginDate2 = DateHelper.GetCurDate(now2);
                endDate2 = DateHelper.GetCurDate(now2);
                break;
            case "w":
                beginDate = DateHelper.GetWeekFirst(now);
                endDate = DateHelper.GetWeekLast(now);
                beginDate2 = DateHelper.GetWeekFirst(now2);
                endDate2 = DateHelper.GetWeekLast(now2);
                break;
            case "m":
                beginDate = DateHelper.GetMonthFirst(now);
                endDate = DateHelper.GetMonthLast(now);
                beginDate2 = DateHelper.GetMonthFirst(now2);
                endDate2 = DateHelper.GetMonthLast(now2);
                break;
            case "y":
                beginDate = DateHelper.GetYearFirst(now);
                endDate = DateHelper.GetYearLast(now);
                beginDate2 = DateHelper.GetYearFirst(now2);
                endDate2 = DateHelper.GetYearLast(now2);
                break;
        }

        string query = "1=1";
        if (keywords != "")
        {
            query = "ItemName like '%" + keywords + "%'";
        }

        DataTable newlists = new DataTable();
        lists = bll.GetItemListByCompare(userId, beginDate, endDate, beginDate2, endDate2, query, groupType, "asc");
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
        decimal zhiCurTotal = 0m;
        decimal shouCurTotal = 0m;
        int countCurTotal = 0;

        decimal zhiPrevTotal = 0m;
        decimal shouPrevTotal = 0m;
        int countPrevTotal = 0;

        foreach (DataRow row in dt.Rows)
        {
            decimal shouPriceCur = Convert.IsDBNull(row["ShouRuPriceCur"]) ? 0 : Convert.ToDecimal(row["ShouRuPriceCur"]);
            decimal zhiPriceCur = Convert.IsDBNull(row["ZhiChuPriceCur"]) ? 0 : Convert.ToDecimal(row["ZhiChuPriceCur"]);
            int countNumCur = Convert.IsDBNull(row["CountNumCur"]) ? 0 : Convert.ToInt32(row["CountNumCur"]);

            decimal shouPricePrev = Convert.IsDBNull(row["ShouRuPricePrev"]) ? 0 : Convert.ToDecimal(row["ShouRuPricePrev"]);
            decimal zhiPricePrev = Convert.IsDBNull(row["ZhiChuPricePrev"]) ? 0 : Convert.ToDecimal(row["ZhiChuPricePrev"]);
            int countNumPrev = Convert.IsDBNull(row["CountNumPrev"]) ? 0 : Convert.ToInt32(row["CountNumPrev"]);

            zhiCurTotal += zhiPriceCur;
            shouCurTotal += shouPriceCur;
            countCurTotal += countNumCur;

            zhiPrevTotal += zhiPricePrev;
            shouPrevTotal += shouPricePrev;
            countPrevTotal += countNumPrev;
        }

        this.ZhiCurTotalLab.Text = "- " + zhiCurTotal.ToString("0.0##");
        this.ShouCurTotalLab.Text = "+ " + shouCurTotal.ToString("0.0##");
        this.CountNumCurTotalLab.Text = countCurTotal.ToString();

        this.ZhiPrevTotalLab.Text = "- " + zhiPrevTotal.ToString("0.0##");
        this.ShouPrevTotalLab.Text = "+ " + shouPrevTotal.ToString("0.0##");
        this.CountNumPrevTotalLab.Text = countPrevTotal.ToString();

        this.JieZhiChuTotalLab.Text = (zhiCurTotal - zhiPrevTotal).ToString("0.0##");
        this.JieShouRuTotalLab.Text = (shouCurTotal - shouPrevTotal).ToString("0.0##");
        this.JieCountNumTotalLab.Text = (countCurTotal - countPrevTotal).ToString();

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
            this.titleHid.Value = "消费比较";
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

    //取日期标签
    protected string GetDateLabel(string date, string showType)
    {
        DateTime now = Convert.ToDateTime(date).Date;
        string result = "";

        switch (showType)
        {
            case "d":
                result = now.ToString("dd") + "日";
                break;
            case "w":
                result = DateHelper.GetWeekNumOfYear(now) + "周";
                break;
            case "m":
                result = now.ToString("MM") + "月";
                break;
            case "y":
                result = now.ToString("yy") + "年";
                break;
        }

        return result;
    }

}