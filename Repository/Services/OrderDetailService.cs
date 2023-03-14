using Repository.Models;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Services
{
    public interface IOrderDetailService
    {
        public List<OrderDetail> GetAll();
        public void Create(OrderDetail orderDetail);
        public void Update(OrderDetail orderDetail);
        public bool Delete(OrderDetail orderDetail);
    }
    public class OrderDetailService : IOrderDetailService
    {
        private readonly RepositoryBase<OrderDetail> _repository;

        public OrderDetailService()
        {
            _repository = new RepositoryBase<OrderDetail>();
        }

        public void Create(OrderDetail orderDetail)
        {
            _repository.Create(orderDetail);
        }

        public bool Delete(OrderDetail orderDetail)
        {
            _repository.Delete(orderDetail);
            return true;
        }

        public List<OrderDetail> GetAll()
        {
            return _repository.GetAll().ToList();
        }

        public void Update(OrderDetail orderDetail)
        {
            _repository.Update(orderDetail);    
        }
    }
}
