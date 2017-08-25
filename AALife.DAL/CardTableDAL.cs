using AALife.DBUtility;
using AALife.Model;
using System;
using System.Data;
using System.Data.SqlClient;

namespace AALife.DAL
{
    public class CardTableDAL
    {
        private static readonly string PARM_USER_ID = "@UserID";
        private static readonly string PARM_CARD_ID = "@CDID";
        private static readonly string PARM_CARD_NAME = "@CardName";
        private static readonly string PARM_BEGIN_DATE = "@BeginDate";
        private static readonly string PARM_END_DATE = "@EndDate";

        private static readonly string SQL_SELECT_CARD_LIST = string.Format(@"select * from CardTableView with(nolock) where UserID = {0} order by CDID asc", PARM_USER_ID);
        private static readonly string SQL_SELECT_CARD_LIST_WITH_HOME = string.Format(@"select * from CardTableView with(nolock) where UserID = {0} and CardShow = 1 order by CDID asc", PARM_USER_ID);
        private static readonly string SQL_SELECT_CARD_LIST_BY_DATE = string.Format(@"select * from CardTable with(nolock) where ModifyDate between {0} and {1} order by CardID desc", PARM_BEGIN_DATE, PARM_END_DATE);
        private static readonly string SQL_SELECT_CARD_LIST_WITH_SYNC = string.Format(@"select * from CardTable with(nolock) where UserID = {0} and Synchronize = 1 order by CDID asc", PARM_USER_ID);
        private static readonly string SQL_INSERT_CARD = "InsertCard_v5";
        private static readonly string SQL_UPDATE_CARD = "UpdateCard_v5";
        private static readonly string SQL_SELECT_MAX_CARD_ID = string.Format(@"select isnull(max(CDID),0)+1 from CardTable with(nolock) where UserID = {0}", PARM_USER_ID);
        private static readonly string SQL_SELECT_CARD_BY_CARD_ID = string.Format(@"select * from CardTableView with(nolock) where UserID = {0} and CDID = {1}", PARM_USER_ID, PARM_CARD_ID);
        private static readonly string SQL_SELECT_CARD_BY_CARD_NAME = string.Format(@"select * from CardTableView with(nolock) where UserID = {0} and CardName = {1}", PARM_USER_ID, PARM_CARD_NAME);
        private static readonly string SQL_UPDATE_CARD_LIST_WEB_BACK = string.Format(@"update CardTable set Synchronize = 0 where UserID = {0}", PARM_USER_ID);
        
        /// <summary>
        /// 取首页钱包列表
        /// </summary>
        public DataTable GetCardListWithHome(int userId)
        {
            DataTable lists = new DataTable();

            SqlParameter parm = new SqlParameter(PARM_USER_ID, SqlDbType.Int);
            parm.Value = userId;

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.Text, SQL_SELECT_CARD_LIST_WITH_HOME, parm))
            {
                lists.Load(rdr);
            }

