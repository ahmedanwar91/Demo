using System;

namespace DemoApp.API.Dtos
{
    public class userforlistdto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string KnownAs { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
      
              public string city { get; set; }
        public string country { get; set; }
        public string photoUrl { get; set; }

        
    }
}