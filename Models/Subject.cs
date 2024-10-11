using System.ComponentModel.DataAnnotations.Schema;

namespace StudentProject.Models
{
    public class Subject
    {
        public int ID { get; set; }
        public string Name { get; set; }

        [ForeignKey(nameof(Doctor))]
        public int Doc_Id { get; set; }

        // Navigation Properties
        public List<Stu_Sub>? Students { get; set; }
        public Doctor? Doctor { get; set; }
    }
}