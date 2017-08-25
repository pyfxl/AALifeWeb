using AALife.DAL;
using System;
using System.Data;

namespace AALife.BLL
{
    public class MonthBLL
    {
        private static readonly MonthDAL dal = new MonthDAL();
        
        /// <summary>
        /// 取每月商品列表
        /// </summary>
        public DataTable GetMonthList(int userId, DateTime itemBuyDate)
        {
            return dal.GetMonthList(userId, itemBuyDate);
        }

        /// <summary>
        /// 取每月商品明细
        /// </summary>
        public DataTable GetMonthListByItemBuyDate(int userId, DateTime itemBuyDate)
        {
            return dal.GetMonthListByItemBuyDate(userId, itemBuyDate);
        }

        /// <summary>
        /// 取商品分类总计
        /// </summary>
        public DataTable GetFenLeiZongJiList(int userId, DateTime itemBuyDate)
        {
            return dal.GetFenLeiZongJiList(userId, itemBuyDate);
        }

        /// <summary>
        /// 取商品分类总计明细
        /// </summary>
        public DataTable GetFenLeiZongJiMingXiList(int userId, DateTime itemBuyDate, int catTypeId, out decimal priceMax)
        {
            return dal.GetFenLeiZongJiMingXiList(userId, itemBuyDate, catTypeId, out priceMax);
        }

        /// <summary>
        /// 取商品次数排行
        /// </summary>
        public DataTable GetItemNumTopList(int userId, DateTime itemBuyDate)
        {
            return dal.GetItemNumTopList(userId, itemBuyDate);
        }

        /// <summary>
        /// 取商品次数排行明细
        /// </summary>
        public DataTable GetItemNumDetailList(int userId, DateTime itemBuyDate, int catTypeId, string itemType, string itemName, string orderBy)
        {
            return dal.GetItemNumDetailList(userId, itemBuyDate, catTypeId, itemType, itemName, orderBy);
        }

        /// <summary>
        /// 取商品单价排行
        /// </summary>
        public DataTable GetItemPriceTopList(int userId, DateTime itemBuyDate)
        {
            return dal.GetItemPriceTopList(userId, itemBuyDate);
        }

        /// <summary>
        /// 取商品日期排行
        /// </summary>
        public DataTable GetItemDateTopList(int userId, DateTime itemBuyDate, string orderBy, out decimal priceMax)
        {
            return dal.GetItemDateTopList(userId, itemBuyDate, orderBy, out priceMax);
        }

        /// <summary>
        /// 取商品区间统计
        /// </summary>
        public DataTable GetQuJianTongJiList(int userId)
        {
            return dal.GetQuJianTongJiList(userId);
        }

        /// <summary>
        /// 取商品推荐分析
        /// </summary>
        public DataTable GetTuiJianFenXiList(int userId)
        {
            return dal.GetTuiJianFenXiList(userId);
        }

        /// <summary>
        /// 取商品比较分析
        /// </summary>
        public DataTable GetBiJiaoFenXiList(int userId, DateTime itemBuyDate, string orderBy, out decimal priceMax)
        {
            return dal.GetBiJiaoFenXiList(userId, itemBuyDate, orderBy, out priceMax);
        }

        /// <summary>
        /// 取商品比较明细
        /// </summary>
        public DataTable GetBiJiaoMingXiList(int userId, DateTime itemBuyDate, int catTypeId, out decimal priceMax, out int countMax)
        {
            return dal.GetBiJiaoMingXiList(userId, itemBuyDate, catTypeId, out priceMax, out countMax);
        }

        /// <summary>
        /// 取商品间隔分析
        /// </summary>
        public DataTable GetJianGeFenXiList(int userId, int pageNumber, int pagePerNumber, out int howManyItems, out int notBuyMax)
        {
            return dal.GetJianGeFenXiList(userId, pageNumber, pagePerNumber, out howManyItems, out notBuyMax);
        }

