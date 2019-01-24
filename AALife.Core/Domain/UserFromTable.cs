using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AALife.Core.Domain
{
    [Table("tab_UserFromTable")]
    public partial class UserFromTable : BaseEntity
    {
        [MaxLength(10)]
        [Display(Name = "来自")]
        public string UserFrom { get; set; }

        [Required]
        [MaxLength(10)]
        [Display(Name = "来自名称")]
        public string UserFromName { get; set; }

    }
}
