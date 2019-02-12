using AALife.Core.Domain.Customers;
using System.Data.Entity.ModelConfiguration;

namespace AALife.Core.Mapping.Customers
{
    public partial class CustomerRoleMap : EntityTypeConfiguration<CustomerRole>
    {
        public CustomerRoleMap()
        {
            this.ToTable("bse_CustomerRole");
            this.HasKey(cr => cr.Id);
            this.Property(cr => cr.Name).IsRequired().HasMaxLength(20);
            this.Property(cr => cr.SystemName).HasMaxLength(20);
        }
    }
}