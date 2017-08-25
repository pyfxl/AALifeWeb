using System;
using System.Web.UI.WebControls;
using System.Data;
using AALife.BLL;
using AALife.Model;
using System.Transactions;

public partial class UserZhuanTi : WebPage
{
    private ZhuanTiTableBLL bll = new ZhuanTiTableBLL();
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
        this.List.DataSource = bll.GetZhuanTiList(userId);
        this.List.DataBind();
    }

    //更新操作
    protected void List_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int ztId = Convert.ToInt32(List.DataKeys[e.RowIndex].Value);
        string zhuanTiImage = ((HiddenField)List.Rows[e.RowIndex].FindControl("ZhuanTiImageHid")).Value;
        string zhuanTiName = ((TextBox)List.Rows[e.RowIndex].FindControl("ZhuanTiNameBox")).Text.Trim();
        string zhuanTiNameHid = ((HiddenField)List.Rows[e.RowIndex].FindControl("ZhuanTiNameHid")).Value;

        FileUpload zhuanTiImageUpload = (FileUpload)List.Rows[e.RowIndex].FindControl("ZhuanTiImageUpload");
        if (zhuanTiImageUpload.HasFile)
        {
            zhuanTiImage = SaveZhuanTiImage(zhuanTiImageUpload, zhuanTiImage, ztId);
            if (zhuanTiImage == "")
            {
                return;
            }
        }

        if (zhuanTiName == "")
        {
            Utility.Alert(this, "专题名称未填写！");
            return;
        }

        ZhuanTiInfo zhuanTi = new ZhuanTiInfo();

        if (zhuanTiName != zhuanTiNameHid)
        {
            zhuanTi = bll.GetZhuanTiByZhuanTiName(userId, zhuanTiName);
            if (zhuanTi.ZhuanTiID > 0)
            {
                Utility.Alert(this, "专题已存在，不能重复添加！");
                return;
            }
        }

        zhuanTi.ZhuanTiImage = zhuanTiImage;
        zhuanTi.ZhuanTiName = zhuanTiName;
        zhuanTi.UserID = userId;
        zhuanTi.ModifyDate = DateTime.Now;
        zhuanTi.Synchronize = 1;
        zhuanTi.ZTID = ztId;

        bool success = bll.UpdateZhuanTi(zhuanTi);
        if (success)
        {
            Utility.Alert(this, "更新成功。");

            List.EditIndex = -1;
            BindGrid();
        }
        else
        {
            Utility.Alert(this, "更新失败！");
        }
    }

    //取消操作
    protected void List_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        List.EditIndex = -1;
        BindGrid();
    }

    //更新进入操作
    protected void List_RowEditing(object sender, GridViewEditEventArgs e)
    {
        List.EditIndex = e.NewEditIndex;
        BindGrid();
    }

    //行数据绑定
    protected void List_RowDataBound(object sender, GridViewRowEventArgs e)
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

    //添加操作
    protected void Button1_Click(object sender, EventArgs e)
    {
        int ztId = bll.GetMaxZhuanTiId(userId);
        string zhuanTiImage = "";
        string zhuanTiName = this.ZhuanTiNameIns.Text.Trim();

        if (this.ZhuanTiImageIns.HasFile)
        {
            zhuanTiImage = SaveZhuanTiImage(this.ZhuanTiImageIns, zhuanTiImage, ztId);
            if (zhuanTiImage == "")
            {
                return;
            }
        }
        else
        {
            Utility.Alert(this, "专题图片未选择！");
            return;
        }

        if (zhuanTiName == "")
        {
            Utility.Alert(this, "专题名称未填写！");
            return;
        }

        ZhuanTiInfo zhuanTi = bll.GetZhuanTiByZhuanTiName(userId, zhuanTiName);
        zhuanTi.ZhuanTiName = zhuanTiName;
        zhuanTi.ZhuanTiImage = zhuanTiImage;
        zhuanTi.ZhuanTiLive = 1;
        zhuanTi.Synchronize = 1;
        zhuanTi.UserID = userId;
        zhuanTi.ModifyDate = DateTime.Now;
        zhuanTi.ZTID = ztId;

        if (zhuanTi.ZhuanTiID > 0)
        {
            Utility.Alert(this, "专题已存在，不能重复添加！");
            return;
        }

        bool success = bll.InsertZhuanTi(zhuanTi);
        if (success)
        {
            Utility.Alert(this, "添加成功。", "UserZhuanTi.aspx");
        }
        else
        {
            Utility.Alert(this, "添加失败！");
        }
    }

    //删除操作
    protected void List_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int ztId = Convert.ToInt32(List.DataKeys[e.RowIndex].Value);
        string zhuanTiImage = ((HiddenField)List.Rows[e.RowIndex].FindControl("ZhuanTiImageHid")).Value;

        ZhuanTiInfo zhuanTi = bll.GetZhuanTiByZhuanTiId(userId, ztId);
        zhuanTi.ZhuanTiLive = 0;
        zhuanTi.Synchronize = 1;
        zhuanTi.ModifyDate = DateTime.Now;

        using (TransactionScope ts = new TransactionScope())
        {
            bool success = false;

            DataTable lists = item_bll.GetItemListAllByZhuanTiId(userId, ztId);
            if (lists.Rows.Count > 0)
            {
                foreach (DataRow dr in lists.Rows)
                {
                    int itemId = Convert.ToInt32(dr["ItemID"]);
                    int itemAppId = Convert.ToInt32(dr["ItemAppID"]);

                    success = item_bll.DeleteItem(userId, itemId, itemAppId);
                    if (!success)
                    {
                        break;
                    }
                }
            }
            else
            {
                success = true;
            }

            if (success)
            {
                success = bll.UpdateZhuanTi(zhuanTi);
            }
            else 
            { 
                Utility.Alert(this, "消费删除失败！");
                return;
            }
            
            if (success)
            {
                Utility.Alert(this, "删除成功。");

                List.EditIndex = -1;
                BindGrid();
            }
            else
            {
                Utility.Alert(this, "专题删除失败！");
            }

            ts.Complete();
        }

        //删除专题图片
        if (ImageHelper.CanDelete(zhuanTiImage))
        {
            try
            {
                ImageHelper.DeleteZhuanTiImage(zhuanTiImage);
            }
            catch { }
        }
    }

    //保存图片
    private string SaveZhuanTiImage(FileUpload file, string zhuanTiImage, int ztId)
    {
        string fileName = file.FileName;
        if (!ImageHelper.CanUpload(file.FileName))
        {
            Utility.Alert(this, "图片文件格式不支持！");
            return "";
        }
        zhuanTiImage = ImageHelper.GetZhuanTiImageName(userId, ztId, fileName);
        string imagePath = ImageHelper.GetZhuanTiImagePath(zhuanTiImage);
        try
        {
            file.SaveAs(imagePath);
            ImageHelper.SaveImage(imagePath, 200, 200);
        }
        catch
        {
            Utility.Alert(this, "专题图片上传失败！");
            return "";
        }

        return zhuanTiImage;
    }

}
