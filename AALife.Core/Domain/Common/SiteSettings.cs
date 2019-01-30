using AALife.Core.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AALife.Core.Domain.Common
{
    public class SiteSettings : ISettings
    {
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
        public int? PageNumber { get; set; }

        /// <summary>
        /// 记录数组[10,30,50]
        /// </summary>
        [Display(Name = "记录数组")]
        public string PageNumberArrays { get; set; }

        /// <summary>
        /// 密钥Key
        /// </summary>
        [Display(Name = "密钥")]
        public string EncryptionKey { get; set; }
    }
}
