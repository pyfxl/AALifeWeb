using AALife.Service.Models;
using System.Data.Entity;

namespace AALife.Service.Kendoui
{
    public class UserTableService : BaseService<UserTable>
    {
        public UserTableService(DbContext context)
               : base(context)
        {
        }
    }
}
