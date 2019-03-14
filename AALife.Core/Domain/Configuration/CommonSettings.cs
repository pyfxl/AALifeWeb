using AALife.Core.Configuration;
using System.ComponentModel.DataAnnotations;

namespace AALife.Core.Domain.Configuration
{
    public class CommonSettings : ISettings
    {

        #region 邮件配置

        [Display(Name = "邮件地址")]
        public string Email { get; set; }

        [Display(Name = "邮件友好名称")]
        public string DisplayName { get; set; }

        [Display(Name = "主机")]
        public string Host { get; set; }

        [Display(Name = "端口")]
        public int Port { get; set; }

        [Display(Name = "用户名")]
        public string Username { get; set; }

        [Display(Name = "密码")]
        public string Password { get; set; }

        [Display(Name = "SSL")]
        public bool EnableSsl { get; set; }

        [Display(Name = "使用默认证书")]
        public bool UseDefaultCredentials { get; set; }

        [Display(Name = "测试邮件地址")]
        public string SendTestEmailTo { get; set; }

        #endregion

        /// <summary>
        /// EncryptionKey
        /// </summary>
        public string EncryptionKey { get; set; }
    }
}
