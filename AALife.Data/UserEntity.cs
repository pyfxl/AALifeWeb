using AALife.Core;
using AALife.Data.Domain;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Script.Serialization;

namespace AALife.Data
{
    /// <summary>
    /// Base class for entities
    /// </summary>
    public abstract partial class UserEntity : BaseEntity
    {
        /// <summary>
        /// ���÷�
        /// </summary>
        public byte Live { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        public byte? Rank { get; set; }

        /// <summary>
        /// ͬ��
        /// </summary>
        public byte Synchronize { get; set; }

        /// <summary>
        /// �޸�����
        /// </summary>
        public DateTime? ModifyDate { get; set; }

        /// <summary>
        /// ��ע
        /// </summary>
        [MaxLength(100)]
        public string Remark { get; set; }

        /// <summary>
        /// ͼƬ
        /// </summary>
        [MaxLength(200)]
        public string Image { get; set; }

        /// <summary>
        /// Gets or sets the customer identifier
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the customer
        /// </summary>
        [JsonIgnore]
        public virtual UserTable User { get; set; }

    }

}
