using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace AALife.Service.EF
{
    public class BaseBLL
    {
        /// <summary>
        /// 计算两个日期差多少秒
        /// </summary>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <returns></returns>
        protected long DateDiff(DateTime d1, DateTime d2)
        {
            long t1 = d1.Ticks;
            long t2 = d2.Ticks;
            return (t1 - t2) / 10000000;
        }
    }
}
