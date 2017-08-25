using AALife.DBUtility;
using AALife.Model;
using System;
using System.Data;
using System.Data.SqlClient;

namespace AALife.DAL
{
    public class UserCategoryTableDAL
    {
        private static readonly string PARM_USER_ID = "@UserID";
        private static readonly string PARM_CATEGORY_TYPE_NAME = "@CategoryTypeName";
        private static readonly string PARM_CATEGORY_TYPE_ID = "@CategoryTypeID";
        private static readonly string PARM_CATEGORY_RATE = "@CategoryRate";
        private static readonly string PARM_BEGIN_DATE = "@BeginDate";
        private static readonly string PARM_END_DATE = "@EndDate";

        private static readonly string SQL_SELECT_USER_CATEGORY_LIST = string.Format(@"select *, dbo.GetCategoryTypeRate_v5(CategoryTypePrice, {1}) as CategoryTypeRate from dbo.CategoryTypeTableFunc_v5({0}) where CategoryTypeLive = 1 order by CategoryTypeID asc", PARM_USER_ID, PARM_CATEGORY_RATE);
        private static readonly string SQL_SELECT_USER_CATEGORY_BY_DATE = string.Format(@"select * from UserCategoryTable with(nolock) where ModifyDate between {0} and {1} order by UserCategoryID desc", PARM_BEGIN_DATE, PARM_END_DATE);
        private static readonly string SQL_SELECT_USER_CATEGORY_BY_NAME = string.Format(@"select * from dbo.CategoryTypeTableFunc_v5({0}) where CategoryTypeName = {1} and CategoryTypeLive = 1", PARM_USER_ID, PARM_CATEGORY_TYPE_NAME);
        private static readonly string SQL_SELECT_USER_CATEGORY_LIST_WITH_SYNC = string.Format(@"select * from UserCategoryTable with(nolock) where UserID = {0} and Synchronize = 1 order by UserCategoryID asc", PARM_USER_ID);
        private static readonly string SQL_SELECT_MAX_CATEGORY_TYPE_ID = string.Format(@"select max(CategoryTypeID)+1 from dbo.CategoryTypeTableFunc_v5({0})", PARM_USER_ID);
        private static readonly string SQL_INSERT_USER_CATEGORY = "InsertUserCategory_v5";
        private static readonly string SQL_UPDATE_USER_CATEGORY_PROCEDURE = "UpdateUserCategory_v5";
        private static readonly string SQL_DELETE_USER_CATEGORY_PROCEDURE = "DeleteUserCategory_v5";
        private static readonly string SQL_DELETE_USER_CATEGORY = string.Format(@"delete from UserCategoryTable where UserID = {0} and CategoryTypeID = {1}", PARM_USER_ID, PARM_CATEGORY_TYPE_ID);
        private static readonly string SQL_UPDATE_CATEGORY_LIST_WEB_BACK = string.Format(@"update UserCategoryTable set Synchronize = 0 where UserID = {0}", PARM_USER_ID);
        private static readonly string SQL_SELECT_USER_CATEGORY_EXISTS_WITH_SYNC = string.Format(@"select count(0) from UserCategoryTable with(nolock) where UserID = {0} and CategoryTypeID = {1}", PARM_USER_ID, PARM_CATEGORY_TYPE_ID);
        
        /// <summary>
        /// 取用户类别列表
        /// </summary>
        public DataTable GetUserCategoryList(int userId, int categoryRate)
        {
            DataTable lists = new DataTable();

            SqlParameter[] parms = {
                    new SqlParameter(PARM_USER_ID, SqlDbType.Int),
                    new SqlParameter(PARM_CATEGORY_RATE, SqlDbType.Int)
            };
            parms[0].Value = userId;
            parms[1].Value = categoryRate;

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.Text, SQL_SELECT_USER_CATEGORY_LIST, parms))
            {
                lists.Load(rdr);
            }

