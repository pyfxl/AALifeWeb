﻿using AALife.Core.Configuration;
using System.ComponentModel.DataAnnotations;

namespace AALife.Core.Domain.Configuration
{
    public class SiteSettings : ISettings
    {
        public SiteSettings()
        {
            SiteUrl = "";
            SiteName = "AA生活记账";
            SiteAuthor = "冯湘灵";
            SiteKeywords = "记账2018,记账app,记账软件,记账网站,在线记账,云记账,简单记账,AA生活记账,好用的记账软件";
            SiteDescription = "AA生活记账是一款记录收入、支出、借还消费，使用简单的手机记账APP。包括类别排行、次数排行、单价排行、日期排行、区间统计、推荐统计，比较分析、收支分析、借还分析、趣味统计，和区间记账、专题记账、语音记账、数据同步、数据导出、云备份，我的钱包、钱包转账、转账明细、类别预警、搜索等实用功能。";
            PageNumber = 50;
            PageNumbers = "[10, 30, 50, 100]";
        }

        /// <summary>
        /// 站点地址
        /// </summary>
        [Display(Name = "网站地址")]
        public string SiteUrl { get; set; }

        /// <summary>
        /// 站点名称
        /// </summary>
        [Display(Name = "站点名称")]
        public string SiteName { get; set; }

        /// <summary>
        /// 网站作者
        /// </summary>
        [Display(Name = "网站作者")]
        public string SiteAuthor { get; set; }

        /// <summary>
        /// 关键字
        /// </summary>
        [Display(Name = "关键字")]
        public string SiteKeywords { get; set; }

        /// <summary>
        /// 网站描述
        /// </summary>
        [Display(Name = "网站描述")]
        public string SiteDescription { get; set; }

        /// <summary>
        /// 每页记录数
        /// </summary>
        [Display(Name = "每页记录数")]
        public int PageNumber { get; set; }

        /// <summary>
        /// 记录数组[10,30,50]
        /// </summary>
        [Display(Name = "记录数组")]
        public string PageNumbers { get; set; }

    }
}
