using AALife.Core.Domain.Customers;
using System.Data.Entity.ModelConfiguration;

namespace AALife.Core.Mapping.Customers
{
    public partial class CustomerMap : EntityTypeConfiguration<Customer>
    {
        public CustomerMap()
        {
            this.ToTable("bse_Customer");
            this.HasKey(c => c.Id);
            this.Property(u => u.Username).HasMaxLength(20);
            this.Property(u => u.Email).HasMaxLength(200);

            this.Property(pr => pr.Password).HasMaxLength(50);
            this.Property(pr => pr.PasswordSalt).HasMaxLength(10);

            this.HasMany(c => c.CustomerRoles)
                .WithMany()
                .Map(m => m.ToTable("bse_Customer_CustomerRole_Mapping"));
        }
    }
}