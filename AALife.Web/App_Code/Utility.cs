using System;
using System.Web;
using System.Web.UI;

public class Utility
{
    static Utility()
    {
    }

    //进入页面后提示不转向
    public static void Alert(Page page, string msg)
    {
        string key = "AlertMessage";
        string script = String.Format("alert('{0}');", msg);
        page.ClientScript.RegisterStartupScript(typeof(Page), key, GetJavaScript(script), true);
    }

    //进入页面前提示转向
    public static void Alert(Page page, string msg, string url)
    {
        string key = "AlertMessage";
        string script = String.Format("alert('{0}');window.location='{1}';", msg, url);
        page.ClientScript.RegisterStartupScript(typeof(Page), key, GetJavaScript(script), true);//RegisterClientScriptBlock
    }
    
    //confirm
    public static void Confirm(Page page, string msg, string page1, string page2)
    {
        string key = "ConfirmMessage";
        string script = String.Format("confirm('{0}')?window.location='{1}':window.location='{2}';", msg, page1, page2);
        page.ClientScript.RegisterStartupScript(typeof(Page), key, GetJavaScript(script), true);
    }

    //包装script
    private static string GetJavaScript(string script)
    {
        return "setTimeout(function(){" + script + "}, 0)";
    }

    //替换sql字符
    public static string ReplaceSql(string str)
    {
        str = str.Replace("'", "''");
        return str;
    }

    //替换字符
    public static string ReplaceString(string str)
    {
        str = str.Replace("&", "&amt;");
        str = str.Replace("<", "&lt;");
        str = str.Replace(">", "&gt;");
        str = str.Replace("\"", "&quot;");
        str = str.Replace(" ", "&nbsp;");
        str = str.Replace("'", "''");
        return str;
    }

    //还原字符
    public static string UnReplaceString(string str)
    {
        str = str.Replace("&amt;", "&");
        str = str.Replace("&lt;", "<");
        str = str.Replace("&gt;", ">");
        str = str.Replace("&quot;", "\"");
        str = str.Replace("&nbsp;", " ");
        str = str.Replace("''", "'");
        return str;
    }
    
    //取随机数
    public static string GetRandomNumber(int minNum, int maxNum)
    {
        Random rad = new Random();
        return rad.Next(minNum, maxNum).ToString();
    }

    //取随机秒
    private static string GetRandomTime()
    {
        return DateTime.Now.ToString("ff");
    }

    //传入日期处理
    public static DateTime GetRequestDate(string date)
    {
        if (date != null && date != "" && ValidHelper.CheckDate(date))
        {
            HttpContext.Current.Session["TodayDate"] = date;
            return Convert.ToDateTime(date);
        } 
        else if (HttpContext.Current.Session["TodayDate"] != null)
        {
            return Convert.ToDateTime(HttpContext.Current.Session["TodayDate"]);
        }
        else
        {
            HttpContext.Current.Session["TodayDate"] = DateTime.Now.ToString("yyyy-MM-dd");
            return DateTime.Now;
        }
    }

    //传入日期处理
    public static DateTime GetRequestDate2(string date)
    {
        if (date != null && date != "" && ValidHelper.CheckDate(date))
        {
            //HttpContext.Current.Session["TodayDate"] = date;
            return Convert.ToDateTime(date);
        }
        else if (HttpContext.Current.Session["TodayDate"] != null)
        {
            return Convert.ToDateTime(HttpContext.Current.Session["TodayDate"]).AddMonths(-1);
        }
        else
        {
            //HttpContext.Current.Session["TodayDate"] = DateTime.Now.ToString("yyyy-MM-dd");
            return DateTime.Now.AddMonths(-1);
        }
    }

    //取周几
    public static string GetWeekStr(int i)
    {
        string[] weekArr = { "周日", "周一", "周二", "周三", "周四", "周五", "周六" };
        return weekArr[i];
    }

    //取周几
    public static string GetWeekStrShort(int i)
    {
        string[] weekArr = { "日", "一", "二", "三", "四", "五", "六" };
        return weekArr[i];
    }
}
