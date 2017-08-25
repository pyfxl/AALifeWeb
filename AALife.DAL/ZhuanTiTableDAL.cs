using AALife.DBUtility;
using AALife.Model;
using System;
using System.Data;
using System.Data.SqlClient;

namespace AALife.DAL
{
    public class ZhuanTiTableDAL
    {
        private static readonly string PARM_USER_ID = "@UserID";
        private static readonly string PARM_ZT_ID = "@ZTID";
        private static readonly string PARM_ZHUANTI_NAME = "@ZhuanTiName";
        private static readonly string PARM_BEGIN_DATE = "@BeginDate";
        private static readonly string PARM_END_DATE = "@EndDate";

        private static readonly string SQL_SELECT_ZHUANTI_LIST = string.Format(@"select * from ZhuanTiTableView with(nolock) where UserID = {0} order by ZTID asc", PARM_USER_ID);
        private static readonly string SQL_SELECT_ZHUANTI_LIST_BY_DATE = string.Format(@"select * from ZhuanTiTable with(nolock) where ModifyDate between {0} and {1} order by ZhuanTiID desc", PARM_BEGIN_DATE, PARM_END_DATE);
        private static readonly string SQL_SELECT_ZHUANTI_LIST_WITH_SYNC = string.Format(@"select * from ZhuanTiTable with(nolock) where UserID = {0} and Synchronize = 1 order by ZTID asc", PARM_USER_ID);
        private static readonly string SQL_INSERT_ZHUANTI = "InsertZhuanTi_v5";
        private static readonly string SQL_UPDATE_ZHUANTI = "UpdateZhuanTi_v5";
        private static readonly string SQL_SELECT_MAX_ZHUANTI_ID = string.Format(@"select isnull(max(ZTID),0)+1 from ZhuanTiTable with(nolock) where UserID = {0}", PARM_USER_ID);
        private static readonly string SQL_SELECT_ZHUANTI_BY_ZHUANTI_ID = string.Format(@"select * from ZhuanTiTableView with(nolock) where UserID = {0} and ZTID = {1}", PARM_USER_ID, PARM_ZT_ID);
        private static readonly string SQL_SELECT_ZHUANTI_BY_ZHUANTI_NAME = string.Format(@"select * from ZhuanTiTableView with(nolock) where UserID = {0} and ZhuanTiName = {1}", PARM_USER_ID, PARM_ZHUANTI_NAME);
        private static readonly string SQL_UPDATE_ZHUANTI_LIST_WEB_BACK = string.Format(@"update ZhuanTiTable set Synchronize = 0 where UserID = {0}", PARM_USER_ID);
        
        /// <summary>
        /// 取专题列表
        /// </summary>
        public DataTable GetZhuanTiList(int userId)
        {
            DataTable lists = new DataTable();

            SqlParameter parm = new SqlParameter(PARM_USER_ID, SqlDbType.Int);
            parm.Value = userId;

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.Text, SQL_SELECT_ZHUANTI_LIST, parm))
            {
                lists.Load(rdr);
            }

