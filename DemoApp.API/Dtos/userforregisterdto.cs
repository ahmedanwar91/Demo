using System.ComponentModel.DataAnnotations;

namespace DemoApp.API.Dtos
{
    public class userforregisterdto
    {
         [Required]
         public string Username { get; set; }
        public string fullname { get; set; }
        public string Email { get; set; }
       [StringLength(8,MinimumLength=4,ErrorMessage="Password length must be min 4 and max 8 char")]
        public string Password { get; set; }
          public bool Enable { get; set; }
    }
}