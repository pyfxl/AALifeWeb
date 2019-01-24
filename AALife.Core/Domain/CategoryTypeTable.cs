using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AALife.Core.Domain
{
    [Table("tab_CategoryTypeTable")]
    public partial class CategoryTypeTable : UserEntity
    {
        /// <summary>
        /// 类别名称
        /// </summary>
        [MaxLength(20)]
        [Required]
        public string CategoryTypeName { get; set; }

        /// <summary>
        /// 同步Id
        /// </summary>
        [Required]
        public int CategoryTypeId { get; set; }
        
    }
}
