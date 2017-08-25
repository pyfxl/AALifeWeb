using AALife.DAL;
using AALife.Model;
using System;
using System.Data;

namespace AALife.BLL
{
    public class UserTableBLL
    {
        private static readonly UserTableDAL dal = new UserTableDAL();

        /// <summary>
        /// 根据用户ID取用户，返回UserInfo
        /// </summary>
        public UserInfo GetUserByUserId(int userId)
        {
            return dal.GetUserByUserId(userId);
        }

        /// <summary>
        /// 根据用户ID取用户，返回DataTable
        /// </summary>
        public DataTable GetUserDataTableByUserId(int userId)
        {
            return dal.GetUserDataTableByUserId(userId);
        }

        /// <summary>
        /// 根据用户名取用户
        /// </summary>
        public UserInfo GetUserByUserName(string userName)
        {
            return dal.GetUserByUserName(userName);
        }

        /// <summary>
        /// 根据用户名和密码取用户
        /// </summary>
        public UserInfo GetUserByUserPassword(string userName, string userPassword)
        {
            return dal.GetUserByUserPassword(userName, userPassword);
        }

        /// <summary>
        /// 根据时间段取用户列表
        /// </summary>
        public DataTable GetUserListByDate(DateTime beginDate, DateTime endDate)
        {
            return dal.GetUserListByDate(beginDate, endDate);
        }

        /// <summary>
        /// 取用户列表
        /// </summary>
        public DataTable GetUserList()
        {
            return dal.GetUserList();
        }

        /// <summary>
        /// 根据关键字取用户列表
        /// </summary>
        public DataTable GetUserListByKeywords(string keywords)
        {
            return dal.GetUserListByKeywords(keywords);
        }

        /// <summary>
        /// 取同步用户列表
        /// </summary>
        public DataTable GetUserListWithSync(int userId)
        {
            return dal.GetUserListWithSync(userId);
        }

        /// <summary>
        /// 插入用户
        /// </summary>
        public bool InsertUser(UserInfo user)
        {
            return dal.InsertUser(user);
        }

        /// <summary>
        /// 修改用户
        /// </summary>
        public bool UpdateUser(UserInfo user)
        {
            return dal.UpdateUser(user);
        }

        /// <summary>
        /// 用户是否存在
        /// </summary>
        public bool UserExists(string userName)
        {
            return dal.UserExists(userName);
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        public bool UserLogin(string userName, string userPassword)
        {
            return dal.UserLogin(userName, userPassword);
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        public bool DeleteUser(int userId)
        {
            return dal.DeleteUser(userId);
        }

        /// <summary>
        /// 删除用户数据
        /// </summary>
        public bool DeleteUserData(int userId)
        {
            return dal.DeleteUserData(userId);
        }

        /// <summary>
        /// 取用户工作日列表
        /// </summary>
        public DataTable GetUserWorkDay()
        {
            return dal.GetUserWorkDay();
        }

        /// <summary>
        /// 修改用户同步返回
        /// </summary>
        public bool UpdateUserListWebBack(int userId)
        {
            return dal.UpdateUserListWebBack(userId);
        }

        /// <summary>
        /// 修改同步状态
        /// </summary>
        public bool UpdateSyncByUserId(int userId)
        {
            return dal.UpdateSyncByUserId(userId);
        }

    }
}
