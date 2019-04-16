using System;
using System.Net;
using AALife.Core;
using AALife.Core.Domain.Configuration;
using AALife.Core.Services.Configuration;
using AALife.Core.Services.Tasks;

namespace AALife.Core.Services.Common
{
    /// <summary>
    /// Represents a task for keeping the site alive
    /// </summary>
    public partial class KeepAliveTask : ITask
    {
        private readonly ISettingService _settingService;

        public KeepAliveTask(ISettingService settingService)
        {
            this._settingService = settingService;
        }

        /// <summary>
        /// Executes a task
        /// </summary>
        public void Execute()
        {
            string url = _settingService.LoadSetting<SiteSettings>(default(Guid)).SiteUrl + "/keepalive/index";
            using (var wc = new WebClient())
            {
                wc.DownloadString(url);
            }
        }
    }
}
