using AALife.Core.Domain;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AALife.Core
{
    /// <summary>
    /// Base class for entities
    /// </summary>
    public abstract partial class UserEntity : BaseEntity
    {
        /// <summary>
        /// �û�Id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// �û�
        /// </summary>
        [JsonIgnore]
        public virtual UserTable User { get; set; }

    }

}
