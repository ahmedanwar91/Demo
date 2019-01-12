using System;
using System.Collections.Generic;
using DemoApp.API.Models;

namespace DemoApp.API.Dtos
{
    public class userfordetailsdto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string KnownAs { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public string Introduction { get; set; }
        public string LookingFor { get; set; }
        public string interests { get; set; }

        public string city { get; set; }
        public string country { get; set; }
        public string photoUrl { get; set; }

        public ICollection<photofordetailsdto> Photos { get; set; }

        
    }
}