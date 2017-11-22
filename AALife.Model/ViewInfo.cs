using System;
namespace AALife.Model
{
    /// <summary>
    /// ViewInfo:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class ViewInfo
    {
        public ViewInfo()
        {
            ViewID = 0;
            PageID = 0;
            UserID = 0;
            DateStart = DateTime.Now;
            DateEnd = DateTime.Now;
            Portal = "";
            Version = "";
            Browser = "";
            Width = 0;
            Height = 0;
            IP = "";
            Synchronize = 1;
            Remark = "";
            Network = "";
        }
        #region Model
        private int _viewid;
        private int _pageid;
        private int _userid;
        private DateTime _datestart;
        private DateTime _dateend;
        private string _portal;
        private string _version;
        private string _browser;
        private int? _width;
        private int? _height;
        private string _ip;
        private byte _synchronize;
        private string _remark;
        private string _network;
        /// <summary>
        /// 编号
        /// </summary>
        public int ViewID
        {
            set { _viewid = value; }
            get { return _viewid; }
        }
        /// <summary>
        /// 页面ID
        /// </summary>
        public int PageID
        {
            set { _pageid = value; }
            get { return _pageid; }
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
        /// 开始访问日期
        /// </summary>
        public DateTime DateStart
        {
            set { _datestart = value; }
            get { return _datestart; }
        }
        /// <summary>
        /// 结束访问日期
        /// </summary>
        public DateTime DateEnd
        {
            set { _dateend = value; }
            get { return _dateend; }
        }
        /// <summary>
        /// 平台（Window，Android）
        /// </summary>
        public string Portal
        {
            set { _portal = value; }
            get { return _portal; }
        }
        /// <summary>
        /// 版本号
        /// </summary>
        public string Version
        {
            set { _version = value; }
            get { return _version; }
        }
        /// <summary>
        /// 浏览器
        /// </summary>
        public string Browser
        {
            set { _browser = value; }
            get { return _browser; }
        }
        /// <summary>
        /// 屏幕宽度
        /// </summary>
        public int? Width
        {
            set { _width = value; }
            get { return _width; }
        }
        /// <summary>
        /// 屏幕高度
        /// </summary>
        public int? Height
        {
            set { _height = value; }
            get { return _height; }
        }
        /// <summary>
        /// IP地址
        /// </summary>
        public string IP
        {
            set { _ip = value; }
            get { return _ip; }
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
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 网络
        /// </summary>
        public string Network
        {
            set { _network = value; }
            get { return _network; }
        }
        #endregion Model

    }
}

