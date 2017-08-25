using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;

/// <summary>
/// ValidHelper 的摘要说明
/// </summary>
public class ValidHelper
{
	static ValidHelper()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
    }

    //验证价格
    public static bool CheckDouble(string str)
    {
        Regex regex = new Regex(@"^\-?\d+\.?\d*$");
        if (regex.IsMatch(str))
        {
            return true;
        }
        return false;
    }

    //验证日期
    public static bool CheckDate(string str)
    {
        Regex regex = new Regex(@"((1|2)\d{3})-(0[1-9]|10|11|12)-(0[1-9]$|1\d$|2\d$|3[0-1]$)");
        if (regex.IsMatch(str))
        {
            return true;
        }
        return false;
    }

    //验证邮箱
    public static bool CheckEmail(string str)
    {
        Regex regex = new Regex(@"^([A-Za-z0-9]+[_|\-]?)+@([A-Za-z0-9]+([_|\-]?[A-Za-z0-9]+)*)+\.(([A-Za-z]{2,4})|([A-Za-z]{3}\.[A-Za-z]{2}))$");
        if (regex.IsMatch(str))
        {
            return true;
        }
        return false;
    }

    //验证数字
    public static bool CheckNumber(string str)
    {
        Regex regex = new Regex(@"^[1-9]+[0-9]*$");
        if (regex.IsMatch(str))
        {
            return true;
        }
        return false;
    }

    //验证手机
    public static bool CheckPhone(string str)
    {
        Regex regex = new Regex(@"^[0-9]{11}$");
        if (regex.IsMatch(str))
        {
            return true;
        }
        return false;
    }

    //验证长度
    public static bool CheckLength(string str, int num)
    {
        if (str.Length < num || str.Length > 12)
        {
            return false;
        }

        return true;
    }

}