using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoApp.API.Data
{
    public class ZawajRepository : IZawajRepository
    {
        private readonly DataContext _Context;
        public ZawajRepository(DataContext Context)
        {
            _Context = Context;

        }
        public void Add<T>(T entity) where T : class
        {
             _Context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _Context.Remove(entity);
        }

        public async Task<Photo> GetPhoto(int id)
        {
            var photo=await  _Context.Photos.FirstOrDefaultAsync(u=>u.Id==id);
            
            return photo;
        }

        public async Task<User> GetUser(int id)
        {
          //  var users=await _Context.Users.FirstOrDefaultAsync(u=>u.Id==id);
           // return users;
            var user=await  _Context.Users.Include(u=>u.Photos).FirstOrDefaultAsync(u=>u.Id==id);
            
            return user;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var users=await _Context.Users.Include(u=>u.Photos).ToListAsync();
            return users;
        }
         
 public async Task<Photo> GetMainPhotoForUser(int userId)
        {
           return await _Context.Photos.Where(u=>u.UserId==userId).FirstOrDefaultAsync(p=>p.IsMain);
           
        }
        public async Task<bool> SaveAll()
        {
             return await _Context.SaveChangesAsync()>0;
        }
    }
}