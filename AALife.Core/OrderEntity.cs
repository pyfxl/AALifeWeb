using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AALife.Core
{
    public partial class OrderEntity : BaseEntity
    {
        public byte? Rank { get; set; }

        [MaxLength(20)]
        public string OrderNo { get; set; }

        public string GetOrderNo(OrderEntity parent)
        {
            if (parent == null)
                return this.Rank.Value.ToString("00");

            return string.Format("{0}.{1}", parent.OrderNo, this.Rank.Value.ToString("00"));
        }

    }
}
