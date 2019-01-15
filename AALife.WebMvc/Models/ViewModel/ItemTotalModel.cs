using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AALife.WebMvc.Models.ViewModel
{
    public class ItemTotalModel
    {
        /// <summary>
        /// 收入数量
        /// </summary>
        public int ShouRuCount { get; set; }

        /// <summary>
        /// 收入金额
        /// </summary>
        public decimal ShouRuAmount { get; set; }

        /// <summary>
        /// 支出数量
        /// </summary>
        public int ZhiChuCount { get; set; }

        /// <summary>
        /// 支出金额
        /// </summary>
        public decimal ZhiChuAmount { get; set; }

        /// <summary>
        /// 结存，收入减支出
        /// </summary>
        public decimal JieCunAmount { get { return ShouRuAmount - ZhiChuAmount; } }

        /// <summary>
        /// 借入金额
        /// </summary>
        public decimal JieRuAmount { get; set; }

        /// <summary>
        /// 还出金额
        /// </summary>
        public decimal HuanChuAmount { get; set; }

        /// <summary>
        /// 欠还金额，借入减还出
        /// </summary>
        public decimal QianHuanAmount { get { return JieRuAmount - HuanChuAmount; } }

        /// <summary>
        /// 借出金额
        /// </summary>
        public decimal JieChuAmount { get; set; }

        /// <summary>
        /// 还入金额
        /// </summary>
        public decimal HuanRuAmount { get; set; }

        /// <summary>
        /// 未还金额，还入减借出
        /// </summary>
        public decimal WeiHuanAmount { get { return HuanRuAmount - JieChuAmount; } }

    }
}