using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AALife.WebMvc.Models.ViewModel
{
    public class ItemViewModel
    {
        public int ItemID { get; set; }
        
        public string ItemName { get; set; }
        
        public int CategoryTypeID { get; set; }
        
        public decimal ItemPrice { get; set; }
        
        public DateTime ItemBuyDate { get; set; }
        
        public byte? Recommend { get; set; }
        
        public DateTime ModifyDate { get; set; }
        
        public byte Synchronize { get; set; }
        
        public int? ItemAppID { get; set; }
        
        public int? RegionID { get; set; }
        
        public string RegionType { get; set; }
        
        public string ItemType { get; set; }
        
        public int? ZhuanTiID { get; set; }
        
        public int CardID { get; set; }
        
        public string Remark { get; set; }
        
        public int UserID { get; set; }

        public DateTime? ItemBuyDateStart { get; set; }
        public DateTime? ItemBuyDateEnd { get; set; }
        public string UserName { get; set; }
        public int UserWorkDay { get; set; }
        public string ItemTypeName { get; set; }
        public string CategoryTypeName { get; set; }
        public string ZhuanTiName { get; set; }
        public string CardName { get; set; }
        public string RegionName { get; set; }

    }
}