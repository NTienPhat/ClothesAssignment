using Repository.Models;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Services
{
    public interface IOrderdetailService
    {
        public List<OrderDetail> GetAll();
        public void Create(OrderDetail oder);
        public void Update(OrderDetail category);
        public bool Delete(OrderDetail category);
    }
    public class OrderDetailService : IOrderdetailService
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
