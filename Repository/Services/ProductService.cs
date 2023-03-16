using Repository.Models;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Services
{
    public interface IProductService
    {
        public IQueryable<Product> GetAll();
        public Product GetOne(Guid id);

        public void Create(Product product);
        public void Update(Product product);
        public bool Delete(Product product);
    }
    public class ProductService : IProductService
    {
        private readonly RepositoryBase<Product> _repository;
        public ProductService()
        {
            _repository = new RepositoryBase<Product>();
        }

        public void Create(Product product)
        {
            _repository.Create(product);
        }

        public bool Delete(Product product)
        {
            _repository.Delete(product);
            return true;
        }

        public IQueryable<Product> GetAll()
        {
            return _repository.GetAll();
        }

        public Product GetOne(Guid id)
        {
            return _repository.GetAll().SingleOrDefault(x => x.Id == id);
        }

        public void Update(Product product)
        {
            _repository.Update(product);
        }
    }
}
