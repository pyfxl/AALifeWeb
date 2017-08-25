using System;
namespace AALife.Model
{
    /// <summary>
    /// ZhuanTiInfo:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class ZhuanTiInfo
    {
        public ZhuanTiInfo()
        {
            ZhuanTiID = 0;
            ZhuanTiName = "";
            ZhuanTiImage = "none.gif";
            UserID = 0;
            ZhuanTiLive = 1;
            Synchronize = 1;
            ModifyDate = DateTime.Now;
            ZTID = 0;
        }
        #region Model
        private int _zhuantiid;
        private string _zhuantiname;
        private string _zhuantiimage;
        private int _userid;
        private byte _zhuantilive;
        private byte _synchronize;
        private DateTime _modifydate;
        private int? _ztid;
        /// <summary>
        /// 编号
        /// </summary>
        public int ZhuanTiID
        {
            set { _zhuantiid = value; }
            get { return _zhuantiid; }
        }
        /// <summary>
        /// 专题名称
        /// </summary>
        public string ZhuanTiName
        {
            set { _zhuantiname = value; }
            get { return _zhuantiname; }
        }
        /// <summary>
        /// 专题图片
        /// </summary>
        public string ZhuanTiImage
        {
            set { _zhuantiimage = value; }
            get { return _zhuantiimage; }
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
        /// 可用否
        /// </summary>
        public byte ZhuanTiLive
        {
            set { _zhuantilive = value; }
            get { return _zhuantilive; }
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
        public int? ZTID
        {
            set { _ztid = value; }
            get { return _ztid; }
        }
        #endregion Model

    }
}

