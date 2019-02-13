using AALife.Core.Domain.Users;
using System.Data.Entity.ModelConfiguration;

namespace AALife.Core.Mapping.Users
{
    public partial class UserMap : EntityTypeConfiguration<UserTable>
    {
        public UserMap()
        {
            this.ToTable("tab_UserTable");
            this.HasKey(c => c.Id);
            this.Property(u => u.UserName).IsRequired().HasMaxLength(20);
            this.Property(u => u.UserPassword).IsRequired().HasMaxLength(50);
            this.Property(u => u.PasswordSalt).HasMaxLength(10);
            this.Property(u => u.UserNickName).HasMaxLength(50);
            this.Property(u => u.UserEmail).HasMaxLength(200);
            this.Property(u => u.UserImage).HasMaxLength(200);
            this.Property(u => u.UserTheme).HasMaxLength(10);
            this.Property(u => u.UserFrom).IsRequired().HasMaxLength(10);

            this.HasMany(c => c.UserRoles)
                .WithMany()
                .Map(m => m.ToTable("tab_User_Role_Mapping"));

        }
    }
}