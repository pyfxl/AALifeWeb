using AALife.Core.Domain.Messages;
using System.Data.Entity.ModelConfiguration;

namespace AALife.Core.Mapping.Messages
{
    public partial class MessageTemplateMap : EntityTypeConfiguration<MessageTemplate>
    {
        public MessageTemplateMap()
        {
            this.ToTable("bse_MessageTemplate");
            this.HasKey(p => p.Id);
            this.Property(p => p.Name).IsRequired().HasMaxLength(20);
            this.Property(p => p.SystemName).IsRequired().HasMaxLength(20);
            this.Property(p => p.Subject).IsRequired().HasMaxLength(100);
            this.Property(p => p.Body).IsRequired().HasMaxLength(1000);
        }
    }
}
