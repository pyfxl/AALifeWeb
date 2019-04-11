using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AALife.WebMvc.Models.ViewModel
{
    public partial class UserPositionModel : BaseViewModel
    {
        public string Name { get; set; }

        public string Notes { get; set; }
    }
}