using Microsoft.AspNetCore.Identity;

namespace StudentProject.Models
{
    public class Admin
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public Role Role { get; set; }
    }
}

        public enum Role
        {
            Student,
            Admin
        }

   

