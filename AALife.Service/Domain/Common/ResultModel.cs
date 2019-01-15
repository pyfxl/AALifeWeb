using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AALife.Service.Domain.Common
{
    public class ResultModel
    {
        /// <summary>
        /// 编码
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// 成功内容
        /// </summary>
        public string success { get; set; }

        /// <summary>
        /// 错误内容
        /// </summary>
        public string error { get; set; }
    }
}
