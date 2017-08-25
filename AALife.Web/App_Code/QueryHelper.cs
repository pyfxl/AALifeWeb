using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

/// <summary>
/// QueryHelper 的摘要说明
/// </summary>
public class QueryHelper
{
	public QueryHelper()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
    }
    
    //取日期选择
    public static string GetSpinDate(string str, int day, string type)
    {
        DateTime date = DateTime.Now.Date;
        if (!UtilityHelper.StringIsEmpty(str))
        {
            date = Convert.ToDateTime(str).AddDays(day);
        }

        string result = "";
        switch (type)
        {
            case "d":
                result = date.ToString("yyyy-MM-dd") + " " + DateHelper.GetWeekName(date);
                break;
            case "w":
                result = date.ToString("yyyy年") + " " + DateHelper.GetWeekNumOfYear(date) + "周";
                break;
            case "m":
                result = date.ToString("yyyy年MM月");
                break;
            case "y":
                result = date.ToString("yyyy年");
                break;
        }

        return result;
    }

    //取日期选择值
    public static string GetSpinDateVal(string str, int day, string type)
    {
        DateTime date = DateTime.Now.Date;
        if (!UtilityHelper.StringIsEmpty(str))
        {
            date = Convert.ToDateTime(str);
        }

        switch (type)
        {
            case "d":
                date = date.AddDays(day);
                break;
            case "w":
                int d = (day > 0 ? 7 : -7);
                date = date.AddDays(d);
                break;
            case "m":
                date = date.AddMonths(day);
                break;
            case "y":
                date = date.AddYears(day);
                break;
        }

        return date.ToString("yyyy-MM-dd");
    }

    //取收支颜色
    public static string GetColorStr(string str)
    {
        if (str == "sr" || str == "jr" || str == "hr")
        {
            return "shoucolor";
        }
        else
        {
            return "zhicolor";
        }
    }

    //取收支价格
    public static string GetPriceDot(object price, string str)
    {
        if (Convert.IsDBNull(price)) return "";

        if (str == "sr" || str == "jr" || str == "hr")
        {
            return "+ " + Convert.ToDecimal(price).ToString("0.0##");
        }
        else
        {
            return "- " + Convert.ToDecimal(price).ToString("0.0##");
        }
    }

    //设置排序箭头
    public static string GetSortArrow(string str, string sort, string by)
    {
        if (str != sort) return "";

        return by == "asc" ? "↑" : "↓";
    }

    //验证URL有效性
    public static string GetQueryURL(string url)
    {
        if (url == "") return "";
        
        string[] arr = url.Split('&');
        if (arr.Length == 1) return "";

        bool result = true;
        foreach (string str in arr)
        {
            string[] group = str.Split('=');
            switch(group[0].ToLower())
            {
                case "itemtype":
                    if (group[1] == "null")
                    {
                        result = false;
                    }
                    break;
                case "catid":
                    if (group[1] == "null")
                    {
                        result = false;
                    }
                    break;
                case "cardid":
                    if (group[1] == "null")
                    {
                        result = false;
                    }
                    break;
            }

            if (!result) break;
        }

        return result ? url : "";
    }

}