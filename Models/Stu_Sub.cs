using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentProject.Models
{
	
	public class Stu_Sub
	{
		public int ID { get; set; }

		[ForeignKey(nameof(students))]
        public int Stu_Id { get; set; }
		[ForeignKey(nameof(subjects))]
		public int Sub_Id { get; set; }

		//Navigation Property

		public Student? students { get; set; }	
		public Subject? subjects { get; set; }

	}
}
