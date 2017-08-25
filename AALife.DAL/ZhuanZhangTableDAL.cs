using AALife.DBUtility;
using AALife.Model;
using System;
using System.Data;
using System.Data.SqlClient;

namespace AALife.DAL
{
    public class ZhuanZhangTableDAL
    {
        private static readonly string PARM_USER_ID = "@UserID";
        private static readonly string PARM_ZZ_ID = "@ZZID";
        private static readonly string PARM_BEGIN_DATE = "@BeginDate";
        private static readonly string PARM_END_DATE = "@EndDate";

        private static readonly string SQL_SELECT_ZHUANZHANG_LIST = string.Format(@"select * from ZhuanZhangTableView with(nolock) where UserID = {0} order by ZhuanZhangDate desc", PARM_USER_ID);
        private static readonly string SQL_SELECT_ZHUANZHANG_LIST_BY_DATE = string.Format(@"select * from ZhuanZhangTable with(nolock) where ModifyDate between {0} and {1} order by ZhuanZhangDate desc", PARM_BEGIN_DATE, PARM_END_DATE);
        private static readonly string SQL_INSERT_ZHUANZHANG = "InsertZhuanZhang_v5";
        private static readonly string SQL_UPDATE_ZHUANZHANG = "UpdateZhuanZhang_v5"; 
        private static readonly string SQL_SELECT_MAX_ZHUANZHANG_ID = string.Format(@"select isnull(max(ZZID),0)+1 from ZhuanZhangTable with(nolock) where UserID = {0}", PARM_USER_ID);
        private static readonly string SQL_SELECT_ZHUANZHANG_BY_ZZID = string.Format(@"select * from ZhuanZhangTableView with(nolock) where UserID = {0} and ZZID = {1}", PARM_USER_ID, PARM_ZZ_ID);
        private static readonly string SQL_SELECT_ZHUANZHANG_LIST_WITH_SYNC = string.Format(@"select * from ZhuanZhangTable with(nolock) where UserID = {0} and Synchronize = 1 order by ZZID asc", PARM_USER_ID);
        private static readonly string SQL_UPDATE_ZHUANZHANG_LIST_WEB_BACK = string.Format(@"update ZhuanZhangTable set Synchronize = 0 where UserID = {0}", PARM_USER_ID);
        
        /// <summary>
        /// 取转账列表
        /// </summary>
        public DataTable GetZhuanZhangList(int userId)
        {
            DataTable lists = new DataTable();

            SqlParameter parm = new SqlParameter(PARM_USER_ID, SqlDbType.Int);
            parm.Value = userId;

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.Text, SQL_SELECT_ZHUANZHANG_LIST, parm))
            {
                lists.Load(rdr);
            }

