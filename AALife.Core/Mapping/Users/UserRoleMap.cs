using AALife.Core.Domain.Users;
using System.Data.Entity.ModelConfiguration;

namespace AALife.Core.Mapping.Users
{
    public partial class UserRoleMap : EntityTypeConfiguration<UserRole>
    {
        public UserRoleMap()
        {
            this.ToTable("tab_UserRole");
            this.HasKey(cr => cr.Id);
            this.Property(cr => cr.Name).IsRequired().HasMaxLength(50);
            this.Property(cr => cr.SystemName).HasMaxLength(20);
        }
    }
}