<%@ WebService Language="C#" Class="WorkDay" %>

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
public class WorkDay  : System.Web.Services.WebService
{

    [WebMethod]
    public ListViewModel<WorkDayTable> GetWorkDay()
    {
        WorkDayBLL bll = new WorkDayBLL();

        List<WorkDayTable> lists = bll.GetWorkDay().ToList();

        var result = new ListViewModel<WorkDayTable>
        {
            rows = lists,
            total = lists.Count()
        };

        return result;
    }

}