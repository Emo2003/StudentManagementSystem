using System.ComponentModel.DataAnnotations.Schema;

namespace StudentProject.Models
{
	public class Expense
	{
		public int ID { get; set; }
		public decimal Amount { get; set; }
		public DateTime Date_Of_Payment { get; set; }

		[ForeignKey(nameof(student))]
		public int Stu_Id { get; set; }

		// Navigation Property
		public Student? student { get; set; }
		
	}
}
