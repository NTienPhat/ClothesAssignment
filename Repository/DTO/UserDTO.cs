using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DTO
{
    public partial class UserDTO
    {
        public UserDTO()
        {
            Carts = new HashSet<CartDTO>();
            Orders = new HashSet<OrderDTO>();
        }

        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }

        public virtual ICollection<CartDTO> Carts { get; set; }
        public virtual ICollection<OrderDTO> Orders { get; set; }
    }
}
