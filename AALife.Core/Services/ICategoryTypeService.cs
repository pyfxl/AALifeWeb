using AALife.Core.Domain;
using System.Collections.Generic;

namespace AALife.Core.Services
{
    public interface ICategoryTypeService : IBaseUserService<CategoryTypeTable>, IBaseService<CategoryTypeTable>
    {
        CategoryTypeTable GetCategoryType(int userId, int categoryTypeId);

        int GetMaxId(int userId);
    }
}
