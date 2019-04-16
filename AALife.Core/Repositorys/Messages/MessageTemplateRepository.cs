﻿using AALife.Core.Domain.Messages;

namespace AALife.Core.Repositorys.Messages
{
    public partial class MessageTemplateRepository : EfRepository<MessageTemplate, int>, IMessageTemplateRepository
    {
        public MessageTemplateRepository(IDbContext context) : base(context)
        {
        }
    }
}
