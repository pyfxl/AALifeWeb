using AALife.DBUtility;
using AALife.Model;
using System;
using System.Data;
using System.Data.SqlClient;

namespace AALife.DAL
{
    public class ViewTableDAL
    {
        private static readonly string PARM_PAGE_ID = "@PageID";
        private static readonly string PARM_USER_ID = "@UserID";
        private static readonly string PARM_DATE_START = "@DateStart";
        private static readonly string PARM_DATA_END = "@DateEnd";
        private static readonly string PARM_PORTAL = "@Portal";
        private static readonly string PARM_VERSION = "@Version";
        private static readonly string PARM_BROWSER = "@Browser";
        private static readonly string PARM_WIDTH = "@Width";
        private static readonly string PARM_HEIGHT = "@Height";
        private static readonly string PARM_IP = "@IP";
        private static readonly string PARM_REMARK = "@Remark";
        private static readonly string PARM_NETWORK = "@Network";

        private static readonly string SQL_INSERT_VIEW = "InsertView_v7";
        
        /// <summary>
        /// 插入查看
        /// </summary>
        public bool InsertView(ViewInfo view)
        {
            SqlParameter[] parms = ModelToParms(view);

            int result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionString, CommandType.StoredProcedure, SQL_INSERT_VIEW, parms);

            return result > 0;
        }
        
        /// <summary>
        /// 钱包实体转参数
        /// </summary>
        public static SqlParameter[] ModelToParms(ViewInfo view)
        {
            SqlParameter[] parms = {
                    new SqlParameter("@PageID", SqlDbType.Int),
                    new SqlParameter("@UserID", SqlDbType.Int),
                    new SqlParameter("@DateStart", SqlDbType.DateTime),
                    new SqlParameter("@DateEnd", SqlDbType.DateTime),
                    new SqlParameter("@Portal", SqlDbType.NVarChar, 10),
                    new SqlParameter("@Version", SqlDbType.NVarChar, 10),
                    new SqlParameter("@Browser", SqlDbType.NVarChar, 10),
                    new SqlParameter("@Width", SqlDbType.Int),
                    new SqlParameter("@Height", SqlDbType.Int),
                    new SqlParameter("@IP", SqlDbType.NVarChar, 50),
                    new SqlParameter("@Remark", SqlDbType.NVarChar, 100),
                    new SqlParameter("@Network", SqlDbType.NVarChar, 10)
            };
            parms[0].Value = view.PageID;
            parms[1].Value = view.UserID;
            parms[2].Value = view.DateStart;
            parms[3].Value = view.DateEnd;
            parms[4].Value = view.Portal;
            parms[5].Value = view.Version;
            parms[6].Value = view.Browser;
            parms[7].Value = view.Width;
            parms[8].Value = view.Height;
            parms[9].Value = view.IP;
            parms[10].Value = view.Remark;
            parms[11].Value = view.Network;

            return parms;
        }
        
    }
}
