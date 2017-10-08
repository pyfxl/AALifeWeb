<%@ WebService Language="C#" Class="UserFrom" %>

using AALife.EF.BLL;
using AALife.EF.Models;
using AALife.EF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
[System.Web.Script.Services.ScriptService]
public class UserFrom  : System.Web.Services.WebService
{

    [WebMethod]
    public ListViewModel<UserFromTable> GetUserFrom()
    {
        UserFromBLL bll = new UserFromBLL();

        List<UserFromTable> lists = bll.GetUserFrom().ToList();

        var result = new ListViewModel<UserFromTable>
        {
            rows = lists,
            total = lists.Count()
        };

        return result;
    }

}