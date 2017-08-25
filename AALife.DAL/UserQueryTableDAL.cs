using AALife.DBUtility;
using AALife.Model;
using System;
using System.Data;
using System.Data.SqlClient;

namespace AALife.DAL
{
    public class UserQueryTableDAL
    {
        private static readonly string PARM_USER_ID = "@UserID";
        private static readonly string PARM_URL = "@URL";
        private static readonly string PARM_VALUE = "@Value";
        private static readonly string PARM_NAME = "@NAME";
        private static readonly string PARM_QUERY_ID = "@QUERYID";

        private static readonly string SQL_SELECT_USER_QUERY_LIST = string.Format(@"select * from UserQueryTable with(nolock) where UserID = {0} and UserQueryLive = 1", PARM_USER_ID);
        private static readonly string SQL_SELECT_USER_QUERY_BY_URL = string.Format(@"select * from UserQueryTable with(nolock) where UserID = {0} and UserQueryLive = 1 and UserQueryURL = {1}", PARM_USER_ID, PARM_URL);
        private static readonly string SQL_SELECT_USER_QUERY_BY_VALUE = string.Format(@"select * from UserQueryTable with(nolock) where UserID = {0} and UserQueryLive = 1 and UserQueryValue = {1}", PARM_USER_ID, PARM_VALUE);
        private static readonly string SQL_SELECT_USER_QUERY_BY_NAME = string.Format(@"select * from UserQueryTable with(nolock) where UserID = {0} and UserQueryLive = 1 and UserQueryName = {1}", PARM_USER_ID, PARM_NAME);
        private static readonly string SQL_SELECT_USER_QUERY_BY_ID = string.Format(@"select * from UserQueryTable with(nolock) where UserID = {0} and UserQueryLive = 1 and UserQueryID = {1}", PARM_USER_ID, PARM_QUERY_ID);
        private static readonly string SQL_INSERT_USER_QUERY = "InsertUserQuery_v6";
        private static readonly string SQL_UPDATE_USER_QUERY = "UpdateUserQuery_v6";
        
        /// <summary>
        /// 取查询列表
        /// </summary>
        public DataTable GetUserQueryList(int userId)
        {
            DataTable lists = new DataTable();

            SqlParameter parm = new SqlParameter(PARM_USER_ID, SqlDbType.Int);
            parm.Value = userId;

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.Text, SQL_SELECT_USER_QUERY_LIST, parm))
            {
                lists.Load(rdr);
            }

