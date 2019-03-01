using AALife.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AALife.WebMvc.Models.ViewModel
{
    public partial class PermissionViewModel : BaseViewModel
    {
        public int ParentId { get; set; }

        public string AreaName { get; set; }

        public string ControllerName { get; set; }

        public string ActionName { get; set; }

        public string OrderNo { get; set; }

        public byte? Rank { get; set; }

        public string Name { get; set; }

    }
}
