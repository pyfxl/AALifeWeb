using AALife.DAL;
using AALife.Model;
using System;
using System.Data;

namespace AALife.BLL
{
    public class ZhuanTiTableBLL
    {
        private static readonly ZhuanTiTableDAL dal = new ZhuanTiTableDAL();

        /// <summary>
        /// 取专题列表
        /// </summary>
        public DataTable GetZhuanTiList(int userId)
        {
            return dal.GetZhuanTiList(userId);
        }

        /// <summary>
        /// 根据时间段取专题列表
        /// </summary>
        public DataTable GetZhuanTiListByDate(DateTime beginDate, DateTime endDate)
        {
            return dal.GetZhuanTiListByDate(beginDate, endDate);
        }

        /// <summary>
        /// 取同步专题列表
        /// </summary>
        public DataTable GetZhuanTiListWithSync(int userId)
        {
            return dal.GetZhuanTiListWithSync(userId);
        }

        /// <summary>
        /// 插入专题
        /// </summary>
        public bool InsertZhuanTi(ZhuanTiInfo zhuanTi)
        {
            return dal.InsertZhuanTi(zhuanTi);
        }

        /// <summary>
        /// 修改专题
        /// </summary>
        public bool UpdateZhuanTi(ZhuanTiInfo zhuanTi)
        {
            return dal.UpdateZhuanTi(zhuanTi);
        }

        /// <summary>
        /// 取最大专题ID
        /// </summary>
        public int GetMaxZhuanTiId(int userId)
        {
            return dal.GetMaxZhuanTiId(userId);
        }

        /// <summary>
        /// 根据专题ID取专题，返回实体
        /// </summary>
        public ZhuanTiInfo GetZhuanTiByZhuanTiId(int userId, int ztId)
        {
            return dal.GetZhuanTiByZhuanTiId(userId, ztId);
        }

        /// <summary>
        /// 根据专题ID取专题，返回DataTable
        /// </summary>
        public DataTable GetZhuanTiDataTableByZhuanTiId(int userId, int ztId)
        {
            return dal.GetZhuanTiDataTableByZhuanTiId(userId, ztId);
        }

        /// <summary>
        /// 根据专题名称取专题
        /// </summary>
        public ZhuanTiInfo GetZhuanTiByZhuanTiName(int userId, string zhuanTiName)
        {
            return dal.GetZhuanTiByZhuanTiName(userId, zhuanTiName);
        }

        /// <summary>
        /// 修改专题同步返回
        /// </summary>
        public bool UpdateZhuanTiListWebBack(int userId)
        {
            return dal.UpdateZhuanTiListWebBack(userId);
        }

    }
}
