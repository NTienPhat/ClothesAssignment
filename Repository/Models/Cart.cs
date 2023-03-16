using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
#nullable disable

namespace Repository.Models
{
    public partial class Cart
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
        public int Quantity { get; set; }
        [NotMapped]
        public decimal TotalMoney => Quantity * Product.Price;

        public virtual Product Product { get; set; }
        public virtual User User { get; set; }
    }
}
