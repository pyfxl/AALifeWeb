using AALife.EF.BLL;
using AALife.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class api_UserTable : ApiBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    [WebMethod]
    public static DataModel GetUserTable()
    {
        UserTableBLL bll = new UserTableBLL();
        List<UserTableView> userTable = bll.GetUserTable().ToList();
        DataModel data = new DataModel();
        data.List = userTable;
        data.Total = userTable.Count();
        //string json = Newtonsoft.Json.JsonConvert.SerializeObject(userTable);

        GZipEncodePage();
        return data;
    }

    public class DataModel
    {
        public List<UserTableView> List { get; set; }
        public int Total { get; set; }
    }
}