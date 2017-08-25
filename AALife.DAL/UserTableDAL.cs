using AALife.DBUtility;
using AALife.Model;
using NLog;
using System;
using System.Data;
using System.Data.SqlClient;

namespace AALife.DAL
{
    public class UserTableDAL
    {
        public static Logger log = LogManager.GetCurrentClassLogger();

        private static readonly string PARM_USER_ID = "@UserID";
        private static readonly string PARM_USER_NAME = "@UserName";
        private static readonly string PARM_USER_PASSWORD = "@UserPassword";
        private static readonly string PARM_BEGIN_DATE = "@BeginDate";
        private static readonly string PARM_END_DATE = "@EndDate";
        private static readonly string PARM_KEYWORDS = "@Keywords";

        private static readonly string SQL_SELECT_USER_BY_USER_ID = string.Format(@"select * from UserTable with(nolock) where UserID = {0}", PARM_USER_ID);
        private static readonly string SQL_SELECT_USER_BY_USER_NAME = string.Format(@"select * from UserTable with(nolock) where UserName collate Chinese_PRC_CS_AS_WS = {0}", PARM_USER_NAME);
        private static readonly string SQL_SELECT_USER_BY_USER_PASSWORD = string.Format(@"select * from UserTable with(nolock) where UserName collate Chinese_PRC_CS_AS_WS = {0} and UserPassword = {1}", PARM_USER_NAME, PARM_USER_PASSWORD);
        private static readonly string SQL_SELECT_USER_LIST_BY_DATE = string.Format(@"select * from UserTable with(nolock) where CreateDate between {0} and {1} order by UserID desc", PARM_BEGIN_DATE, PARM_END_DATE);
        private static readonly string SQL_SELECT_USER_LIST = "select * from UserTable order by UserID desc";
        private static readonly string SQL_SELECT_USER_LIST_BY_KEYWORDS = "GetUserListByKeywords_v5";
        private static readonly string SQL_SELECT_USER_LIST_WITH_SYNC = string.Format(@"select * from UserTable with(nolock) where UserID = {0} and Synchronize = 1", PARM_USER_ID);
        private static readonly string SQL_INSERT_USER = "InsertUser_v5";
        private static readonly string SQL_UPDATE_USER = "UpdateUser_v5";
        private static readonly string SQL_SELECT_USER_EXISTS = string.Format(@"select count(0) from UserTable with(nolock) where UserName = {0}", PARM_USER_NAME);
        private static readonly string SQL_SELECT_USER_LOGIN = string.Format(@"select count(0) from UserTable with(nolock) where UserName collate Chinese_PRC_CS_AS_WS = {0} and UserPassword = {1}", PARM_USER_NAME, PARM_USER_PASSWORD);
        private static readonly string SQL_DELETE_USER = "DeleteUser_v5";
        private static readonly string SQL_DELETE_USER_DATA = "DeleteUserData_v5";
        private static readonly string SQL_SELECT_USER_WORK_DAY = "select * from WorkDayTable";
        private static readonly string SQL_UPDATE_USER_LIST_WEB_BACK = string.Format(@"update UserTable set Synchronize = 0 where UserID = {0}", PARM_USER_ID);
        private static readonly string SQL_UPDATE_SYNC_BY_USER_ID = "UpdateSyncByUserId_v5";

        /// <summary>
        /// 根据用户ID取用户，返回UserInfo
        /// </summary>
        public UserInfo GetUserByUserId(int userId)
        {
            UserInfo user = new UserInfo();

            SqlParameter parm = new SqlParameter(PARM_USER_ID, SqlDbType.Int);
            parm.Value = userId;

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.Text, SQL_SELECT_USER_BY_USER_ID, parm))
            {
                while (rdr.Read())
                {
                    user = DataToModel(rdr);
                }
            }

