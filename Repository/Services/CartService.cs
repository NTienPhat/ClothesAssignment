using Repository.Models;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Services
{
    public interface ICartService
    {
        public List<Cart> GetAll(Guid userId);
        public Cart Create(Cart cart);
        public Cart Update(Cart cart);
        public bool Delete(Cart cart);
    }

    public class CartService : ICartService
    {
        private readonly RepositoryBase<Cart> _repository;
        public CartService()
        {
            _repository = new RepositoryBase<Cart>();
        }

        public Cart Create(Cart cart)
        {
            return _repository.CreateEntity(cart);
        }

        public bool Delete(Cart cart)
        {
            _repository.Delete(cart);
            return true;
        }

        public List<Cart> GetAll(Guid userId)
        {
            return _repository.GetAll().Where(x => x.UserId == userId).ToList();
        }

        public Cart Update(Cart cart)
        {
            return _repository.UpdatEntity(cart);
        }
    }
}
