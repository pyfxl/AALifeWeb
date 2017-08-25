using AALife.DAL;
using AALife.Model;
using System;
using System.Data;

namespace AALife.BLL
{
    public class CardTableBLL
    {
        private static readonly CardTableDAL dal = new CardTableDAL();

        /// <summary>
        /// 取首页钱包列表
        /// </summary>
        public DataTable GetCardListWithHome(int userId)
        {
            return dal.GetCardListWithHome(userId);
        }

        /// <summary>
        /// 取钱包列表
        /// </summary>
        public DataTable GetCardList(int userId)
        {
            return dal.GetCardList(userId);
        }

        /// <summary>
        /// 根据时间段取钱包
        /// </summary>
        public DataTable GetCardListByDate(DateTime beginDate, DateTime endDate)
        {
            return dal.GetCardListByDate(beginDate, endDate);
        }

        /// <summary>
        /// 取同步钱包列表
        /// </summary>
        public DataTable GetCardListWithSync(int userId)
        {
            return dal.GetCardListWithSync(userId);
        }

        /// <summary>
        /// 插入钱包
        /// </summary>
        public bool InsertCard(CardInfo card)
        {
            return dal.InsertCard(card);
        }

        /// <summary>
        /// 修改钱包
        /// </summary>
        public bool UpdateCard(CardInfo card)
        {
            return dal.UpdateCard(card);
        }

        /// <summary>
        /// 取最大钱包ID
        /// </summary>
        public int GetMaxCardId(int userId)
        {
            return dal.GetMaxCardId(userId);
        }

        /// <summary>
        /// 根据钱包ID取钱包，返回CardInfo
        /// </summary>
        public CardInfo GetCardByCardId(int userId, int cardId)
        {
            return dal.GetCardByCardId(userId, cardId);
        }

        /// <summary>
        /// 根据钱包ID取钱包，返回DataTable
        /// </summary>
        public DataTable GetCardDataTableByCardId(int userId, int cardId)
        {
            return dal.GetCardDataTableByCardId(userId, cardId);
        }

        /// <summary>
        /// 根据钱包名称取钱包
        /// </summary>
        public CardInfo GetCardByCardName(int userId, string cardName)
        {
            return dal.GetCardByCardName(userId, cardName);
        }

        /// <summary>
        /// 修改钱包同步返回
        /// </summary>
        public bool UpdateCardListWebBack(int userId)
        {
            return dal.UpdateCardListWebBack(userId);
        }

    }
}
