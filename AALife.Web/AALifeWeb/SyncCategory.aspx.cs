using AALife.BLL;
using AALife.Model;
using System;
using System.Data;

public partial class AALifeWeb_SyncCategory : System.Web.UI.Page
{
    private UserCategoryTableBLL bll = new UserCategoryTableBLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        int catId = Convert.ToInt32(Request.Form["catid"]);
        string catName = Request.Form["catname"].ToString();
        string catPrice = Request.Form["catprice"] ?? "";
        byte catLive = Convert.ToByte(Request.Form["catlive"]);
        int userId = Convert.ToInt32(Request.Form["userid"]);

        UserCategoryInfo category = new UserCategoryInfo();
        category.CategoryTypeID = catId;
        category.CategoryTypeName = catName;
        if (catPrice != "") category.CategoryTypePrice = Convert.ToInt32(catPrice);
        category.UserID = userId;
        category.CategoryTypeLive = catLive;
        category.Synchronize = 0;

        bool success = bll.UserCategoryExistsWithSync(userId, catId);
        if (success)
        {
            success = bll.UpdateUserCategory(category);
        }
        else
        {
            success = bll.InsertUserCategory(category);
        }
                        
        string result = "{";
        if (success)
        {
            result += "\"result\":\"ok\"";
        }
        else
        {
            result += "\"result\":\"error\"";
        } 
        result += "}";

        Response.Write(result);
        Response.End();
    }
}