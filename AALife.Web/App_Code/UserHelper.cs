using AALife.BLL;
using AALife.Model;
using NS_OpenApiV3;
using NS_SnsNetWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// UserHelper 的摘要说明
/// </summary>
public class UserHelper
{
	public UserHelper()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    //保存用户Session
    public static void SaveSession(UserInfo user)
    {
        HttpContext.Current.Session["UserID"] = user.UserID;
        HttpContext.Current.Session["UserName"] = user.UserName;
        HttpContext.Current.Session["UserNickName"] = user.UserNickName;
        HttpContext.Current.Session["UserTheme"] = user.UserTheme;
        HttpContext.Current.Session["UserLevel"] = user.UserLevel.ToString();
        HttpContext.Current.Session["UserFrom"] = user.UserFrom;
        HttpContext.Current.Session["UserWorkDay"] = user.UserWorkDay;
        HttpContext.Current.Session["UserFunction"] = user.UserFunction;
        HttpContext.Current.Session["CategoryRate"] = user.CategoryRate;
        HttpContext.Current.Session["IsUpdate"] = user.IsUpdate;
    }

    //取用户功能设置链接
    public static string GetUserFunctionText(string str)
    {
        return GetUserFunctionText(str, false);
    }

    //取用户功能设置链接
    public static string GetUserFunctionText(string str, bool isNew)
    {
        string result = "";
        result = (isNew ? "" : "<p><img src='/Images/Others/ico_meet.gif' border='0' alt='' /> 常用功能：</p>");

        if (str != "")
        {
            string[] arr = str.Split(',');

            for (int i = 0; i < arr.Length; i++)
            {
                switch (arr[i])
                {
                    case "1":
                        result += "<a href='FenLeiZongJi.aspx'>消费分类排行</a>";
                        break;
                    case "2":
                        result += "<a href='ItemNumTop.aspx'>消费次数排行</a>";
                        break;
                    case "3":
                        result += "<a href='ItemPriceTop.aspx'>消费单价排行</a>";
                        break;
                    case "4":
                        result += "<a href='ItemDateTop.aspx'>消费日期排行</a>";
                        break;
                    case "5":
                        result += "<a href='QuJianTongJi.aspx'>消费区间统计</a>";
                        break;
                    case "6":
                        result += "<a href='TuiJianFenXi.aspx'>消费推荐统计</a>";
                        break;
                    case "7":
                        result += "<a href='BiJiaoFenXi.aspx'>消费分析比较</a>";
                        break;
                    case "8":
                        result += "<a href='JianGeFenXi.aspx'>消费间隔分析</a>";
                        break;
                    case "9":
                        result += "<a href='TianShuFenXi.aspx'>消费天数分析</a>";
                        break;
                    case "10":
                        result += "<a href='JiaGeFenXi.aspx'>消费价格分析</a>";
                        break;
                    case "11":
                        result += "<a href='JieHuanFenXi.aspx'>收支借还分析</a>";
                        break;
                    case "12":
                        result += "<a href='QuWeiTongJi.aspx'>趣味统计分析</a>";
                        break;
                    case "13":
                        result += "<a href='UserAdmin.aspx'>用户资料</a>";
                        break;
                    case "14":
                        result += "<a href='UserBoundAdmin.aspx'>用户绑定</a>";
                        break;
                    case "16":
                        result += "<a href='UserDataAdmin.aspx'>数据管理</a>";
                        break;
                    case "17":
                        result += "<a href='UserFunctionSetting.aspx'>菜单设置</a>";
                        break;
                    case "15":
                        result += "<a href='UserCategoryAdmin.aspx'>类别管理</a>";
                        break;
                    case "20":
                        result += "<a href='UserZhuanTi.aspx'>用户专题</a>";
                        break;
                    case "22":
                        result += "<a href='UserCardAdmin.aspx'>钱包管理</a>";
                        break;
                    case "18":
                        result += "<a href='SearchItem.aspx'>消费搜索</a>";
                        break;
                    case "21":
                        result += "<a href='Helper.aspx'>网站说明</a>";
                        break;
                    case "19":
                        result += "<a href='UserLogout.aspx'>用户退出</a>";
                        break;
                }
            }

            result += (isNew ? "" : "<a href='UserFunctionSetting.aspx' class='linkedit'>更改</a>");
        }
        else
        {
            result += (isNew ? "<a href='UserAdmin.aspx'>用户中心</a>" : "<a href='UserFunctionSetting.aspx'>未设置</a>");
        }

        return result;
    }

