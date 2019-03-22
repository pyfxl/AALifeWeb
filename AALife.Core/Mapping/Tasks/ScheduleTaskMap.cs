using AALife.Core.Domain.Tasks;
using System.Data.Entity.ModelConfiguration;

namespace AALife.Data.Mapping.Tasks
{
    public partial class ScheduleTaskMap : EntityTypeConfiguration<ScheduleTask>
    {
        public ScheduleTaskMap()
        {
            this.ToTable("bse_ScheduleTask");
            this.HasKey(t => t.Id);
            this.Property(p => p.Name).IsRequired().HasMaxLength(20);
            this.Property(p => p.SystemName).IsRequired().HasMaxLength(20);
            this.Property(t => t.Type).IsRequired().HasMaxLength(200);
        }
    }
}