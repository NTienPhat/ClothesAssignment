using System;
using System.Collections.Generic;

#nullable disable

namespace Repository.Models
{
    public partial class Order
    {
        public Guid Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderNumber { get; set; }
    }
}
