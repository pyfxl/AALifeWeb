using AALife.DBUtility;
using AALife.Model;
using System;
using System.Data;
using System.Data.SqlClient;

namespace AALife.DAL
{
    public class ItemTableDAL
    {
        private static readonly string PARM_KEYWORDS = "@Keywords";
        private static readonly string PARM_USER_ID = "@UserID";
        private static readonly string PARM_TO_USER_ID = "@ToUserID";
        private static readonly string PARM_ITEM_BUY_DATE = "@ItemBuyDate";
        private static readonly string PARM_ITEM_ID = "@ItemID";
        private static readonly string PARM_REGION_ID = "@RegionID";
        private static readonly string PARM_ITEM_APP_ID = "@ItemAppID";
        private static readonly string PARM_ZT_ID = "@ZTID";
        private static readonly string PARM_CATEGORY_ID = "@CategoryTypeID";
        private static readonly string PARM_RECOMMEND = "@Recommend";
        private static readonly string PARM_CD_ID = "@CDID";
        private static readonly string PARM_ITEM_NAME = "@ItemName";
        private static readonly string PARM_ITEM_TYPE = "@ItemType";
        private static readonly string PARM_ITEM_PRICE = "@ItemPrice";
        private static readonly string PARM_BEGIN_DATE = "@BeginDate";
        private static readonly string PARM_END_DATE = "@EndDate";
        private static readonly string PARM_BEGIN_DATE_2 = "@BeginDate2";
        private static readonly string PARM_END_DATE_2 = "@EndDate2";
        private static readonly string PARM_QUERY = "@Query";
        private static readonly string PARM_LABEL = "@Label";
        private static readonly string PARM_VALUE = "@Value";
        private static readonly string PARM_SORT = "@Sort";

