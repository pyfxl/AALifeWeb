using AALife.Core.Domain.Configuration;
using System.Data.Entity.ModelConfiguration;

namespace AALife.Core.Mapping.Configuration
{
    public partial class SettingMap : EntityTypeConfiguration<Setting>
    {
        public SettingMap()
        {
            this.ToTable("bse_Setting");
            this.HasKey(s => s.Id);
            this.Property(s => s.Name).IsRequired().HasMaxLength(200);
            this.Property(s => s.Value).IsRequired().HasMaxLength(2000);
        }
    }
}