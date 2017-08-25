using AALife.DBUtility;
using System;
using System.Data;
using System.Data.SqlClient;

namespace AALife.DAL
{
    public class MonthDAL
    {
        private static readonly string PARM_USER_ID = "@UserID";
        private static readonly string PARM_ITEM_BUY_DATE = "@ItemBuyDate";
        private static readonly string PARM_CATEGORY_ID = "@CategoryTypeID";
        private static readonly string PARM_ORDER_BY = "@OrderBy";
        private static readonly string PARM_ITEM_TYPE = "@ItemType";
        private static readonly string PARM_ITEM_NAME = "@ItemName";
        private static readonly string PARM_PRICE_MAX = "@PriceMax";
        private static readonly string PARM_COUNT_MAX = "@CountMax";
        private static readonly string PARM_PAGE_NUMBER = "@PageNumber";
        private static readonly string PARM_PAGE_PER_NUMBER = "@PagePerNumber";
        private static readonly string PARM_NOT_BUY_MAX = "@NotBuyMax";
        private static readonly string PARM_HOW_MANY_ITEMS = "@HowManyItems";
        private static readonly string PARM_BEGIN_DATE = "@BeginDate";
        private static readonly string PARM_END_DATE = "@EndDate";

        private static readonly string SQL_SELECT_MONTH_LIST = string.Format(@"select * from MonthListSumView with(nolock) where UserID = {0} and convert(nvarchar(7), ItemBuyDate, 120) = convert(nvarchar(7), {1}, 120) order by ItemBuyDate asc", PARM_USER_ID, PARM_ITEM_BUY_DATE);
        private static readonly string SQL_SELECT_MONTH_LIST_BY_ITEM_BUY_DATE = string.Format(@"select * from MonthListView with(nolock) where UserID = {0} and convert(nvarchar(10), ItemBuyDate, 120) = convert(nvarchar(10), {1}, 120) order by ItemID asc", PARM_USER_ID, PARM_ITEM_BUY_DATE);
        private static readonly string SQL_SELECT_FEN_LEI_ZONG_JI_LIST = "GetFenLeiZongJiList_v5";
        private static readonly string SQL_SELECT_FEN_LEI_ZONG_JI_MING_XI_LIST = "GetFenLeiZongJiMingXiList_v5";
        private static readonly string SQL_SELECT_ITEM_NUM_TOP_LIST = "GetItemNumTopList_v5";
        private static readonly string SQL_SELECT_ITEM_NUM_DETAIL_LIST = "GetItemNumDetailList_v5";
        private static readonly string SQL_SELECT_ITEM_PRICE_TOP_LIST = "GetItemPriceTopList_v5";
        private static readonly string SQL_SELECT_ITEM_DATE_TOP_LIST = "GetItemDateTopList_v5";
        private static readonly string SQL_SELECT_QU_JIAN_TONG_JI_LIST = "GetQuJianTongJiList_v5";
        private static readonly string SQL_SELECT_TUI_JIAN_FEN_XI_LIST = string.Format(@"select * from ItemTableView with(nolock) where UserID = {0} and Recommend = 1 order by ItemBuyDate desc", PARM_USER_ID);
        private static readonly string SQL_SELECT_BI_JIAO_FEN_XI_LIST = "GetBiJiaoFenXiList_v5";
        private static readonly string SQL_SELECT_BI_JIAO_MING_XI_LIST = "GetBiJiaoMingXiList_v5";
        private static readonly string SQL_SELECT_JIAN_GE_FEN_XI_LIST = "GetJianGeFenXiList_v5";
        private static readonly string SQL_SELECT_TIAN_SHU_FEN_XI_LIST = "GetTianShuFenXiList_v5";
        private static readonly string SQL_SELECT_JIA_GE_FEN_XI_LIST = "GetJiaGeFenXiList_v5";
        private static readonly string SQL_SELECT_JIA_GE_FEN_XI_MING_XI_LIST = "GetJiaGeFenXiMingXiList_v5";
        private static readonly string SQL_SELECT_JIE_HUAN_FEN_XI_LIST = "GetJieHuanFenXiList_v5";
        private static readonly string SQL_SELECT_SHOU_ZHI_JIE_HUAN_LIST = "GetShouZhiJieHuanList_v5";
        private static readonly string SQL_SELECT_SHOU_ZHI_JIE_HUAN_LIST_V6 = "GetShouZhiJieHuanList_v6";
        
