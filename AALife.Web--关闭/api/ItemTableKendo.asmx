<%@ WebService Language="C#" Class="ItemTable" %>

using Yanzi.Core.Kendoui;
using AALife.Service.Kendoui;
using AALife.Service.EF;
using AALife.Service.Domain.Common;
using AALife.Service.Domain.ViewModel;
using AALife.Service.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Web.Services;
using NLog;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
[System.Web.Script.Services.ScriptService]
public class ItemTable  : System.Web.Services.WebService
{
    private static Logger log = LogManager.GetCurrentClassLogger();

    [WebMethod]
    public DataSourceResult GetItemTable(DataSourceRequest query)
    {
        ApiBase.GZipEncodePage();

        var grid = new DataSourceResult();

        try
        {
            var db = new AALifeDbContext();
            db.Database.CommandTimeout = 180;
            ItemTableService bll = new ItemTableService(db);
            //if (query.Sort == null || !query.Sort.Any())
            //{
            //    query.Sort = new List<Sort>() { new Sort() { Field = "ItemBuyDate", Dir = "desc" } };
            //}
            grid = bll.GetKendoDataSource(query, a => new { a.ModifyDate });
        }
        catch(Exception ex)
        {
            grid.Errors = "加载出错！";
            log.Error(ex);
        }

        return grid;
    }

}