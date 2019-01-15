using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AALife.Core.Domain.Configuration
{
    /// <summary>
    /// Represents a setting
    /// </summary>
    [Table("tab_Settings")]
    public partial class Settings : BaseEntity
    {
        public Settings() { }
        
        public Settings(string name, string value, int storeId = 0) {
            this.Name = name;
            this.Value = value;
            this.StoreId = storeId;
        }
        
        /// <summary>
        /// Gets or sets the name
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the value
        /// </summary>
        [MaxLength(2000)]
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the store for which this setting is valid. 0 is set when the setting is for all stores
        /// </summary>
        public int StoreId { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
