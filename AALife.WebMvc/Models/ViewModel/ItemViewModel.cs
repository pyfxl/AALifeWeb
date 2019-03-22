using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AALife.WebMvc.Models.ViewModel
{
    public partial class ItemViewModel : BaseViewModel
    {
        public string ItemName { get; set; }
        
        public int? CategoryTypeId { get; set; }
        
        public decimal ItemPrice { get; set; }
        
        public DateTime ItemBuyDate { get; set; }
        
        public byte? Recommend { get; set; }
        
        public DateTime ModifyDate { get; set; }
        
        public byte Synchronize { get; set; }
        
        public int? AppId { get; set; }
        
        public int? RegionId { get; set; }
        
        public string RegionType { get; set; }
        
        public string ItemType { get; set; }
        
        public int? ZhuanTiId { get; set; }
        
        public int? CardId { get; set; }
        
        public string Remark { get; set; }
        
        public int UserId { get; set; }

        public DateTime? ItemBuyDateStart { get; set; }
        public DateTime? ItemBuyDateEnd { get; set; }

        public string UserName { get; set; }
        public string ItemTypeName { get; set; }
        public string CategoryTypeName { get; set; }
        public string ZhuanTiName { get; set; }
        public string CardName { get; set; }
        public string RegionName { get; set; }

    }
}