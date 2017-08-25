using AALife.DBUtility;
using AALife.Model;
using System;
using System.Data;
using System.Data.SqlClient;

namespace AALife.DAL
{
    public class OAuthTableDAL
    {
        private static readonly string PARM_USER_ID = "@UserID";
        private static readonly string PARM_OPEN_ID = "@OpenID";
        private static readonly string PARM_OAUTH_ID = "@OAuthID";
        private static readonly string PARM_TO_USER_ID = "@OldUserID";
        private static readonly string PARM_BEGIN_DATE = "@BeginDate";
        private static readonly string PARM_END_DATE = "@EndDate";

        private static readonly string SQL_SELECT_OAUTH_LIST = string.Format(@"select * from OAuthTableView with(nolock) where UserID = {0} and OAuthBound = 1 order by OAuthID desc", PARM_USER_ID);
        private static readonly string SQL_SELECT_OAUTH_LIST_BY_DATE = string.Format(@"select * from OAuthTable with(nolock) where ModifyDate between {0} and {1} order by OAuthID desc", PARM_BEGIN_DATE, PARM_END_DATE);
        private static readonly string SQL_SELECT_OAUTH_LIST_BY_USER_ID = string.Format(@"select * from OAuthTable with(nolock) where UserID = {0}", PARM_USER_ID);
        private static readonly string SQL_INSERT_OAUTH = "InsertOAuth_v5";
        private static readonly string SQL_UPDATE_OAUTH = "UpdateOAuth_v5";
        private static readonly string SQL_SELECT_OAUTH_LOGIN = string.Format(@"select count(0) from OAuthTable with(nolock) where OpenID = {0}", PARM_OPEN_ID);
        private static readonly string SQL_SELECT_OAUTH_BY_USER_ID = string.Format(@"select * from OAuthTable with(nolock) where UserID = {0}", PARM_USER_ID);
        private static readonly string SQL_OAUTH_BOUND_CANCEL = string.Format(@"update OAuthTable set OAuthBound = 0, UserID = OldUserID, ModifyDate = getdate() where OAuthID = {0}", PARM_OAUTH_ID);
        private static readonly string SQL_OAUTH_BOUND_NEW_USER = string.Format(@"update OAuthTable set OAuthBound = 1, ModifyDate = getdate() where UserID = {0}", PARM_USER_ID);
        private static readonly string SQL_OAUTH_BOUND_OLD_USER = string.Format(@"update OAuthTable set OAuthBound = 1, UserID = {0}, ModifyDate = getdate() where UserID = {1}", PARM_TO_USER_ID, PARM_USER_ID);
        private static readonly string SQL_SELECT_OAUTH_BY_OPEN_ID = string.Format(@"select * from OAuthTable with(nolock) where OpenID = {0}", PARM_OPEN_ID);

        /// <summary>
        /// 取第三方登录列表
        /// </summary>
        public DataTable GetOAuthList(int userId)
        {
            DataTable lists = new DataTable();

            SqlParameter parm = new SqlParameter(PARM_USER_ID, SqlDbType.Int);
            parm.Value = userId;

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.Text, SQL_SELECT_OAUTH_LIST, parm))
            {
                lists.Load(rdr);
            }

