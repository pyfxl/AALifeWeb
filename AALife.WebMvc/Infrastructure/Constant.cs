using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AALife.WebMvc
{
    public static class Constant
    {
        public static int PageNumber = 50;

        public static int UserWorkDay = 5;

        public static int PasswordSaltSize = 5;

        public static readonly Dictionary<string, string> ItemTypeDic = new Dictionary<string, string>()
        {
            { "sr", "收入" },
            { "zc", "支出" },
            { "jr", "借入" },
            { "jc", "借出" },
            { "hr", "还入" },
            { "hc", "还出" }
        };

        public static readonly Dictionary<string, string> RegionTypeDic = new Dictionary<string, string>()
        {
            { "", "---" },
            { "d", "每日" },
            { "w", "每周" },
            { "m", "每月" },
            { "j", "每季" },
            { "y", "每年" },
            { "b", "工作日" }
        };

        public static readonly Dictionary<string, string> UserThemeDic = new Dictionary<string, string>()
        {
            { "main", "低调红" },
            { "gold", "土豪金" },
            { "blue", "屌丝蓝" }
        };

        public static readonly Dictionary<byte, string> UserLevelDic = new Dictionary<byte, string>()
        {
            { 1, "用户" },
            { 9, "管理员" }
        };

        public static readonly Dictionary<string, string> UserFromDic = new Dictionary<string, string>()
        {
            { "qz", "QQ空间" },
            { "qq", "QQ登录" },
            { "py", "朋友网" },
            { "sjqq", "手机QQ" },
            { "qw", "Web QQ" },
            { "web", "网站" },
            { "sj", "手机" },
            { "sjweb", "手机Web" },
            { "sjapp", "手机APP" },
            { "360", "360手机助手" },
            { "bd", "百度手机助手" },
            { "lx", "联想乐商店" },
            { "mi", "小米应用商店" },
            { "tx", "腾讯应用宝" },
            { "wdj", "豌豆荚" },
            { "yyh", "应用汇" },
            { "qt", "其它" },
            { "mz", "魅族应用商店" },
            { "az", "安卓市场" },
            { "upd", "APP升级" }
        };

        public static readonly Dictionary<string, string> UserWorkDayDic = new Dictionary<string, string>()
        {
            { "1", "只周一上班" },
            { "2", "一到二上班" },
            { "3", "一到三上班" },
            { "4", "一到四上班" },
            { "5", "周末双休" },
            { "6", "周末单休" },
            { "7", "全周无休" }
        };

    }
}