using System;
using AALife.Core.Domain.Configuration;
using AALife.Core.Services.Configuration;
using AALife.Core.Services.Logging;
using AALife.Core.Services.Tasks;

namespace AALife.Core.Services.Messages
{
    /// <summary>
    /// Represents a task for sending queued message 
    /// </summary>
    public partial class QueuedMessagesSendTask : ITask
    {
        private readonly IQueuedEmailService _queuedEmailService;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        private readonly ISettingService _settingService;

        public QueuedMessagesSendTask(IQueuedEmailService queuedEmailService,
            IEmailSender emailSender, 
            ILogger logger,
            ISettingService settingService)
        {
            this._queuedEmailService = queuedEmailService;
            this._emailSender = emailSender;
            this._logger = logger;
            this._settingService = settingService;
        }

        /// <summary>
        /// Executes a task
        /// </summary>
        public virtual void Execute()
        {
            var emailAccount = _settingService.LoadSetting<CommonSettings>(default(Guid));

            var maxTries = 30;
            var queuedEmails = _queuedEmailService.SearchEmails(null, null, null, null,
                true, true, maxTries, false, 0, 500);
            foreach (var queuedEmail in queuedEmails)
            {
                try
                {
                    _emailSender.SendEmail(emailAccount, 
                       queuedEmail.Subject, 
                       queuedEmail.Body,
                       queuedEmail.From, 
                       queuedEmail.FromName, 
                       queuedEmail.To, 
                       queuedEmail.ToName);

                    queuedEmail.SentDate = DateTime.Now;
                }
                catch (Exception exc)
                {
                    _logger.Error(string.Format("Error sending e-mail. {0}", exc.Message), exc);
                }
                finally
                {
                    queuedEmail.SentTries = queuedEmail.SentTries + 1;
                    _queuedEmailService.UpdateQueuedEmail(queuedEmail);
                }
            }
        }
    }
}
