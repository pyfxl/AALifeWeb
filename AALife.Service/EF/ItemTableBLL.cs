using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Linq.Dynamic;
using Mapster;
using AALife.Service.Model.ViewModel;
using AALife.Service.Model.Query;
using AALife.Service.Models;

namespace AALife.Service.EF
{
    /// <summary>
    /// 消费表业务逻辑
    /// </summary>
    public class ItemTableBLL : BaseBLL
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ItemTableBLL()
        {
            TypeAdapterConfig<Models.ItemTable, ItemTableViewModel>.NewConfig()
                .Map(dest => dest.ItemID, src => src.ItemID)
                .Map(dest => dest.ItemName, src => src.ItemName)
                .Map(dest => dest.CategoryTypeID, src => src.CategoryTypeID)
                .Map(dest => dest.ItemPrice, src => src.ItemPrice)
                .Map(dest => dest.ItemBuyDate, src => src.ItemBuyDate)
                .Map(dest => dest.UserID, src => src.UserID)
                .Map(dest => dest.Recommend, src => src.Recommend)
                .Map(dest => dest.ModifyDate, src => src.ModifyDate)
                .Map(dest => dest.Synchronize, src => src.Synchronize)
                .Map(dest => dest.ItemAppID, src => src.ItemAppID)
                .Map(dest => dest.RegionID, src => src.RegionID)
                .Map(dest => dest.RegionType, src => src.RegionType)
                .Map(dest => dest.ItemType, src => src.ItemType)
                .Map(dest => dest.ZhuanTiID, src => src.ZhuanTiID)
                .Map(dest => dest.CardID, src => src.CardID)
                .Map(dest => dest.ItemTypeName, src => src.ItemTypeTable.ItemTypeName)
                .Map(dest => dest.CardName, src => src.CardID)
                .Compile();
        }

        /// <summary>
        /// 获取消费列表
        /// </summary>
        /// <param name="pageModels"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public IEnumerable<ItemTableViewModel> GetItemTable(QueryPageModel pageModels, out int count)
        {
            using (var db = new AALifeDbContext())
            {
                //默认
                var lists = db.Set<Models.ItemTable>()
                    .AsNoTracking()
                    .Where(a => a.ItemBuyDate >= pageModels.startDate && a.ItemBuyDate <= pageModels.endDate);

                //关键字
                if (pageModels.keySearch != null && pageModels.keySearch.Any())
                {
                    lists = db.Set<Models.ItemTable>().AsNoTracking().Where(a => a.ItemName.Contains(pageModels.keySearch));
                }

                //用户id
                if (pageModels.userId.GetValueOrDefault() > 0)
                {
                    lists = lists.Where(a => a.UserID == pageModels.userId);
                }

                #region groupjoin old
                ////结果
                //var viewModel = lists
                //    .Include(a => a.ItemTypeTable)
                //    .Include(a => a.CategoryTypeTable)
                //    .Include(a => a.RegionTypeTable)
                //    .GroupJoin(db.CardTable, a => new { a.UserID, a.CardID.Value }, b => new { b.UserID, b.CDID.Value }, (a, b) => new
                //    {
                //        ItemTable = a,
                //        CardTable = b
                //    })
                //    .GroupJoin(db.ZhuanTiTable, a => new { a.ItemTable.UserID, a.ItemTable.ZhuanTiID.Value }, b => new { b.UserID, b.ZTID.Value }, (a, b) => new
                //    {
                //        ItemTable = a.ItemTable,
                //        CardTable = a.CardTable,
                //        ZhuanTiTable = b
                //    })
                //    .GroupJoin(db.UserCategoryTable, a => new { a.ItemTable.UserID, a.ItemTable.CategoryTypeID }, b => new { b.UserID, b.CategoryTypeID }, (a, b) => new
                //    {
                //        ItemTable = a.ItemTable,
                //        CardTable = a.CardTable,
                //        ZhuanTiTable = a.ZhuanTiTable,
                //        UserCategoryTable = b
                //    })
                //    .Select(a => new ItemTableViewModel
                //    {
                //        ItemID = a.ItemTable.ItemID,
                //        ItemName = a.ItemTable.ItemName,
                //        CategoryTypeID = a.ItemTable.CategoryTypeID,
                //        CategoryTypeName = a.UserCategoryTable.FirstOrDefault().CategoryTypeName ?? a.ItemTable.CategoryTypeTable.CategoryTypeName,
                //        ItemPrice = a.ItemTable.ItemPrice,
                //        ItemBuyDate = a.ItemTable.ItemBuyDate,
                //        UserID = a.ItemTable.UserID,
                //        Recommend = a.ItemTable.Recommend,
                //        ModifyDate = a.ItemTable.ModifyDate,
                //        Synchronize = a.ItemTable.Synchronize,
                //        ItemAppID = a.ItemTable.ItemAppID,
                //        RegionID = a.ItemTable.RegionID,
                //        RegionType = a.ItemTable.RegionType,
                //        RegionTypeName = a.ItemTable.RegionTypeTable.RegionTypeName,
                //        ItemType = a.ItemTable.ItemType,
                //        ItemTypeName = a.ItemTable.ItemTypeTable.ItemTypeName,
                //        ZhuanTiID = a.ItemTable.ZhuanTiID,
                //        ZhuanTiName = a.ZhuanTiTable.FirstOrDefault().ZhuanTiName,
                //        CardID = a.ItemTable.CardID,
                //        CardName = a.CardTable.FirstOrDefault().CardName ?? Constant.CardName
                //    });
                #endregion

                //结果
                var viewModel = lists
                    .Include(a => a.ItemTypeTable)
                    .ProjectToType<ItemTableViewModel>();

                //排序
                if (pageModels.sort != null && pageModels.sort.Count > 0)
                {
                    viewModel = viewModel.OrderBy(pageModels.sortString);
                }
                else
                {
                    viewModel = viewModel.OrderByDescending(a => a.ItemID);
                }

                //总数
                count = viewModel.Count();

                //返回
                return viewModel.Skip(pageModels.skip).Take(pageModels.take).ToList();
            }
        }
                
    }
}
