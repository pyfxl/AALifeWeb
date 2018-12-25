using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SexSpider.Core.Models
{
    public class SexSpiders
    {
        [Key]
        public int SiteId { get; set; }

        [Required]
        [MaxLength(10)]
        public string SiteRank { get; set; }

        [Required]
        public byte VipLevel { get; set; }

        [Required]
        public byte IsHided { get; set; }

        [Required]
        [MaxLength(50)]
        public string SiteName { get; set; }

        [Required]
        [MaxLength(200)]
        public string ListPage { get; set; }

        [Required]
        [MaxLength(10)]
        public string PageEncode { get; set; }

        [Required]
        [MaxLength(100)]
        public string Domain { get; set; }

        [Required]
        [MaxLength(200)]
        public string SiteLink { get; set; }

        [MaxLength(50)]
        public string MainDiv { get; set; }

        [MaxLength(50)]
        public string ThumbDiv { get; set; }

        [MaxLength(50)]
        public string ListDiv { get; set; }

        [MaxLength(50)]
        public string ImageDiv { get; set; }

        [MaxLength(50)]
        public string PageDiv { get; set; }

        public byte? PageLevel { get; set; }

        [MaxLength(50)]
        public string ListFilter { get; set; }

        [MaxLength(50)]
        public string ImageFilter { get; set; }

        [MaxLength(50)]
        public string PageFilter { get; set; }

        [MaxLength(10)]
        public string DocType { get; set; }

        [MaxLength(10)]
        public string ImgType { get; set; }

        [MaxLength(50)]
        public string SiteFilter { get; set; }

        [MaxLength(1000)]
        public string SiteReplace { get; set; }

        [MaxLength(50)]
        public string LastStart { get; set; }
    }
}