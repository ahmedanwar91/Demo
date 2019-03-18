using System;
using System.Threading.Tasks;
using DemoApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoApp.API.Data
{
    public class AuthRepository : IAuthRepository
    {
        public DataContext _Context { get; set; }
        public AuthRepository(DataContext context)
        {
            this._Context = context;

        }
        public async Task<User> Login(string username, string password)
        {
            try
            {
                  var user = await _Context.Users.Include(p=>p.Photos).FirstOrDefaultAsync(x=>x.Username==username);
              // var user=await _Context.Users.FirstOrDefaultAsync(x=>x.Username==username);
               if(user==null) return null;
               if(!VerifyPasswordHash(password,user.PasswordSalt,user.PasswordHash))
               return null;
               return user;
            }
            catch
            {
                return null;
            }
           
        }

        private bool VerifyPasswordHash(string password, byte[] passwordSalt, byte[] passwordHash)
        {
            using(var hmac=new System.Security.Cryptography.HMACSHA512(passwordSalt)) {
                 
                var ComputedHash=hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < ComputedHash.Length; i++)
                {
                    if(ComputedHash[i]!=passwordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
             
        }

        public async Task<User> Register(User user, string password)
        {
             try
             {
                 byte[] PasswordHash,PasswordSolt;
                 createPasswordHash( password,out PasswordHash,out PasswordSolt );
                 user.PasswordSalt=PasswordSolt;
                 user.PasswordHash=PasswordHash;
                 await _Context.Users.AddAsync(user);
                 await _Context.SaveChangesAsync();

                 return user;
             }
             catch (System.Exception)
             {
                 
                 throw;
             }
        }

        private void createPasswordHash( string password, out byte[] passwordHash,  out byte[] PasswordSolt)
        {
            using(var hmac=new System.Security.Cryptography.HMACSHA512()) {
                PasswordSolt=hmac.Key;
                passwordHash=hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

        }

        public async Task<bool> UserExists(string username)
        {
           if(await _Context.Users.AnyAsync(x=>x.Username==username))  return true;
           return false;
        }
    }
}