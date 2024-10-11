using System.ComponentModel.DataAnnotations.Schema;

namespace StudentProject.Models
{
    public class Doctor
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

        // Navigation Properties
        public List<Subject>? Subject { get; set; }
    }
}