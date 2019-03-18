using System;
using System.ComponentModel.DataAnnotations;
 
  

namespace DemoApp.API.Dtos
{
    public class userforregisterdto
    {
         [Required]
         public string username { get; set; }
        public string fullname { get; set; }
        public string email { get; set; }
       [StringLength(8,MinimumLength=4,ErrorMessage="Password length must be min 4 and max 8 char")]
        public string password { get; set; }
         public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string KnownAs { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
          public string city { get; set; }
        public string country { get; set; }
        public userforregisterdto()
        {
            Created=DateTime.Now;
            LastActive=DateTime.Now;
        }
          
    }
}