using System;
using System.Collections.Generic;

#nullable disable

namespace Repository.Models
{
    public partial class Discount
    {
        public Discount()
        {
            Orders = new HashSet<Order>();
        }

        public Guid Id { get; set; }
        public string DiscountName { get; set; }
        public string DiscountStatus { get; set; }
        public decimal DiscountRate { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
