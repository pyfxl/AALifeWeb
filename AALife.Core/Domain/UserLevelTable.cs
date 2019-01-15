using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AALife.Core.Domain
{
    [Table("tab_UserLevelTable")]
    public partial class UserLevelTable : BaseEntity
    {
        [Key]
        [Display(Name = "等级")]
        public byte UserLevel { get; set; }

        [Required]
        [MaxLength(10)]
        [Display(Name = "等级名称")]
        public string UserLevelName { get; set; }

    }
}