    //取用户功能设置链接
    public static DataTable GetUserFunction(string str)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("FunctionURL", typeof(string));
        dt.Columns.Add("FunctionName", typeof(string));

        if (str != "")
        {
            string[] arr = str.Split(',');

            for (int i = 0; i < arr.Length; i++)
            {
                DataRow dr = dt.NewRow();

                switch (arr[i])
                {
                    case "1":
                        dr[0] = "FenLeiZongJi.aspx";
                        dr[1] = "消费分类排行";
                        break;
                    case "2":
                        dr[0] = "ItemNumTop.aspx";
                        dr[1] = "消费次数排行";
                        break;
                    case "3":
                        dr[0] = "ItemPriceTop.aspx";
                        dr[1] = "消费单价排行";
                        break;
                    case "4":
                        dr[0] = "ItemDateTop.aspx";
                        dr[1] = "消费日期排行";
                        break;
                    case "5":
                        dr[0] = "QuJianTongJi.aspx";
                        dr[1] = "消费区间统计";
                        break;
                    case "6":
                        dr[0] = "TuiJianFenXi.aspx";
                        dr[1] = "消费推荐统计";
                        break;
                    case "7":
                        dr[0] = "BiJiaoFenXi.aspx";
                        dr[1] = "消费分析比较";
                        break;
                    case "8":
                        dr[0] = "JianGeFenXi.aspx";
                        dr[1] = "消费间隔分析";
                        break;
                    case "9":
                        dr[0] = "TianShuFenXi.aspx";
                        dr[1] = "消费天数分析";
                        break;
                    case "10":
                        dr[0] = "JiaGeFenXi.aspx";
                        dr[1] = "消费价格分析";
                        break;
                    case "11":
                        dr[0] = "JieHuanFenXi.aspx";
                        dr[1] = "收支借还分析";
                        break;
                    case "12":
                        dr[0] = "QuWeiTongJi.aspx";
                        dr[1] = "趣味统计分析";
                        break;
                    case "13":
                        dr[0] = "UserAdmin.aspx";
                        dr[1] = "用户资料";
                        break;
                    case "14":
                        dr[0] = "UserBoundAdmin.aspx";
                        dr[1] = "用户绑定";
                        break;
                    case "16":
                        dr[0] = "UserDataAdmin.aspx";
                        dr[1] = "数据管理";
                        break;
                    case "17":
                        dr[0] = "UserFunctionSetting.aspx";
                        dr[1] = "菜单设置";
                        break;
                    case "15":
                        dr[0] = "UserCategoryAdmin.aspx";
                        dr[1] = "类别管理";
                        break;
                    case "20":
                        dr[0] = "UserZhuanTi.aspx";
                        dr[1] = "用户专题";
                        break;
                    case "22":
                        dr[0] = "UserCardAdmin.aspx";
                        dr[1] = "钱包管理";
                        break;
                    case "18":
                        dr[0] = "SearchItem.aspx";
                        dr[1] = "消费搜索";
                        break;
                    case "21":
                        dr[0] = "Helper.aspx";
                        dr[1] = "网站说明";
                        break;
                    case "19":
                        dr[0] = "UserLogout.aspx";
                        dr[1] = "用户退出";
                        break;
                }

                dt.Rows.Add(dr);
            }

        }

        return dt;
    }

    public static RstArray GetUserInfo(OpenApiV3 sdk, string openid, string openkey, string pf)
    {
        Dictionary<string, string> param = new Dictionary<string, string>();
        param.Add("openid", openid);
        param.Add("openkey", openkey);
        param.Add("pf", pf);
        //param.Add("userip", "127.0.0.1");

        string script_name = "/v3/user/get_info";
        return sdk.Api(script_name, param);
    }

    public static string GetUserName(string from)
    {
        UserTableBLL bll = new UserTableBLL();
        UserInfo user = new UserInfo();

        string userName = "";
        do
        {
            userName = from + Utility.GetRandomNumber(10000, 99999);
            user = bll.GetUserByUserName(userName);
        } while (user.UserID > 0);

        return userName;
    }

    public static int JoinDay(DateTime date)
    {
        int day = 0;
        day = ((TimeSpan)(DateTime.Now - date)).Days;

        return day + 1;
    }

}