using AALife.Core.Configuration;
using System.ComponentModel.DataAnnotations;

namespace AALife.Data.Domain
{
    public class UserSettings : ISettings
    {
        /// <summary>
        /// 每页记录数
        /// </summary>
        public int? PageNumber { get; set; }

        /// <summary>
        /// 工作日
        /// </summary>
        public int? UserWorkDay { get; set; }

        /// <summary>
        /// 背景Id
        /// </summary>
        public int BackgroundImageId { get; set; }
    }
}
