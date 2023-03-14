using Repository.Models;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Services
{
    public interface IManagementUserService
    {
        public List<User> GetAll();
        public bool Delete(User user);
    }
    public class ManagementUserService : IManagementUserService
    {
        private readonly RepositoryBase<User> _repository;

        public ManagementUserService()
        {
            _repository = new RepositoryBase<User>();
        }

        public bool Delete(User user)
        {
            _repository.Delete(user);
            return true;
        }

        public List<User> GetAll()
        {
            return _repository.GetAll().ToList();
        }
    }
}
