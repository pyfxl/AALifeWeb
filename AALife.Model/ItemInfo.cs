using System;
namespace AALife.Model
{
    /// <summary>
    /// ItemTable:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class ItemInfo
    {
        public ItemInfo()
        {
            ItemName = "";
            CategoryTypeID = 0;
            ItemPrice = 0m;
            ItemBuyDate = DateTime.Now;
            UserID = 0;
            Recommend = 0;
            ModifyDate = DateTime.Now;
            Synchronize = 1;
            ItemAppID = 0;
            RegionID = 0;
            RegionType = "";
            ItemType = "zc";
            ZhuanTiID = 0;
            CardID = 0;
        }
        #region Model
        private int _itemid;
        private string _itemname;
        private int _categorytypeid;
        private decimal _itemprice;
        private DateTime _itembuydate;
        private int _userid;
        private byte _recommend;
        private DateTime _modifydate;
        private byte _synchronize;
        private int? _itemappid;
        private int? _regionid;
        private string _regiontype;
        private string _itemtype;
        private int? _zhuantiid;
        private int? _cardid;
        /// <summary>
        /// 编号
        /// </summary>
        public int ItemID
        {
            set { _itemid = value; }
            get { return _itemid; }
        }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string ItemName
        {
            set { _itemname = value; }
            get { return _itemname; }
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
        /// 商品价格
        /// </summary>
        public decimal ItemPrice
        {
            set { _itemprice = value; }
            get { return _itemprice; }
        }
        /// <summary>
        /// 购买日期
        /// </summary>
        public DateTime ItemBuyDate
        {
            set { _itembuydate = value; }
            get { return _itembuydate; }
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
        /// 推荐否
        /// </summary>
        public byte Recommend
        {
            set { _recommend = value; }
            get { return _recommend; }
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
        /// 商品AppID
        /// </summary>
        public int? ItemAppID
        {
            set { _itemappid = value; }
            get { return _itemappid; }
        }
        /// <summary>
        /// 区间ID
        /// </summary>
        public int? RegionID
        {
            set { _regionid = value; }
            get { return _regionid; }
        }
        /// <summary>
        /// 区间类型
        /// </summary>
        public string RegionType
        {
            set { _regiontype = value; }
            get { return _regiontype; }
        }
        /// <summary>
        /// 商品分类
        /// </summary>
        public string ItemType
        {
            set { _itemtype = value; }
            get { return _itemtype; }
        }
        /// <summary>
        /// 专题ID
        /// </summary>
        public int? ZhuanTiID
        {
            set { _zhuantiid = value; }
            get { return _zhuantiid; }
        }
        /// <summary>
        /// 卡ID
        /// </summary>
        public int? CardID
        {
            set { _cardid = value; }
            get { return _cardid; }
        }
        #endregion Model

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            ItemInfo item = (ItemInfo)obj;
            return item.ItemName.Equals(this.ItemName) && item.ItemBuyDate.ToString("yyyy-MM-dd").Equals(this.ItemBuyDate.ToString("yyyy-MM-dd")) &&
                   item.Recommend == this.Recommend && item.ZhuanTiID == this.ZhuanTiID && item.CardID == this.CardID && item.CategoryTypeID == this.CategoryTypeID &&
                   item.ItemPrice == this.ItemPrice && item.ItemType.Equals(this.ItemType);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("UserID: {0}, ItemID:{1}, AppID:{2}, CatID:{3}, Name:{4}, Price:{5}, Date:{6}, CardID:{7}, Type: {8}, Recommend: {9}, ReginID: {10}, ReginType: {11}, ZTID: {12}", UserID, ItemID, ItemAppID, CategoryTypeID, ItemName, ItemPrice, ItemBuyDate, CardID, ItemType, Recommend, RegionID, RegionType == null ? "" : RegionType, ZhuanTiID);
        }

    }
}

