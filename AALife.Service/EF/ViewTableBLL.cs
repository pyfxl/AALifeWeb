using AALife.Service.Domain.Common;
using AALife.Service.Domain.ViewModel;
using AALife.Service.Models;
using Mapster;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using System.Data.Entity.Core.Objects;
using Kendo.DynamicLinq;

namespace AALife.Service.EF
{
    public class ViewTableBLL : BaseBLL
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ViewTableBLL()
        {
            TypeAdapterConfig<ViewTable, ViewTableViewModel>.NewConfig()
                .Map(dest => dest.ViewID, src => src.ViewID)
                .Map(dest => dest.PageID, src => src.PageID)
                .Map(dest => dest.UserID, src => src.UserID)
                .Map(dest => dest.DateStart, src => src.DateStart)
                .Map(dest => dest.DateEnd, src => src.DateEnd)
                .Map(dest => dest.ViewSeconds, src => DbFunctions.DiffSeconds(src.DateStart, src.DateEnd))
                .Map(dest => dest.Portal, src => src.Portal)
                .Map(dest => dest.Version, src => src.Version)
                .Map(dest => dest.Browser, src => src.Browser)
                .Map(dest => dest.Width, src => src.Width)
                .Map(dest => dest.Height, src => src.Height)
                .Map(dest => dest.IP, src => src.IP)
                .Map(dest => dest.Synchronize, src => src.Synchronize)
                .Map(dest => dest.Remark, src => src.Remark)
                .Map(dest => dest.Network, src => src.Network)
                .Map(dest => dest.CreateDate, src => src.CreateDate)
                .Map(dest => dest.PageName, src => src.ViewPageTable.PageName)
                .Map(dest => dest.PageTitle, src => src.ViewPageTable.PageTitle)
                .Compile();
        }

        /// <summary>
        /// 获取消费列表
        /// </summary>
        /// <param name="query"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public DataSourceResult GetViewTable(int take, int skip, IEnumerable<Sort> sort, Filter filter, IEnumerable<Aggregator> aggregates)
        {
            using (var db = new AALifeDbContext())
            {
                //默认
                var lists = db.Set<ViewTable>()
                    .AsNoTracking()
                    .AsQueryable();
                
                //结果
                var result = lists
                    .Include(a => a.ViewPageTable)
                    .ProjectToType<ViewTableViewModel>();

                //排序
                result = result.OrderByDescending(a => a.ViewID);

                //返回
                return result.ToDataSourceResult(take, skip, sort, filter, aggregates);
            }
        }

        /// <summary>
        /// 页面表
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public IEnumerable<ViewPageTable> GetViewPageTable(out int count)
        {
            using(var db = new AALifeDbContext())
            {
                //默认
                var lists = db.Set<ViewPageTable>()
                    .AsNoTracking()
                    .AsQueryable();

                //总数
                count = lists.Count();

                return lists.ToList();
            }
        }
    }
}
