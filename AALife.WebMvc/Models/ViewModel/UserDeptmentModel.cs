using AALife.Core.Domain.Configuration;
using AALife.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AALife.WebMvc.Models.ViewModel
{
    public partial class UserDeptmentModel : BaseViewModel<Guid>
    {
        public string Name { get; set; }

        public Guid? ParentId { get; set; }

        public UserDeptment Parent { get; set; }

        public string Notes { get; set; }

        public string Code { get; set; }

        public string OrgType { get; set; }

        public int OrgLevel { get; set; }

        public bool hasChildren { get; set; }

    }
}