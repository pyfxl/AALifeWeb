using System;
using System.Web.UI.WebControls;
using System.Data;
using AALife.BLL;
using System.Collections.Generic;
using AALife.Model;
using System.Data.SqlTypes;

public partial class AdminItemList : AdminPage
{
    private ItemTableBLL bll = new ItemTableBLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            DateTime beginDate = DateTime.Now.Date;
            DateTime endDate = beginDate.AddDays(1).Date;
            DataTable lists = bll.GetItemListByDate(beginDate, endDate);
            this.List.DataSource = lists;
            this.List.DataBind();
            this.Label1.Text = "记录：" + lists.Rows.Count;
        }
    }

    private void BindGrid()
    {
        DateTime beginDate = SqlDateTime.MinValue.Value;
        DateTime endDate = SqlDateTime.MaxValue.Value;
        DataTable lists = bll.GetItemListByDate(beginDate, endDate);
        this.List.DataSource = lists;
        this.List.DataBind();
        this.Label1.Text = "记录：" + lists.Rows.Count;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        this.List.AllowPaging = true;
        this.List.PageSize = 36;
        BindGrid();
    }

    protected void List_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.List.PageIndex = e.NewPageIndex;
        BindGrid();
    }

    protected void Button4_Click(object sender, EventArgs e)
    {
        string keywords = this.KeyBox.Text.Trim().Replace("%", "");
        if (keywords == "")
        {
            Utility.Alert(this, "关键字不能为空！");
            return;
        }

        DataTable lists = bll.GetItemListAllByKeywords(keywords);
        this.List.AllowPaging = false;
        this.List.PageIndex = 0;
        this.List.DataSource = lists;
        this.List.DataBind();
        this.Label1.Text = "记录：" + lists.Rows.Count;
    }
}