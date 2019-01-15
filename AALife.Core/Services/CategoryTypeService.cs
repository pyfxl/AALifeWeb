using AALife.Core.Caching;
using AALife.Core.Domain;
using System.Collections.Generic;
using System.Linq;

namespace AALife.Core.Services
{
    public partial class CategoryTypeService : ICategoryTypeService
    {
        private readonly ICacheManager _cacheManager;
        private readonly IRepository<CategoryTypeTable> _categoryTypeRepository;
        private readonly IDbContext _dbContext;

        private const string CATEGORYTYPE_BY_UID_KEY = "aalife.categorytype.user.{0}";

        public CategoryTypeService(ICacheManager cacheManager,
            IRepository<CategoryTypeTable> categoryTypeRepository, 
            IDbContext dbContext)
        {
            this._cacheManager = cacheManager;
            this._categoryTypeRepository = categoryTypeRepository;
            this._dbContext = dbContext;
        }

        public virtual IList<CategoryTypeTable> GetAllCategoryType(int userId)
        {
            string key = string.Format(CATEGORYTYPE_BY_UID_KEY, userId);
            return _cacheManager.Get(key, () => 
            {
                var query = _categoryTypeRepository.Table;
                query = query.Where(c => c.UserID == userId && c.CategoryTypeLive == 1);
                query = query.OrderBy(c => c.CategoryTypeID);

                return query.ToList();
            });
        }

        public CategoryTypeTable GetCategoryType(int userId, int categoryTypeId)
        {
            var query = _categoryTypeRepository.Table;
            query = query.Where(c => c.UserID == userId && c.CategoryTypeID == categoryTypeId && c.CategoryTypeLive == 1);
            
            return query.FirstOrDefault();
        }
    }
}
