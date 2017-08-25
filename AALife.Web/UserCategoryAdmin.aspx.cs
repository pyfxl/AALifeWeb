using System;
using System.Web.UI.WebControls;
using System.Data;
using AALife.BLL;
using AALife.Model;
using System.Transactions;

public partial class UserCategoryAdmin : WebPage
{
    private UserCategoryTableBLL bll = new UserCategoryTableBLL();
    private ItemTableBLL item_bll = new ItemTableBLL();
    private int userId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        userId = Convert.ToInt32(Session["UserID"]);

        if (!IsPostBack)
        {
            BindGrid();
        }
    }

    //初始列表
    private void BindGrid()
    {
        int categoryRate = Convert.ToInt32(Session["CategoryRate"]);
        DataTable catTypeList = bll.GetUserCategoryList(userId, categoryRate);

        this.CatTypeList.DataSource = catTypeList;
        this.CatTypeList.DataBind();

        this.CatIdEmpIns.Text = (catTypeList.Rows.Count + 1).ToString();
    } 

    //类别更新操作
    protected void CatTypeList_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int catTypeId = Convert.ToInt32(CatTypeList.DataKeys[e.RowIndex].Value);
        string catTypeName = ((TextBox)CatTypeList.Rows[e.RowIndex].FindControl("CatTypeNameBox")).Text.Trim();
        string catTypeNameHid = ((HiddenField)CatTypeList.Rows[e.RowIndex].FindControl("CatTypeNameHid")).Value;
        string catTypePrice = ((TextBox)CatTypeList.Rows[e.RowIndex].FindControl("CatTypePriceBox")).Text.Trim();

        if (catTypeName == "")
        {
            Utility.Alert(this, "类别名称未填写！");
            return;
        }

        if (catTypePrice != "")
        {
            if (!ValidHelper.CheckNumber(catTypePrice))
            {
                Utility.Alert(this, "类别预算填写错误！");
                return;
            }
        }
        else
        {
            catTypePrice = "0";
        }

        bool success = false;
        UserCategoryInfo category = new UserCategoryInfo();

        if (catTypeName != catTypeNameHid)
        {
            category = bll.GetUserCategoryByName(userId, catTypeName);
            if (category.CategoryTypeID > 0)
            {
                Utility.Alert(this, "类别已存在，不能重复添加！");
                return;
            }
        }

        category.CategoryTypeID = catTypeId;
        category.CategoryTypeName = catTypeName;
        category.CategoryTypePrice = Convert.ToInt32(catTypePrice);
        category.UserID = userId;
        category.CategoryTypeLive = 1;
        category.Synchronize = 1;
        category.ModifyDate = DateTime.Now;

        using (TransactionScope ts = new TransactionScope())
        {
            success = bll.DeleteUserCategory(userId, catTypeId);
            success = bll.InsertUserCategory(category);
            
            ts.Complete();
        }
            
        if (success)
        {
            Utility.Alert(this, "更新成功。");

            CatTypeList.EditIndex = -1;
            BindGrid();
        }
        else
        {
            Utility.Alert(this, "更新失败！");
        }
    }

    //类别取消操作
    protected void CatTypeList_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        CatTypeList.EditIndex = -1;
        BindGrid();
    }

    //类别更新进入操作
    protected void CatTypeList_RowEditing(object sender, GridViewEditEventArgs e)
    {
        CatTypeList.EditIndex = e.NewEditIndex;
        BindGrid();
    }

    //行数据绑定
    protected void CatTypeList_RowDataBound(object sender, GridViewRowEventArgs e)
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

    //添加类别操作
    protected void Button1_Click(object sender, EventArgs e)
    {
        string catTypeName = this.CatTypeNameEmpIns.Text.Trim();
        string catTypePrice = this.CatTypePriceEmpIns.Text.Trim();

        if (catTypeName == "")
        {
            Utility.Alert(this, "类别名称未填写！");
            return;
        }

        if (catTypePrice != "")
        {
            if (!ValidHelper.CheckNumber(catTypePrice))
            {
                Utility.Alert(this, "类别预算填写错误！");
                return;
            }
        }
        else
        {
            catTypePrice = "0";
        }

        UserCategoryInfo category = bll.GetUserCategoryByName(userId, catTypeName);
        category.CategoryTypeID = bll.GetMaxCategoryTypeId(userId);
        category.CategoryTypeName = catTypeName;
        category.CategoryTypePrice = Convert.ToInt32(catTypePrice);
        category.UserID = userId;
        category.CategoryTypeLive = 1;
        category.Synchronize = 1;
        category.ModifyDate = DateTime.Now;

        if (category.UserCategoryID > 0)
        {
            Utility.Alert(this, "类别已存在，不能重复添加！");
            return;
        }

        bool success = bll.InsertUserCategory(category);
        if (success)
        {
            Utility.Alert(this, "添加成功。", "UserCategoryAdmin.aspx");
        }
        else
        {
            Utility.Alert(this, "添加失败！");
        }
    }

    //类别删除操作
    protected void CatTypeList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int catTypeId = Convert.ToInt32(CatTypeList.DataKeys[e.RowIndex].Value);
        string catTypeName = ((Label)CatTypeList.Rows[e.RowIndex].FindControl("CatTypeNameLab")).Text;
        string catTypePrice = ((Label)CatTypeList.Rows[e.RowIndex].FindControl("CatTypePriceLab")).Text;
        
        DataTable items = item_bll.GetItemListByCategoryId(userId, catTypeId);
        if (items.Rows.Count > 0)
        {
            Utility.Alert(this, "不能删除已使用的类别！");
            return;
        }

        if(this.CatTypeList.Rows.Count == 1)
        {
            Utility.Alert(this, "不能删除最后一个类别！");
            return;
        }

        UserCategoryInfo category = new UserCategoryInfo();
        category.CategoryTypeID = catTypeId;
        category.CategoryTypeName = catTypeName;
        category.CategoryTypePrice = Convert.ToInt32(catTypePrice);
        category.UserID = userId;
        category.CategoryTypeLive = 0;
        category.Synchronize = 1;
        category.ModifyDate = DateTime.Now;

        bool success = bll.DeleteUserCategory(category);
        if (success)
        {
            Utility.Alert(this, "删除成功。");

            CatTypeList.EditIndex = -1;
            BindGrid();
        }
        else
        {
            Utility.Alert(this, "删除失败！");
        }
    }

}
