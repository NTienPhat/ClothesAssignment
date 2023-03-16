using Repository.Model;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Services
{
    public interface ICategoryService
    {
        public List<Category> GetAll();
        public void Create(Category category);
        public void Update(Category category);
        public bool Delete(Category category);
    }
    public class CategoryService : ICategoryService
    {
        private readonly RepositoryBase<Category> _repository;
        public CategoryService()
        {
            _repository = new RepositoryBase<Category>();
        }

        public void Create(Category category)
        {
            _repository.Create(category);
        }

        public bool Delete(Category category)
        {
            _repository.Delete(category);
            return true;
        }

        public List<Category> GetAll()
        {
            return _repository.GetAll().ToList();
        }

        public void Update(Category category)
        {
            _repository.Update(category);
        }
    }
}
