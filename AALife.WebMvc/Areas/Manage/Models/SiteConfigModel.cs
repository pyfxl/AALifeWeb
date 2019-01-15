using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AALife.WebMvc.Areas.Manage.Models
{
    public class SiteConfigModel
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "连接串")]
        public string DbConnectionString { get; set; }

        [Display(Name = "连接类型")]
        public string DbProviderName { get; set; }

        [Display(Name = "网站域名")]
        public string WebSite { get; set; }

        [Display(Name = "网站名称")]
        public string SiteName { get; set; }

        [Display(Name = "网站作者")]
        public string SiteAuthor { get; set; }

        [Display(Name = "关键字")]
        public string SiteKeywords { get; set; }

        [Display(Name = "网站描述")]
        public string SiteDescription { get; set; }

        [Display(Name = "页记录数")]
        public string PagePerNumber { get; set; }

        [Display(Name = "工作日")]
        public string UserWorkDay { get; set; }

        [Display(Name = "预算率%")]
        public string CategoryRate { get; set; }

        [Display(Name = "网站贴士")]
        public string SiteTips { get; set; }

        [Display(Name = "公告版本")]
        public string MessageCode { get; set; }

        [Display(Name = "网站公告")]
        public string SiteMessage { get; set; }

        [Display(Name = "手机公告")]
        public string PhoneMessage { get; set; }

        [Display(Name = "页码数")]
        public string PageNumber { get; set; }

    }
}