            return lists;
        }

        /// <summary>
        /// 根据时间段取专题列表
        /// </summary>
        public DataTable GetZhuanTiListByDate(DateTime beginDate, DateTime endDate)
        {
            DataTable lists = new DataTable();

            SqlParameter[] parms = {
					new SqlParameter(PARM_BEGIN_DATE, SqlDbType.DateTime),
					new SqlParameter(PARM_END_DATE, SqlDbType.DateTime)
			};
            parms[0].Value = beginDate;
            parms[1].Value = endDate;

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.Text, SQL_SELECT_ZHUANTI_LIST_BY_DATE, parms))
            {
                lists.Load(rdr);
            }

            return lists;
        }

        /// <summary>
        /// 取同步专题列表
        /// </summary>
        public DataTable GetZhuanTiListWithSync(int userId)
        {
            DataTable lists = new DataTable();

            SqlParameter parm = new SqlParameter(PARM_USER_ID, SqlDbType.Int);
            parm.Value = userId;

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.Text, SQL_SELECT_ZHUANTI_LIST_WITH_SYNC, parm))
            {
                lists.Load(rdr);
            }

            return lists;
        }

        /// <summary>
        /// 插入专题
        /// </summary>
        public bool InsertZhuanTi(ZhuanTiInfo zhuanTi)
        {
            SqlParameter[] parms = ModelToParms(zhuanTi);

            int result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionString, CommandType.StoredProcedure, SQL_INSERT_ZHUANTI, parms);

            return result > 0;
        }

        /// <summary>
        /// 修改专题
        /// </summary>
        public bool UpdateZhuanTi(ZhuanTiInfo zhuanTi)
        {
            SqlParameter[] parms = ModelToParms(zhuanTi);

            int result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionString, CommandType.StoredProcedure, SQL_UPDATE_ZHUANTI, parms);

            return result > 0;
        }

        /// <summary>
        /// 取最大专题ID
        /// </summary>
        public int GetMaxZhuanTiId(int userId)
        {
            SqlParameter parm = new SqlParameter(PARM_USER_ID, SqlDbType.Int);
            parm.Value = userId;

            int result = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnectionString, CommandType.Text, SQL_SELECT_MAX_ZHUANTI_ID, parm));

            return result % 2 == 0 ? result + 1 : result;
        }

        /// <summary>
        /// 根据专题ID取专题，返回实体
        /// </summary>
        public ZhuanTiInfo GetZhuanTiByZhuanTiId(int userId, int ztId)
        {
            ZhuanTiInfo zhuanTi = new ZhuanTiInfo();

            SqlParameter[] parms = {
                   new SqlParameter(PARM_USER_ID, SqlDbType.Int),
                   new SqlParameter(PARM_ZT_ID, SqlDbType.Int)
            };
            parms[0].Value = userId;
            parms[1].Value = ztId;

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.Text, SQL_SELECT_ZHUANTI_BY_ZHUANTI_ID, parms))
            {
                while (rdr.Read())
                {
                    zhuanTi = DataToModel(rdr);
                }
            }

            return zhuanTi;
        }

        /// <summary>
        /// 根据专题ID取专题，返回DataTable
        /// </summary>
        public DataTable GetZhuanTiDataTableByZhuanTiId(int userId, int ztId)
        {
            DataTable list = new DataTable();

            SqlParameter[] parms = {
                   new SqlParameter(PARM_USER_ID, SqlDbType.Int),
                   new SqlParameter(PARM_ZT_ID, SqlDbType.Int)
            };
            parms[0].Value = userId;
            parms[1].Value = ztId;

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.Text, SQL_SELECT_ZHUANTI_BY_ZHUANTI_ID, parms))
            {
                list.Load(rdr);
            }

            return list;
        }

        /// <summary>
        /// 根据专题名称取专题
        /// </summary>
        public ZhuanTiInfo GetZhuanTiByZhuanTiName(int userId, string zhuanTiName)
        {
            ZhuanTiInfo zhuanTi = new ZhuanTiInfo();

            SqlParameter[] parms = {
                   new SqlParameter(PARM_USER_ID, SqlDbType.Int),
                   new SqlParameter(PARM_ZHUANTI_NAME, SqlDbType.NVarChar, 20)
            };
            parms[0].Value = userId;
            parms[1].Value = zhuanTiName;

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.Text, SQL_SELECT_ZHUANTI_BY_ZHUANTI_NAME, parms))
            {
                while (rdr.Read())
                {
                    zhuanTi = DataToModel(rdr);
                }
            }

            return zhuanTi;
        }

        /// <summary>
        /// 修改专题同步返回
        /// </summary>
        public bool UpdateZhuanTiListWebBack(int userId)
        {
            SqlParameter parm = new SqlParameter(PARM_USER_ID, SqlDbType.Int);
            parm.Value = userId;

            int result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionString, CommandType.Text, SQL_UPDATE_ZHUANTI_LIST_WEB_BACK, parm);

            return result > 0;
        }

        /// <summary>
        /// 专题实体转参数
        /// </summary>
        public static SqlParameter[] ModelToParms(ZhuanTiInfo zhuanTi)
        {
            SqlParameter[] parms = {
					new SqlParameter("@ZhuanTiID", SqlDbType.Int),
					new SqlParameter("@ZhuanTiName", SqlDbType.NVarChar, 20),
					new SqlParameter("@ZhuanTiImage", SqlDbType.NVarChar, 200),
					new SqlParameter("@UserID", SqlDbType.Int),
					new SqlParameter("@ZhuanTiLive", SqlDbType.TinyInt),
					new SqlParameter("@Synchronize", SqlDbType.TinyInt),
					new SqlParameter("@ModifyDate", SqlDbType.DateTime),
					new SqlParameter("@ZTID", SqlDbType.Int)
			};
            parms[0].Value = zhuanTi.ZhuanTiID;
            parms[1].Value = zhuanTi.ZhuanTiName;
            parms[2].Value = zhuanTi.ZhuanTiImage;
            parms[3].Value = zhuanTi.UserID;
            parms[4].Value = zhuanTi.ZhuanTiLive;
            parms[5].Value = zhuanTi.Synchronize;
            parms[6].Value = zhuanTi.ModifyDate;
            parms[7].Value = zhuanTi.ZTID;

            return parms;
        }

        /// <summary>
        /// 数据转专题实体
        /// </summary>
        public static ZhuanTiInfo DataToModel(SqlDataReader rdr)
        {
            ZhuanTiInfo zhuanTi = new ZhuanTiInfo();
            if (!rdr.IsDBNull(0)) zhuanTi.ZhuanTiID = rdr.GetInt32(0);
            if (!rdr.IsDBNull(1)) zhuanTi.ZhuanTiName = rdr.GetString(1);
            if (!rdr.IsDBNull(2)) zhuanTi.ZhuanTiImage = rdr.GetString(2);
            if (!rdr.IsDBNull(3)) zhuanTi.UserID = rdr.GetInt32(3);
            if (!rdr.IsDBNull(4)) zhuanTi.ZhuanTiLive = rdr.GetByte(4);
            if (!rdr.IsDBNull(5)) zhuanTi.Synchronize = rdr.GetByte(5);
            if (!rdr.IsDBNull(6)) zhuanTi.ModifyDate = rdr.GetDateTime(6);
            if (!rdr.IsDBNull(7)) zhuanTi.ZTID = rdr.GetInt32(7);

            return zhuanTi;
        }

    }
}
