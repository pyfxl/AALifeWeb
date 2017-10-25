using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AALife.Service.Model.Query
{
    public class QueryPageModel : QueryModel
    {
        /// <summary>
        /// 页数
        /// </summary>
        public int page { get; set; }

        /// <summary>
        /// 页大小
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

    }
}
