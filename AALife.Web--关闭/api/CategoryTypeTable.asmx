<%@ WebService Language="C#" Class="ItemTable" %>

using Yanzi.Core.Kendoui;
using AALife.Service.Kendoui;
using AALife.Service.EF;
using AALife.Service.Domain.Common;
using AALife.Service.Domain.ViewModel;
using AALife.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Web.Services;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
[System.Web.Script.Services.ScriptService]
public class ItemTable  : System.Web.Services.WebService
{

    [WebMethod]
    public DataTable GetCategoryType()
    {
        UserCategoryTableBLL bll = new UserCategoryTableBLL();

        var lists = bll.GetUserCategoryList(1, 1);

        var results = new List<dynamic>();

        //foreach (DataRow row in lists.Rows)
        //{
        //    dynamic model = new ExpandoObject();
        //    model.value = row["CategoryTypeID"].ToString();
        //    model.text = row["CategoryTypeName"].ToString();

        //    results.Add(model);
        //}

        return lists;
    }

}