using AALife.Core.Domain.Logging;
using System.Data.Entity.ModelConfiguration;

namespace AALife.Core.Mapping.Logging
{
    public partial class ActivityLogMap : EntityTypeConfiguration<ActivityLog>
    {
        public ActivityLogMap()
        {
            this.ToTable("bse_ActivityLog");
            this.HasKey(al => al.Id);
            this.Property(al => al.Comment).IsRequired();
            this.Property(al => al.IpAddress).HasMaxLength(200);

            //this.HasRequired(al => al.ActivityLogType)
            //    .WithMany()
            //    .HasForeignKey(al => al.ActivityLogTypeId);

            //this.HasRequired(al => al.User)
            //    .WithMany()
            //    .HasForeignKey(al => al.UserId);
        }
    }
}
