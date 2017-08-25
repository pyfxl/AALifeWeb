using AALife.DAL;
using AALife.Model;
using System;
using System.Data;

namespace AALife.BLL
{
    public class UserQueryTableBLL
    {
        private static readonly UserQueryTableDAL dal = new UserQueryTableDAL();

        /// <summary>
        /// 取专题查询
        /// </summary>
        public DataTable GetUserQueryList(int userId)
        {
            return dal.GetUserQueryList(userId);
        }
        
        /// <summary>
        /// 插入专题
        /// </summary>
        public bool InsertUserQuery(UserQueryInfo query)
        {
            return dal.InsertUserQuery(query);
        }

        /// <summary>
        /// 修改专题
        /// </summary>
        public bool UpdateUserQuery(UserQueryInfo query)
        {
            return dal.UpdateUserQuery(query);
        }

        /// <summary>
        /// 根据URL取查询，返回实体
        /// </summary>
        public UserQueryInfo GetUserQueryByURL(int userId, string url)
        {
            return dal.GetUserQueryByURL(userId, url);
        }
        
        /// <summary>
        /// 根据Value取查询，返回实体
        /// </summary>
        public UserQueryInfo GetUserQueryByValue(int userId, string value)
        {
            return dal.GetUserQueryByValue(userId, value);
        }

        /// <summary>
        /// 根据名称取查询，返回实体
        /// </summary>
        public UserQueryInfo GetUserQueryByName(int userId, string name)
        {
            return dal.GetUserQueryByName(userId, name);
        }

        /// <summary>
        /// 根据ID取查询，返回实体
        /// </summary>
        public UserQueryInfo GetUserQueryById(int userId, int queryId)
        {
            return dal.GetUserQueryById(userId, queryId);
        }

    }
}
