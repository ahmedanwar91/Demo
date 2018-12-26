using System.ComponentModel.DataAnnotations;

namespace DemoApp.API.Dtos
{
    public class userforlogindto
    {
        [Required]
         public string Username { get; set; }
              public string Password { get; set; }
        
    }
}