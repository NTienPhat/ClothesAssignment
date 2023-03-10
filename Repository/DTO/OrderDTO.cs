using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DTO
{
    public partial class OrderDTO
    {
        public OrderDTO()
        {
            OrderDetails = new HashSet<OrderDetailDTO>();
        }

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Total { get; set; }
        public Guid DiscountId { get; set; }
        public string OrderStatus { get; set; }

        public virtual DiscountDTO Discount { get; set; }
        public virtual UserDTO User { get; set; }
        public virtual ICollection<OrderDetailDTO> OrderDetails { get; set; }
    }
}
