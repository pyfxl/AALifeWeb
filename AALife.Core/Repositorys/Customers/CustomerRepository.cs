using AALife.Core.Domain.Customers;

namespace AALife.Core.Repositorys.Customers
{
    public partial class CustomerRepository : EfRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(IDbContext context) : base(context)
        {
        }
    }
}
