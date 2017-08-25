using AALife.DAL;
using AALife.Model;
using System;
using System.Data;

namespace AALife.BLL
{
    public class OAuthTableBLL
    {
        private static readonly OAuthTableDAL dal = new OAuthTableDAL();

        /// <summary>
        /// 取第三方登录列表
        /// </summary>
        public DataTable GetOAuthList(int userId)
        {
            return dal.GetOAuthList(userId);
        }

        /// <summary>
        /// 根据时间段取第三方登录列表
        /// </summary>
        public DataTable GetOAuthListByDate(DateTime beginDate, DateTime endDate)
        {
            return dal.GetOAuthListByDate(beginDate, endDate);
        }

        /// <summary>
        /// 取第三方登录列表，返回DataTable
        /// </summary>
        public DataTable GetOAuthListDataTableByUserId(int userId)
        {
            return dal.GetOAuthListDataTableByUserId(userId);
        }

        /// <summary>
        /// 插入第三方登录
        /// </summary>
        public bool InsertOAuth(OAuthInfo oAuth)
        {
            return dal.InsertOAuth(oAuth);
        }

        /// <summary>
        /// 修改第三方登录
        /// </summary>
        public bool UpdateOAuth(OAuthInfo oauth)
        {
            return dal.UpdateOAuth(oauth);
        }

        /// <summary>
        /// 根据OpenID登录第三方登录
        /// </summary>
        public bool OAuthLoginByOpenId(string openId)
        {
            return dal.OAuthLoginByOpenId(openId);
        }

        /// <summary>
        /// 根据UserID取第三方登录
        /// </summary>
        public OAuthInfo GetOAuthByUserId(int userId)
        {
            return dal.GetOAuthByUserId(userId);
        }

        /// <summary>
        /// 根据OpenID取第三方登录
        /// </summary>
        public OAuthInfo GetOAuthByOpenId(string openId)
        {
            return dal.GetOAuthByOpenId(openId);
        }

        /// <summary>
        /// 第三方登录绑定解除
        /// </summary>
        public bool OAuthBoundCancel(int oauthId)
        {
            return dal.OAuthBoundCancel(oauthId);
        }

        /// <summary>
        /// 第三方登录绑定新用户
        /// </summary>
        public bool OAuthBoundNewUser(int userId)
        {
            return dal.OAuthBoundNewUser(userId);
        }

        /// <summary>
        /// 第三方登录绑定旧用户
        /// </summary>
        public bool OAuthBoundOldUser(int userId, int toUserId)
        {
            return dal.OAuthBoundOldUser(userId, toUserId);
        }

    }
}
