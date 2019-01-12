using System.Collections.Generic;
using System.Threading.Tasks;
using DemoApp.API.Models;

namespace DemoApp.API.Data
{
    public interface IZawajRepository
    {
         void Add<T>(T entity) where T:class;

          void Delete<T>(T entity) where T:class;
          Task<bool> SaveAll();

          Task<IEnumerable<User>> GetUsers();
          Task<User> GetUser(int id);
         
    }
}