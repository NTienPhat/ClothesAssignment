using Repository.Models;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Services
{
    public interface IDiscountService
    {
        public List<Discount> GetAll();
        public void Create(Discount discount);
        public void Update(Discount discount);
        public bool Delete(Discount discount);
    }
    public class DisocuntService : IDiscountService
    {
        private readonly RepositoryBase<Discount> _repository;
        public DisocuntService()
        {
            _repository = new RepositoryBase<Discount>();
        }

        public void Create(Discount discount)
        {
            _repository.Create(discount);
        }

        public bool Delete(Discount discount)
        {
            _repository.Delete(discount);
            return true;
        }

        public List<Discount> GetAll()
        {
            return _repository.GetAll().ToList();
        }

        public void Update(Discount discount)
        {
            _repository.Update(discount);
        }
    }
}