            return user;
        }

        /// <summary>
        /// 根据用户ID取用户，返回DataTable
        /// </summary>
        public DataTable GetUserDataTableByUserId(int userId)
        {
            DataTable user = new DataTable();

            SqlParameter parm = new SqlParameter(PARM_USER_ID, SqlDbType.Int);
            parm.Value = userId;

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.Text, SQL_SELECT_USER_BY_USER_ID, parm))
            {
                user.Load(rdr);
            }

            return user;
        }

        /// <summary>
        /// 根据用户名取用户
        /// </summary>
        public UserInfo GetUserByUserName(string userName)
        {
            UserInfo user = new UserInfo();

            SqlParameter parm = new SqlParameter(PARM_USER_NAME, SqlDbType.NVarChar, 20);
            parm.Value = userName;

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.Text, SQL_SELECT_USER_BY_USER_NAME, parm))
            {
                while (rdr.Read())
                {
                    user = DataToModel(rdr);
                }
            }

            return user;
        }

        /// <summary>
        /// 根据用户名和密码取用户
        /// </summary>
        public UserInfo GetUserByUserPassword(string userName, string userPassword)
        {
            UserInfo user = new UserInfo();

            SqlParameter[] parms = { 
                    new SqlParameter(PARM_USER_NAME, SqlDbType.NVarChar, 20), 
                    new SqlParameter(PARM_USER_PASSWORD, SqlDbType.NVarChar, 20) 
            };
            parms[0].Value = userName;
            parms[1].Value = userPassword;

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.Text, SQL_SELECT_USER_BY_USER_PASSWORD, parms))
            {
                while (rdr.Read())
                {
                    user = DataToModel(rdr);
                }
            }

            return user;
        }

        /// <summary>
        /// 根据时间段取用户列表
        /// </summary>
        public DataTable GetUserListByDate(DateTime beginDate, DateTime endDate)
        {
            DataTable lists = new DataTable();

            SqlParameter[] parms = {
					new SqlParameter(PARM_BEGIN_DATE, SqlDbType.DateTime),
					new SqlParameter(PARM_END_DATE, SqlDbType.DateTime)
			};
            parms[0].Value = beginDate;
            parms[1].Value = endDate;

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.Text, SQL_SELECT_USER_LIST_BY_DATE, parms))
            {
                lists.Load(rdr);
            }

            return lists;
        }

        /// <summary>
        /// 取用户列表
        /// </summary>
        public DataTable GetUserList()
        {
            DataTable lists = new DataTable();

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.Text, SQL_SELECT_USER_LIST, null))
            {
                lists.Load(rdr);
            }

            return lists;
        }

        /// <summary>
        /// 根据关键字取用户列表
        /// </summary>
        public DataTable GetUserListByKeywords(string keywords)
        {
            DataTable lists = new DataTable();

            SqlParameter parm = new SqlParameter(PARM_KEYWORDS, SqlDbType.NVarChar, 20);
            parm.Value = keywords;

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.StoredProcedure, SQL_SELECT_USER_LIST_BY_KEYWORDS, parm))
            {
                lists.Load(rdr);
            }

            return lists;
        }

        /// <summary>
        /// 取同步用户列表
        /// </summary>
        public DataTable GetUserListWithSync(int userId)
        {
            DataTable lists = new DataTable();

            SqlParameter parm = new SqlParameter(PARM_USER_ID, SqlDbType.Int);
            parm.Value = userId;

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.Text, SQL_SELECT_USER_LIST_WITH_SYNC, parm))
            {
                lists.Load(rdr);
            }

            return lists;
        }

        /// <summary>
        /// 插入用户
        /// </summary>
        public bool InsertUser(UserInfo user)
        {
            SqlParameter[] parms = ModelToParms(user);

            int result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionString, CommandType.StoredProcedure, SQL_INSERT_USER, parms);
            
            return result > 0;
        }

        /// <summary>
        /// 修改用户
        /// </summary>
        public bool UpdateUser(UserInfo user)
        {
            SqlParameter[] parms = ModelToParms(user);

            int result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionString, CommandType.StoredProcedure, SQL_UPDATE_USER, parms);

            return result > 0;
        }

        /// <summary>
        /// 用户是否存在
        /// </summary>
        public bool UserExists(string userName)
        {
            SqlParameter parm = new SqlParameter(PARM_USER_NAME, SqlDbType.NVarChar, 20);
            parm.Value = userName;

            int result = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnectionString, CommandType.Text, SQL_SELECT_USER_EXISTS, parm));

            return result > 0;
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        public bool UserLogin(string userName, string userPassword)
        {
            SqlParameter[] parms = { 
                    new SqlParameter(PARM_USER_NAME, SqlDbType.NVarChar, 20), 
                    new SqlParameter(PARM_USER_PASSWORD, SqlDbType.NVarChar, 20) 
            };
            parms[0].Value = userName;
            parms[1].Value = userPassword;

            int result = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnectionString, CommandType.Text, SQL_SELECT_USER_LOGIN, parms));

            return result > 0;
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        public bool DeleteUser(int userId)
        {
            SqlParameter parm = new SqlParameter(PARM_USER_ID, SqlDbType.Int);
            parm.Value = userId;

            int result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionString, CommandType.StoredProcedure, SQL_DELETE_USER, parm);

            return result > 0;
        }

        /// <summary>
        /// 删除用户数据
        /// </summary>
        public bool DeleteUserData(int userId)
        {
            SqlParameter parm = new SqlParameter(PARM_USER_ID, SqlDbType.Int);
            parm.Value = userId;

            int result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionString, CommandType.StoredProcedure, SQL_DELETE_USER_DATA, parm);

            return result > 0;
        }

        /// <summary>
        /// 取用户工作日列表
        /// </summary>
        public DataTable GetUserWorkDay()
        {
            DataTable dt = new DataTable();

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.Text, SQL_SELECT_USER_WORK_DAY, null))
            {
                dt.Load(rdr);
            }

            return dt;
        }

        /// <summary>
        /// 修改用户同步返回
        /// </summary>
        public bool UpdateUserListWebBack(int userId)
        {
            SqlParameter parm = new SqlParameter(PARM_USER_ID, SqlDbType.Int);
            parm.Value = userId;

            int result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionString, CommandType.Text, SQL_UPDATE_USER_LIST_WEB_BACK, parm);

            return result > 0;
        }

        /// <summary>
        /// 修改同步状态
        /// </summary>
        public bool UpdateSyncByUserId(int userId)
        {
            SqlParameter parm = new SqlParameter(PARM_USER_ID, SqlDbType.Int);
            parm.Value = userId;

            int result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionString, CommandType.StoredProcedure, SQL_UPDATE_SYNC_BY_USER_ID, parm);

            return result > 0;
        }

        /// <summary>
        /// 用户实体转参数
        /// </summary>
        public static SqlParameter[] ModelToParms(UserInfo user)
        {
            SqlParameter[] parms = {
					new SqlParameter("@UserID", SqlDbType.Int),
					new SqlParameter("@UserName", SqlDbType.NVarChar, 20),
					new SqlParameter("@UserPassword", SqlDbType.NVarChar, 20),
					new SqlParameter("@UserNickName", SqlDbType.NVarChar, 50),
					new SqlParameter("@UserImage", SqlDbType.NVarChar, 200),
					new SqlParameter("@UserPhone", SqlDbType.NVarChar, 20),
					new SqlParameter("@UserEmail", SqlDbType.NVarChar, 100),
					new SqlParameter("@UserTheme", SqlDbType.NVarChar, 10),
					new SqlParameter("@UserLevel", SqlDbType.TinyInt),
					new SqlParameter("@UserFrom", SqlDbType.NVarChar, 10),
					new SqlParameter("@ModifyDate", SqlDbType.DateTime),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@UserCity", SqlDbType.NVarChar, 20),
					new SqlParameter("@UserMoney", SqlDbType.Decimal),
					new SqlParameter("@UserWorkDay", SqlDbType.NVarChar, 2),
					new SqlParameter("@UserFunction", SqlDbType.NVarChar, 20),
					new SqlParameter("@CategoryRate", SqlDbType.Int),
					new SqlParameter("@Synchronize", SqlDbType.TinyInt),
					new SqlParameter("@MoneyStart", SqlDbType.Decimal),
					new SqlParameter("@IsUpdate", SqlDbType.TinyInt)
			};
            parms[0].Value = user.UserID;
            parms[1].Value = user.UserName;
            parms[2].Value = user.UserPassword;
            parms[3].Value = user.UserNickName;
            parms[4].Value = user.UserImage;
            parms[5].Value = user.UserPhone;
            parms[6].Value = user.UserEmail;
            parms[7].Value = user.UserTheme;
            parms[8].Value = user.UserLevel;
            parms[9].Value = user.UserFrom;
            parms[10].Value = user.ModifyDate;
            parms[11].Value = user.CreateDate;
            parms[12].Value = user.UserCity;
            parms[13].Value = user.UserMoney;
            parms[14].Value = user.UserWorkDay;
            parms[15].Value = user.UserFunction;
            parms[16].Value = user.CategoryRate;
            parms[17].Value = user.Synchronize;
            parms[18].Value = user.MoneyStart;
            parms[19].Value = user.IsUpdate;

            return parms;
        }

        /// <summary>
        /// 数据转用户实体
        /// </summary>
        public static UserInfo DataToModel(SqlDataReader rdr)
        {
            UserInfo user = new UserInfo();
            if (!rdr.IsDBNull(0)) user.UserID = rdr.GetInt32(0);
            if (!rdr.IsDBNull(1)) user.UserName = rdr.GetString(1);
            if (!rdr.IsDBNull(2)) user.UserPassword = rdr.GetString(2);
            if (!rdr.IsDBNull(3)) user.UserNickName = rdr.GetString(3);
            if (!rdr.IsDBNull(4)) user.UserImage = rdr.GetString(4);
            if (!rdr.IsDBNull(5)) user.UserPhone = rdr.GetString(5);
            if (!rdr.IsDBNull(6)) user.UserEmail = rdr.GetString(6);
            if (!rdr.IsDBNull(7)) user.UserTheme = rdr.GetString(7);
            if (!rdr.IsDBNull(8)) user.UserLevel = rdr.GetByte(8);
            if (!rdr.IsDBNull(9)) user.UserFrom = rdr.GetString(9);
            if (!rdr.IsDBNull(10)) user.ModifyDate = rdr.GetDateTime(10);
            if (!rdr.IsDBNull(11)) user.CreateDate = rdr.GetDateTime(11);
            if (!rdr.IsDBNull(12)) user.UserCity = rdr.GetString(12);
            if (!rdr.IsDBNull(13)) user.UserMoney = rdr.GetDecimal(13);
            if (!rdr.IsDBNull(14)) user.UserWorkDay = rdr.GetString(14);
            if (!rdr.IsDBNull(15)) user.UserFunction = rdr.GetString(15);
            if (!rdr.IsDBNull(16)) user.CategoryRate = rdr.GetInt32(16);
            if (!rdr.IsDBNull(17)) user.Synchronize = rdr.GetByte(17);
            if (!rdr.IsDBNull(18)) user.MoneyStart = rdr.GetDecimal(18);
            if (!rdr.IsDBNull(19)) user.IsUpdate = rdr.GetByte(19);

            return user;
        }

    }
}
