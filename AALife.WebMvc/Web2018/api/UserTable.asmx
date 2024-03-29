﻿<%@ WebService Language="C#" Class="UserTable" %>

using AALife.Service.EF;
using AALife.Service.Domain.Common;
using AALife.Service.Domain.ViewModel;
using System;
using System.Linq;
using System.Web.Services;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
[System.Web.Script.Services.ScriptService]
public class UserTable : System.Web.Services.WebService
{

    [WebMethod]
    public ListModel<UserTableViewModel> GetUserTable(QueryPageModel query)
    {
        ApiBase.GZipEncodePage();

        var result = new ListModel<UserTableViewModel>();

        try
        {
            UserTableBLL bll = new UserTableBLL();

            int count = 0;
            var lists = bll.GetUserTable(query, out count);

            result.rows = lists.ToList();
            result.total = count;
        }
        catch(Exception ex)
        {
            result.error = "加载出错！";
        }

        return result;
    }

    [WebMethod]
    public ResultModel UpdateUserTable(UserTableViewModel models)
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
    public ResultModel AddUserTable(UserTableViewModel models)
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
    public ResultModel RemoveUserTable(UserTableViewModel models)
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

    [WebMethod]
    public string Hello(string name)
    {
        return "Hello" + name;
    }

}
