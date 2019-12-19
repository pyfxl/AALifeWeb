using AALife.Service.Models;
using System.Linq;
using Yanzi.Core.KendoDapper;

namespace AALife.Service.Dapper
{
    public class UserTableBLL : BaseBLL
    {
        public DataSourceResult GetDapperDataSource(DataSourceRequest request)
        {
            var results = new AALifeDbContext().UserTable.AsQueryable();
            return results.ToDataSourceResult(request, a => new { a.UserID });
        }
    }
}
