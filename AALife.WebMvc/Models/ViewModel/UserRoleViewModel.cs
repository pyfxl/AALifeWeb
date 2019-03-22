using AALife.Data.Domain;
using System;
using System.Web.Script.Serialization;

namespace AALife.WebMvc.Models.ViewModel
{
    public partial class UserRoleViewModel : UserViewModel
    {
        public bool IsCurrentRole { get; set; }
    }
}