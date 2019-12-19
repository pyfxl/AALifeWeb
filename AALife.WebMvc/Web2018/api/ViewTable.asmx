<%@ WebService Language="C#" Class="ViewTable" %>

using AALife.Service.EF;
using AALife.Service.Models;
using AALife.Service.Domain.Common;
using AALife.Service.Domain.ViewModel;
using System;
using System.Linq;
using System.Web.Services;
using Kendo.DynamicLinq;
using System.Collections.Generic;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
[System.Web.Script.Services.ScriptService]
public class ViewTable  : System.Web.Services.WebService
{

    [WebMethod]
    public DataSourceResult GetViewTable(int take, int skip, IEnumerable<Sort> sort, Filter filter, IEnumerable<Aggregator> aggregates)
    {
        ApiBase.GZipEncodePage();

        ViewTableBLL bll = new ViewTableBLL();

        return bll.GetViewTable(take, skip, sort, filter, aggregates);
    }

    [WebMethod]
    public ListModel<ViewPageTable> GetViewPageTable()
    {
        ApiBase.GZipEncodePage();

        var result = new ListModel<ViewPageTable>();

        try
        {
            ViewTableBLL bll = new ViewTableBLL();

            int count = 0;
            var lists = bll.GetViewPageTable(out count);

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