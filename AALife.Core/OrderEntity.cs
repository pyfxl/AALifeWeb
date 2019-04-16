using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AALife.Core
{
    public partial class OrderEntity<TPrimaryKey> : BaseEntity<TPrimaryKey>
    {
        public byte? Rank { get; set; }

        [MaxLength(20)]
        public string OrderNo { get; set; }

        public string GetOrderNo(OrderEntity<TPrimaryKey> parent)
        {
            if (parent == null)
                return this.Rank.Value.ToString("00");

            return string.Format("{0}.{1}", parent.OrderNo, this.Rank.Value.ToString("00"));
        }

    }

    public partial class OrderEntity : OrderEntity<int>
    {
    }

}
