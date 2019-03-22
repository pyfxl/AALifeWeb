using AALife.Core.Domain.Messages;
using System.Data.Entity.ModelConfiguration;

namespace AALife.Core.Mapping.Messages
{
    public partial class QueuedEmailMap : EntityTypeConfiguration<QueuedEmail>
    {
        public QueuedEmailMap()
        {
            this.ToTable("bse_QueuedEmail");
            this.HasKey(p => p.Id);
            this.Property(p => p.From).IsRequired().HasMaxLength(200);
            this.Property(p => p.FromName).HasMaxLength(20);
            this.Property(p => p.To).IsRequired().HasMaxLength(200);
            this.Property(p => p.ToName).HasMaxLength(20);
            this.Property(p => p.Subject).IsRequired().HasMaxLength(100);
            this.Property(p => p.Body).IsRequired().HasMaxLength(1000);
        }
    }
}