            return lists;
        }

        /// <summary>
        /// 根据时间段取第三方登录列表
        /// </summary>
        public DataTable GetOAuthListByDate(DateTime beginDate, DateTime endDate)
        {
            DataTable lists = new DataTable();

            SqlParameter[] parms = {
					new SqlParameter(PARM_BEGIN_DATE, SqlDbType.DateTime),
					new SqlParameter(PARM_END_DATE, SqlDbType.DateTime)
			};
            parms[0].Value = beginDate;
            parms[1].Value = endDate;

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.Text, SQL_SELECT_OAUTH_LIST_BY_DATE, parms))
            {
                lists.Load(rdr);
            }

            return lists;
        }

        /// <summary>
        /// 取第三方登录列表，返回DataTable
        /// </summary>
        public DataTable GetOAuthListDataTableByUserId(int userId)
        {
            DataTable oauth = new DataTable();

            SqlParameter parm = new SqlParameter(PARM_USER_ID, SqlDbType.Int);
            parm.Value = userId;

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.Text, SQL_SELECT_OAUTH_LIST_BY_USER_ID, parm))
            {
                oauth.Load(rdr);
            }

            return oauth;
        }

        /// <summary>
        /// 插入第三方登录
        /// </summary>
        public bool InsertOAuth(OAuthInfo oauth)
        {
            SqlParameter[] parms = ModelToParms(oauth);

            int result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionString, CommandType.StoredProcedure, SQL_INSERT_OAUTH, parms);

            return result > 0;
        }

        /// <summary>
        /// 修改第三方登录
        /// </summary>
        public bool UpdateOAuth(OAuthInfo oauth)
        {
            SqlParameter[] parms = ModelToParms(oauth);

            int result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionString, CommandType.StoredProcedure, SQL_UPDATE_OAUTH, parms);

            return result > 0;
        }

        /// <summary>
        /// 根据OpenID登录第三方登录
        /// </summary>
        public bool OAuthLoginByOpenId(string openId)
        {
            SqlParameter parm = new SqlParameter(PARM_OPEN_ID, SqlDbType.NVarChar, 100);
            parm.Value = openId;

            int result = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnectionString, CommandType.Text, SQL_SELECT_OAUTH_LOGIN, parm));

            return result > 0;
        }

        /// <summary>
        /// 根据UserID取第三方登录
        /// </summary>
        public OAuthInfo GetOAuthByUserId(int userId)
        {
            OAuthInfo oauth = new OAuthInfo();

            SqlParameter parm = new SqlParameter(PARM_USER_ID, SqlDbType.Int);
            parm.Value = userId;

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.Text, SQL_SELECT_OAUTH_BY_USER_ID, parm))
            {
                while (rdr.Read())
                {
                    oauth = DataToModel(rdr);
                }
            }

            return oauth;
        }

        /// <summary>
        /// 根据OpenID取第三方登录
        /// </summary>
        public OAuthInfo GetOAuthByOpenId(string openId)
        {
            OAuthInfo oauth = new OAuthInfo();

            SqlParameter parm = new SqlParameter(PARM_OPEN_ID, SqlDbType.NVarChar, 100);
            parm.Value = openId;

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.Text, SQL_SELECT_OAUTH_BY_OPEN_ID, parm))
            {
                while (rdr.Read())
                {
                    oauth = DataToModel(rdr);
                }
            }

            return oauth;
        }

        /// <summary>
        /// 第三方登录绑定解除
        /// </summary>
        public bool OAuthBoundCancel(int oauthId)
        {
            SqlParameter parm = new SqlParameter(PARM_OAUTH_ID, SqlDbType.Int);
            parm.Value = oauthId;

            int result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionString, CommandType.Text, SQL_OAUTH_BOUND_CANCEL, parm);

            return result > 0;
        }

        /// <summary>
        /// 第三方登录绑定新用户
        /// </summary>
        public bool OAuthBoundNewUser(int userId)
        {
            SqlParameter parm = new SqlParameter(PARM_USER_ID, SqlDbType.Int);
            parm.Value = userId;

            int result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionString, CommandType.Text, SQL_OAUTH_BOUND_NEW_USER, parm);

            return result > 0;
        }

        /// <summary>
        /// 第三方登录绑定旧用户
        /// </summary>
        public bool OAuthBoundOldUser(int userId, int toUserId)
        {
            SqlParameter[] parms = {
                    new SqlParameter(PARM_USER_ID, SqlDbType.Int),
                    new SqlParameter(PARM_TO_USER_ID, SqlDbType.Int)
            };
            parms[0].Value = userId;
            parms[1].Value = toUserId;

            int result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionString, CommandType.Text, SQL_OAUTH_BOUND_OLD_USER, parms);

            return result > 0;
        }

        /// <summary>
        /// 第三方登录实体转参数
        /// </summary>
        public static SqlParameter[] ModelToParms(OAuthInfo oauth)
        {
            SqlParameter[] parms = {
					new SqlParameter("@OAuthID", SqlDbType.Int),
					new SqlParameter("@OpenID", SqlDbType.NVarChar, 100),
					new SqlParameter("@AccessToken", SqlDbType.NVarChar, 100),
					new SqlParameter("@UserID", SqlDbType.Int),
					new SqlParameter("@OldUserID", SqlDbType.Int),
					new SqlParameter("@OAuthBound", SqlDbType.TinyInt),
					new SqlParameter("@OAuthFrom", SqlDbType.NVarChar, 10),
					new SqlParameter("@ModifyDate", SqlDbType.DateTime)
			};
            parms[0].Value = oauth.OAuthID;
            parms[1].Value = oauth.OpenID;
            parms[2].Value = oauth.AccessToken;
            parms[3].Value = oauth.UserID;
            parms[4].Value = oauth.OldUserID;
            parms[5].Value = oauth.OAuthBound;
            parms[6].Value = oauth.OAuthFrom;
            parms[7].Value = oauth.ModifyDate;

            return parms;
        }

        /// <summary>
        /// 数据转第三方登录实体
        /// </summary>
        public static OAuthInfo DataToModel(SqlDataReader rdr)
        {
            OAuthInfo oauth = new OAuthInfo();
            if (!rdr.IsDBNull(0)) oauth.OAuthID = rdr.GetInt32(0);
            if (!rdr.IsDBNull(1)) oauth.OpenID = rdr.GetString(1);
            if (!rdr.IsDBNull(2)) oauth.AccessToken = rdr.GetString(2);
            if (!rdr.IsDBNull(3)) oauth.UserID = rdr.GetInt32(3);
            if (!rdr.IsDBNull(4)) oauth.OldUserID = rdr.GetInt32(4);
            if (!rdr.IsDBNull(5)) oauth.OAuthBound = rdr.GetByte(5);
            if (!rdr.IsDBNull(6)) oauth.OAuthFrom = rdr.GetString(6);
            if (!rdr.IsDBNull(7)) oauth.ModifyDate = rdr.GetDateTime(7);

            return oauth;
        }

    }
}
