using AALife.Service.Domain.Common;
using AALife.Service.Domain.ViewModel;
using AALife.Service.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Dynamic;
using Yanzi.Core.KendoDapper;

namespace AALife.Service.Dapper
{
    public class ItemTableBLL : BaseBLL
    {
        /// <summary>
        /// 获取消费列表
        /// </summary>
        /// <param name="pageModels"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public IEnumerable<ItemTableViewModel> GetItemTable(QueryPageModel pageModels, out int count)
        {            
            using (var conn = OpenConnection())
            {
                var param = new DynamicParameters();
                param.Add("@UserID", pageModels.userId);
                param.Add("@DateType", pageModels.dateType);
                param.Add("@BeginDate", pageModels.startDate);
                param.Add("@EndDate", pageModels.endDate);
                param.Add("@Keywords", pageModels.keySearch);
                param.Add("@Sorts", pageModels.sortString);
                param.Add("@PageNumber", pageModels.page);
                param.Add("@PagePerNumber", pageModels.pageSize == 0 ? pageModels.rows : pageModels.pageSize);
                param.Add("@HowManyItems", 0, DbType.Int32, ParameterDirection.Output);

                var lists = conn.Query<ItemTableViewModel>("GetItemListWithPage_v7", param, null, true, null, CommandType.StoredProcedure);

                count = param.Get<int>("@HowManyItems");

                return lists;
            }
        }

        public DataSourceResult GetDapperDataSource(DataSourceRequest request)
        {
            return request.ToDataSourceResult<ItemTableView2019, ItemTable>(base.sqlConnection, "ItemID desc");
        }

        public IEnumerable<UserCategoryTable> GetUserCategoryTable(int userId)
        {
            using(var conn = new SqlConnection(base.sqlConnection))
            {
                return conn.Query<UserCategoryTable>(string.Format(@"SELECT * FROM CategoryTypeTableFunc_v5({0}) WHERE CategoryTypeLive = 1", userId));
            }
        }
    }
}
