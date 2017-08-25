using System;
namespace AALife.Model
{
    /// <summary>
    /// ZhuanZhangInfo:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class ZhuanZhangInfo
    {
        public ZhuanZhangInfo()
        {
            ZhuanZhangID = 0;
            ZhuanZhangFrom = 0;
            ZhuanZhangTo = 0;
            ZhuanZhangDate = DateTime.Now;
            ZhuanZhangMoney = 0m;
            ZhuanZhangLive = 1;
            Synchronize = 1;
            ModifyDate = DateTime.Now;
            UserID = 0;
            ZhuanZhangNote = "";
            ZZID = 0;
        }
        #region Model
        private int _zhuanzhangid;
        private int _zhuanzhangfrom;
        private int _zhuanzhangto;
        private DateTime _zhuanzhangdate;
        private decimal _zhuanzhangmoney;
        private byte _zhuanzhanglive;
        private byte _synchronize;
        private DateTime _modifydate;
        private int _userid;
        private string _zhuanzhangnote;
        private int _zzid;
        /// <summary>
        /// 编号
        /// </summary>
        public int ZhuanZhangID
        {
            set { _zhuanzhangid = value; }
            get { return _zhuanzhangid; }
        }
        /// <summary>
        /// 转账来自
        /// </summary>
        public int ZhuanZhangFrom
        {
            set { _zhuanzhangfrom = value; }
            get { return _zhuanzhangfrom; }
        }
        /// <summary>
        /// 转账给
        /// </summary>
        public int ZhuanZhangTo
        {
            set { _zhuanzhangto = value; }
            get { return _zhuanzhangto; }
        }
        /// <summary>
        /// 转账日期
        /// </summary>
        public DateTime ZhuanZhangDate
        {
            set { _zhuanzhangdate = value; }
            get { return _zhuanzhangdate; }
        }
        /// <summary>
        /// 转账金额
        /// </summary>
        public decimal ZhuanZhangMoney
        {
            set { _zhuanzhangmoney = value; }
            get { return _zhuanzhangmoney; }
        }
        /// <summary>
        /// 可用否
        /// </summary>
        public byte ZhuanZhangLive
        {
            set { _zhuanzhanglive = value; }
            get { return _zhuanzhanglive; }
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
        /// 用户ID
        /// </summary>
        public int UserID
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 转账备注
        /// </summary>
        public string ZhuanZhangNote
        {
            set { _zhuanzhangnote = value; }
            get { return _zhuanzhangnote; }
        }
        /// <summary>
        /// 同步ID
        /// </summary>
        public int ZZID
        {
            set { _zzid = value; }
            get { return _zzid; }
        }
        #endregion Model

    }
}

