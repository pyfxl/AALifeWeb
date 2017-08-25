using AALife.DAL;
using System.Data;

namespace AALife.BLL
{
    public class DeleteTableBLL
    {
        private static readonly DeleteTableDAL dal = new DeleteTableDAL();

        /// <summary>
        /// 取删除列表
        /// </summary>
        public DataTable GetDeleteList()
        {
            return dal.GetDeleteList();
        }

        /// <summary>
        /// 根据用户ID取删除列表
        /// </summary>
        public DataTable GetDeleteListByUserId(int userId)
        {
            return dal.GetDeleteListByUserId(userId);
        }

        /// <summary>
        /// 修改删除同步返回
        /// </summary>
        public bool UpdateDeleteListWebBack(int userId)
        {
            return dal.UpdateDeleteListWebBack(userId);
        }

    }
}
