using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Data;

namespace StudentProject.Models
{
    public class StudentContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-4OLNOM4;DataBase=StudentsManagement;Trusted_Connection=True;TrustServerCertificate=True");
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Student> students { get; set; }
        public DbSet<Doctor> doctors { get; set; }
        public DbSet<Admin> admins { get; set; }
        public DbSet<Subject> subjects { get; set; }
        public DbSet<Expense> expenses { get; set; }
        public DbSet<Stu_Sub> stu_Subs { get; set; }
    }
}