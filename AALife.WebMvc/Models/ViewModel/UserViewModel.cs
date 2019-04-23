using AALife.Data.Domain;
using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace AALife.WebMvc.Models.ViewModel
{
    public partial class UserViewModel : BaseViewModel<Guid>
    {
        public string UserName { get; set; }

        public string UserCode { get; set; }

        public string FirstName { get; set; }

        public string UserPhone { get; set; }

        public string UserEmail { get; set; }
        
        public DateTime CreateDate { get; set; }

        public string Remark { get; set; }

        public bool IsAdmin { get; set; }

        public virtual UserPosition Position { get; set; }

    }
}