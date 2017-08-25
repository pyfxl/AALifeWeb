using System;
using System.Collections.Generic;
using System.Data;
using System.Web;

/// <summary>
/// ItemHelper 的摘要说明
/// </summary>
public class ItemHelper
{
	static ItemHelper()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
    }

    //借还价格格式
    public static string JieHuanColor(string str, int type)
    {
        if (str == "") return "";

        double price = Convert.ToDouble(str);
        if (price == 0)
        {
            return "pricenone";
        }
        else if (price < 0)
        {
            return type == 0 ? "pricered" : "priceblue";
        }
        else
        {
            return type == 0 ? "priceblue" : "pricered";
        }
    }
    
    //隐藏用户名
    public static string HideUserName(string name)
    {
        string result = name;
        if (name.StartsWith("aa") || name.StartsWith("qz") || name.StartsWith("qq") || name.StartsWith("py") || name.StartsWith("qw") || name.StartsWith("sjqq"))
        {
            //result = name.Remove(name.Length - 2) + "**";
            result = name.Replace(name.Substring(name.Length-5, 3), "***");
        }

        return result;
    }
    
    //取星期
    public static string GetWeekString(string date, int type)
    {
        DateTime d = Convert.ToDateTime(date);

        string[,] weekArr = { { "周日", "周一", "周二", "周三", "周四", "周五", "周六" }, { "日", "一", "二", "三", "四", "五", "六" } };

        return weekArr[type, Convert.ToInt32(d.DayOfWeek)];
    }

    //取图表数据
    public static string GetChartData(DataTable dt, string prop)
    {
        string str = "";
        if (dt.Rows.Count > 0)
        {
            int index = 0;
            foreach (DataRow dr in dt.Rows)
            {
                index++;
                if (prop == "ItemBuyDate")
                {
                    str += Convert.ToDateTime(dr["ItemBuyDate"]).ToString("yyyy-MM-dd");
                }
                else
                {
                    str += dr[prop].ToString();
                }
                if (index < dt.Rows.Count) str += ",";
            }
        }

        return str;
    }

}