using Repository.Models;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Services
{
    public interface IOrderService
    {
        public List<Order> GetAll();
        public void Create(Order oder);
        public void Update(Order oder);
        public bool Delete(Order oder);
    }
    public class OrderService : IOrderService
    {
        private readonly RepositoryBase<Order> _repository;
        public OrderService()
        {
            _repository = new RepositoryBase<Order>();
        }

        public void Create(Order oder)
        {
            _repository.Create(oder);
        }

        public bool Delete(Order oder)
        {
            _repository.Delete(oder);
            return true;
        }

        public List<Order> GetAll()
        {
            return _repository.GetAll().ToList();
        }

        public void Update(Order oder)
        {
            _repository.Update(oder);
        }
    }
}
