using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

/// <summary>
/// DateHelper 的摘要说明
/// </summary>
public class DateHelper
{
	public DateHelper()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    //取当前天
    public static DateTime GetCurDate(DateTime date)
    {
        return date;
    }

    //取本周一
    public static DateTime GetWeekFirst(DateTime date)
    {
        int week = Convert.ToInt32(date.DayOfWeek);
        int diff = (-1) * week + 1;
        return date.AddDays(diff);
    }

    //取本周末
    public static DateTime GetWeekLast(DateTime date)
    {
        int week = Convert.ToInt32(date.DayOfWeek);
        int diff = 7 - week;
        return date.AddDays(diff);
    }

    //取本月第一天
    public static DateTime GetMonthFirst(DateTime date)
    {
        return date.AddDays(-(date.Day) + 1);
    }

    //取本月最后一天
    public static DateTime GetMonthLast(DateTime date)
    {
        return date.AddMonths(1).AddDays(-(date.Day));
    }

    //取本年第一天
    public static DateTime GetYearFirst(DateTime date)
    {
        return date.AddMonths(-(date.Month) + 1).AddDays(-(date.Day) + 1);
    }

    //取本年最后一天
    public static DateTime GetYearLast(DateTime date)
    {
        return date.AddYears(1).AddMonths(-(date.Month) + 1).AddDays(-(date.Day));
    }

    //取最小SQL日期
    public static DateTime GetSqlMinDate()
    {
        return SqlDateTime.MinValue.Value.Date;
    }

    //取最大SQL日期
    public static DateTime GetSqlMaxDate()
    {
        return SqlDateTime.MaxValue.Value.Date;
    }

    //取本年第几周
    public static int GetWeekNumOfYear(DateTime date)
    {
        int days = date.DayOfYear + (7 - (int)date.DayOfWeek);
        return days / 7 + (days % 7 == 0 ? 0 : 1);
    }  

    //取周几
    public static string GetWeekName(DateTime date)
    {
        int w = (int)date.DayOfWeek;
        string[] weekArr = { "周日", "周一", "周二", "周三", "周四", "周五", "周六" };
        return weekArr[w];
    }

}