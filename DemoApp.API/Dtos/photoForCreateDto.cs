using System;
using Microsoft.AspNetCore.Http;
namespace DemoApp.API.Dtos
{
    public class photoForCreateDto
    {
        public photoForCreateDto()
        {
            DateAdded=DateTime.Now;
        }
        public string Url { get; set; }
         public  IFormFile File { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
            
    }
}