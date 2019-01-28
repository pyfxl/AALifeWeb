using AALife.Core;
using AALife.Core.Caching;
using AALife.Data.Domain;

namespace AALife.Data.Services
{
    public partial class CategoryTypeService : BaseUserService<CategoryTypeTable>, ICategoryTypeService
    {
        public CategoryTypeService(IRepository<CategoryTypeTable> repository, 
            ICacheManager cacheManager, 
            IDbContext dbContext)
            : base(repository, cacheManager, dbContext)
        {
        }

    }
}