            return lists;
        }

        /// <summary>
        /// 插入查询
        /// </summary>
        public bool InsertUserQuery(UserQueryInfo query)
        {
            SqlParameter[] parms = ModelToParms(query);

            int result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionString, CommandType.StoredProcedure, SQL_INSERT_USER_QUERY, parms);

            return result > 0;
        }

        /// <summary>
        /// 修改查询
        /// </summary>
        public bool UpdateUserQuery(UserQueryInfo query)
        {
            SqlParameter[] parms = ModelToParms(query);

            int result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionString, CommandType.StoredProcedure, SQL_UPDATE_USER_QUERY, parms);

            return result > 0;
        }

        /// <summary>
        /// 根据URL取查询，返回实体
        /// </summary>
        public UserQueryInfo GetUserQueryByURL(int userId, string url)
        {
            UserQueryInfo query = new UserQueryInfo();

            SqlParameter[] parms = {
                   new SqlParameter(PARM_USER_ID, SqlDbType.Int),
                   new SqlParameter(PARM_URL, SqlDbType.NVarChar, 200)
            };
            parms[0].Value = userId;
            parms[1].Value = url;

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.Text, SQL_SELECT_USER_QUERY_BY_URL, parms))
            {
                while (rdr.Read())
                {
                    query = DataToModel(rdr);
                }
            }

            return query;
        }

        /// <summary>
        /// 根据Value取查询，返回实体
        /// </summary>
        public UserQueryInfo GetUserQueryByValue(int userId, string value)
        {
            UserQueryInfo query = new UserQueryInfo();

            SqlParameter[] parms = {
                   new SqlParameter(PARM_USER_ID, SqlDbType.Int),
                   new SqlParameter(PARM_VALUE, SqlDbType.NVarChar, 200)
            };
            parms[0].Value = userId;
            parms[1].Value = value;

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.Text, SQL_SELECT_USER_QUERY_BY_VALUE, parms))
            {
                while (rdr.Read())
                {
                    query = DataToModel(rdr);
                }
            }

            return query;
        }

        /// <summary>
        /// 根据名称取查询，返回实体
        /// </summary>
        public UserQueryInfo GetUserQueryByName(int userId, string name)
        {
            UserQueryInfo query = new UserQueryInfo();

            SqlParameter[] parms = {
                   new SqlParameter(PARM_USER_ID, SqlDbType.Int),
                   new SqlParameter(PARM_NAME, SqlDbType.NVarChar, 20)
            };
            parms[0].Value = userId;
            parms[1].Value = name;

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.Text, SQL_SELECT_USER_QUERY_BY_NAME, parms))
            {
                while (rdr.Read())
                {
                    query = DataToModel(rdr);
                }
            }

            return query;
        }

        /// <summary>
        /// 根据ID取查询，返回实体
        /// </summary>
        public UserQueryInfo GetUserQueryById(int userId, int queryId)
        {
            UserQueryInfo query = new UserQueryInfo();

            SqlParameter[] parms = {
                   new SqlParameter(PARM_USER_ID, SqlDbType.Int),
                   new SqlParameter(PARM_QUERY_ID, SqlDbType.Int)
            };
            parms[0].Value = userId;
            parms[1].Value = queryId;

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.Text, SQL_SELECT_USER_QUERY_BY_ID, parms))
            {
                while (rdr.Read())
                {
                    query = DataToModel(rdr);
                }
            }

            return query;
        }

        /// <summary>
        /// 查询实体转参数
        /// </summary>
        public static SqlParameter[] ModelToParms(UserQueryInfo query)
        {
            SqlParameter[] parms = {
					new SqlParameter("@UserQueryID", SqlDbType.Int),
					new SqlParameter("@UserQueryName", SqlDbType.NVarChar, 20),
					new SqlParameter("@UserQueryURL", SqlDbType.NVarChar, 200),
					new SqlParameter("@UserQueryValue", SqlDbType.NVarChar, 200),
					new SqlParameter("@UserQueryLive", SqlDbType.TinyInt),
					new SqlParameter("@UserID", SqlDbType.Int),
					new SqlParameter("@ModifyDate", SqlDbType.DateTime)
			};
            parms[0].Value = query.UserQueryID;
            parms[1].Value = query.UserQueryName;
            parms[2].Value = query.UserQueryURL;
            parms[3].Value = query.UserQueryValue;
            parms[4].Value = query.UserQueryLive;
            parms[5].Value = query.UserID;
            parms[6].Value = query.ModifyDate;

            return parms;
        }

        /// <summary>
        /// 数据转查询实体
        /// </summary>
        public static UserQueryInfo DataToModel(SqlDataReader rdr)
        {
            UserQueryInfo query = new UserQueryInfo();
            if (!rdr.IsDBNull(0)) query.UserQueryID = rdr.GetInt32(0);
            if (!rdr.IsDBNull(1)) query.UserQueryName = rdr.GetString(1);
            if (!rdr.IsDBNull(2)) query.UserQueryURL = rdr.GetString(2);
            if (!rdr.IsDBNull(3)) query.UserQueryValue = rdr.GetString(3);
            if (!rdr.IsDBNull(4)) query.UserQueryLive = rdr.GetByte(4);
            if (!rdr.IsDBNull(5)) query.UserID = rdr.GetInt32(5);
            if (!rdr.IsDBNull(6)) query.ModifyDate = rdr.GetDateTime(6);

            return query;
        }

    }
}
