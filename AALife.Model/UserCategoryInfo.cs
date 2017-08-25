using System;
namespace AALife.Model
{
    /// <summary>
    /// UserCategoryInfo:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class UserCategoryInfo
    {
        public UserCategoryInfo()
        {
            UserCategoryID = 0;
            CategoryTypeName = "";
            UserID = 0;
            CategoryTypeID = 0;
            CategoryTypeLive = 1;
            ModifyDate = DateTime.Now;
            Synchronize = 1;
            CategoryTypePrice = 0m;
        }
        #region Model
        private int _usercategoryid;
        private string _categorytypename;
        private int _userid;
        private int _categorytypeid;
        private byte _categorytypelive;
        private DateTime _modifydate;
        private byte _synchronize;
        private decimal _categorytypeprice;
        /// <summary>
        /// 编号
        /// </summary>
        public int UserCategoryID
        {
            set { _usercategoryid = value; }
            get { return _usercategoryid; }
        }
        /// <summary>
        /// 类别名称
        /// </summary>
        public string CategoryTypeName
        {
            set { _categorytypename = value; }
            get { return _categorytypename; }
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
        /// 类别ID
        /// </summary>
        public int CategoryTypeID
        {
            set { _categorytypeid = value; }
            get { return _categorytypeid; }
        }
        /// <summary>
        /// 可用否
        /// </summary>
        public byte CategoryTypeLive
        {
            set { _categorytypelive = value; }
            get { return _categorytypelive; }
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
        /// 同步
        /// </summary>
        public byte Synchronize
        {
            set { _synchronize = value; }
            get { return _synchronize; }
        }
        /// <summary>
        /// 预算金额
        /// </summary>
        public decimal CategoryTypePrice
        {
            set { _categorytypeprice = value; }
            get { return _categorytypeprice; }
        }
        #endregion Model

    }
}

