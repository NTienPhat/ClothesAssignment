using System;
using System.Collections.Generic;

#nullable disable

namespace Repository.Model
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public Guid Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderNumber { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
