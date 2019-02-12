using AALife.Core.Domain.Customers;

namespace AALife.Core.Repositorys.Customers
{
    public partial class CustomerRoleRepository : EfRepository<CustomerRole>, ICustomerRoleRepository
    {
        public CustomerRoleRepository(IDbContext context) : base(context)
        {
        }
    }
}
