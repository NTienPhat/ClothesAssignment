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
        public List<Order> SearchByKeyword(string keyword);
    }

    public class OrderService : IOrderService
    {
        private readonly RepositoryBase<Order> _repository;

        public OrderService()
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
        public List<Order> GetAll()
        {
            return _repository.GetAll().ToList();
        }
        public List<Order> SearchByKeyword(string keyword)
        {
            try
            {
                var x = _repository.GetAll().ToList().Where(x => x.OrderNumber.Contains(keyword));
                if (x != null)
                {
                    return (List<Order>)x;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}