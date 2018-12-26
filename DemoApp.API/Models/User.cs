using System;

namespace DemoApp.API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string fullname { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public bool Enable { get; set; }
        public DateTime CreatedDate { get; set; }
 

    }
}