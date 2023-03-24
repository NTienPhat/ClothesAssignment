using Repository.Models;
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
        public DateTime OrderDate { get; set; }
        public string OrderNumber { get; set; }

        public virtual ICollection<OrderDetailDTO> OrderDetails { get; set; }
    }
}
