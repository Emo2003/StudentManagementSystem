using System.ComponentModel.DataAnnotations.Schema;

namespace StudentProject.Models
{
	public class Student
	{
		public int ID { get; set; }
		public string Name { get; set; }
		public string Address { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
		public string Password { get; set; }
		public DateTime DateOfBirth { get; set; }
		public string Level { get; set; }
		public Decimal TotalMark {  get; set; }
		public Decimal GPA { get; set; }

		[ForeignKey(nameof(subject))]
		public int Sub_Id { get; set; }

		// Navigation Property
		public Subject? subject { get; set; }
		 public List< Stu_Sub>? subjects { get; set; }
		public Expense? expense { get; set; }
		
	}
}
