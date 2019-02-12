using AALife.Core.Domain.Customers;
using System.Data.Entity.ModelConfiguration;

namespace AALife.Core.Mapping.Customers
{
    public partial class CustomerPasswordMap : EntityTypeConfiguration<CustomerPassword>
    {
        public CustomerPasswordMap()
        {
            this.ToTable("bse_CustomerPassword");
            this.HasKey(password => password.Id);

            this.Property(pr => pr.Password).HasMaxLength(50);
            this.Property(pr => pr.PasswordSalt).HasMaxLength(10);

            this.HasRequired(password => password.Customer)
                .WithMany()
                .HasForeignKey(password => password.CustomerId);
        }
    }
}