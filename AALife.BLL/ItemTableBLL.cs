using AALife.DAL;
using AALife.Model;
using System;
using System.Data;
using System.Transactions;

namespace AALife.BLL
{
    public class ItemTableBLL
    {
        private static readonly ItemTableDAL dal = new ItemTableDAL();
        private static readonly UserTableDAL user_dal = new UserTableDAL();

        /// <summary>
        /// 根据日期取商品列表
        /// </summary>
        public DataTable GetItemList(int userId, DateTime itemBuyDate)
        {
            return dal.GetItemList(userId, itemBuyDate);
        }

        /// <summary>
        /// 根据日期范围取商品列表
        /// </summary>
        public DataTable GetItemList(int userId, DateTime beginDate, DateTime endDate)
        {
            return dal.GetItemList(userId, beginDate, endDate);
        }
        
        /// <summary>
        /// 根据日期范围取商品统计
        /// </summary>
        public DataTable GetItemListByGroup(int userId, DateTime beginDate, DateTime endDate, string myQuery, string myLabel, string mySort)
        {
            return dal.GetItemListByGroup(userId, beginDate, endDate, myQuery, myLabel, mySort);
        }

        /// <summary>
        /// 根据日期范围取商品比较
        /// </summary>
        public DataTable GetItemListByCompare(int userId, DateTime beginDate, DateTime endDate, DateTime beginDate2, DateTime endDate2, string myQuery, string myLabel, string mySort)
        {
            return dal.GetItemListByCompare(userId, beginDate, endDate, beginDate2, endDate2, myQuery, myLabel, mySort);
        }

        /// <summary>
        /// 取下拉数据
        /// </summary>
        public DataTable GetListBoxData(int userId, string myLabel, string myValue, string mySort)
        {
            return dal.GetListBoxData(userId, myLabel, myValue, mySort);
        }

        /// <summary>
        /// 根据关键字取商品列表
        /// </summary>
        public DataTable GetItemListByKeywords(int userId, string keywords)
        {
            return dal.GetItemListByKeywords(userId, keywords);
        }

        /// <summary>
        /// 根据时间段取商品列表
        /// </summary>
        public DataTable GetItemListByDate(DateTime beginDate, DateTime endDate)
        {
            return dal.GetItemListByDate(beginDate, endDate);
        }

        /// <summary>
        /// 根据用户ID取商品列表
        /// </summary>
        public DataTable GetItemListByUserId(int userId)
        {
            return dal.GetItemListByUserId(userId);
        }

        /// <summary>
        /// 根据关键字取所有商品列表
        /// </summary>
        public DataTable GetItemListAllByKeywords(string keywords)
        {
            return dal.GetItemListAllByKeywords(keywords);
        }

        /// <summary>
        /// 取同步商品列表
        /// </summary>
        public DataTable GetItemListWithSync(int userId)
        {
            return dal.GetItemListWithSync(userId);
        }

        /// <summary>
        /// 根据商品ID取商品
        /// </summary>
        public ItemInfo GetItemByItemId(int itemId)
        {
            return dal.GetItemByItemId(itemId);
        }

        /// <summary>
        /// 检查商品是否存在
        /// </summary>
        public bool CheckItemExists(ItemInfo item)
        {
            return dal.CheckItemExists(item);
        }

        /// <summary>
        /// 根据区间ID取商品列表
        /// </summary>
        public DataTable GetItemListByRegionId(int userId, int regionId)
        {
            return dal.GetItemListByRegionId(userId, regionId);
        }

        /// <summary>
        /// 根据专题ID取商品列表
        /// </summary>
        public DataTable GetItemListByZhuanTiId(int userId, int ztId)
        {
            return dal.GetItemListByZhuanTiId(userId, ztId);
        }

        /// <summary>
        /// 根据专题ID取所有商品列表
        /// </summary>
        public DataTable GetItemListAllByZhuanTiId(int userId, int ztId)
        {
            return dal.GetItemListAllByZhuanTiId(userId, ztId);
        }

