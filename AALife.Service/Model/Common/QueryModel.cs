using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AALife.Service.Model.Common
{
    public class QueryModel
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

        /// <summary>
        /// 用户id
        /// </summary>
        public int? userId { get; set; }

        /// <summary>
        /// 过滤
        /// </summary>
        public List<string> filter { get; set; }

        /// <summary>
        /// 过滤string，用于where(string)
        /// </summary>
        public string filterString
        {
            get
            {
                if (this.filter == null) return "";

                StringBuilder sb = new StringBuilder();
                this.filter.ForEach((a) =>
                {
                    sb.Append(string.Format("{0} or ", a));
                });
                sb.Append("1=2");

                return "(" + sb.ToString() + ")";
            }
        }
        
        /// <summary>
        /// 排序
        /// </summary>
        public List<SortModel> sort { get; set; }

        /// <summary>
        /// 排序string，用于where(string)
        /// </summary>
        public string sortString
        {
            get
            {
                if (this.sort == null) return "";

                StringBuilder sb = new StringBuilder();
                this.sort.ForEach((a) =>
                {
                    sb.Append(string.Format("{0} {1},", a.field, a.dir));
                });

                return sb.ToString().TrimEnd(',');
            }
        }

    }
}