            return lists;
        }

        /// <summary>
        /// 根据时间段取转账列表
        /// </summary>
        public DataTable GetZhuanZhangListByDate(DateTime beginDate, DateTime endDate)
        {
            DataTable lists = new DataTable();

            SqlParameter[] parms = {
					new SqlParameter(PARM_BEGIN_DATE, SqlDbType.DateTime),
					new SqlParameter(PARM_END_DATE, SqlDbType.DateTime)
			};
            parms[0].Value = beginDate;
            parms[1].Value = endDate;

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.Text, SQL_SELECT_ZHUANZHANG_LIST_BY_DATE, parms))
            {
                lists.Load(rdr);
            }

            return lists;
        }

        /// <summary>
        /// 插入转账
        /// </summary>
        public bool InsertZhuanZhang(ZhuanZhangInfo zhuan)
        {
            SqlParameter[] parms = ModelToParms(zhuan);

            int result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionString, CommandType.StoredProcedure, SQL_INSERT_ZHUANZHANG, parms);

            return result > 0;
        }

        /// <summary>
        /// 修改转账
        /// </summary>
        public bool UpdateZhuanZhang(ZhuanZhangInfo zhuan)
        {
            SqlParameter[] parms = ModelToParms(zhuan);

            int result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionString, CommandType.StoredProcedure, SQL_UPDATE_ZHUANZHANG, parms);

            return result > 0;
        }

        /// <summary>
        /// 取最大转账ID
        /// </summary>
        public int GetMaxZhuanZhangId(int userId)
        {
            SqlParameter parm = new SqlParameter(PARM_USER_ID, SqlDbType.Int);
            parm.Value = userId;

            int result = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnectionString, CommandType.Text, SQL_SELECT_MAX_ZHUANZHANG_ID, parm));

            return result % 2 == 0 ? result + 1 : result;
        }

        /// <summary>
        /// 取同步转账列表
        /// </summary>
        public DataTable GetZhuanZhangListWithSync(int userId)
        {
            DataTable lists = new DataTable();

            SqlParameter parm = new SqlParameter(PARM_USER_ID, SqlDbType.Int);
            parm.Value = userId;

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.Text, SQL_SELECT_ZHUANZHANG_LIST_WITH_SYNC, parm))
            {
                lists.Load(rdr);
            }

            return lists;
        }

        /// <summary>
        /// 根据转账ID取转账，返回实体
        /// </summary>
        public ZhuanZhangInfo GetZhuanZhangByZZID(int userId, int zzId)
        {
            ZhuanZhangInfo zhang = new ZhuanZhangInfo();

            SqlParameter[] parms = {
                   new SqlParameter(PARM_USER_ID, SqlDbType.Int),
                   new SqlParameter(PARM_ZZ_ID, SqlDbType.Int)
            };
            parms[0].Value = userId;
            parms[1].Value = zzId;

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.Text, SQL_SELECT_ZHUANZHANG_BY_ZZID, parms))
            {
                while (rdr.Read())
                {
                    zhang = DataToModel(rdr);
                }
            }

            return zhang;
        }

        /// <summary>
        /// 修改转账同步返回
        /// </summary>
        public bool UpdateZhuanZhangListWebBack(int userId)
        {
            SqlParameter parm = new SqlParameter(PARM_USER_ID, SqlDbType.Int);
            parm.Value = userId;

            int result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionString, CommandType.Text, SQL_UPDATE_ZHUANZHANG_LIST_WEB_BACK, parm);

            return result > 0;
        }

        /// <summary>
        /// 转账实体转参数
        /// </summary>
        public static SqlParameter[] ModelToParms(ZhuanZhangInfo zhuan)
        {
            SqlParameter[] parms = {
					new SqlParameter("@ZhuanZhangID", SqlDbType.Int),
					new SqlParameter("@ZhuanZhangFrom", SqlDbType.Int),
					new SqlParameter("@ZhuanZhangTo", SqlDbType.Int),
					new SqlParameter("@ZhuanZhangDate", SqlDbType.DateTime),
					new SqlParameter("@ZhuanZhangMoney", SqlDbType.Decimal),
					new SqlParameter("@ZhuanZhangLive", SqlDbType.TinyInt),
					new SqlParameter("@Synchronize", SqlDbType.TinyInt),
					new SqlParameter("@ModifyDate", SqlDbType.DateTime),
					new SqlParameter("@UserID", SqlDbType.Int),
					new SqlParameter("@ZhuanZhangNote", SqlDbType.NVarChar, 100),
					new SqlParameter("@ZZID", SqlDbType.Int)
			};
            parms[0].Value = zhuan.ZhuanZhangID;
            parms[1].Value = zhuan.ZhuanZhangFrom;
            parms[2].Value = zhuan.ZhuanZhangTo;
            parms[3].Value = zhuan.ZhuanZhangDate;
            parms[4].Value = zhuan.ZhuanZhangMoney;
            parms[5].Value = zhuan.ZhuanZhangLive;
            parms[6].Value = zhuan.Synchronize;
            parms[7].Value = zhuan.ModifyDate;
            parms[8].Value = zhuan.UserID;
            parms[9].Value = zhuan.ZhuanZhangNote;
            parms[10].Value = zhuan.ZZID;

            return parms;
        }

        /// <summary>
        /// 数据转转账实体
        /// </summary>
        public static ZhuanZhangInfo DataToModel(SqlDataReader rdr)
        {
            ZhuanZhangInfo zhang = new ZhuanZhangInfo();
            if (!rdr.IsDBNull(0)) zhang.ZhuanZhangID = rdr.GetInt32(0);
            if (!rdr.IsDBNull(1)) zhang.ZhuanZhangFrom = rdr.GetInt32(1);
            if (!rdr.IsDBNull(2)) zhang.ZhuanZhangTo = rdr.GetInt32(2);
            if (!rdr.IsDBNull(3)) zhang.ZhuanZhangDate = rdr.GetDateTime(3);
            if (!rdr.IsDBNull(4)) zhang.ZhuanZhangMoney = rdr.GetDecimal(4);
            if (!rdr.IsDBNull(5)) zhang.ZhuanZhangLive = rdr.GetByte(5);
            if (!rdr.IsDBNull(7)) zhang.Synchronize = rdr.GetByte(6);
            if (!rdr.IsDBNull(6)) zhang.ModifyDate = rdr.GetDateTime(7);
            if (!rdr.IsDBNull(7)) zhang.UserID = rdr.GetInt32(8);
            if (!rdr.IsDBNull(7)) zhang.ZhuanZhangNote = rdr.GetString(9);
            if (!rdr.IsDBNull(7)) zhang.ZZID = rdr.GetInt32(10);

            return zhang;
        }

    }
}
