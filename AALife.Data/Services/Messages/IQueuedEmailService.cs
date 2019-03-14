using AALife.Core.Services;
using AALife.Data.Domain.Messages;
using System;
using System.Collections.Generic;

namespace AALife.Data.Services.Messages
{
    public partial interface IQueuedEmailService : IBaseService<QueuedEmail>
    {
        /// <summary>
        /// Inserts a queued email
        /// </summary>
        /// <param name="queuedEmail">Queued email</param>
        void InsertQueuedEmail(QueuedEmail queuedEmail);

        /// <summary>
        /// Updates a queued email
        /// </summary>
        /// <param name="queuedEmail">Queued email</param>
        void UpdateQueuedEmail(QueuedEmail queuedEmail);

        /// <summary>
        /// Deleted a queued email
        /// </summary>
        /// <param name="queuedEmail">Queued email</param>
        void DeleteQueuedEmail(QueuedEmail queuedEmail);

        /// <summary>
        /// Deleted a queued emails
        /// </summary>
        /// <param name="queuedEmails">Queued emails</param>
        void DeleteQueuedEmails(IList<QueuedEmail> queuedEmails);

        /// <summary>
        /// Gets a queued email by identifier
        /// </summary>
        /// <param name="queuedEmailId">Queued email identifier</param>
        /// <returns>Queued email</returns>
        QueuedEmail GetQueuedEmailById(int queuedEmailId);

        /// <summary>
        /// Get queued emails by identifiers
        /// </summary>
        /// <param name="queuedEmailIds">queued email identifiers</param>
        /// <returns>Queued emails</returns>
        IList<QueuedEmail> GetQueuedEmailsByIds(int[] queuedEmailIds);

    }
}
