using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AALife.Service.Domain.Common
{
    public class QueryPageModel : QueryModel
    {
        /// <summary>
        /// 页数
        /// </summary>
        public int page { get; set; }

        /// <summary>
        /// 行数
        /// </summary>
        public int pageSize { get; set; }

        /// <summary>
        /// 跳过数
        /// </summary>
        public int skip { get; set; }

        /// <summary>
        /// 获取数
        /// </summary>
        public int take { get; set; }

        /// <summary>
        /// 行数（jqgrid）
        /// </summary>
        public int rows { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        public string sidx { get; set; }

        /// <summary>
        /// 排序（desc/asc）
        /// </summary>
        public string sord { get; set; }

    }
}