        private static readonly string SQL_SELECT_TONG_JI_WITH_ITEM_NAME_COUNT = "select top 10 count(ItemName) as CountNum, ItemName, max(ItemBuyDate) as ItemBuyDate from ItemTableTongJiView with(nolock) group by ItemName order by CountNum desc";
        private static readonly string SQL_SELECT_TONG_JI_WITH_ITEM_PRICE_MAX = "select top 10 ItemName, ItemPrice, ItemBuyDate from ItemTableTongJiView with(nolock) order by ItemPrice desc";
        private static readonly string SQL_SELECT_TONG_JI_WITH_ITEM_ADD_LAST = "select top 10 ItemName, ItemPrice, ItemBuyDate from ItemTableTongJiView with(nolock) order by ItemBuyDate desc";
        private static readonly string SQL_SELECT_TONG_JI_WITH_USER_ITEM_COUNT = "select top 10 count(ItemName) as CountNum, UserName, UserFromName from ItemTableTongJiView with(nolock) group by UserID, UserName, UserFromName order by CountNum desc";
        private static readonly string SQL_SELECT_TONG_JI_WITH_USER_ITEM_LAST = "select top 10 UserName, UserFromName, CreateDate from ItemTableTongJiView with(nolock) where RowNum = 1 order by ItemBuyDate desc";
        private static readonly string SQL_SELECT_TONG_JI_WITH_USER_ADD_LAST = "select top 10 isnull(UserNickName, UserName) as UserName, UserFromName, CreateDate from UserTableView with(nolock) order by CreateDate desc";

        private static readonly string SQL_SELECT_TONG_JI_HUO_DONG = "GetAdminTongJiHuoDong_v5";
        private static readonly string SQL_SELECT_TONG_JI_QUAN_BU = "GetAdminTongJiQuanBu_v5";
        private static readonly string SQL_SELECT_TONG_JI_XIN_ZENG = "GetAdminTongJiXinZeng_v5";
        
        /// <summary>
        /// 取每月商品列表
        /// </summary>
        public DataTable GetMonthList(int userId, DateTime itemBuyDate)
        {
            DataTable lists = new DataTable();

            SqlParameter[] parms = {
					new SqlParameter(PARM_USER_ID, SqlDbType.Int),
					new SqlParameter(PARM_ITEM_BUY_DATE, SqlDbType.DateTime)
			};
            parms[0].Value = userId;
            parms[1].Value = itemBuyDate;

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.Text, SQL_SELECT_MONTH_LIST, parms))
            {
                lists.Load(rdr);
            }

