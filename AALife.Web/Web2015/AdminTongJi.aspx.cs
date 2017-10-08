using System;
using System.Data;
using System.Text;
using System.Web.UI.WebControls;
using System.IO;
using System.Web;
using AALife.BLL;

public partial class AdminTongJi : AdminPage
{
    private MonthBLL bll = new MonthBLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.BeginDateBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
            this.EndDateBox.Text = DateTime.Now.ToString("yyyy-MM-dd");

            TotalList.DataSource = bll.GetAdminTongJiQuanBu();
            TotalList.DataBind();

            BindGrid();
        }
    }

    private void BindGrid()
    {
        DateTime beginDate = Convert.ToDateTime(this.BeginDateBox.Text).Date;
        DateTime endDate = Convert.ToDateTime(this.EndDateBox.Text).AddDays(1).Date;

        CreateList.DataSource = bll.GetAdminTongJiXinZeng(beginDate, endDate);
        CreateList.DataBind();

        ActiveList.DataSource = bll.GetAdminTongJiHuoDong(beginDate, endDate);
        ActiveList.DataBind();
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        BindGrid();
    }    

}