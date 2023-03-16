using Microsoft.EntityFrameworkCore;
using Repository.Model;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Services
{
    public interface IUserService
    {
        public List<User> GetAll();
        public void Create(User user);
        public void Update(User user);
        public bool Delete(User user);
        public List<User> SearchByKeyword(string keyword);
    }
    public class UserService : IUserService
    {
        private readonly RepositoryBase<User> _repository;

        public UserService()
        {
            _repository = new RepositoryBase<User>();
        }
        public void Create(User user)
        {
            _repository.Create(user);
        }

        public bool Delete(User user)
        {
            _repository.Delete(user);
            return true;
        }
        public void Update(User user)
        {
            _repository.Update(user);
        }

        public List<User> GetAll()
        {
            return _repository.GetAll().ToList();
        }

        public List<User> SearchByKeyword(string keyword)
        {
            try
            {
                var x = _repository.GetAll().ToList().Where(x => x.FullName.Contains(keyword));
                if (x != null)
                {
                    return (List<User>)x;
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
