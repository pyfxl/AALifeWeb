using AALife.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AALife.WebMvc.Models.ViewModel
{
    public partial class UserPositionModel : BaseViewModel<Guid>
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public string Notes { get; set; }

        public UserPosition Parent { get; set; }

        public Guid? ParentId { get; set; }

        public Guid? DeptmentId { get; set; }

        public bool hasChildren { get; set; }

    }
}