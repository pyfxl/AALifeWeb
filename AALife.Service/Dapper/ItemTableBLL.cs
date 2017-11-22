using AALife.Service.Model.Common;
using AALife.Service.Model.ViewModel;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

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
                param.Add("@PagePerNumber", pageModels.pageSize);
                param.Add("@HowManyItems", 0, DbType.Int32, ParameterDirection.Output);

                var lists = conn.Query<ItemTableViewModel>("GetItemListWithPage_v7", param, null, true, null, CommandType.StoredProcedure);

                count = param.Get<int>("@HowManyItems");

                return lists;
            }
        }
    }
}
