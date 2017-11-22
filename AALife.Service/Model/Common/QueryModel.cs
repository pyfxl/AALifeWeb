using AALife.Service.Model.Enum;
using System;
using System.Collections.Generic;
using System.Dynamic;
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
        /// 使用ItemBuyDate=0，还是ModifyDate=1
        /// </summary>
        public int dateType { get; set; }

        /// <summary>
        /// 过滤
        /// </summary>
        public FilterModel filter { get; set; }

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

        /// <summary>
        /// 过滤string
        /// </summary>
        public string filterString
        {
            get
            {
                return getFilter(filter);
            }
        }

        private string getFilter(FilterModel filter)
        {
            int n = 0;
            StringBuilder sb = new StringBuilder();
            sb.Append("(");
            foreach (var f in filter.filters)
            {
                n++;
                if (f.filters != null && f.filters.Any())
                {
                    sb.Append(getFilter(f));
                }
                else
                {
                    sb.Append(string.Format("{0} {1} \"{2}\"", f.field, operators[f.@operator], f.value));
                }
                if (n < filter.filters.Count) sb.Append(string.Format(" {0} ", filter.logic));
            }
            sb.Append(")");

            return sb.ToString();
        }

        private static readonly IDictionary<string, string> operators = new Dictionary<string, string>
        {
            {"eq", "="},
            {"neq", "!="},
            {"lt", "<"},
            {"lte", "<="},
            {"gt", ">"},
            {"gte", ">="}
        };
    }
}
