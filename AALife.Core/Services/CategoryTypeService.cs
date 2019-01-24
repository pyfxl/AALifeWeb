using AALife.Core.Caching;
using AALife.Core.Domain;
using System.Collections.Generic;
using System.Linq;

namespace AALife.Core.Services
{
    public partial class CategoryTypeService : BaseUserService<CategoryTypeTable>, ICategoryTypeService
    {
        public CategoryTypeService(IRepository<CategoryTypeTable> repository, 
            ICacheManager cacheManager, 
            IDbContext dbContext)
            : base(repository, cacheManager, dbContext)
        {
        }

        public CategoryTypeTable GetCategoryType(int userId, int categoryTypeId)
        {
            var query = _repository.Table;
            query = query.Where(c => c.UserId == userId && c.Live == 1 && c.CategoryTypeId == categoryTypeId);
            
            //未找到记录防止出错
            if(!query.Any())
            {
                return new CategoryTypeTable() { CategoryTypeId = 0, CategoryTypeName = "", Live = 1, UserId = userId };
            }

            return query.FirstOrDefault();
        }

        //取最大id
        public int GetMaxId(int userId)
        {
            var query = _repository.Table;
            query = query.Where(c => c.UserId == userId && c.Live == 1);

            var maxId = query.Max(a => a.CategoryTypeId);
            maxId = maxId + 1;

            return maxId % 2 == 0 ? maxId + 1 : maxId;
        }
    }
}
