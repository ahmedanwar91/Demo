using System.Collections.Generic;
using DemoApp.API.Models;
using Newtonsoft.Json;

namespace DemoApp.API.Data
{
    public class TrialData
    {
        private readonly DataContext _context;
        public TrialData(DataContext context)
        {
            _context = context;

        }

        public void TrialUser(){
            var userData=System.IO.File.ReadAllText("Data/UsersTrialData.json");
            var users=JsonConvert.DeserializeObject<List<User>>(userData);
            foreach (var user in users)
            {
                byte[] passwordhash,passwordsalat;
                createPasswordHash("password",out passwordhash,out passwordsalat);
                user.PasswordHash=passwordhash;
                user.PasswordSalt=passwordsalat;
                user.Username=user.Username.ToLower();
                _context.Users.Add(user);
                
            }
            _context.SaveChanges();
        }

         private void createPasswordHash( string password, out byte[] passwordHash,  out byte[] PasswordSolt)
        {
            using(var hmac=new System.Security.Cryptography.HMACSHA512()) {
                PasswordSolt=hmac.Key;
                passwordHash=hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

        }
    }
}