            return lists;
        }

        /// <summary>
        /// 取钱包列表
        /// </summary>
        public DataTable GetCardList(int userId)
        {
            DataTable lists = new DataTable();

            SqlParameter parm = new SqlParameter(PARM_USER_ID, SqlDbType.Int);
            parm.Value = userId;

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.Text, SQL_SELECT_CARD_LIST, parm))
            {
                lists.Load(rdr);
            }

            return lists;
        }

        /// <summary>
        /// 根据时间段取钱包
        /// </summary>
        public DataTable GetCardListByDate(DateTime beginDate, DateTime endDate)
        {
            DataTable lists = new DataTable();

            SqlParameter[] parms = {
					new SqlParameter(PARM_BEGIN_DATE, SqlDbType.DateTime),
					new SqlParameter(PARM_END_DATE, SqlDbType.DateTime)
			};
            parms[0].Value = beginDate;
            parms[1].Value = endDate;

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.Text, SQL_SELECT_CARD_LIST_BY_DATE, parms))
            {
                lists.Load(rdr);
            }

            return lists;
        }

        /// <summary>
        /// 取同步钱包列表
        /// </summary>
        public DataTable GetCardListWithSync(int userId)
        {
            DataTable lists = new DataTable();

            SqlParameter parm = new SqlParameter(PARM_USER_ID, SqlDbType.Int);
            parm.Value = userId;

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.Text, SQL_SELECT_CARD_LIST_WITH_SYNC, parm))
            {
                lists.Load(rdr);
            }

            return lists;
        }

        /// <summary>
        /// 插入钱包
        /// </summary>
        public bool InsertCard(CardInfo card)
        {
            SqlParameter[] parms = ModelToParms(card);

            int result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionString, CommandType.StoredProcedure, SQL_INSERT_CARD, parms);

            return result > 0;
        }

        /// <summary>
        /// 修改钱包
        /// </summary>
        public bool UpdateCard(CardInfo card)
        {
            SqlParameter[] parms = ModelToParms(card);

            int result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionString, CommandType.StoredProcedure, SQL_UPDATE_CARD, parms);

            return result > 0;
        }

        /// <summary>
        /// 取最大钱包ID
        /// </summary>
        public int GetMaxCardId(int userId)
        {
            SqlParameter parm = new SqlParameter(PARM_USER_ID, SqlDbType.Int);
            parm.Value = userId;

            int result = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnectionString, CommandType.Text, SQL_SELECT_MAX_CARD_ID, parm));

            return result % 2 == 0 ? result + 1 : result;
        }

        /// <summary>
        /// 根据钱包ID取钱包，返回CardInfo
        /// </summary>
        public CardInfo GetCardByCardId(int userId, int cardId)
        {
            CardInfo card = new CardInfo();

            SqlParameter[] parms = {
                    new SqlParameter(PARM_USER_ID, SqlDbType.Int),
                    new SqlParameter(PARM_CARD_ID, SqlDbType.Int)
            };
            parms[0].Value = userId;
            parms[1].Value = cardId;

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.Text, SQL_SELECT_CARD_BY_CARD_ID, parms))
            {
                while (rdr.Read())
                {
                    card = DataToModel(rdr);
                }
            }

            return card;
        }

        /// <summary>
        /// 根据钱包ID取钱包，返回DataTable
        /// </summary>
        public DataTable GetCardDataTableByCardId(int userId, int cardId)
        {
            DataTable card = new DataTable();

            SqlParameter[] parms = {
                    new SqlParameter(PARM_USER_ID, SqlDbType.Int),
                    new SqlParameter(PARM_CARD_ID, SqlDbType.Int)
            };
            parms[0].Value = userId;
            parms[1].Value = cardId;

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.Text, SQL_SELECT_CARD_BY_CARD_ID, parms))
            {
                card.Load(rdr);
            }

            return card;
        }

        /// <summary>
        /// 根据钱包名称取钱包
        /// </summary>
        public CardInfo GetCardByCardName(int userId, string cardName)
        {
            CardInfo card = new CardInfo();

            SqlParameter[] parms = {
                    new SqlParameter(PARM_USER_ID, SqlDbType.Int),
                    new SqlParameter(PARM_CARD_NAME, SqlDbType.NVarChar, 20)
            };
            parms[0].Value = userId;
            parms[1].Value = cardName;

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.Text, SQL_SELECT_CARD_BY_CARD_NAME, parms))
            {
                while (rdr.Read())
                {
                    card = DataToModel(rdr);
                }
            }

            return card;
        }

        /// <summary>
        /// 修改钱包同步返回
        /// </summary>
        public bool UpdateCardListWebBack(int userId)
        {
            SqlParameter parm = new SqlParameter(PARM_USER_ID, SqlDbType.Int);
            parm.Value = userId;

            int result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionString, CommandType.Text, SQL_UPDATE_CARD_LIST_WEB_BACK, parm);

            return result > 0;
        }

        /// <summary>
        /// 钱包实体转参数
        /// </summary>
        public static SqlParameter[] ModelToParms(CardInfo card)
        {
            SqlParameter[] parms = {
					new SqlParameter("@CardID", SqlDbType.Int),
					new SqlParameter("@CardName", SqlDbType.NVarChar, 20),
					new SqlParameter("@CardNumber", SqlDbType.NVarChar, 50),
					new SqlParameter("@CardImage", SqlDbType.NVarChar, 50),
					new SqlParameter("@CardMoney", SqlDbType.Decimal),
					new SqlParameter("@CardLive", SqlDbType.TinyInt),
					new SqlParameter("@Synchronize", SqlDbType.TinyInt),
					new SqlParameter("@ModifyDate", SqlDbType.DateTime),
					new SqlParameter("@CDID", SqlDbType.Int),
					new SqlParameter("@UserID", SqlDbType.Int),
					new SqlParameter("@MoneyStart", SqlDbType.Decimal),
					new SqlParameter("@CardShow", SqlDbType.TinyInt)
			};
            parms[0].Value = card.CardID;
            parms[1].Value = card.CardName;
            parms[2].Value = card.CardNumber;
            parms[3].Value = card.CardImage;
            parms[4].Value = card.CardMoney;
            parms[5].Value = card.CardLive;
            parms[6].Value = card.Synchronize;
            parms[7].Value = card.ModifyDate;
            parms[8].Value = card.CDID;
            parms[9].Value = card.UserID;
            parms[10].Value = card.MoneyStart;
            parms[11].Value = card.CardShow;

            return parms;
        }

        /// <summary>
        /// 数据转钱包实体
        /// </summary>
        public static CardInfo DataToModel(SqlDataReader rdr)
        {
            CardInfo card = new CardInfo();
            if (!rdr.IsDBNull(0)) card.CardID = rdr.GetInt32(0);
            if (!rdr.IsDBNull(1)) card.CardName = rdr.GetString(1);
            if (!rdr.IsDBNull(2)) card.CardNumber = rdr.GetString(2);
            if (!rdr.IsDBNull(3)) card.CardImage = rdr.GetString(3);
            if (!rdr.IsDBNull(4)) card.CardMoney = rdr.GetDecimal(4);
            if (!rdr.IsDBNull(5)) card.CardLive = rdr.GetByte(5);
            if (!rdr.IsDBNull(6)) card.Synchronize = rdr.GetByte(6);
            if (!rdr.IsDBNull(7)) card.ModifyDate = rdr.GetDateTime(7);
            if (!rdr.IsDBNull(8)) card.CDID = rdr.GetInt32(8);
            if (!rdr.IsDBNull(9)) card.UserID = rdr.GetInt32(9);
            if (!rdr.IsDBNull(10)) card.MoneyStart = rdr.GetDecimal(10);
            if (!rdr.IsDBNull(11)) card.CardShow = rdr.GetByte(11);

            return card;
        }

    }
}
