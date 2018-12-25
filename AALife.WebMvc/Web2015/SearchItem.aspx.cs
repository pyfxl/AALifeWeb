using AALife.BLL;
using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class SearchItem : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable lists = new DataTable();
            SearchItemList.DataSource = lists;
            SearchItemList.DataBind();

            //更新总价
            UpdateTotalPrice(lists);
        }
    }

    //搜索按钮
    protected void SubmitButtom_Click(object sender, EventArgs e)
    {
        int userId = Convert.ToInt32(Session["UserID"]);

        string keywords = this.Keywords.Text.Trim().Replace("%", "");
        if (keywords == "")
        {
            Utility.Alert(this, "关键字不能为空！");
            return;
        }

        ItemTableBLL bll = new ItemTableBLL();
        DataTable lists = bll.GetItemListByKeywords(userId, keywords);
        SearchItemList.DataSource = lists;
        SearchItemList.DataBind();

        //更新总价
        UpdateTotalPrice(lists);
    }

    //点击CheckBox统计总价
    protected void ItemCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        double ShouRuPrice = Convert.ToDouble(this.Label2.Text);
        double ZhiChuPrice = Convert.ToDouble(this.Label3.Text);

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
                    case "还入":
                    case "借入":
                        ShouRuPrice += Convert.ToDouble(price.Text);
                        break;
                    case "支出":
                    case "借出":
                    case "还出":
                        ZhiChuPrice += Convert.ToDouble(price.Text);
                        break;
                }
            }
            else
            {
                switch (type.Text)
                {
                    case "收入":
                    case "还入":
                    case "借入":
                        ShouRuPrice -= Convert.ToDouble(price.Text);
                        break;
                    case "支出":
                    case "借出":
                    case "还出":
                        ZhiChuPrice -= Convert.ToDouble(price.Text);
                        break;
                }
            }
        }
        catch
        {
        }

        this.Label2.Text = ShouRuPrice.ToString("0.00#");
        this.Label3.Text = ZhiChuPrice.ToString("0.00#");
    }

    //更新总价
    private void UpdateTotalPrice(DataTable dt)
    {
        double ZhiChuPrice = 0d;
        double ShouRuPrice = 0d;
        foreach (DataRow dr in dt.Rows)
        {
            string itemType = dr["ItemType"].ToString();
            double itemPrice = Convert.ToDouble(dr["ItemPrice"]);
            if (itemType == "zc" || itemType == "jc" || itemType == "hc")
            {
                ZhiChuPrice += itemPrice;
            }
            else
            {
                ShouRuPrice += itemPrice;
            }
        }

        this.Label2.Text = ShouRuPrice.ToString("0.00#");
        this.Label3.Text = ZhiChuPrice.ToString("0.00#");
    }

}
