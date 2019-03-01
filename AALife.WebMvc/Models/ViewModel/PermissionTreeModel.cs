using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AALife.WebMvc.Models.ViewModel
{
    public partial class PermissionTreeModel
    {
        public PermissionTreeModel()
        {
            items = new List<PermissionTreeModel>();
        }

        public int id { get; set; }

        public string text { get; set; }

        public List<PermissionTreeModel> items { get; set; }

        public bool hasChildren { get; set; }
        
        public string spriteCssClass { get; set; }
    }
}