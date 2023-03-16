﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Repository.Model
{
    public partial class Cart
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
        public int Quantity { get; set; }

        public virtual Product Product { get; set; }
        public virtual User User { get; set; }
    }
}
