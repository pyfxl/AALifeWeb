using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AALife.WebMvc
{
    public class Constant
    {
        public static readonly Dictionary<string, string> ItemTypeDic = new Dictionary<string, string>() { { "sr", "收入" }, { "zc", "支出" }, { "jr", "借入" }, { "jc", "借出" }, { "hr", "还入" }, { "hc", "还出" } };

        public static readonly Dictionary<string, string> RegionTypeDic = new Dictionary<string, string>() { { "d", "每日" }, { "w", "每周" }, { "m", "每月" }, { "j", "每季" }, { "y", "每年" }, { "b", "工作日" } };

        public static readonly Dictionary<string, string> ThemeDic = new Dictionary<string, string>() { { "main", "低调红" }, { "gold", "土豪金" }, { "blue", "屌丝蓝" } };

        //cache
        public const string ITEM_ALL_USER = "aalife.item.user.{0}";
    }
}