using AALife.BLL;
using System;
using System.Data;

public partial class UserZhuanZhangDetail : BasePage
{
    private ZhuanZhangTableBLL bll = new ZhuanZhangTableBLL();
    private int userId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        userId = Convert.ToInt32(Session["UserID"]);

        if (!IsPostBack)
        {
            PopulateControls();
        }
    }

    //初始数据
    private void PopulateControls()
    {
        DataTable lists = bll.GetZhuanZhangList(userId);
        ItemList.DataSource = lists;
        ItemList.DataBind();
    }
}