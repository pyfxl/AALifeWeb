using AALife.Core.Caching;
using AALife.Core.Domain.Customers;
using AALife.Core.Repositorys.Customers;

namespace AALife.Core.Services
{
    public partial class CustomerRoleService : BaseService<CustomerRole>, ICustomerRoleService
    {
        public CustomerRoleService(ICustomerRoleRepository repository,
            ICacheManager cacheManager,
            IDbContext dbContext)
            : base(repository, cacheManager, dbContext)
        {
        }
    }
}
