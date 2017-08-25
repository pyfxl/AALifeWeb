using System;
namespace AALife.Model
{
    /// <summary>
    /// OAuthInfo:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class OAuthInfo
    {
        public OAuthInfo()
        {
            OAuthID = 0;
            OpenID = "";
            AccessToken = "";
            UserID = 0;
            OldUserID = 0;
            OAuthBound = 0;
            OAuthFrom = "";
            ModifyDate = DateTime.Now;
        }
        #region Model
        private int _oauthid;
        private string _openid;
        private string _accesstoken;
        private int _userid;
        private int _olduserid;
        private int _oauthbound;
        private string _oauthfrom;
        private DateTime _modifydate;
        /// <summary>
        /// 编号
        /// </summary>
        public int OAuthID
        {
            set { _oauthid = value; }
            get { return _oauthid; }
        }
        /// <summary>
        /// OpenID
        /// </summary>
        public string OpenID
        {
            set { _openid = value; }
            get { return _openid; }
        }
        /// <summary>
        /// AccessToken
        /// </summary>
        public string AccessToken
        {
            set { _accesstoken = value; }
            get { return _accesstoken; }
        }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 旧用户ID
        /// </summary>
        public int OldUserID
        {
            set { _olduserid = value; }
            get { return _olduserid; }
        }
        /// <summary>
        /// 绑定否
        /// </summary>
        public int OAuthBound
        {
            set { _oauthbound = value; }
            get { return _oauthbound; }
        }
        /// <summary>
        /// 来自
        /// </summary>
        public string OAuthFrom
        {
            set { _oauthfrom = value; }
            get { return _oauthfrom; }
        }
        /// <summary>
        /// 修改日期
        /// </summary>
        public DateTime ModifyDate
        {
            set { _modifydate = value; }
            get { return _modifydate; }
        }
        #endregion Model

        public override string ToString()
        {
            return string.Format("UserID:{0}, OldUserID:{1}, OauthID:{2}, OpenID:{3}, Token:{4}, From:{5}", UserID, OldUserID, OAuthID, OpenID, AccessToken, OAuthFrom);
        }

    }
}

