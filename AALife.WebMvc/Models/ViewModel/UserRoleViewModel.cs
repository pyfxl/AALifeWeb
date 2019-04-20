using AALife.Data.Domain;
using System;
using System.Web.Script.Serialization;

namespace AALife.WebMvc.Models.ViewModel
{
    public partial class UserRoleViewModel : UserViewModel
    {
        public bool IsCurrentRole { get; set; }

        public virtual UserPosition Position { get; set; }

        public virtual UserPosition ParentPosition { get; set; }

        public Guid? ParentPositionId { get; set; }
    }
}