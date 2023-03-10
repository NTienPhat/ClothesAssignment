using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DTO
{
    public partial class DiscountDTO
    {
        public DiscountDTO()
        {
            Orders = new HashSet<OrderDTO>();
        }

        public Guid Id { get; set; }
        public string DiscountName { get; set; }
        public string DiscountStatus { get; set; }
        public decimal DiscountRate { get; set; }

        public virtual ICollection<OrderDTO> Orders { get; set; }
    }
}
