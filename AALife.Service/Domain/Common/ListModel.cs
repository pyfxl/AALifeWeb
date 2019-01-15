using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AALife.Service.Domain.Common
{
    public class ListModel<T> : ResultModel
    {
        /// <summary>
        /// 记录
        /// </summary>
        public List<T> rows { get; set; }

        /// <summary>
        /// 当前页数
        /// </summary>
        public int page { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int total { get; set; }

        /// <summary>
        /// 总记录
        /// </summary>
        public int records { get; set; }

    }
}
