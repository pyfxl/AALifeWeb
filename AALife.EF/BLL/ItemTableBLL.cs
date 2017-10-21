using AALife.EF.Enum;
using AALife.EF.Models;
using AALife.EF.ViewModel;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace AALife.EF.BLL
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
            AutoMapper.Mapper.Initialize(m => m.CreateMap<ItemTable, ItemTableViewModel>()
                .ForMember(vm => vm.ItemID, dto => dto.MapFrom(mo => mo.ItemID))
                .ForMember(vm => vm.ItemName, dto => dto.MapFrom(mo => mo.ItemName))
                .ForMember(vm => vm.CategoryTypeID, dto => dto.MapFrom(mo => mo.CategoryTypeID))
                .ForMember(vm => vm.ItemPrice, dto => dto.MapFrom(mo => mo.ItemPrice))
                .ForMember(vm => vm.ItemBuyDate, dto => dto.MapFrom(mo => mo.ItemBuyDate))
                .ForMember(vm => vm.UserID, dto => dto.MapFrom(mo => mo.UserID))
                .ForMember(vm => vm.UserName, dto => dto.MapFrom(mo => mo.UserTable.UserName))
                .ForMember(vm => vm.Recommend, dto => dto.MapFrom(mo => mo.Recommend))
                .ForMember(vm => vm.ModifyDate, dto => dto.MapFrom(mo => mo.ModifyDate))
                .ForMember(vm => vm.Synchronize, dto => dto.MapFrom(mo => mo.Synchronize))
                .ForMember(vm => vm.ItemAppID, dto => dto.MapFrom(mo => mo.ItemAppID))
                .ForMember(vm => vm.RegionID, dto => dto.MapFrom(mo => mo.RegionID))
                .ForMember(vm => vm.RegionType, dto => dto.MapFrom(mo => mo.RegionType))
                .ForMember(vm => vm.RegionTypeName, dto => dto.MapFrom(mo => mo.RegionTypeTable.RegionTypeName))
                .ForMember(vm => vm.ItemType, dto => dto.MapFrom(mo => mo.ItemType))
                .ForMember(vm => vm.ItemTypeName, dto => dto.MapFrom(mo => mo.ItemTypeTable.ItemTypeName))
                .ForMember(vm => vm.ZhuanTiID, dto => dto.MapFrom(mo => mo.ZhuanTiID))
                .ForMember(vm => vm.CardID, dto => dto.MapFrom(mo => mo.CardID)).ReverseMap());
        }
        
        /// <summary>
        /// 获取消费列表
        /// </summary>
        /// <param name="pageModels"></param>
        /// <returns></returns>
        public IEnumerable<ItemTableViewModel> GetItemTable(QueryPageModel pageModels, out int count)
        {
            using (var db = new AALifeDbContext())
            {
                var lists = db.Set<ItemTable>().AsNoTracking().Where(a => a.ItemBuyDate >= pageModels.startDate && a.ItemBuyDate <= pageModels.endDate);

                //关键字
                if (pageModels.keySearch != null && pageModels.keySearch != "")
                {
                    lists = db.Set<ItemTable>().Where(a => a.ItemName.Contains(pageModels.keySearch));
                }

                //用户id
                if (pageModels.userId != null && pageModels.userId > 0)
                {
                    lists = lists.Where(a => a.UserID == pageModels.userId);
                }

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

                //结果
                var viewModel = lists
                    .Include(a => a.ItemTypeTable)
                    .Include(a => a.CategoryTypeTable)
                    .Include(a => a.RegionTypeTable)
                    .Include(a => a.UserTable)
                    .Select(a => new ItemTableViewModel
                    {
                        ItemID = a.ItemID,
                        ItemName = a.ItemName,
                        CategoryTypeID = a.CategoryTypeID,
                        ItemPrice = a.ItemPrice,
                        ItemBuyDate = a.ItemBuyDate,
                        UserID = a.UserID,
                        UserName = a.UserTable.UserNickName ?? a.UserTable.UserName,
                        Recommend = a.Recommend,
                        ModifyDate = a.ModifyDate,
                        Synchronize = a.Synchronize,
                        ItemAppID = a.ItemAppID,
                        RegionID = a.RegionID,
                        RegionType = a.RegionType,
                        RegionTypeName = a.RegionTypeTable.RegionTypeName,
                        ItemType = a.ItemType,
                        ItemTypeName = a.ItemTypeTable.ItemTypeName,
                        ZhuanTiID = a.ZhuanTiID,
                        CardID = a.CardID
                    });

                //默认排序
                var _lists = viewModel.OrderByDescending(a => a.ItemID);

                //排序
                if (pageModels.sort != null && pageModels.sort.Count > 0)
                {
                    int n = 0;
                    pageModels.sort.ForEach(a => {
                        n++;
                        if (n == 1)
                        {
                            _lists = _lists.OrderBy(a.field, a.dir == SortEnum.desc.ToString());
                        }
                        else
                        {
                            _lists = _lists.ThenBy(a.field, a.dir == SortEnum.desc.ToString());
                        }
                    });
                }

                //总数
                count = _lists.Count();

                //返回
                return _lists.Skip(pageModels.skip).Take(pageModels.take).ToList();
            }
        }
        
        /// <summary>
        /// 获取总数量
        /// </summary>
        /// <param name="pageModels"></param>
        /// <returns></returns>
        public int GetItemTableTotal(QueryPageModel pageModels)
        {
            using (IDbConnection conn = OpenConnection())
            {
                string sql = "select count(0) from ItemTable where ItemBuyDate between @start and @end";
                int count = conn.ExecuteScalar<int>(sql, new { start = pageModels.startDate, end = pageModels.endDate });

                if (pageModels.keySearch != null && pageModels.keySearch != "")
                {
                    sql = "select count(0) from ItemTable where ItemName like @key";
                    count = conn.ExecuteScalar<int>(sql, new { key = "%" + pageModels.keySearch + "%" });
                }

                if (pageModels.userId != null && pageModels.userId > 0)
                {
                    sql = "select count(0) from ItemTable where ItemBuyDate between @start and @end and UserID = @uid";
                    count = conn.ExecuteScalar<int>(sql, new { start = pageModels.startDate, end = pageModels.endDate, uid = pageModels.userId });
                }

                return count;
            }
        }
        
    }
}