        /// <summary>
        /// 取商品天数分析
        /// </summary>
        public DataTable GetTianShuFenXiList(int userId, int pageNumber, int pagePerNumber, out int howManyItems, out int notBuyMax)
        {
            return dal.GetTianShuFenXiList(userId, pageNumber, pagePerNumber, out howManyItems, out notBuyMax);
        }

        /// <summary>
        /// 取商品价格分析
        /// </summary>
        public DataTable GetJiaGeFenXiList(int userId, int pageNumber, int pagePerNumber, out int howManyItems, out decimal priceMax)
        {
            return dal.GetJiaGeFenXiList(userId, pageNumber, pagePerNumber, out howManyItems, out priceMax);
        }

        /// <summary>
        /// 取商品价格分析明细
        /// </summary>
        public DataTable GetJiaGeFenXiMingXiList(int userId, string itemType, string itemName, int pageNumber, int pagePerNumber, out int howManyItems, out decimal priceMax)
        {
            return dal.GetJiaGeFenXiMingXiList(userId, itemType, itemName, pageNumber, pagePerNumber, out howManyItems, out priceMax);
        }

        /// <summary>
        /// 取商品收支借还分析
        /// </summary>
        public DataTable GetJieHuanFenXiList(int userId, DateTime itemBuyDate, string orderBy)
        {
            return dal.GetJieHuanFenXiList(userId, itemBuyDate, orderBy);
        }

        /// <summary>
        /// 取最多消费商品统计
        /// </summary>
        public DataTable GetTongJiWithItemNameCount()
        {
            return dal.GetTongJiWithItemNameCount();
        }

        /// <summary>
        /// 取最高消费价格统计
        /// </summary>
        public DataTable GetTongJiWithItemPriceMax()
        {
            return dal.GetTongJiWithItemPriceMax();
        }

        /// <summary>
        /// 取最后消费商品统计
        /// </summary>
        public DataTable GetTongJiWithItemAddLast()
        {
            return dal.GetTongJiWithItemAddLast();
        }

        /// <summary>
        /// 取最多消费用户统计
        /// </summary>
        public DataTable GetTongJiWithUserItemCount()
        {
            return dal.GetTongJiWithUserItemCount();
        }

        /// <summary>
        /// 取最后用户消费商品统计
        /// </summary>
        public DataTable GetTongJiWithUserItemLast()
        {
            return dal.GetTongJiWithUserItemLast();
        }

        /// <summary>
        /// 取最后用户注册会员统计
        /// </summary>
        public DataTable GetTongJiWithUserAddLast()
        {
            return dal.GetTongJiWithUserAddLast();
        }

        /// <summary>
        /// 取商品收支借还列表
        /// </summary>
        public DataTable GetShouZhiJieHuanList(int userId, DateTime itemBuyDate)
        {
            return dal.GetShouZhiJieHuanList(userId, itemBuyDate);
        }

        /// <summary>
        /// 取商品收支借还列表V6
        /// </summary>
        public DataTable GetShouZhiJieHuanListV6(int userId, DateTime itemBuyDate)
        {
            return dal.GetShouZhiJieHuanListV6(userId, itemBuyDate);
        }

        /// <summary>
        /// 根据时间段取活动消费统计
        /// </summary>
        public DataTable GetAdminTongJiHuoDong(DateTime beginDate, DateTime endDate)
        {
            return dal.GetAdminTongJiHuoDong(beginDate, endDate);
        }

        /// <summary>
        /// 根据时间段取全部消费统计
        /// </summary>
        public DataTable GetAdminTongJiQuanBu()
        {
            return dal.GetAdminTongJiQuanBu();
        }

        /// <summary>
        /// 根据时间段取新增消费统计
        /// </summary>
        public DataTable GetAdminTongJiXinZeng(DateTime beginDate, DateTime endDate)
        {
            return dal.GetAdminTongJiXinZeng(beginDate, endDate);
        }

    }
}
