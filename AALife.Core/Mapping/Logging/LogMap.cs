using AALife.Core.Domain.Logging;
using System.Data.Entity.ModelConfiguration;

namespace AALife.Core.Mapping.Logging
{
    public partial class LogMap : EntityTypeConfiguration<Log>
    {
        public LogMap()
        {
            this.ToTable("bse_Log");
            this.HasKey(l => l.Id);
            this.Property(l => l.ShortMessage).IsRequired();
            this.Property(l => l.IpAddress).HasMaxLength(200);

            this.Ignore(l => l.LogLevel);

            //this.HasOptional(l => l.User)
            //    .WithMany()
            //    .HasForeignKey(l => l.UserId)
            //.WillCascadeOnDelete(true);

        }
    }
}