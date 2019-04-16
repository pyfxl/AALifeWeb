using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AALife.Data.Domain
{
    [Table("tab_ZhuanTiTable")]
    public partial class ZhuanTiTable : UserEntity
    {
        /// <summary>
        /// 专题名称
        /// </summary>
        [MaxLength(20)]
        [Required]
        public string ZhuanTiName { get; set; }

        /// <summary>
        /// 同步Id
        /// </summary>
        public int? ZhuanTiId { get; set; }
    }
}