            return lists;
        }

        /// <summary>
        /// 取每月商品明细
        /// </summary>
        public DataTable GetMonthListByItemBuyDate(int userId, DateTime itemBuyDate)
        {
            DataTable lists = new DataTable();

            SqlParameter[] parms = {
					new SqlParameter(PARM_USER_ID, SqlDbType.Int),
					new SqlParameter(PARM_ITEM_BUY_DATE, SqlDbType.DateTime)
			};
            parms[0].Value = userId;
            parms[1].Value = itemBuyDate;

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.Text, SQL_SELECT_MONTH_LIST_BY_ITEM_BUY_DATE, parms))
            {
                lists.Load(rdr);
            }

            return lists;
        }

        /// <summary>
        /// 取商品分类总计
        /// </summary>
        public DataTable GetFenLeiZongJiList(int userId, DateTime itemBuyDate)
        {
            DataTable lists = new DataTable();

            SqlParameter[] parms = {
					new SqlParameter(PARM_USER_ID, SqlDbType.Int),
					new SqlParameter(PARM_ITEM_BUY_DATE, SqlDbType.DateTime)
			};
            parms[0].Value = userId;
            parms[1].Value = itemBuyDate;

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.StoredProcedure, SQL_SELECT_FEN_LEI_ZONG_JI_LIST, parms))
            {
                lists.Load(rdr);
            }

            return lists;
        }

        /// <summary>
        /// 取商品分类总计明细
        /// </summary>
        public DataTable GetFenLeiZongJiMingXiList(int userId, DateTime itemBuyDate, int catTypeId, out decimal priceMax)
        {
            DataTable lists = new DataTable();
            priceMax = 0m;

            SqlParameter[] parms = {
					new SqlParameter(PARM_USER_ID, SqlDbType.Int),
					new SqlParameter(PARM_ITEM_BUY_DATE, SqlDbType.DateTime),
					new SqlParameter(PARM_CATEGORY_ID, SqlDbType.Int),
                    new SqlParameter(PARM_PRICE_MAX, SqlDbType.Decimal)
			};
            parms[0].Value = userId;
            parms[1].Value = itemBuyDate;
            parms[2].Value = catTypeId;
            parms[3].Direction = ParameterDirection.Output;
            parms[3].Scale = 3;

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.StoredProcedure, SQL_SELECT_FEN_LEI_ZONG_JI_MING_XI_LIST, parms))
            {
                lists.Load(rdr);
            }
            
            priceMax = Convert.ToDecimal(parms[3].Value);

            return lists;
        }

        /// <summary>
        /// 取商品次数排行
        /// </summary>
        public DataTable GetItemNumTopList(int userId, DateTime itemBuyDate)
        {
            DataTable lists = new DataTable();

            SqlParameter[] parms = {
					new SqlParameter(PARM_USER_ID, SqlDbType.Int),
					new SqlParameter(PARM_ITEM_BUY_DATE, SqlDbType.DateTime)
			};
            parms[0].Value = userId;
            parms[1].Value = itemBuyDate;

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.StoredProcedure, SQL_SELECT_ITEM_NUM_TOP_LIST, parms))
            {
                lists.Load(rdr);
            }

            return lists;
        }

        /// <summary>
        /// 取商品次数排行明细
        /// </summary>
        public DataTable GetItemNumDetailList(int userId, DateTime itemBuyDate, int catTypeId, string itemType, string itemName, string orderBy)
        {
            DataTable lists = new DataTable();

            SqlParameter[] parms = {
					new SqlParameter(PARM_USER_ID, SqlDbType.Int),
					new SqlParameter(PARM_ITEM_BUY_DATE, SqlDbType.DateTime),
					new SqlParameter(PARM_CATEGORY_ID, SqlDbType.Int),
					new SqlParameter(PARM_ITEM_TYPE, SqlDbType.NVarChar, 10),
					new SqlParameter(PARM_ITEM_NAME, SqlDbType.NVarChar, 20),
					new SqlParameter(PARM_ORDER_BY, SqlDbType.NVarChar, 10)
			};
            parms[0].Value = userId;
            parms[1].Value = itemBuyDate;
            parms[2].Value = catTypeId;
            parms[3].Value = itemType;
            parms[4].Value = itemName;
            parms[5].Value = orderBy;

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.StoredProcedure, SQL_SELECT_ITEM_NUM_DETAIL_LIST, parms))
            {
                lists.Load(rdr);
            }

            return lists;
        }

        /// <summary>
        /// 取商品单价排行
        /// </summary>
        public DataTable GetItemPriceTopList(int userId, DateTime itemBuyDate)
        {
            DataTable lists = new DataTable();

            SqlParameter[] parms = {
					new SqlParameter(PARM_USER_ID, SqlDbType.Int),
					new SqlParameter(PARM_ITEM_BUY_DATE, SqlDbType.DateTime)
			};
            parms[0].Value = userId;
            parms[1].Value = itemBuyDate;

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.StoredProcedure, SQL_SELECT_ITEM_PRICE_TOP_LIST, parms))
            {
                lists.Load(rdr);
            }

            return lists;
        }

        /// <summary>
        /// 取商品日期排行
        /// </summary>
        public DataTable GetItemDateTopList(int userId, DateTime itemBuyDate, string orderBy, out decimal priceMax)
        {
            DataTable lists = new DataTable();

            SqlParameter[] parms = {
					new SqlParameter(PARM_USER_ID, SqlDbType.Int),
					new SqlParameter(PARM_ITEM_BUY_DATE, SqlDbType.DateTime),
					new SqlParameter(PARM_ORDER_BY, SqlDbType.NVarChar, 10),
                    new SqlParameter(PARM_PRICE_MAX, SqlDbType.Decimal)
			};
            parms[0].Value = userId;
            parms[1].Value = itemBuyDate;
            parms[2].Value = orderBy;
            parms[3].Direction = ParameterDirection.Output;
            parms[3].Scale = 3;

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.StoredProcedure, SQL_SELECT_ITEM_DATE_TOP_LIST, parms))
            {
                lists.Load(rdr);
            }

            priceMax = Convert.ToDecimal(parms[3].Value);

            return lists;
        }

        /// <summary>
        /// 取商品区间统计
        /// </summary>
        public DataTable GetQuJianTongJiList(int userId)
        {
            DataTable lists = new DataTable();

            SqlParameter parm = new SqlParameter(PARM_USER_ID, SqlDbType.Int);
            parm.Value = userId;

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.StoredProcedure, SQL_SELECT_QU_JIAN_TONG_JI_LIST, parm))
            {
                lists.Load(rdr);
            }

            return lists;
        }

        /// <summary>
        /// 取商品推荐分析
        /// </summary>
        public DataTable GetTuiJianFenXiList(int userId)
        {
            DataTable lists = new DataTable();

            SqlParameter parm = new SqlParameter(PARM_USER_ID, SqlDbType.Int);
            parm.Value = userId;

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.Text, SQL_SELECT_TUI_JIAN_FEN_XI_LIST, parm))
            {
                lists.Load(rdr);
            }

            return lists;
        }

        /// <summary>
        /// 取商品比较分析
        /// </summary>
        public DataTable GetBiJiaoFenXiList(int userId, DateTime itemBuyDate, string orderBy, out decimal priceMax)
        {
            DataTable lists = new DataTable();

            SqlParameter[] parms = {
					new SqlParameter(PARM_USER_ID, SqlDbType.Int),
					new SqlParameter(PARM_ITEM_BUY_DATE, SqlDbType.DateTime),
					new SqlParameter(PARM_ORDER_BY, SqlDbType.NVarChar, 10),
                    new SqlParameter(PARM_PRICE_MAX, SqlDbType.Decimal)
			};
            parms[0].Value = userId;
            parms[1].Value = itemBuyDate;
            parms[2].Value = orderBy;
            parms[3].Direction = ParameterDirection.Output;

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.StoredProcedure, SQL_SELECT_BI_JIAO_FEN_XI_LIST, parms))
            {
                lists.Load(rdr);
            }

            priceMax = Convert.ToDecimal(parms[3].Value);

            return lists;
        }

        /// <summary>
        /// 取商品比较明细
        /// </summary>
        public DataTable GetBiJiaoMingXiList(int userId, DateTime itemBuyDate, int catTypeId, out decimal priceMax, out int countMax)
        {
            DataTable lists = new DataTable();

            SqlParameter[] parms = {
					new SqlParameter(PARM_USER_ID, SqlDbType.Int),
					new SqlParameter(PARM_ITEM_BUY_DATE, SqlDbType.DateTime),
					new SqlParameter(PARM_CATEGORY_ID, SqlDbType.Int),
                    new SqlParameter(PARM_PRICE_MAX, SqlDbType.Decimal),
                    new SqlParameter(PARM_COUNT_MAX, SqlDbType.Int)
			};
            parms[0].Value = userId;
            parms[1].Value = itemBuyDate;
            parms[2].Value = catTypeId;
            parms[3].Direction = ParameterDirection.Output;
            parms[3].Scale = 3;
            parms[4].Direction = ParameterDirection.Output;

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.StoredProcedure, SQL_SELECT_BI_JIAO_MING_XI_LIST, parms))
            {
                lists.Load(rdr);
            }

            priceMax = Convert.ToDecimal(parms[3].Value);
            countMax = Convert.ToInt32(parms[4].Value);

            return lists;
        }

        /// <summary>
        /// 取商品间隔分析
        /// </summary>
        public DataTable GetJianGeFenXiList(int userId, int pageNumber, int pagePerNumber, out int howManyItems, out int notBuyMax)
        {
            DataTable lists = new DataTable();

            SqlParameter[] parms = {
					new SqlParameter(PARM_USER_ID, SqlDbType.Int),
					new SqlParameter(PARM_PAGE_NUMBER, SqlDbType.Int),
					new SqlParameter(PARM_PAGE_PER_NUMBER, SqlDbType.Int),
					new SqlParameter(PARM_HOW_MANY_ITEMS, SqlDbType.Int),
                    new SqlParameter(PARM_NOT_BUY_MAX, SqlDbType.Int)
			};
            parms[0].Value = userId;
            parms[1].Value = pageNumber;
            parms[2].Value = pagePerNumber;
            parms[3].Direction = ParameterDirection.Output;
            parms[4].Direction = ParameterDirection.Output;

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.StoredProcedure, SQL_SELECT_JIAN_GE_FEN_XI_LIST, parms))
            {
                lists.Load(rdr);
            }

            howManyItems = Convert.ToInt32(parms[3].Value);
            notBuyMax = Convert.ToInt32(parms[4].Value);

            return lists;
        }

        /// <summary>
        /// 取商品天数分析
        /// </summary>
        public DataTable GetTianShuFenXiList(int userId, int pageNumber, int pagePerNumber, out int howManyItems, out int notBuyMax)
        {
            DataTable lists = new DataTable();

            SqlParameter[] parms = {
					new SqlParameter(PARM_USER_ID, SqlDbType.Int),
					new SqlParameter(PARM_PAGE_NUMBER, SqlDbType.Int),
					new SqlParameter(PARM_PAGE_PER_NUMBER, SqlDbType.Int),
					new SqlParameter(PARM_HOW_MANY_ITEMS, SqlDbType.Int),
                    new SqlParameter(PARM_NOT_BUY_MAX, SqlDbType.Int)
			};
            parms[0].Value = userId;
            parms[1].Value = pageNumber;
            parms[2].Value = pagePerNumber;
            parms[3].Direction = ParameterDirection.Output;
            parms[4].Direction = ParameterDirection.Output;

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.StoredProcedure, SQL_SELECT_TIAN_SHU_FEN_XI_LIST, parms))
            {
                lists.Load(rdr);
            }

            howManyItems = Convert.ToInt32(parms[3].Value);
            notBuyMax = Convert.ToInt32(parms[4].Value);

            return lists;
        }

        /// <summary>
        /// 取商品价格分析
        /// </summary>
        public DataTable GetJiaGeFenXiList(int userId, int pageNumber, int pagePerNumber, out int howManyItems, out decimal priceMax)
        {
            DataTable lists = new DataTable();

            SqlParameter[] parms = {
					new SqlParameter(PARM_USER_ID, SqlDbType.Int),
					new SqlParameter(PARM_PAGE_NUMBER, SqlDbType.Int),
					new SqlParameter(PARM_PAGE_PER_NUMBER, SqlDbType.Int),
					new SqlParameter(PARM_HOW_MANY_ITEMS, SqlDbType.Int),
                    new SqlParameter(PARM_PRICE_MAX, SqlDbType.Decimal)
			};
            parms[0].Value = userId;
            parms[1].Value = pageNumber;
            parms[2].Value = pagePerNumber;
            parms[3].Direction = ParameterDirection.Output;
            parms[4].Direction = ParameterDirection.Output;
            parms[4].Scale = 3;

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.StoredProcedure, SQL_SELECT_JIA_GE_FEN_XI_LIST, parms))
            {
                lists.Load(rdr);
            }

            howManyItems = Convert.ToInt32(parms[3].Value);
            priceMax = Convert.ToDecimal(parms[4].Value);

            return lists;
        }

        /// <summary>
        /// 取商品价格分析明细
        /// </summary>
        public DataTable GetJiaGeFenXiMingXiList(int userId, string itemType, string itemName, int pageNumber, int pagePerNumber, out int howManyItems, out decimal priceMax)
        {
            DataTable lists = new DataTable();

            SqlParameter[] parms = {
					new SqlParameter(PARM_USER_ID, SqlDbType.Int),
					new SqlParameter(PARM_ITEM_TYPE, SqlDbType.NVarChar, 10),
					new SqlParameter(PARM_ITEM_NAME, SqlDbType.NVarChar, 20),
					new SqlParameter(PARM_PAGE_NUMBER, SqlDbType.Int),
					new SqlParameter(PARM_PAGE_PER_NUMBER, SqlDbType.Int),
					new SqlParameter(PARM_HOW_MANY_ITEMS, SqlDbType.Int),
                    new SqlParameter(PARM_PRICE_MAX, SqlDbType.Decimal)
			};
            parms[0].Value = userId;
            parms[1].Value = itemType;
            parms[2].Value = itemName;
            parms[3].Value = pageNumber;
            parms[4].Value = pagePerNumber;
            parms[5].Direction = ParameterDirection.Output;
            parms[6].Direction = ParameterDirection.Output;
            parms[6].Scale = 3;

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.StoredProcedure, SQL_SELECT_JIA_GE_FEN_XI_MING_XI_LIST, parms))
            {
                lists.Load(rdr);
            }

            howManyItems = Convert.ToInt32(parms[5].Value);
            priceMax = Convert.ToDecimal(parms[6].Value);

            return lists;
        }

        /// <summary>
        /// 取商品收支借还分析
        /// </summary>
        public DataTable GetJieHuanFenXiList(int userId, DateTime itemBuyDate, string orderBy)
        {
            DataTable lists = new DataTable();

            SqlParameter[] parms = {
					new SqlParameter(PARM_USER_ID, SqlDbType.Int),
					new SqlParameter(PARM_ITEM_BUY_DATE, SqlDbType.DateTime),
					new SqlParameter(PARM_ORDER_BY, SqlDbType.NVarChar, 10)
			};
            parms[0].Value = userId;
            parms[1].Value = itemBuyDate;
            parms[2].Value = orderBy;

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.StoredProcedure, SQL_SELECT_JIE_HUAN_FEN_XI_LIST, parms))
            {
                lists.Load(rdr);
            }

            return lists;
        }

        /// <summary>
        /// 取最多消费商品统计
        /// </summary>
        public DataTable GetTongJiWithItemNameCount()
        {
            DataTable lists = new DataTable();

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.Text, SQL_SELECT_TONG_JI_WITH_ITEM_NAME_COUNT, null))
            {
                lists.Load(rdr);
            }

            return lists;
        }

        /// <summary>
        /// 取最高消费价格统计
        /// </summary>
        public DataTable GetTongJiWithItemPriceMax()
        {
            DataTable lists = new DataTable();

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.Text, SQL_SELECT_TONG_JI_WITH_ITEM_PRICE_MAX, null))
            {
                lists.Load(rdr);
            }

            return lists;
        }

        /// <summary>
        /// 取最后消费商品统计
        /// </summary>
        public DataTable GetTongJiWithItemAddLast()
        {
            DataTable lists = new DataTable();

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.Text, SQL_SELECT_TONG_JI_WITH_ITEM_ADD_LAST, null))
            {
                lists.Load(rdr);
            }

            return lists;
        }

        /// <summary>
        /// 取最多消费用户统计
        /// </summary>
        public DataTable GetTongJiWithUserItemCount()
        {
            DataTable lists = new DataTable();

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.Text, SQL_SELECT_TONG_JI_WITH_USER_ITEM_COUNT, null))
            {
                lists.Load(rdr);
            }

            return lists;
        }

        /// <summary>
        /// 取最后用户消费商品统计
        /// </summary>
        public DataTable GetTongJiWithUserItemLast()
        {
            DataTable lists = new DataTable();

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.Text, SQL_SELECT_TONG_JI_WITH_USER_ITEM_LAST, null))
            {
                lists.Load(rdr);
            }

            return lists;
        }

        /// <summary>
        /// 取最后用户注册会员统计
        /// </summary>
        public DataTable GetTongJiWithUserAddLast()
        {
            DataTable lists = new DataTable();

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.Text, SQL_SELECT_TONG_JI_WITH_USER_ADD_LAST, null))
            {
                lists.Load(rdr);
            }

            return lists;
        }

        /// <summary>
        /// 取商品收支借还列表
        /// </summary>
        public DataTable GetShouZhiJieHuanList(int userId, DateTime itemBuyDate)
        {
            DataTable lists = new DataTable();

            SqlParameter[] parms = {
					new SqlParameter(PARM_USER_ID, SqlDbType.Int),
					new SqlParameter(PARM_ITEM_BUY_DATE, SqlDbType.DateTime)
			};
            parms[0].Value = userId;
            parms[1].Value = itemBuyDate;

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.StoredProcedure, SQL_SELECT_SHOU_ZHI_JIE_HUAN_LIST, parms))
            {
                lists.Load(rdr);
            }

            return lists;
        }

        /// <summary>
        /// 取商品收支借还列表V6
        /// </summary>
        public DataTable GetShouZhiJieHuanListV6(int userId, DateTime itemBuyDate)
        {
            DataTable lists = new DataTable();

            SqlParameter[] parms = {
					new SqlParameter(PARM_USER_ID, SqlDbType.Int),
					new SqlParameter(PARM_ITEM_BUY_DATE, SqlDbType.DateTime)
			};
            parms[0].Value = userId;
            parms[1].Value = itemBuyDate;

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.StoredProcedure, SQL_SELECT_SHOU_ZHI_JIE_HUAN_LIST_V6, parms))
            {
                lists.Load(rdr);
            }

            return lists;
        }

        /// <summary>
        /// 根据时间段取活动消费统计
        /// </summary>
        public DataTable GetAdminTongJiHuoDong(DateTime beginDate, DateTime endDate)
        {
            DataTable lists = new DataTable();

            SqlParameter[] parms = {
					new SqlParameter(PARM_BEGIN_DATE, SqlDbType.DateTime),
					new SqlParameter(PARM_END_DATE, SqlDbType.DateTime)
			};
            parms[0].Value = beginDate;
            parms[1].Value = endDate;

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.StoredProcedure, SQL_SELECT_TONG_JI_HUO_DONG, parms))
            {
                lists.Load(rdr);
            }

            return lists;
        }

        /// <summary>
        /// 根据时间段取全部消费统计
        /// </summary>
        public DataTable GetAdminTongJiQuanBu()
        {
            DataTable lists = new DataTable();

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.StoredProcedure, SQL_SELECT_TONG_JI_QUAN_BU, null))
            {
                lists.Load(rdr);
            }

            return lists;
        }

        /// <summary>
        /// 根据时间段取新增消费统计
        /// </summary>
        public DataTable GetAdminTongJiXinZeng(DateTime beginDate, DateTime endDate)
        {
            DataTable lists = new DataTable();

            SqlParameter[] parms = {
					new SqlParameter(PARM_BEGIN_DATE, SqlDbType.DateTime),
					new SqlParameter(PARM_END_DATE, SqlDbType.DateTime)
			};
            parms[0].Value = beginDate;
            parms[1].Value = endDate;

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.StoredProcedure, SQL_SELECT_TONG_JI_XIN_ZENG, parms))
            {
                lists.Load(rdr);
            }

            return lists;
        }

    }
}
