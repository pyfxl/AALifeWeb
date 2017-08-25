
using AALife.DBUtility;
using System.Data;
using System.Data.SqlClient;
namespace AALife.DAL
{
    public class DeleteTableDAL
    {
        private static readonly string PARM_USER_ID = "@UserID";

        private static readonly string SQL_SELECT_DELETE_LIST = "select ItemID, isnull(ItemAppID, 0) as ItemAppID from DeleteTable";
        private static readonly string SQL_SELECT_DELETE_LIST_BY_USER_ID = string.Format(@"select ItemID, isnull(ItemAppID, 0) as ItemAppID from DeleteTable with(nolock) where UserID = {0} order by DeleteID asc", PARM_USER_ID);
        private static readonly string SQL_UPDATE_DELETE_LIST_WEB_BACK = string.Format(@"delete from DeleteTable where UserID = {0}", PARM_USER_ID);

        /// <summary>
        /// 取删除列表
        /// </summary>
        public DataTable GetDeleteList()
        {
            DataTable lists = new DataTable();

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.Text, SQL_SELECT_DELETE_LIST, null))
            {
                lists.Load(rdr);
            }

            return lists;
        }

        /// <summary>
        /// 根据用户ID取删除列表
        /// </summary>
        public DataTable GetDeleteListByUserId(int userId)
        {
            DataTable lists = new DataTable();

            SqlParameter parm = new SqlParameter(PARM_USER_ID, SqlDbType.Int);
            parm.Value = userId;

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.Text, SQL_SELECT_DELETE_LIST_BY_USER_ID, parm))
            {
                lists.Load(rdr);
            }

            return lists;
        }

        /// <summary>
        /// 修改删除同步返回
        /// </summary>
        public bool UpdateDeleteListWebBack(int userId)
        {
            SqlParameter parm = new SqlParameter(PARM_USER_ID, SqlDbType.Int);
            parm.Value = userId;

            int result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionString, CommandType.Text, SQL_UPDATE_DELETE_LIST_WEB_BACK, parm);

            return result > 0;
        }

    }
}
