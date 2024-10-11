using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentProject.Models;
using System.Security.Claims;

namespace StudentProject.Controllers
{
    public class StudentController : Controller
    {
        private StudentContext context;
        public StudentController()
        {
            context = new();
        }
        public IActionResult Index() { 
        List<Student> students = context.students.Include(u=>u.subject).ToList();
            return View(students);
    
                      }
        public IActionResult Details(int id)
        {
            if (id <= 0) return View("Error");
            Student? student = context.students.Where(u => u.ID == id).SingleOrDefault();
            if (student == null) return View("Error");
            TempData["Action"] = "Users Details Successfully";
            return View(student);

        }
        public IActionResult AddNew()
        {
            List<Subject>? subjects=context.subjects.ToList();
            ViewBag.Subjects = subjects;
       
            return View();
        }
        public IActionResult Update(int id)
        {
            Student? student = context.students.Where(u => u.ID == id).SingleOrDefault();
            if (student == null) return View("Error");
            ViewBag.Subjects = context.subjects.ToList();
        
            return View(student);
        }
        public IActionResult Delete(int id)
        {
            Student? student = context.students.Where(u => u.ID == id).SingleOrDefault();
            if (student == null) return View("Error");
            TempData["Action"] = "User Deleted Successfully";
            context.students.Remove(student);
            context.SaveChanges();

            return RedirectToAction
          ("Index");
        }
        public IActionResult AddDB(Student student)
        {
            context.students.Add(student);
            TempData["Action"] = "User Added Successfully";
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult UpdateDB(int id, Student student)
        {
            Student? olduser = context.students.SingleOrDefault
                (u => u.ID == id);
            if (olduser == null) return View("Error");
            olduser.Name = student.Name;
            olduser.Email = student.Email;
            olduser.DateOfBirth = student.DateOfBirth;
            olduser.PhoneNumber= student.PhoneNumber;
            olduser.Address = student.Address;
            olduser.Level = student.Level;
            olduser.Sub_Id = student.Sub_Id;
            olduser.TotalMark= student.TotalMark;
            olduser.GPA = student.GPA;
            olduser.Password = student.Password;
            TempData["Action"] = "User Update Successfully";
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Profile()
        {
            var student = context.students
                .Include(s => s.subject)
                .FirstOrDefault(x => x.Email == User.FindFirstValue(ClaimTypes.Email));

            if (student != null)
            {
                return View(student);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
    }
}
