using System;
namespace AALife.Model
{
    /// <summary>
    /// UserEntity:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class UserInfo
    {
        public UserInfo()
        {
            UserName = "";
            UserPassword = "";
            UserNickName = "";
            UserImage = "none.gif";
            UserPhone = "";
            UserEmail = "";
            UserTheme = "main";
            UserLevel = 1;
            UserFrom = "web";
            ModifyDate = DateTime.Now;
            CreateDate = DateTime.Now;
            UserCity = "";
            UserMoney = 0m;
            UserWorkDay = "5";
            UserFunction = "";
            CategoryRate = 90;
            Synchronize = 0;
            MoneyStart = 0m;
            IsUpdate = 0;
        }
        #region Model
        private int _userid;
        private string _username;
        private string _userpassword;
        private string _usernickname;
        private string _userimage;
        private string _userphone;
        private string _useremail;
        private string _usertheme;
        private byte _userlevel;
        private string _userfrom;
        private DateTime _modifydate;
        private DateTime _createdate;
        private string _usercity;
        private decimal _usermoney;
        private string _userworkday;
        private string _userfunction;
        private int _categoryrate;
        private byte _synchronize;
        private decimal _moneystart;
        private byte _isupdate;
        /// <summary>
        /// 编号
        /// </summary>
        public int UserID
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 密码
        /// </summary>
        public string UserPassword
        {
            set { _userpassword = value; }
            get { return _userpassword; }
        }
        /// <summary>
        /// 昵称
        /// </summary>
        public string UserNickName
        {
            set { _usernickname = value; }
            get { return _usernickname; }
        }
        /// <summary>
        /// 头像
        /// </summary>
        public string UserImage
        {
            set { _userimage = value; }
            get { return _userimage; }
        }
        /// <summary>
        /// 电话
        /// </summary>
        public string UserPhone
        {
            set { _userphone = value; }
            get { return _userphone; }
        }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string UserEmail
        {
            set { _useremail = value; }
            get { return _useremail; }
        }
        /// <summary>
        /// 主题
        /// </summary>
        public string UserTheme
        {
            set { _usertheme = value; }
            get { return _usertheme; }
        }
        /// <summary>
        /// 级别
        /// </summary>
        public byte UserLevel
        {
            set { _userlevel = value; }
            get { return _userlevel; }
        }
        /// <summary>
        /// 来自
        /// </summary>
        public string UserFrom
        {
            set { _userfrom = value; }
            get { return _userfrom; }
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
        /// 创建日期
        /// </summary>
        public DateTime CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// 城市
        /// </summary>
        public string UserCity
        {
            set { _usercity = value; }
            get { return _usercity; }
        }
        /// <summary>
        /// 钱包
        /// </summary>
        public decimal UserMoney
        {
            set { _usermoney = value; }
            get { return _usermoney; }
        }
        /// <summary>
        /// 工作日
        /// </summary>
        public string UserWorkDay
        {
            set { _userworkday = value; }
            get { return _userworkday; }
        }
        /// <summary>
        /// 常用功能
        /// </summary>
        public string UserFunction
        {
            set { _userfunction = value; }
            get { return _userfunction; }
        }
        /// <summary>
        /// 预算率
        /// </summary>
        public int CategoryRate
        {
            set { _categoryrate = value; }
            get { return _categoryrate; }
        }
        /// <summary>
        /// 同步
        /// </summary>
        public byte Synchronize
        {
            set { _synchronize = value; }
            get { return _synchronize; }
        }
        /// <summary>
        /// 钱包初始
        /// </summary>
        public decimal MoneyStart
        {
            set { _moneystart = value; }
            get { return _moneystart; }
        }
        /// <summary>
        /// 是否升级
        /// </summary>
        public byte IsUpdate
        {
            set { _isupdate = value; }
            get { return _isupdate; }
        }
        #endregion Model

        public override string ToString()
        {
            return string.Format("UserID:{0}, Pass:***, Name:{1}, Nick:{2}, Image:{3}, From:{4}, IsUpdate:{5}", UserID, UserName, UserNickName, UserImage, UserFrom, IsUpdate);
        }

    }
}

