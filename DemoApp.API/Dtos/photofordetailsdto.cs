using System;

namespace DemoApp.API.Dtos
{
    public class photofordetailsdto
    {
       public int Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public bool IsMain { get; set; }   
    }
}