using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AALife.Service.Domain.ViewModel
{
    public class ViewTableViewModel
    {
        public int ViewID { get; set; }
        public int PageID { get; set; }
        public int UserID { get; set; }
        public System.DateTime DateStart { get; set; }
        public System.DateTime DateEnd { get; set; }
        public string Portal { get; set; }
        public string Version { get; set; }
        public string Browser { get; set; }
        public Nullable<int> Width { get; set; }
        public Nullable<int> Height { get; set; }
        public string IP { get; set; }
        public byte Synchronize { get; set; }
        public string Remark { get; set; }
        public System.DateTime CreateDate { get; set; }
        public string Network { get; set; }

        public string PageName { get; set; }
        public string PageTitle { get; set; }
        public int ViewSeconds { get; set; }
    }
}
