using AALife.Service.Models;
using System.Data.Entity;

namespace AALife.Service.Kendoui
{
    public class ItemTableService : BaseService<ItemTable>
    {
        public ItemTableService(DbContext context) 
            : base(context)
        {
        }
    }
}
