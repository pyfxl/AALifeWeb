using AALife.Service.Models;
using Yanzi.Core.KendoDapper;

namespace AALife.Service.Dapper
{
    public class UserTableBLL : BaseBLL
    {
        public DataSourceResult GetDapperDataSource(DataSourceRequest request)
        {
            return "UserTable".ToDataSourceResult<UserTable>(request, "UserID desc");
        }
    }
}
