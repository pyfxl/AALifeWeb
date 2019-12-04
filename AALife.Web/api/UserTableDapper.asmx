<%@ WebService Language="C#" Class="UserTable" %>

using Yanzi.Core.Kendoui;
using AALife.Service.Kendoui;
using AALife.Service.EF;
using AALife.Service.Domain.Common;
using AALife.Service.Domain.ViewModel;
using AALife.Service.Models;
using System;
using System.Linq;
using System.Web.Services;
using System.Collections.Generic;
using NLog;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
[System.Web.Script.Services.ScriptService]
public class UserTable : System.Web.Services.WebService
{
    private static Logger log = LogManager.GetCurrentClassLogger();

    [WebMethod]
    public DataSourceResult GetUserTable(DataSourceRequest query)
    {
        ApiBase.GZipEncodePage();

        var grid = new DataSourceResult();

        try
        {
            var db = new AALifeDbContext();
            UserTableService bll = new UserTableService(db);
            if(query.Sort == null || !query.Sort.Any())
            {
                query.Sort = new List<Sort>() { new Sort() { Field = "CreateDate", Dir = "desc" } };
            }
            grid = bll.GetKendoDataSource(query);
        }
        catch(Exception ex)
        {
            grid.Errors = "加载出错！";
            log.Error(ex);
        }

        return grid;
    }

}