            return lists;
        }

        /// <summary>
        /// 根据时间段取用户类别列表
        /// </summary>
        public DataTable GetUserCategoryListByDate(DateTime beginDate, DateTime endDate)
        {
            DataTable lists = new DataTable();

            SqlParameter[] parms = {
					new SqlParameter(PARM_BEGIN_DATE, SqlDbType.DateTime),
					new SqlParameter(PARM_END_DATE, SqlDbType.DateTime)
			};
            parms[0].Value = beginDate;
            parms[1].Value = endDate;

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.Text, SQL_SELECT_USER_CATEGORY_BY_DATE, parms))
            {
                lists.Load(rdr);
            }

            return lists;
        }

        /// <summary>
        /// 取同步用户类别列表
        /// </summary>
        public DataTable GetUserCategoryListWithSync(int userId)
        {
            DataTable lists = new DataTable();

            SqlParameter parm = new SqlParameter(PARM_USER_ID, SqlDbType.Int);
            parm.Value = userId;

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.Text, SQL_SELECT_USER_CATEGORY_LIST_WITH_SYNC, parm))
            {
                lists.Load(rdr);
            }

            return lists;
        }

        /// <summary>
        /// 取最大类别ID
        /// </summary>
        public int GetMaxCategoryTypeId(int userId)
        {
            SqlParameter parm = new SqlParameter(PARM_USER_ID, SqlDbType.Int);
            parm.Value = userId;

            int result = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnectionString, CommandType.Text, SQL_SELECT_MAX_CATEGORY_TYPE_ID, parm));

            return result % 2 == 0 ? result + 1 : result;
        }

        /// <summary>
        /// 插入用户类别
        /// </summary>
        public bool InsertUserCategory(UserCategoryInfo category)
        {
            SqlParameter[] parms = ModelToParms(category);

            int result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionString, CommandType.StoredProcedure, SQL_INSERT_USER_CATEGORY, parms);

            return result > 0;
        }

        /// <summary>
        /// 修改用户类别
        /// </summary>
        public bool UpdateUserCategory(UserCategoryInfo category)
        {
            SqlParameter[] parms = ModelToParms(category);

            int result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionString, CommandType.StoredProcedure, SQL_UPDATE_USER_CATEGORY_PROCEDURE, parms);

            return result > 0;
        }

        /// <summary>
        /// 根据类别ID删除用户类别
        /// </summary>
        public bool DeleteUserCategory(int userId, int catTypeId)
        {
            SqlParameter[] parms = {
                    new SqlParameter(PARM_USER_ID, SqlDbType.Int),
                    new SqlParameter(PARM_CATEGORY_TYPE_ID, SqlDbType.Int)
            };
            parms[0].Value = userId;
            parms[1].Value = catTypeId;

            int result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionString, CommandType.Text, SQL_DELETE_USER_CATEGORY, parms);

            return result > 0;
        }

        /// <summary>
        /// 删除用户类别
        /// </summary>
        public bool DeleteUserCategory(UserCategoryInfo category)
        {
            SqlParameter[] parms = ModelToParms(category);

            int result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionString, CommandType.StoredProcedure, SQL_DELETE_USER_CATEGORY_PROCEDURE, parms);

            return result > 0;
        }

        /// <summary>
        /// 根据类别名称取用户类别
        /// </summary>
        public UserCategoryInfo GetUserCategoryByName(int userId, string catTypeName)
        {
            UserCategoryInfo category = new UserCategoryInfo();

            SqlParameter[] parms = {
                    new SqlParameter(PARM_USER_ID, SqlDbType.Int),
                    new SqlParameter(PARM_CATEGORY_TYPE_NAME, SqlDbType.NVarChar, 20)
            };
            parms[0].Value = userId;
            parms[1].Value = catTypeName;

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.Text, SQL_SELECT_USER_CATEGORY_BY_NAME, parms))
            {
                while (rdr.Read())
                {
                    category = DataToModel(rdr);
                }
            }

            return category;
        }

        /// <summary>
        /// 修改类别同步返回
        /// </summary>
        public bool UpdateCategoryListWebBack(int userId)
        {
            SqlParameter parm = new SqlParameter(PARM_USER_ID, SqlDbType.Int);
            parm.Value = userId;

            int result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionString, CommandType.Text, SQL_UPDATE_CATEGORY_LIST_WEB_BACK, parm);

            return result > 0;
        }

        /// <summary>
        /// 同步用户类别是否存在
        /// </summary>
        public bool UserCategoryExistsWithSync(int userId, int catTypeId)
        {
            SqlParameter[] parms = {
                    new SqlParameter(PARM_USER_ID, SqlDbType.Int),
                    new SqlParameter(PARM_CATEGORY_TYPE_ID, SqlDbType.Int)
            };
            parms[0].Value = userId;
            parms[1].Value = catTypeId;

            int result = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnectionString, CommandType.Text, SQL_SELECT_USER_CATEGORY_EXISTS_WITH_SYNC, parms));

            return result > 0;
        }

        /// <summary>
        /// 用户类别实体转参数
        /// </summary>
        public static SqlParameter[] ModelToParms(UserCategoryInfo category)
        {
            SqlParameter[] parms = {
					new SqlParameter("@CategoryTypeID", SqlDbType.Int),
					new SqlParameter("@CategoryTypeName", SqlDbType.NVarChar, 20),
					new SqlParameter("@CategoryTypePrice", SqlDbType.Decimal),
					new SqlParameter("@UserID", SqlDbType.Int),
					new SqlParameter("@CategoryTypeLive", SqlDbType.TinyInt),
					new SqlParameter("@Synchronize", SqlDbType.TinyInt),
					new SqlParameter("@ModifyDate", SqlDbType.DateTime)
			};
            parms[0].Value = category.CategoryTypeID;
            parms[1].Value = category.CategoryTypeName;
            parms[2].Value = category.CategoryTypePrice;
            parms[3].Value = category.UserID;
            parms[4].Value = category.CategoryTypeLive;
            parms[5].Value = category.Synchronize;
            parms[6].Value = category.ModifyDate;

            return parms;
        }

        /// <summary>
        /// 数据转类别实体
        /// </summary>
        public static UserCategoryInfo DataToModel(SqlDataReader rdr)
        {
            UserCategoryInfo category = new UserCategoryInfo();
            if (!rdr.IsDBNull(0)) category.CategoryTypeID = rdr.GetInt32(0);
            if (!rdr.IsDBNull(1)) category.CategoryTypeName = rdr.GetString(1);
            if (!rdr.IsDBNull(2)) category.CategoryTypePrice = rdr.GetDecimal(2);

            return category;
        }

    }
}
