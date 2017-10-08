<%@ WebService Language="C#" Class="UserTable" %>

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
public class UserTable : System.Web.Services.WebService
{

    [WebMethod]
    public ListViewModel<AALife.EF.Models.UserTable> GetUserTable(DateTime startDate, DateTime endDate, string key)
    {
        ApiBase.GZipEncodePage();

        var result = new ListViewModel<AALife.EF.Models.UserTable>();

        try
        {
            UserTableBLL bll = new UserTableBLL();

            var lists = new List<AALife.EF.Models.UserTable>();
            if (key != "" && key != null)
            {
                lists = bll.GetUserTable(key).ToList();
            }
            else
            {
                lists = bll.GetUserTable(startDate, endDate).ToList();
            }

            result.rows = lists;
            result.total = lists.Count();
        }
        catch(Exception ex)
        {
            result.error = "加载出错！";
        }

        return result;
    }

    [WebMethod]
    public ResultModel UpdateUserTable(AALife.EF.Models.UserTable models)
    {
        string error = "";
        try
        {
            UserTableBLL bll = new UserTableBLL();
            bll.UpdateUserTable(models);
        }
        catch
        {
            error = "更新出错！";
        }

        return new ResultModel { error = error };
    }

    [WebMethod]
    public ResultModel AddUserTable(AALife.EF.Models.UserTable models)
    {
        string error = "";
        try
        {
            UserTableBLL bll = new UserTableBLL();
            bool exists = bll.CheckUserExists(models, "UserName");
            if (exists)
            {
                error = "用户名重复！";
            }
            else
            {
                bll.AddUserTable(models);
            }
        }
        catch
        {
            error = "添加错误！";
        }

        return new ResultModel { error = error };
    }

    [WebMethod]
    public ResultModel RemoveUserTable(AALife.EF.Models.UserTable models)
    {
        string error = "";
        try
        {
            UserTableBLL bll = new UserTableBLL();
            bll.RemoveUserTable(models);
        }
        catch
        {
            error = "删除错误！";
        }

        return new ResultModel { error = error };
    }

}
