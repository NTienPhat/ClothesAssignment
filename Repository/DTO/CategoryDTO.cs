
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DTO
{
    public partial class CategoryDTO
    {
        public CategoryDTO()
        {
            Products = new HashSet<ProductDTO>();
        }
        public Guid Id { get; set; }
        [Required]
        public string CategoryName { get; set; }
        public virtual ICollection<ProductDTO> Products { get; set; }
    }
}