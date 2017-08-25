using System;
namespace AALife.Model
{
    /// <summary>
    /// CardInfo:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class CardInfo
    {
        public CardInfo()
        {
            CardID = 0;
            CardName = "";
            CardNumber = "";
            CardImage = "";
            CardMoney = 0m;
            CardLive = 1;
            Synchronize = 1;
            ModifyDate = DateTime.Now;
            CDID = 0;
            UserID = 0;
            MoneyStart = 0m;
            CardShow = 0;
        }
        #region Model
        private int _cardid;
        private string _cardname;
        private string _cardnumber;
        private string _cardimage;
        private decimal _cardmoney;
        private byte _cardlive;
        private byte _synchronize;
        private DateTime _modifydate;
        private int? _cdid;
        private int _userid;
        private decimal _moneystart;
        private byte _cardShow;
        /// <summary>
        /// 编号
        /// </summary>
        public int CardID
        {
            set { _cardid = value; }
            get { return _cardid; }
        }
        /// <summary>
        /// 卡名称
        /// </summary>
        public string CardName
        {
            set { _cardname = value; }
            get { return _cardname; }
        }
        /// <summary>
        /// 卡号
        /// </summary>
        public string CardNumber
        {
            set { _cardnumber = value; }
            get { return _cardnumber; }
        }
        /// <summary>
        /// 卡图片
        /// </summary>
        public string CardImage
        {
            set { _cardimage = value; }
            get { return _cardimage; }
        }
        /// <summary>
        /// 卡余额
        /// </summary>
        public decimal CardMoney
        {
            set { _cardmoney = value; }
            get { return _cardmoney; }
        }
        /// <summary>
        /// 卡可用
        /// </summary>
        public byte CardLive
        {
            set { _cardlive = value; }
            get { return _cardlive; }
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
        /// 修改日期
        /// </summary>
        public DateTime ModifyDate
        {
            set { _modifydate = value; }
            get { return _modifydate; }
        }
        /// <summary>
        /// 同步ID
        /// </summary>
        public int? CDID
        {
            set { _cdid = value; }
            get { return _cdid; }
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
        /// 钱包初始
        /// </summary>
        public decimal MoneyStart
        {
            set { _moneystart = value; }
            get { return _moneystart; }
        }
        /// <summary>
        /// 首页显示
        /// </summary>
        public byte CardShow
        {
            set { _cardShow = value; }
            get { return _cardShow; }
        }
        #endregion Model

    }
}

