using System;
namespace AALife.Model
{
    /// <summary>
    /// UserQueryInfo:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class UserQueryInfo
    {
        public UserQueryInfo()
        {
            UserQueryID = 0;
            UserQueryName = "";
            UserQueryURL = "";
            UserQueryValue = "";
            UserQueryLive = 1;
            ModifyDate = DateTime.Now;
            UserID = 0;
        }
        #region Model
        private int _userqueryid;
        private string _userqueryname;
        private string _userqueryurl;
        private string _userqueryvalue;
        private byte _zhuanzhanglive;
        private DateTime _modifydate;
        private int _userid;
        /// <summary>
        /// 主键ID
        /// </summary>
        public int UserQueryID
        {
            set { _userqueryid = value; }
            get { return _userqueryid; }
        }
        /// <summary>
        /// 查询名称
        /// </summary>
        public string UserQueryName
        {
            set { _userqueryname = value; }
            get { return _userqueryname; }
        }
        /// <summary>
        /// 查询URL
        /// </summary>
        public string UserQueryURL
        {
            set { _userqueryurl = value; }
            get { return _userqueryurl; }
        }
        /// <summary>
        /// 查询值
        /// </summary>
        public string UserQueryValue
        {
            set { _userqueryvalue = value; }
            get { return _userqueryvalue; }
        }
        /// <summary>
        /// 显示否
        /// </summary>
        public byte UserQueryLive
        {
            set { _zhuanzhanglive = value; }
            get { return _zhuanzhanglive; }
        }
        /// <summary>
        /// 修改日期
        /// </summary>
        public DateTime ModifyDate
        {
            set { _modifydate = value; }
            get { return _modifydate; }
        }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID
        {
            set { _userid = value; }
            get { return _userid; }
        }
        #endregion Model

    }
}

