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
          
    }
}