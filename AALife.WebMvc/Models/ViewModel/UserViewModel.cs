using AALife.Data.Domain;
using System;
using System.Web.Script.Serialization;

namespace AALife.WebMvc.Models.ViewModel
{
    public partial class UserViewModel : BaseViewModel
    {
        public string UserName { get; set; }

        public string UserNickName { get; set; }

        public string UserNameFull { get; set; }

        public string UserImage { get; set; }

        public string UserImageFull { get; set; }

        public string UserFrom { get; set; }

        public string UserFromName { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public string Remark { get; set; }

    }
}