        private static readonly string SQL_SELECT_ITEM_LIST_BY_KEYWORDS = "GetItemListByKeywords_v5";
        private static readonly string SQL_SELECT_ITEM_LIST = "GetItemList_v5";
        private static readonly string SQL_SELECT_ITEM_LIST_BY_DATE_V6 = "GetItemListByDate_v6";
        private static readonly string SQL_SELECT_ITEM_LIST_BY_GROUP_V6 = "GetItemListByGroup_v6";
        private static readonly string SQL_SELECT_ITEM_LIST_BY_COMPARE_V6 = "GetItemListByCompare_v6";
        private static readonly string SQL_SELECT_LIST_BOX_DATA_V6 = "GetListBoxData_v6";
        private static readonly string SQL_SELECT_ITEM_LIST_BY_DATE = string.Format(@"select * from ItemTable with(nolock) where ModifyDate between {0} and {1} order by ItemID desc", PARM_BEGIN_DATE, PARM_END_DATE);
        private static readonly string SQL_SELECT_ITEM_COUNT = string.Format(@"select count(0) from ItemTable with(nolock) where UserID = {0}", PARM_USER_ID);
        private static readonly string SQL_SELECT_ITEM_LIST_BY_USER_ID = string.Format(@"select * from ItemTable with(nolock) where UserID = {0} order by ItemID desc", PARM_USER_ID);
        private static readonly string SQL_SELECT_ITEM_LIST_ALL_BY_KEYWORDS = string.Format(@"select * from ItemTable with(nolock) where ItemName like '%'+{0}+'%' order by ItemID desc", PARM_KEYWORDS);
        private static readonly string SQL_SELECT_ITEM_LIST_WITH_SYNC = string.Format(@"select top 500 * from ItemTableViewSync with(nolock) where UserID = {0} and Synchronize = 1 order by ItemID asc", PARM_USER_ID);
        private static readonly string SQL_SELECT_ITEM_TYPE_LIST = string.Format(@"select * from ItemTypeTable with(nolock) order by Rank asc");
        private static readonly string SQL_SELECT_REGION_TYPE_LIST = string.Format(@"select * from RegionTypeTable with(nolock) order by Rank asc");
        private static readonly string SQL_INSERT_ITEM = "InsertItem_v5";
        private static readonly string SQL_UPDATE_ITEM = "UpdateItem_v5";
        private static readonly string SQL_SELECT_ITEM_BY_ITEM_ID = string.Format(@"select * from ItemTable with(nolock) where ItemID = {0}", PARM_ITEM_ID);
        private static readonly string SQL_CHECK_ITEM_EXISTS = string.Format(@"select * from ItemTable with(nolock) where ItemName = {0} and Convert(nvarchar(10), ItemBuyDate, 23) = {1} and Recommend = {2} and ZhuanTiID = {3} and CardID = {4} and CategoryTypeID = {5} and ItemPrice = {6} and ItemType = {7}", PARM_ITEM_NAME, PARM_ITEM_BUY_DATE, PARM_RECOMMEND, PARM_ZT_ID, PARM_CD_ID, PARM_CATEGORY_ID, PARM_ITEM_PRICE, PARM_ITEM_TYPE);
        private static readonly string SQL_SELECT_ITEM_LIST_BY_REGION_ID = string.Format(@"select * from ItemTable with(nolock) where UserID = {0} and RegionID = {1}", PARM_USER_ID, PARM_REGION_ID);
        private static readonly string SQL_DELETE_ITEM = "DeleteItem_v5";
        private static readonly string SQL_SELECT_MAX_REGION_ID = string.Format(@"select isnull(max(RegionID),0)+1 from ItemTable with(nolock) where UserID = {0}", PARM_USER_ID);
        private static readonly string SQL_SELECT_ITEM_LIST_BY_ZHUANTI_ID = string.Format(@"select * from ItemTableView with(nolock) where UserID = {0} and ZhuanTiID = {1} order by ItemBuyDate desc", PARM_USER_ID, PARM_ZT_ID);
        private static readonly string SQL_SELECT_ITEM_LIST_ALL_BY_ZHUANTI_ID = string.Format(@"select * from ItemTable with(nolock) where UserID = {0} and ZhuanTiID = {1} order by ItemBuyDate asc", PARM_USER_ID, PARM_ZT_ID);
        private static readonly string SQL_SELECT_ITEM_LIST_BY_CATEGORY_ID = string.Format(@"select * from ItemTable with(nolock) where UserID = {0} and CategoryTypeID = {1} order by ItemBuyDate asc", PARM_USER_ID, PARM_CATEGORY_ID);
        private static readonly string SQL_SELECT_ITEM_LIST_BY_CARD_ID = string.Format(@"select * from ItemTableViewAll with(nolock) where UserID = {0} and CardID = {1} order by ItemBuyDate desc", PARM_USER_ID, PARM_CD_ID);
        private static readonly string SQL_UPDATE_ITEM_TO_USER = string.Format(@"update ItemTable set UserID = {0}, ModifyDate = getdate() where UserID = {1}", PARM_TO_USER_ID, PARM_USER_ID);
        private static readonly string SQL_SELECT_ITEM_NAME_LSIT_BY_CAREGORY_ID = string.Format(@"select ItemName, count(ItemName) as CountNum from ItemTableView with(nolock) where UserID = {0} and CategoryTypeID = {1} group by ItemName, CategoryTypeID order by CountNum desc", PARM_USER_ID, PARM_CATEGORY_ID);
        private static readonly string SQL_SELECT_ITEM_NAME_LIST_BY_KEYWORDS = string.Format(@"select ItemName, count(ItemName) as CountNum from ItemTableView with(nolock) where UserID = {0} and ItemName like '%'+{1}+'%' group by ItemName order by CountNum desc", PARM_USER_ID, PARM_KEYWORDS);
        private static readonly string SQL_SELECT_ITEM_PRICE_LIST_BY_ITEM_NAME = string.Format(@"select ItemPrice, count(ItemPrice) as CountNum from ItemTableView with(nolock) where UserID = {0} and ItemName = {1} group by ItemPrice order by CountNum desc", PARM_USER_ID, PARM_ITEM_NAME);
        private static readonly string SQL_SELECT_ITEM_EXPORT_LIST = "GetItemExportList_v5";
        private static readonly string SQL_SELECT_ITEM_EXPORT_LIST_V6 = "GetItemExportList_v6";
        private static readonly string SQL_UPDATE_BALANCE_MONEY = "UpdateBalanceMoney_v5";
        private static readonly string SQL_UPDATE_ITEM_LIST_WEB_BACK = string.Format(@"update ItemTable set Synchronize = 0, ItemAppID = {0} where ItemID = {1}", PARM_ITEM_APP_ID, PARM_ITEM_ID);
        private static readonly string SQL_UPDATE_ITEM_LIST_WEB_BACK_BY_USER_ID = string.Format(@"update ItemTable set Synchronize = 0, ItemAppID = 0 where UserID = {0}", PARM_USER_ID);
        private static readonly string SQL_SELECT_ITEM_BY_ITEM_APP_ID = string.Format(@"select * from ItemTable with(nolock) where UserID = {0} and ItemAppID = {1}", PARM_USER_ID, PARM_ITEM_APP_ID);
        private static readonly string SQL_UPDATE_ITEM_BY_ITEM_APP_ID = "UpdateItemByItemAppId_v5";
        private static readonly string SQL_DELETE_ITEM_WITH_SYNC = string.Format(@"delete from ItemTable where UserID = {0} and (ItemID = {1} or ItemAppID = {2})", PARM_USER_ID, PARM_ITEM_APP_ID, PARM_ITEM_ID);
        private static readonly string SQL_SELECT_ITEM_EXISTS_WITH_SYNC = string.Format(@"select * from ItemTable with(nolock) where UserID = {0} and (ItemID = {1} or ItemAppID = {2})", PARM_USER_ID, PARM_ITEM_APP_ID, PARM_ITEM_ID);
        
