using AALife.Core.Configuration;
using System.ComponentModel.DataAnnotations;
using System.Web.Script.Serialization;

namespace AALife.Data
{
    public class DefaultSettings : ISettings
    {
        public DefaultSettings()
        {
            DefaultTheme = "main";
            DefaultLevel = 1;
            DefaultFrom = "web";
            PageNumber = 50;
            PageNumbers = "[10, 30, 50, 100]";
        }

        /// <summary>
        /// 默认主题
        /// </summary>
        [Display(Name = "默认主题")]
        public string DefaultTheme { get; set; }

        /// <summary>
        /// 默认级别
        /// </summary>
        [Display(Name = "默认级别")]
        public int DefaultLevel { get; set; }

        /// <summary>
        /// 默认来自
        /// </summary>
        [Display(Name = "默认来自")]
        public string DefaultFrom { get; set; }

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
