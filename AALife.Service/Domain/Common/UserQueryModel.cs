using Kendo.DynamicLinq;
using System;

namespace AALife.Service.Domain.Common
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
