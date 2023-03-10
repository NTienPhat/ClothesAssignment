using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DTO
{
    public partial class ProductDTO
    {
        public ProductDTO()
        {
            Carts = new HashSet<CartDTO>();
            OrderDetails = new HashSet<OrderDetailDTO>();
        }

        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public int InStock { get; set; }
        public Guid CategoryId { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }

        public virtual CategoryDTO Category { get; set; }
        public virtual ICollection<CartDTO> Carts { get; set; }
        public virtual ICollection<OrderDetailDTO> OrderDetails { get; set; }
    }
}
