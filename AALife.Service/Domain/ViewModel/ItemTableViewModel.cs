using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AALife.Service.Domain.ViewModel
{
    public class ItemTableViewModel
    {
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public int CategoryTypeID { get; set; }
        public decimal ItemPrice { get; set; }
        public System.DateTime ItemBuyDate { get; set; }
        public int UserID { get; set; }
        public byte Recommend { get; set; }
        public System.DateTime ModifyDate { get; set; }
        public byte Synchronize { get; set; }
        public Nullable<int> ItemAppID { get; set; }
        public Nullable<int> RegionID { get; set; }
        public string RegionType { get; set; }
        public string ItemType { get; set; }
        public Nullable<int> ZhuanTiID { get; set; }
        public Nullable<int> CardID { get; set; }

        public string ItemTypeName { get; set; }
        public string CategoryTypeName { get; set; }
        public string UserName { get; set; }
        public string RegionTypeName { get; set; }
        public string CardName { get; set; }
        public string ZhuanTiName { get; set; }
        public string Remark { get; set; }
    }
}
