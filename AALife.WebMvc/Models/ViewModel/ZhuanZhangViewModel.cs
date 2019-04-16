using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AALife.WebMvc.Models.ViewModel
{
    public class ZhuanZhangViewModel : BaseViewModel
    {
        /// <summary>
        /// 转账来自
        /// </summary>
        public int ZhuanZhangFrom { get; set; }

        /// <summary>
        /// 转账给
        /// </summary>
        public int ZhuanZhangTo { get; set; }

        /// <summary>
        /// 转账日期
        /// </summary>
        public DateTime ZhuanZhangDate { get; set; }

        /// <summary>
        /// 转账金额
        /// </summary>
        public decimal ZhuanZhangMoney { get; set; }

        /// <summary>
        /// 同步Id
        /// </summary>
        public int? ZhuanZhangId { get; set; }

        public Guid UserId { get; set; }

        public string ZhuanZhangFromName { get; set; }

        public string ZhuanZhangToName { get; set; }
    }
}