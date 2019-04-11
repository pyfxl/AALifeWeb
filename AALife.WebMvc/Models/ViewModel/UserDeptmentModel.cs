using AALife.Core.Domain.Configuration;
using AALife.Data.Domain;
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

        public UserDeptment Parent { get; set; }

        public string Category { get; set; }

        public string CategoryName { get; set; }

        public string Notes { get; set; }
    }
}