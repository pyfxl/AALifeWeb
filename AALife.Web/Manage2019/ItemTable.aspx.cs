using AALife.BLL;
using AALife.Service.EF;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Manage_ItemTable : AdminPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public string GetItemType()
    {
        AALife.Service.EF.ItemTableBLL bll = new AALife.Service.EF.ItemTableBLL();

        var results = bll.GetItemType();

        return Newtonsoft.Json.JsonConvert.SerializeObject(results.Select(a => new { text = a.ItemTypeName, value = a.ItemType }));
    }

    public string GetRegionType()
    {
        AALife.Service.EF.ItemTableBLL bll = new AALife.Service.EF.ItemTableBLL();

        var results = bll.GetRegionType();

        return Newtonsoft.Json.JsonConvert.SerializeObject(results.Select(a => new { text = a.RegionTypeName, value = a.RegionType }));
    }

    public string GetCategoryType(string userId, int categoryRate)
    {
        UserCategoryTableBLL bll = new UserCategoryTableBLL();

        var lists = bll.GetUserCategoryList(1, categoryRate);

        var results = new List<dynamic>();

        foreach (DataRow row in lists.Rows)
        {
            dynamic model = new ExpandoObject();
            model.value = row["CategoryTypeID"].ToString();
            model.text = row["CategoryTypeName"].ToString();

            results.Add(model);
        }

        return Newtonsoft.Json.JsonConvert.SerializeObject(results);
    }
}