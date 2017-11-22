using Kendo.DynamicLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AALife.Service.Model.Common
{
    public class UserQueryModel : DataSourceRequest
    {
        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime startDate { get; set; }

        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime endDate { get; set; }

        /// <summary>
        /// 关键字
        /// </summary>
        public string keySearch { get; set; }

    }
}
