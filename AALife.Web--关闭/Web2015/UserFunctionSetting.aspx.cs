using System;
using AALife.BLL;
using AALife.Model;

public partial class UserFunctionSetting : BasePage
{
    private UserTableBLL bll = new UserTableBLL();
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
        string[] arr = Session["UserFunction"].ToString().Split(',');
        for (int i = 0; i < arr.Length; i++)
        {
            switch (arr[i])
            {
                case "1":
                    this.FenLeiZongJi.Checked = true;
                    break;
                case "2":
                    this.ItemNumTop.Checked = true;
                    break;
                case "3":
                    this.ItemPriceTop.Checked = true;
                    break;
                case "4":
                    this.ItemDateTop.Checked = true;
                    break;
                case "5":
                    this.QuJianTongJi.Checked = true;
                    break;
                case "6":
                    this.TuiJianFenXi.Checked = true;
                    break;
                case "7":
                    this.BiJiaoFenXi.Checked = true;
                    break;
                case "8":
                    this.JianGeFenXi.Checked = true;
                    break;
                case "9":
                    this.TianShuFenXi.Checked = true;
                    break;
                case "10":
                    this.JiaGeFenXi.Checked = true;
                    break;
                case "11":
                    this.JieHuanFenXi.Checked = true;
                    break;
                case "12":
                    this.QuWeiTongJi.Checked = true;
                    break;
                case "13":
                    this.UserAdmin.Checked = true;
                    break;
                case "14":
                    this.UserBoundAdmin.Checked = true;
                    break;
                case "15":
                    this.UserCategoryAdmin.Checked = true;
                    break;
                case "16":
                    this.UserDataAdmin.Checked = true;
                    break;
                case "17":
                    this.UserFunction.Checked = true;
                    break;
                case "18":
                    this.SearchItem.Checked = true;
                    break;
                case "19":
                    this.UserLogout.Checked = true;
                    break;
                case "20":
                    this.UserZhuanTi.Checked = true;
                    break;
                case "21":
                    this.AboutUs.Checked = true;
                    break;
                case "22":
                    this.UserCardAdmin.Checked = true;
                    break;
            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    { 
        string value = "";

        if (this.FenLeiZongJi.Checked) value += "1,";
        if (this.ItemNumTop.Checked) value += "2,";
        if (this.ItemPriceTop.Checked) value += "3,";
        if (this.ItemDateTop.Checked) value += "4,";
        if (this.QuJianTongJi.Checked) value += "5,";
        if (this.TuiJianFenXi.Checked) value += "6,";

        if (this.BiJiaoFenXi.Checked) value += "7,";
        if (this.JianGeFenXi.Checked) value += "8,";
        if (this.TianShuFenXi.Checked) value += "9,";
        if (this.JiaGeFenXi.Checked) value += "10,";
        if (this.JieHuanFenXi.Checked) value += "11,";
        if (this.QuWeiTongJi.Checked) value += "12,";

        if (this.UserAdmin.Checked) value += "13,";
        if (this.UserBoundAdmin.Checked) value += "14,";
        if (this.UserDataAdmin.Checked) value += "16,";
        if (this.UserFunction.Checked) value += "17,";
        if (this.UserCardAdmin.Checked) value += "22,";

        if (this.UserCategoryAdmin.Checked) value += "15,";
        if (this.SearchItem.Checked) value += "18,";
        if (this.UserLogout.Checked) value += "19,";
        if (this.UserZhuanTi.Checked) value += "20,";
        if (this.AboutUs.Checked) value += "21,";

        if (value != "")
        {
            value = value.Remove(value.Length - 1);

            string[] arr = value.Split(',');
            if (arr.Length > 4)
            {
                Utility.Alert(this, "选择数量不能大于4个。");
                return;
            }
        }

        UserInfo user = bll.GetUserByUserId(userId);
        user.Synchronize = 1;
        user.UserFunction = value;
        user.ModifyDate = DateTime.Now;

        bool success = bll.UpdateUser(user);
        if (success)
        {
            Session["UserFunction"] = value;
            Utility.Alert(this, "修改成功。", "Default.aspx");
        }
        else
        {
            Utility.Alert(this, "修改失败！");
        }
    }
}
