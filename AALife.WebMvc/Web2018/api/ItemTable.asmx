<%@ WebService Language="C#" Class="ItemTable" %>

using AALife.Service.Dapper;
using AALife.Service.Domain.Common;
using AALife.Service.Domain.ViewModel;
using System;
using System.Linq;
using System.Web.Services;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
[System.Web.Script.Services.ScriptService]
public class ItemTable  : System.Web.Services.WebService
{

    [WebMethod]
    public ListModel<ItemTableViewModel> GetItemTable(QueryPageModel pageModels)
    {
        ApiBase.GZipEncodePage();

        var result = new ListModel<ItemTableViewModel>();

        try
        {
            ItemTableBLL bll = new ItemTableBLL();

            int count = 0;
            var lists = bll.GetItemTable(pageModels, out count);

            result.rows = lists.ToList();
            result.total = count;
        }
        catch(Exception ex)
        {
            result.error = "加载出错！";
        }

        return result;
    }

}