        /// <summary>
        /// 根据日期取商品列表
        /// </summary>
        public DataTable GetItemList(int userId, DateTime itemBuyDate)
        {
            DataTable lists = new DataTable();

            SqlParameter[] parms = {
					new SqlParameter(PARM_USER_ID, SqlDbType.Int),
					new SqlParameter(PARM_ITEM_BUY_DATE, SqlDbType.DateTime)
			};
            parms[0].Value = userId;
            parms[1].Value = itemBuyDate;

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.StoredProcedure, SQL_SELECT_ITEM_LIST, parms))
            {
                lists.Load(rdr);
            }

            return lists;
        }

        /// <summary>
        /// 根据日期范围取商品列表
        /// </summary>
        public DataTable GetItemList(int userId, DateTime beginDate, DateTime endDate)
        {
            DataTable lists = new DataTable();

            SqlParameter[] parms = {
					new SqlParameter(PARM_USER_ID, SqlDbType.Int),
					new SqlParameter(PARM_BEGIN_DATE, SqlDbType.DateTime),
					new SqlParameter(PARM_END_DATE, SqlDbType.DateTime)
			};
            parms[0].Value = userId;
            parms[1].Value = beginDate;
            parms[2].Value = endDate;

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.StoredProcedure, SQL_SELECT_ITEM_LIST_BY_DATE_V6, parms))
            {
                lists.Load(rdr);
            }

            return lists;
        }

        /// <summary>
        /// 根据日期范围取商品统计
        /// </summary>
        public DataTable GetItemListByGroup(int userId, DateTime beginDate, DateTime endDate, string myQuery, string myLabel, string mySort)
        {
            DataTable lists = new DataTable();

            SqlParameter[] parms = {
					new SqlParameter(PARM_USER_ID, SqlDbType.Int),
					new SqlParameter(PARM_BEGIN_DATE, SqlDbType.DateTime),
					new SqlParameter(PARM_END_DATE, SqlDbType.DateTime),
					new SqlParameter(PARM_QUERY, SqlDbType.NVarChar, 100),
					new SqlParameter(PARM_LABEL, SqlDbType.NVarChar, 100),
					new SqlParameter(PARM_SORT, SqlDbType.NVarChar, 10)
			};
            parms[0].Value = userId;
            parms[1].Value = beginDate;
            parms[2].Value = endDate;
            parms[3].Value = myQuery;
            parms[4].Value = myLabel;
            parms[5].Value = mySort;

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.StoredProcedure, SQL_SELECT_ITEM_LIST_BY_GROUP_V6, parms))
            {
                lists.Load(rdr);
            }

            return lists;
        }

        /// <summary>
        /// 根据日期范围取商品比较
        /// </summary>
        public DataTable GetItemListByCompare(int userId, DateTime beginDate, DateTime endDate, DateTime beginDate2, DateTime endDate2, string myQuery, string myLabel, string mySort)
        {
            DataTable lists = new DataTable();

            SqlParameter[] parms = {
					new SqlParameter(PARM_USER_ID, SqlDbType.Int),
					new SqlParameter(PARM_BEGIN_DATE, SqlDbType.DateTime),
					new SqlParameter(PARM_END_DATE, SqlDbType.DateTime),
					new SqlParameter(PARM_BEGIN_DATE_2, SqlDbType.DateTime),
					new SqlParameter(PARM_END_DATE_2, SqlDbType.DateTime),
					new SqlParameter(PARM_QUERY, SqlDbType.NVarChar, 100),
					new SqlParameter(PARM_LABEL, SqlDbType.NVarChar, 100),
					new SqlParameter(PARM_SORT, SqlDbType.NVarChar, 10)
			};
            parms[0].Value = userId;
            parms[1].Value = beginDate;
            parms[2].Value = endDate;
            parms[3].Value = beginDate2;
            parms[4].Value = endDate2;
            parms[5].Value = myQuery;
            parms[6].Value = myLabel;
            parms[7].Value = mySort;

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.StoredProcedure, SQL_SELECT_ITEM_LIST_BY_COMPARE_V6, parms))
            {
                lists.Load(rdr);
            }

            return lists;
        }

        /// <summary>
        /// 取下拉数据
        /// </summary>
        public DataTable GetListBoxData(int userId, string myLabel, string myValue, string mySort)
        {
            DataTable lists = new DataTable();

            SqlParameter[] parms = {
					new SqlParameter(PARM_USER_ID, SqlDbType.Int),
					new SqlParameter(PARM_LABEL, SqlDbType.NVarChar, 100),
					new SqlParameter(PARM_VALUE, SqlDbType.NVarChar, 100),
					new SqlParameter(PARM_SORT, SqlDbType.NVarChar, 10)
			};
            parms[0].Value = userId;
            parms[1].Value = myLabel;
            parms[2].Value = myValue;
            parms[3].Value = mySort;

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.StoredProcedure, SQL_SELECT_LIST_BOX_DATA_V6, parms))
            {
                lists.Load(rdr);
            }

            return lists;
        }

        /// <summary>
        /// 根据关键字取商品列表
        /// </summary>
        public DataTable GetItemListByKeywords(int userId, string keywords)
        {
            DataTable lists = new DataTable();

            SqlParameter[] parms = {
					new SqlParameter(PARM_USER_ID, SqlDbType.Int),
					new SqlParameter(PARM_KEYWORDS, SqlDbType.NVarChar, 20)
			};
            parms[0].Value = userId;
            parms[1].Value = keywords;

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.StoredProcedure, SQL_SELECT_ITEM_LIST_BY_KEYWORDS, parms))
            {
                lists.Load(rdr);
            }

            return lists;
        }

        /// <summary>
        /// 根据时间段取商品列表
        /// </summary>
        public DataTable GetItemListByDate(DateTime beginDate, DateTime endDate)
        {
            DataTable lists = new DataTable();

            SqlParameter[] parms = {
					new SqlParameter(PARM_BEGIN_DATE, SqlDbType.DateTime),
					new SqlParameter(PARM_END_DATE, SqlDbType.DateTime)
			};
            parms[0].Value = beginDate;
            parms[1].Value = endDate;

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.Text, SQL_SELECT_ITEM_LIST_BY_DATE, parms))
            {
                lists.Load(rdr);
            }

            return lists;
        }

        /// <summary>
        /// 根据用户ID取商品列表
        /// </summary>
        public DataTable GetItemListByUserId(int userId)
        {
            DataTable lists = new DataTable();

            SqlParameter parm = new SqlParameter(PARM_USER_ID, SqlDbType.Int);
            parm.Value = userId;

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.Text, SQL_SELECT_ITEM_LIST_BY_USER_ID, parm))
            {
                lists.Load(rdr);
            }

            return lists;
        }

        /// <summary>
        /// 根据关键字取所有商品列表
        /// </summary>
        public DataTable GetItemListAllByKeywords(string keywords)
        {
            DataTable lists = new DataTable();

            SqlParameter parm = new SqlParameter(PARM_KEYWORDS, SqlDbType.NVarChar, 20);
            parm.Value = keywords;

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.Text, SQL_SELECT_ITEM_LIST_ALL_BY_KEYWORDS, parm))
            {
                lists.Load(rdr);
            }

            return lists;
        }

        /// <summary>
        /// 取同步商品列表
        /// </summary>
        public DataTable GetItemListWithSync(int userId)
        {
            DataTable lists = new DataTable();

            SqlParameter parm = new SqlParameter(PARM_USER_ID, SqlDbType.Int);
            parm.Value = userId;

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.Text, SQL_SELECT_ITEM_LIST_WITH_SYNC, parm))
            {
                lists.Load(rdr);
            }

            return lists;
        }

        /// <summary>
        /// 根据商品ID取商品
        /// </summary>
        public ItemInfo GetItemByItemId(int itemId)
        {
            ItemInfo item = new ItemInfo();

            SqlParameter parm = new SqlParameter(PARM_ITEM_ID, SqlDbType.Int);
            parm.Value = itemId;

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.Text, SQL_SELECT_ITEM_BY_ITEM_ID, parm))
            {
                while (rdr.Read())
                {
                    item = DataToModel(rdr);
                }
            }

            return item;
        }

        /// <summary>
        /// 检查商品是否存在
        /// </summary>
        public bool CheckItemExists(ItemInfo item)
        {
            SqlParameter[] parms = {
					new SqlParameter(PARM_ITEM_NAME, SqlDbType.NVarChar, 20),
					new SqlParameter(PARM_ITEM_BUY_DATE, SqlDbType.Date),
					new SqlParameter(PARM_RECOMMEND, SqlDbType.TinyInt),
					new SqlParameter(PARM_ZT_ID, SqlDbType.Int),
					new SqlParameter(PARM_CD_ID, SqlDbType.Int),
					new SqlParameter(PARM_CATEGORY_ID, SqlDbType.Int),
                    new SqlParameter(PARM_ITEM_PRICE, SqlDbType.Decimal), 
                    new SqlParameter(PARM_ITEM_TYPE, SqlDbType.NVarChar, 10)
			};
            parms[0].Value = item.ItemName;
            parms[1].Value = item.ItemBuyDate.ToString("yyyy-MM-dd");
            parms[2].Value = item.Recommend;
            parms[3].Value = item.ZhuanTiID;
            parms[4].Value = item.CardID;
            parms[5].Value = item.CategoryTypeID;
            parms[6].Value = item.ItemPrice;
            parms[7].Value = item.ItemType;

            int result = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnectionString, CommandType.Text, SQL_CHECK_ITEM_EXISTS, parms));
            
            return result > 0;
        }

        /// <summary>
        /// 根据区间ID取商品列表
        /// </summary>
        public DataTable GetItemListByRegionId(int userId, int regionId)
        {
            DataTable lists = new DataTable();

            SqlParameter[] parms = {
					new SqlParameter(PARM_USER_ID, SqlDbType.Int),
					new SqlParameter(PARM_REGION_ID, SqlDbType.Int)
			};
            parms[0].Value = userId;
            parms[1].Value = regionId;

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.Text, SQL_SELECT_ITEM_LIST_BY_REGION_ID, parms))
            {
                lists.Load(rdr);
            }

            return lists;
        }

        /// <summary>
        /// 根据专题ID取商品列表
        /// </summary>
        public DataTable GetItemListByZhuanTiId(int userId, int ztId)
        {
            DataTable lists = new DataTable();

            SqlParameter[] parms = {
					new SqlParameter(PARM_USER_ID, SqlDbType.Int),
					new SqlParameter(PARM_ZT_ID, SqlDbType.Int)
			};
            parms[0].Value = userId;
            parms[1].Value = ztId;

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.Text, SQL_SELECT_ITEM_LIST_BY_ZHUANTI_ID, parms))
            {
                lists.Load(rdr);
            }

            return lists;
        }

        /// <summary>
        /// 根据专题ID取所有商品列表
        /// </summary>
        public DataTable GetItemListAllByZhuanTiId(int userId, int ztId)
        {
            DataTable lists = new DataTable();

            SqlParameter[] parms = {
					new SqlParameter(PARM_USER_ID, SqlDbType.Int),
					new SqlParameter(PARM_ZT_ID, SqlDbType.Int)
			};
            parms[0].Value = userId;
            parms[1].Value = ztId;

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.Text, SQL_SELECT_ITEM_LIST_ALL_BY_ZHUANTI_ID, parms))
            {
                lists.Load(rdr);
            }

            return lists;
        }

        /// <summary>
        /// 根据钱包ID取商品列表
        /// </summary>
        public DataTable GetItemListByCardId(int userId, int cardId)
        {
            DataTable lists = new DataTable();

            SqlParameter[] parms = {
					new SqlParameter(PARM_USER_ID, SqlDbType.Int),
					new SqlParameter(PARM_CD_ID, SqlDbType.Int)
			};
            parms[0].Value = userId;
            parms[1].Value = cardId;

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.Text, SQL_SELECT_ITEM_LIST_BY_CARD_ID, parms))
            {
                lists.Load(rdr);
            }

            return lists;
        }

        /// <summary>
        /// 根据类别ID取商品列表
        /// </summary>
        public DataTable GetItemListByCategoryId(int userId, int catTypeId)
        {
            DataTable lists = new DataTable();

            SqlParameter[] parms = {
					new SqlParameter(PARM_USER_ID, SqlDbType.Int),
					new SqlParameter(PARM_CATEGORY_ID, SqlDbType.Int)
			};
            parms[0].Value = userId;
            parms[1].Value = catTypeId;

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.Text, SQL_SELECT_ITEM_LIST_BY_CATEGORY_ID, parms))
            {
                lists.Load(rdr);
            }

            return lists;
        }

        /// <summary>
        /// 取商品分类列表
        /// </summary>
        public DataTable GetItemTypeList()
        {
            DataTable lists = new DataTable();

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.Text, SQL_SELECT_ITEM_TYPE_LIST, null))
            {
                lists.Load(rdr);
            }

            return lists;
        }

        /// <summary>
        /// 取商品区间列表
        /// </summary>
        public DataTable GetRegionTypeList()
        {
            DataTable lists = new DataTable();

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.Text, SQL_SELECT_REGION_TYPE_LIST, null))
            {
                lists.Load(rdr);
            }

            return lists;
        }
        
        /// <summary>
        /// 插入商品
        /// </summary>
        public bool InsertItem(ItemInfo item)
        {
            SqlParameter[] parms = ModelToParms(item);

            int result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionString, CommandType.StoredProcedure, SQL_INSERT_ITEM, parms);

            return result > 0;
        }

        /// <summary>
        /// 修改商品
        /// </summary>
        public bool UpdateItem(ItemInfo item)
        {
            SqlParameter[] parms = ModelToParms(item);

            int result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionString, CommandType.StoredProcedure, SQL_UPDATE_ITEM, parms);

            return result > 0;
        }

        /// <summary>
        /// 删除商品
        /// </summary>
        public bool DeleteItem(int userId, int itemId, int itemAppId)
        {
            SqlParameter[] parms = { 
                    new SqlParameter(PARM_USER_ID, SqlDbType.Int),
                    new SqlParameter(PARM_ITEM_ID, SqlDbType.Int),
                    new SqlParameter(PARM_ITEM_APP_ID, SqlDbType.Int)
            };
            parms[0].Value = userId;
            parms[1].Value = itemId;
            parms[2].Value = itemAppId;

            int result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionString, CommandType.StoredProcedure, SQL_DELETE_ITEM, parms);

            return result > 0;
        }

        /// <summary>
        /// 取最大区间ID
        /// </summary>
        public int GetMaxRegionId(int userId)
        {
            SqlParameter parm = new SqlParameter(PARM_USER_ID, SqlDbType.Int);
            parm.Value = userId;

            int result = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnectionString, CommandType.Text, SQL_SELECT_MAX_REGION_ID, parm));

            return result % 2 == 0 ? result + 1 : result;
        }

        /// <summary>
        /// 更新商品到其它用户
        /// </summary>
        public bool UpdateItemToUser(int userId, int toUserId)
        {
            SqlParameter[] parms = {
                    new SqlParameter(PARM_USER_ID, SqlDbType.Int),
                    new SqlParameter(PARM_TO_USER_ID, SqlDbType.Int)
            };
            parms[0].Value = userId;
            parms[1].Value = toUserId;

            int result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionString, CommandType.Text, SQL_UPDATE_ITEM_TO_USER, parms);

            return result > 0;
        }

        /// <summary>
        /// 根据类别ID取商品名称列表
        /// </summary>
        public DataTable GetItemNameListByCategoryId(int userId, int catTypeId)
        {
            DataTable lists = new DataTable();

            SqlParameter[] parms = {
					new SqlParameter(PARM_USER_ID, SqlDbType.Int),
					new SqlParameter(PARM_CATEGORY_ID, SqlDbType.Int)
			};
            parms[0].Value = userId;
            parms[1].Value = catTypeId;

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.Text, SQL_SELECT_ITEM_NAME_LSIT_BY_CAREGORY_ID, parms))
            {
                lists.Load(rdr);
            }

            return lists;
        }

        /// <summary>
        /// 根据关键字取商品名称列表
        /// </summary>
        public DataTable GetItemNameListByKeywords(int userId, string keywords)
        {
            DataTable lists = new DataTable();

            SqlParameter[] parms = {
					new SqlParameter(PARM_USER_ID, SqlDbType.Int),
					new SqlParameter(PARM_KEYWORDS, SqlDbType.NVarChar, 20)
			};
            parms[0].Value = userId;
            parms[1].Value = keywords;

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.Text, SQL_SELECT_ITEM_NAME_LIST_BY_KEYWORDS, parms))
            {
                lists.Load(rdr);
            }

            return lists;
        }

        /// <summary>
        /// 根据商品名称取商品价格列表
        /// </summary>
        public DataTable GetItemPriceListByItemName(int userId, string itemName)
        {
            DataTable lists = new DataTable();

            SqlParameter[] parms = {
					new SqlParameter(PARM_USER_ID, SqlDbType.Int),
					new SqlParameter(PARM_ITEM_NAME, SqlDbType.NVarChar, 20)
			};
            parms[0].Value = userId;
            parms[1].Value = itemName;

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.Text, SQL_SELECT_ITEM_PRICE_LIST_BY_ITEM_NAME, parms))
            {
                lists.Load(rdr);
            }

            return lists;
        }

        /// <summary>
        /// 取导出商品列表
        /// </summary>
        public DataTable GetItemExportList(int userId)
        {
            DataTable lists = new DataTable();

            SqlParameter parm = new SqlParameter(PARM_USER_ID, SqlDbType.Int);
            parm.Value = userId;

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.StoredProcedure, SQL_SELECT_ITEM_EXPORT_LIST, parm))
            {
                lists.Load(rdr);
            }

            return lists;
        }

        /// <summary>
        /// 取导出商品列表
        /// </summary>
        public DataTable GetItemExportList(int userId, DateTime beginDate, DateTime endDate)
        {
            DataTable lists = new DataTable();

            SqlParameter[] parms = {
					new SqlParameter(PARM_USER_ID, SqlDbType.Int),
					new SqlParameter(PARM_BEGIN_DATE, SqlDbType.DateTime),
					new SqlParameter(PARM_END_DATE, SqlDbType.DateTime)
			};
            parms[0].Value = userId;
            parms[1].Value = beginDate;
            parms[2].Value = endDate;

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.StoredProcedure, SQL_SELECT_ITEM_EXPORT_LIST_V6, parms))
            {
                lists.Load(rdr);
            }

            return lists;
        }

        /// <summary>
        /// 旧版更新钱包余额
        /// </summary>
        public bool UpdateBalanceMoney(int userId, int itemId, string itemType, decimal itemPrice, string type, int cardId)
        {
            SqlParameter[] parms = { 
                    new SqlParameter(PARM_USER_ID, SqlDbType.Int), 
                    new SqlParameter(PARM_ITEM_ID, SqlDbType.Int), 
                    new SqlParameter(PARM_ITEM_TYPE, SqlDbType.NVarChar, 10), 
                    new SqlParameter(PARM_ITEM_PRICE, SqlDbType.Decimal), 
                    new SqlParameter("@Type", SqlDbType.NVarChar, 10), 
                    new SqlParameter(PARM_CD_ID, SqlDbType.Int)
            };
            parms[0].Value = userId;
            parms[1].Value = itemId;
            parms[2].Value = itemType;
            parms[3].Value = itemPrice;
            parms[4].Value = type;
            parms[5].Value = cardId;

            int result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionString, CommandType.StoredProcedure, SQL_UPDATE_BALANCE_MONEY, parms);

            return result > 0;
        }

        /// <summary>
        /// 修改商品同步返回
        /// </summary>
        public bool UpdateItemListWebBack(int itemId, int itemAppId)
        {
            SqlParameter[] parms = { 
                    new SqlParameter(PARM_ITEM_ID, SqlDbType.Int),
                    new SqlParameter(PARM_ITEM_APP_ID, SqlDbType.Int)
            };
            parms[0].Value = itemId;
            parms[1].Value = itemAppId;

            int result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionString, CommandType.Text, SQL_UPDATE_ITEM_LIST_WEB_BACK, parms);

            return result > 0;
        }

        /// <summary>
        /// 根据UserID修改商品同步返回
        /// </summary>
        public bool UpdateItemListWebBackByUserId(int userId)
        {
            SqlParameter parm = new SqlParameter(PARM_USER_ID, SqlDbType.Int);
            parm.Value = userId;

            int result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionString, CommandType.Text, SQL_UPDATE_ITEM_LIST_WEB_BACK_BY_USER_ID, parm);

            return result > 0;
        }

        /// <summary>
        /// 根据商品AppID取商品
        /// </summary>
        public ItemInfo GetItemByItemAppId(int userId, int itemAppId)
        {
            ItemInfo item = new ItemInfo();

            SqlParameter[] parms = {
					new SqlParameter(PARM_USER_ID, SqlDbType.Int),
					new SqlParameter(PARM_ITEM_APP_ID, SqlDbType.Int)
			};
            parms[0].Value = userId;
            parms[1].Value = itemAppId;

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.Text, SQL_SELECT_ITEM_BY_ITEM_APP_ID, parms))
            {
                while (rdr.Read())
                {
                    item = DataToModel(rdr);
                }
            }

            return item;
        }

        /// <summary>
        /// 根据商品AppID修改商品
        /// </summary>
        public bool UpdateItemByItemAppId(ItemInfo item)
        {
            SqlParameter[] parms = ModelToParms(item);

            int result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionString, CommandType.StoredProcedure, SQL_UPDATE_ITEM_BY_ITEM_APP_ID, parms);

            return result > 0;
        }

        /// <summary>
        /// 同步删除商品
        /// </summary>
        public bool DeleteItemWithSync(int userId, int itemId, int itemAppId)
        {
            SqlParameter[] parms = { 
                    new SqlParameter(PARM_USER_ID, SqlDbType.Int),
                    new SqlParameter(PARM_ITEM_ID, SqlDbType.Int),
                    new SqlParameter(PARM_ITEM_APP_ID, SqlDbType.Int)
            };
            parms[0].Value = userId;
            parms[1].Value = itemId;
            parms[2].Value = itemAppId;

            int result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionString, CommandType.Text, SQL_DELETE_ITEM_WITH_SYNC, parms);

            return result > 0;
        }

        /// <summary>
        /// 商品实体转参数
        /// </summary>
        public static SqlParameter[] ModelToParms(ItemInfo item)
        {
            SqlParameter[] parms = {
					new SqlParameter("@ItemID", SqlDbType.Int),
					new SqlParameter("@ItemName", SqlDbType.NVarChar, 20),
					new SqlParameter("@CategoryTypeID", SqlDbType.Int),
					new SqlParameter("@ItemPrice", SqlDbType.Decimal),
					new SqlParameter("@ItemBuyDate", SqlDbType.DateTime),
					new SqlParameter("@UserID", SqlDbType.Int),
					new SqlParameter("@Recommend", SqlDbType.TinyInt),
					new SqlParameter("@ModifyDate", SqlDbType.DateTime),
					new SqlParameter("@Synchronize", SqlDbType.TinyInt),
					new SqlParameter("@ItemAppID", SqlDbType.Int),
					new SqlParameter("@RegionID", SqlDbType.Int),
					new SqlParameter("@RegionType", SqlDbType.NVarChar, 10),
					new SqlParameter("@ItemType", SqlDbType.NVarChar, 10),
					new SqlParameter("@ZhuanTiID", SqlDbType.Int),
					new SqlParameter("@CardID", SqlDbType.Int),
			};
            parms[0].Value = item.ItemID;
            parms[1].Value = item.ItemName;
            parms[2].Value = item.CategoryTypeID;
            parms[3].Value = item.ItemPrice;
            parms[4].Value = item.ItemBuyDate;
            parms[5].Value = item.UserID;
            parms[6].Value = item.Recommend;
            parms[7].Value = item.ModifyDate;
            parms[8].Value = item.Synchronize;
            parms[9].Value = item.ItemAppID;
            parms[10].Value = item.RegionID;
            parms[11].Value = item.RegionType;
            parms[12].Value = item.ItemType;
            parms[13].Value = item.ZhuanTiID;
            parms[14].Value = item.CardID;

            return parms;
        }

        /// <summary>
        /// 数据转商品实体
        /// </summary>
        public static ItemInfo DataToModel(SqlDataReader rdr)
        {
            ItemInfo item = new ItemInfo();
            if (!rdr.IsDBNull(0)) item.ItemID = rdr.GetInt32(0);
            if (!rdr.IsDBNull(1)) item.ItemName = rdr.GetString(1);
            if (!rdr.IsDBNull(2)) item.CategoryTypeID = rdr.GetInt32(2);
            if (!rdr.IsDBNull(3)) item.ItemPrice = rdr.GetDecimal(3);
            if (!rdr.IsDBNull(4)) item.ItemBuyDate = rdr.GetDateTime(4);
            if (!rdr.IsDBNull(5)) item.UserID = rdr.GetInt32(5);
            if (!rdr.IsDBNull(6)) item.Recommend = rdr.GetByte(6);
            if (!rdr.IsDBNull(7)) item.ModifyDate = rdr.GetDateTime(7);
            if (!rdr.IsDBNull(8)) item.Synchronize = rdr.GetByte(8);
            if (!rdr.IsDBNull(9)) item.ItemAppID = rdr.GetInt32(9);
            if (!rdr.IsDBNull(10)) item.RegionID = rdr.GetInt32(10);
            if (!rdr.IsDBNull(11)) item.RegionType = rdr.GetString(11);
            if (!rdr.IsDBNull(12)) item.ItemType = rdr.GetString(12);
            if (!rdr.IsDBNull(13)) item.ZhuanTiID = rdr.GetInt32(13);
            if (!rdr.IsDBNull(14)) item.CardID = rdr.GetInt32(14);

            return item;
        }

        /// <summary>
        /// 同步商品是否存在
        /// </summary>
        public bool ItemExistsWithSync(int userId, int itemId, int itemAppId)
        {
            SqlParameter[] parms = { 
                    new SqlParameter(PARM_USER_ID, SqlDbType.Int),
                    new SqlParameter(PARM_ITEM_ID, SqlDbType.Int),
                    new SqlParameter(PARM_ITEM_APP_ID, SqlDbType.Int)
            };
            parms[0].Value = userId;
            parms[1].Value = itemId;
            parms[2].Value = itemAppId;

            int result = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnectionString, CommandType.Text, SQL_SELECT_ITEM_EXISTS_WITH_SYNC, parms));

            return result > 0;
        }

    }
}
