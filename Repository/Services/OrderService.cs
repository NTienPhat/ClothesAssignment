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
        public void Create(Order order);
        public void Update(Order order);
        public bool Delete(Order order);
    }

    public class ManagementOrderService : IOrderService
    {
        private readonly RepositoryBase<Order> _repository;

        public ManagementOrderService()
        {
            _repository = new RepositoryBase<Order>();
        }

        public void Create(Order order)
        {
            _repository.Create(order);
        }

        public bool Delete(Order order)
        {
            _repository.Delete(order);
            return true;
        }
        public void Update(Order order)
        {
            _repository.Update(order);
        }
        public List<Order> GetAll() { 
            return _repository.GetAll().ToList();
        }
    }
}
