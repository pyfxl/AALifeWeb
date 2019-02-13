using AALife.Core.Domain.Security;
using System.Data.Entity.ModelConfiguration;

namespace AALife.Core.Mapping.Security
{
    public partial class PermissionRecordMap : EntityTypeConfiguration<PermissionRecord>
    {
        public PermissionRecordMap()
        {
            this.ToTable("bse_PermissionRecord");
            this.HasKey(pr => pr.Id);
            this.Property(pr => pr.Name).IsRequired().HasMaxLength(50);
            this.Property(pr => pr.SystemName).IsRequired().HasMaxLength(20);
            this.Property(pr => pr.Category).IsRequired().HasMaxLength(10);

            this.HasMany(pr => pr.UserRoles)
                .WithMany(cr => cr.PermissionRecords)
                .Map(m => m.ToTable("bse_PermissionRecord_Role_Mapping"));
        }
    }
}