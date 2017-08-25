using AALife.DAL;
using AALife.Model;
using System;
using System.Data;

namespace AALife.BLL
{
    public class UserCategoryTableBLL
    {
        private static readonly UserCategoryTableDAL dal = new UserCategoryTableDAL();

        /// <summary>
        /// 取用户类别列表
        /// </summary>
        public DataTable GetUserCategoryList(int userId, int categoryRate)
        {
            return dal.GetUserCategoryList(userId, categoryRate);
        }

        /// <summary>
        /// 根据时间段取用户类别列表
        /// </summary>
        public DataTable GetUserCategoryListByDate(DateTime beginDate, DateTime endDate)
        {
            return dal.GetUserCategoryListByDate(beginDate, endDate);
        }

        /// <summary>
        /// 取用户类别列表
        /// </summary>
        public DataTable GetUserCategoryList(int userId)
        {
            return dal.GetUserCategoryList(userId, 0);
        }

        /// <summary>
        /// 取同步用户类别列表
        /// </summary>
        public DataTable GetUserCategoryListWithSync(int userId)
        {
            return dal.GetUserCategoryListWithSync(userId);
        }

        /// <summary>
        /// 取最大类别ID
        /// </summary>
        public int GetMaxCategoryTypeId(int userId)
        {
            return dal.GetMaxCategoryTypeId(userId);
        }

        /// <summary>
        /// 插入用户类别
        /// </summary>
        public bool InsertUserCategory(UserCategoryInfo category)
        {
            return dal.InsertUserCategory(category);
        }

        /// <summary>
        /// 修改用户类别
        /// </summary>
        public bool UpdateUserCategory(UserCategoryInfo category)
        {
            return dal.UpdateUserCategory(category);
        }

        /// <summary>
        /// 根据类别ID删除用户类别
        /// </summary>
        public bool DeleteUserCategory(int userId, int catTypeId)
        {
            return dal.DeleteUserCategory(userId, catTypeId);
        }

        /// <summary>
        /// 删除用户类别
        /// </summary>
        public bool DeleteUserCategory(UserCategoryInfo category)
        {
            return dal.DeleteUserCategory(category);
        }

        /// <summary>
        /// 根据类别名称取用户类别
        /// </summary>
        public UserCategoryInfo GetUserCategoryByName(int userId, string catTypeName)
        {
            return dal.GetUserCategoryByName(userId, catTypeName);
        }

        /// <summary>
        /// 修改类别同步返回
        /// </summary>
        public bool UpdateCategoryListWebBack(int userId)
        {
            return dal.UpdateCategoryListWebBack(userId);
        }

        /// <summary>
        /// 同步用户类别是否存在
        /// </summary>
        public bool UserCategoryExistsWithSync(int userId, int catTypeId)
        {
            return dal.UserCategoryExistsWithSync(userId, catTypeId);
        }

    }
}
