<%@ WebService Language="C#" Class="UserFrom" %>

using AALife.Service.EF;
using AALife.Service.Models;
using AALife.Service.Model.KendoUI;
using System.Linq;
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

        var lists = bll.GetUserFrom();

        var result = new ListViewModel<UserFromTable>
        {
            rows = lists.ToList(),
            total = lists.Count()
        };

        return result;
    }

}