        /// <summary>
        /// 根据钱包ID取商品列表
        /// </summary>
        public DataTable GetItemListByCardId(int userId, int cardId)
        {
            return dal.GetItemListByCardId(userId, cardId);
        }

        /// <summary>
        /// 根据类别ID取商品列表
        /// </summary>
        public DataTable GetItemListByCategoryId(int userId, int catTypeId)
        {
            return dal.GetItemListByCategoryId(userId, catTypeId);
        }

        /// <summary>
        /// 取商品分类列表
        /// </summary>
        public DataTable GetItemTypeList()
        {
            return dal.GetItemTypeList();
        }

        /// <summary>
        /// 取商品区间列表
        /// </summary>
        public DataTable GetRegionTypeList()
        {
            return dal.GetRegionTypeList();
        }

        /// <summary>
        /// 插入商品
        /// </summary>
        public bool InsertItem(ItemInfo item)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                bool success = dal.InsertItem(item);
                UserInfo user = user_dal.GetUserByUserId(item.UserID);

                if (user.IsUpdate == 0 && success)
                {
                    success = UpdateBalanceMoney(user.UserID, item.ItemID, item.ItemType, item.ItemPrice, "insert", (int)item.CardID);
                }

                ts.Complete();

                return success;
            }
        }
        
        /// <summary>
        /// 同步插入商品
        /// </summary>
        public bool InsertItemWithSync(ItemInfo item)
        {
            return dal.InsertItem(item);
        }

        /// <summary>
        /// 插入区间消费商品
        /// </summary>
        public bool InsertItem(ItemInfo item, DateTime itemBuyDate1, DateTime itemBuyDate2)
        {
            string regionType = item.RegionType;
            item.RegionID = GetMaxRegionId(item.UserID);
            int monthRegion = GetMonthRegion(regionType, itemBuyDate1, itemBuyDate2);
            string workDay = GetUserWorkDay(item.UserID);

            //检查区间范围，不合理会抛异常
            CheckRegion(regionType, itemBuyDate1, itemBuyDate2);

            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    for (int i = 0; i <= monthRegion; i++)
                    {
                        DateTime? date = GetItemBuyDate(i, regionType, itemBuyDate1, workDay);
                        if (date == null)
                        {
                            continue;
                        }

                        item.ItemBuyDate = (DateTime)date;
                        InsertItem(item);
                    }

                    ts.Complete();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 检查区间范围
        /// </summary>
        public void CheckRegion(string regionType, DateTime itemBuyDate1, DateTime itemBuyDate2)
        {
            int monthRegion = GetMonthRegion(regionType, itemBuyDate1, itemBuyDate2);
            int maxRegion = 0;
            string regionStr = "";
            GetMaxRegion(regionType, ref maxRegion, ref regionStr);

            if (monthRegion <= 0 || monthRegion >= maxRegion)
            {
                throw new Exception("区间日期设置不合理！" + regionStr);
            }
        }

        /// <summary>
        /// 取临时区间列表
        /// </summary>
        public DataTable GetRegionList(ItemInfo item, string itemTypeText, DateTime itemBuyDate1, DateTime itemBuyDate2)
        {
            DataTable list = new DataTable();
            list.Columns.Add("消费分类", typeof(string));
            list.Columns.Add("商品名称", typeof(string));
            list.Columns.Add("商品价格", typeof(string));
            list.Columns.Add("购买日期", typeof(string));

            string regionType = item.RegionType;
            int monthRegion = GetMonthRegion(regionType, itemBuyDate1, itemBuyDate2); 
            string workDay = GetUserWorkDay(item.UserID);

            for (int i = 0; i <= monthRegion; i++)
            {
                DateTime? date = GetItemBuyDate(i, regionType, itemBuyDate1, workDay);
                if (date == null)
                {
                    continue;
                }

                list.Rows.Add(itemTypeText, item.ItemName, item.ItemPrice.ToString("N2"), ((DateTime)date).ToString("yyyy-MM-dd"));
            }

            return list;
        }

        /// <summary>
        /// 修改商品
        /// </summary>
        public bool UpdateItem(ItemInfo item)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                bool success = true;
                UserInfo user = user_dal.GetUserByUserId(item.UserID);

                if (user.IsUpdate == 0)
                {
                    success = UpdateBalanceMoney(user.UserID, item.ItemID, item.ItemType, item.ItemPrice, "update", (int)item.CardID);
                }

                if (success)
                {
                    success = dal.UpdateItem(item);
                }

                if (user.IsUpdate == 0 && success)
                {
                    success = UpdateBalanceMoney(user.UserID, item.ItemID, item.ItemType, item.ItemPrice, "insert", (int)item.CardID);
                }

                ts.Complete();

                return success;
            }
        }
        
        /// <summary>
        /// 同步修改商品
        /// </summary>
        public bool UpdateItemWithSync(ItemInfo item)
        {
            return dal.UpdateItem(item);
        }

        /// <summary>
        /// 删除商品
        /// </summary>
        public bool DeleteItem(int userId, int itemId, int itemAppId)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                bool success = true;

                UserInfo user = user_dal.GetUserByUserId(userId);

                if (user.IsUpdate == 0)
                {
                    ItemInfo item = GetItemByItemId(itemId);
                    success = UpdateBalanceMoney(user.UserID, item.ItemID, item.ItemType, item.ItemPrice, "delete", (int)item.CardID);
                }

                if (success)
                {
                    dal.DeleteItem(userId, itemId, itemAppId);
                }

                ts.Complete();

                return success;
            }
        }

        /// <summary>
        /// 取最大区间ID
        /// </summary>
        public int GetMaxRegionId(int userId)
        {
            return dal.GetMaxRegionId(userId);
        }

        //取区间最大有效数
        private void GetMaxRegion(string regionType, ref int maxRegion, ref string regionStr)
        {
            switch (regionType)
            {
                case "d":
                case "b":
                    maxRegion = 92;
                    regionStr = "应小于3个月。";
                    break;
                case "w":
                    maxRegion = (int)Math.Floor(92 / 7.0);
                    regionStr = "应小于3个月。";
                    break;
                case "j":
                    maxRegion = 12;
                    regionStr = "应小于3年。";
                    break;
                case "m":
                    maxRegion = 36;
                    regionStr = "应小于3年。";
                    break;
                case "y":
                    maxRegion = 15;
                    regionStr = "应小于15年。";
                    break;
            }
        }

        //取购买日期
        private DateTime? GetItemBuyDate(int i, string regionType, DateTime itemBuyDate1, string workDay)
        {
            DateTime? itemBuyDate = null;

            switch (regionType)
            {
                case "d":
                    itemBuyDate = itemBuyDate1.AddDays(i);
                    break;
                case "w":
                    itemBuyDate = itemBuyDate1.AddDays(i * 7);
                    break;
                case "m":
                    itemBuyDate = itemBuyDate1.AddMonths(i);
                    break;
                case "j":
                    itemBuyDate = itemBuyDate1.AddMonths(i * 3);
                    break;
                case "y":
                    itemBuyDate = itemBuyDate1.AddYears(i);
                    break;
                case "b":
                    DateTime curDate = itemBuyDate1.AddDays(i);
                    if (IsWorkDay(curDate, workDay))
                    {
                        itemBuyDate = curDate;
                    }
                    break;
            }

            return itemBuyDate;
        }

        //取两日日期的区间最大数
        private int GetMonthRegion(string retionType, DateTime date1, DateTime date2)
        {
            int result = 0;

            switch (retionType)
            {
                case "d":
                case "b":
                    result = ((TimeSpan)(date2 - date1)).Days;
                    break;
                case "w":
                    result = (int)Math.Floor(((TimeSpan)(date2 - date1)).Days / 7.0);
                    break;
                case "m":
                    result = ((date2.Year - date1.Year) * 12) + (date2.Month - date1.Month);
                    break;
                case "j":
                    result = (4 * (date2.Year - date1.Year)) + ((int)Math.Ceiling(date2.Month / 3.0)) - ((int)Math.Ceiling(date1.Month / 3.0));
                    break;
                case "y":
                    result = (date2.Year - date1.Year);
                    break;
            }

            return result;
        }

        //判断是否工作日
        private bool IsWorkDay(DateTime date, string day)
        {
            int week = Convert.ToInt32(date.DayOfWeek);
            switch (day)
            {
                case "1":
                    if (week != 1) return false;
                    break;
                case "2":
                    if (week > 2 || week == 0) return false;
                    break;
                case "3":
                    if (week > 3 || week == 0) return false;
                    break;
                case "4":
                    if (week > 4 || week == 0) return false;
                    break;
                case "5":
                    if (week > 5 || week == 0) return false;
                    break;
                case "6":
                    if (week == 0) return false;
                    break;
            }

            return true;
        }

        //取用户工作日
        private string GetUserWorkDay(int userId)
        {
            UserInfo user = user_dal.GetUserByUserId(userId);
            return user.UserWorkDay;
        }

        /// <summary>
        /// 更新商品到其它用户
        /// </summary>
        public bool UpdateItemToUser(int userId, int toUserId)
        {
            return dal.UpdateItemToUser(userId, toUserId);
        }

        /// <summary>
        /// 根据类别ID取商品名称列表
        /// </summary>
        public DataTable GetItemNameListByCategoryId(int userId, int catTypeId)
        {
            return dal.GetItemNameListByCategoryId(userId, catTypeId);
        }

        /// <summary>
        /// 根据关键字取商品名称列表
        /// </summary>
        public DataTable GetItemNameListByKeywords(int userId, string keywords)
        {
            return dal.GetItemNameListByKeywords(userId, keywords);
        }

        /// <summary>
        /// 根据商品名称取商品价格列表
        /// </summary>
        public DataTable GetItemPriceListByItemName(int userId, string itemName)
        {
            return dal.GetItemPriceListByItemName(userId, itemName);
        }

        /// <summary>
        /// 取导出商品列表
        /// </summary>
        public DataTable GetItemExportList(int userId)
        {
            return dal.GetItemExportList(userId);
        }
        
        /// <summary>
        /// 取导出商品列表
        /// </summary>
        public DataTable GetItemExportList(int userId, DateTime beginDate, DateTime endDate)
        {
            return dal.GetItemExportList(userId, beginDate, endDate);
        }

        /// <summary>
        /// 旧版更新钱包余额
        /// </summary>
        public bool UpdateBalanceMoney(int userId, int itemId, string itemType, decimal itemPrice, string type, int cardId)
        {
            return dal.UpdateBalanceMoney(userId, itemId, itemType, itemPrice, type, cardId);
        }

        /// <summary>
        /// 修改商品同步返回
        /// </summary>
        public bool UpdateItemListWebBack(int itemId, int itemAppId)
        {
            return dal.UpdateItemListWebBack(itemId, itemAppId);
        }

        /// <summary>
        /// 根据UserID修改商品同步返回
        /// </summary>
        public bool UpdateItemListWebBackByUserId(int userId)
        {
            return dal.UpdateItemListWebBackByUserId(userId);
        }

        /// <summary>
        /// 根据商品AppID取商品
        /// </summary>
        public ItemInfo GetItemByItemAppId(int userId, int itemAppId)
        {
            return dal.GetItemByItemAppId(userId, itemAppId);
        }

        /// <summary>
        /// 根据商品AppID修改商品
        /// </summary>
        public bool UpdateItemByItemAppId(ItemInfo item)
        {
            return dal.UpdateItemByItemAppId(item);
        }

        /// <summary>
        /// 同步删除商品
        /// </summary>
        public bool DeleteItemWithSync(int userId, int itemId, int itemAppId)
        {
            return dal.DeleteItemWithSync(userId, itemId, itemAppId);
        }

        /// <summary>
        /// 同步商品是否存在
        /// </summary>
        public bool ItemExistsWithSync(int userId, int itemId, int itemAppId)
        {
            return dal.ItemExistsWithSync(userId, itemId, itemAppId);
        }

    }
}
