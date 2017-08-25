using AALife.DAL;
using AALife.Model;
using System;
using System.Data;

namespace AALife.BLL
{
    public class ZhuanZhangTableBLL
    {
        private static readonly ZhuanZhangTableDAL dal = new ZhuanZhangTableDAL();
        
        /// <summary>
        /// 取转账列表
        /// </summary>
        public DataTable GetZhuanZhangList(int userId)
        {
            return dal.GetZhuanZhangList(userId);
        }

        /// <summary>
        /// 根据时间段取转账列表
        /// </summary>
        public DataTable GetZhuanZhangListByDate(DateTime beginDate, DateTime endDate)
        {
            return dal.GetZhuanZhangListByDate(beginDate, endDate);
        }

        /// <summary>
        /// 插入转账
        /// </summary>
        public bool InsertZhuanZhang(ZhuanZhangInfo zhuan)
        {
            return dal.InsertZhuanZhang(zhuan);
        }

        /// <summary>
        /// 修改转账
        /// </summary>
        public bool UpdateZhuanZhang(ZhuanZhangInfo zhang)
        {
            return dal.UpdateZhuanZhang(zhang);
        }

        /// <summary>
        /// 取最大转账ID
        /// </summary>
        public int GetMaxZhuanZhangId(int userId)
        {
            return dal.GetMaxZhuanZhangId(userId);
        }

        /// <summary>
        /// 取同步转账列表
        /// </summary>
        public DataTable GetZhuanZhangListWithSync(int userId)
        {
            return dal.GetZhuanZhangListWithSync(userId);
        }

        /// <summary>
        /// 根据转账ID取转账，返回实体
        /// </summary>
        public ZhuanZhangInfo GetZhuanZhangByZZID(int userId, int zzId)
        {
            return dal.GetZhuanZhangByZZID(userId, zzId);
        }

        /// <summary>
        /// 修改转账同步返回
        /// </summary>
        public bool UpdateZhuanZhangListWebBack(int userId)
        {
            return dal.UpdateZhuanZhangListWebBack(userId);
        }

    }
}
