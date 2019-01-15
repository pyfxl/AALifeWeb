using AALife.Core.Domain;
using System.Collections.Generic;

namespace AALife.Core.Services
{
    public interface ICategoryTypeService
    {
        IList<CategoryTypeTable> GetAllCategoryType(int userId);

        CategoryTypeTable GetCategoryType(int userId, int categoryTypeId);
    }
}
