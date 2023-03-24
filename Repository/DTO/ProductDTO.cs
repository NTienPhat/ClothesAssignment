using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required]
        public string ProductName { get; set; }
        [Required]
        [Range(1, 500)]
        public int InStock { get; set; }
        [Required]
        [ValidateNever]
        public Guid CategoryId { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Color { get; set; }
        [Required]
        public string Size { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Range(1, 99999999)]
        [Required]
        [ValidateNever]
        public string Image { get; set; }
        [ValidateNever]

        public virtual CategoryDTO Category { get; set; }
        public virtual ICollection<CartDTO> Carts { get; set; }
        public virtual ICollection<OrderDetailDTO> OrderDetails { get; set; }
    }
}