using AALife.BLL;
using AALife.Model;
using System;
using System.Data;
using System.Web;

public partial class AALifeWeb_SyncUserInfo : SyncBase
{
    private UserTableBLL bll = new UserTableBLL();
    private CardTableBLL card_bll = new CardTableBLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        int userId = Convert.ToInt32(Request.Form["userid"]);
        string userFrom = Request.Form["userfrom"].ToString();
        string userMoney = Request.Form["usermoney"] ?? "";
        string userWorkDay = Request.Form["userworkday"] ?? "";
        string categoryRate = Request.Form["categoryrate"] ?? "";
        string isUpdate = Request.Form["isupdate"] ?? "0";

        if (userFrom.Length > 5)
        {
            userFrom = userFrom.Replace("_", "");
            userFrom = userFrom.Substring(5);
        }

        bool success = false;
        if (userId > 0)
        {
            UserInfo user = bll.GetUserByUserId(userId);
            user.UserFrom = userFrom;
            user.ModifyDate = DateTime.Now;
            user.Synchronize = 0;
            if (userMoney != "") user.UserMoney = Convert.ToDecimal(userMoney);
            if (userWorkDay != "") user.UserWorkDay = userWorkDay;
            if (categoryRate != "") user.CategoryRate = Convert.ToInt32(categoryRate);            
            if (isUpdate == "1")
            {
                user.UserMoney = 0;
                user.MoneyStart = Convert.ToDecimal(userMoney);
                user.IsUpdate = 1;
            }

            success = bll.UpdateUser(user);
        }
                
        string result = "{";

        if (success)
        {
            result += "\"result\":\"1\"";
        }
        else
        {
            result += "\"result\":\"0\"";
        }

        result += "}";

        Response.Write(result);
        Response.End();
    }
}