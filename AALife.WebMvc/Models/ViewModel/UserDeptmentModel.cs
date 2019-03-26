using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AALife.WebMvc.Models.ViewModel
{
    public partial class UserDeptmentModel : BaseViewModel
    {
        public string Name { get; set; }

        public int? ParentId { get; set; }

        public string ParentName { get; set; }
    }
}