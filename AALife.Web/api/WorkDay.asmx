<%@ WebService Language="C#" Class="WorkDay" %>

using AALife.Service.EF;
using AALife.Service.Models;
using AALife.Service.Model.Common;
using System.Linq;
using System.Web.Services;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
[System.Web.Script.Services.ScriptService]
public class WorkDay  : System.Web.Services.WebService
{

    [WebMethod]
    public ListModel<WorkDayTable> GetWorkDay()
    {
        WorkDayBLL bll = new WorkDayBLL();

        var lists = bll.GetWorkDay();

        var result = new ListModel<WorkDayTable>
        {
            rows = lists.ToList(),
            total = lists.Count()
        };

        return result;
    }

}