using AALife.EF.BLL;
using AALife.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class api_UserTable_GetUserTable : BaseApi
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            //UserTableBLL bll = new UserTableBLL();
            //List<UserTable> userTable = bll.GetUserTable().ToList();
            //string json = Newtonsoft.Json.JsonConvert.SerializeObject(userTable);

            //GZipEncodePage();
            //Response.Write(json);
            //Response.End();
        }

    }

    [WebMethod]
    public static string CallMe()
    {
        return "You called me on " + DateTime.Now.ToString();
    }

    [WebMethod]
    public static string GetUserTable()
    {
        UserTableBLL bll = new UserTableBLL();
        List<UserTable> userTable = bll.GetUserTable().ToList();
        return Newtonsoft.Json.JsonConvert.SerializeObject(userTable);
    }
}