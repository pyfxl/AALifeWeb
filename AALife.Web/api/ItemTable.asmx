<%@ WebService Language="C#" Class="ItemTable" %>

using AALife.EF.BLL;
using AALife.EF.Models;
using AALife.EF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
[System.Web.Script.Services.ScriptService]
public class ItemTable  : System.Web.Services.WebService
{

    [WebMethod]
    public ListViewModel<AALife.EF.Models.ItemTable> GetItemTableDapper(AALife.EF.ViewModel.QueryPageModel pageModels)
    {
        ApiBase.GZipEncodePage();

        var result = new ListViewModel<AALife.EF.Models.ItemTable>();

        try
        {
            ItemTableBLL bll = new ItemTableBLL();

            var lists = bll.GetItemTableDapper(pageModels.startDate, pageModels.endDate);

            result.rows = lists.ToList();
        }
        catch(Exception ex)
        {
            result.error = "加载出错！";
        }

        return result;
    }

    [WebMethod]
    public ListViewModel<AALife.EF.Models.ItemTable> GetItemTable(AALife.EF.ViewModel.QueryPageModel pageModels)
    {
        ApiBase.GZipEncodePage();

        var result = new ListViewModel<AALife.EF.Models.ItemTable>();

        try
        {
            ItemTableBLL bll = new ItemTableBLL();

            var lists = new List<AALife.EF.Models.ItemTable>();
            if (pageModels.key != "" && pageModels.key != null)
            {
                lists = bll.GetItemTable(pageModels.key).ToList();
            }
            else
            {
                if (pageModels.userId > 0 && pageModels.userId != null)
                {
                    lists = bll.GetItemTable(pageModels.startDate, pageModels.endDate, pageModels.userId.Value).ToList();
                }
                else
                {
                    lists = bll.GetItemTable(pageModels.startDate, pageModels.endDate).ToList();
                }
            }

            result.rows = lists.Skip(pageModels.skip).Take(pageModels.take).ToList();
            result.total = lists.Count();
        }
        catch(Exception ex)
        {
            result.error = "加载出错！";
        }

        return result;
    }

}