using System.ComponentModel.DataAnnotations;

namespace DemoApp.API.Models
{
    public class Employee
    {
       
        [Key]
        public int id { get; set; }
        public string fullname { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public bool enable { get; set; }
        public bool isdeleted { get; set; }
    }
}