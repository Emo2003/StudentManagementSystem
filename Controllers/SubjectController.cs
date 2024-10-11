using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StudentProject.Models;

namespace StudentProject.Controllers
{
    public class SubjectController : Controller
    {
        private StudentContext context;
        public SubjectController()
        {
            context = new();
        }
        public IActionResult Index()
        {
            List<Subject> subject = context.subjects.Include(st => st.Doctor).ToList();
            return View(subject);
        }

        public IActionResult Details(int id)
        {
            if (id <= 0) return View("Error");
            Subject? subject = context.subjects.Include(st => st.Doctor).Where(st => st.ID == id).SingleOrDefault();
            if (subject == null) return View("Error");
            TempData["Action"] = "Users Details Successfully";
            return View(subject);
        }

       
        public IActionResult AddNew()
        {
            List<Doctor>? doctor = context.doctors.ToList();
            ViewBag.doctors = doctor;
        
            return View();
        }

        public IActionResult Update(int id)
        {
            Subject? subject = context.subjects.Where(st => st.ID == id).SingleOrDefault();
            if (subject == null) return View("Error");
            ViewBag.doctors = context.doctors.ToList();
         
            return View(subject);
        }

        public IActionResult Delete(int id)
        {
            Subject? subject = context.subjects.Where(st => st.ID == id).SingleOrDefault();
            if (subject == null) return View("Error");
            TempData["Action"] = "User Deleted Successfully";
            context.subjects.Remove(subject);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
      
        public IActionResult AddDB(Subject subject)
        {
            context.subjects.Add(subject);
            TempData["Action"] = "User Added Successfully";

            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult UpdateDB(int id, Subject subject)
        {
            Subject? oldsub = context.subjects.SingleOrDefault(st => st.ID == id);
            if (oldsub == null) return View("Error");
            oldsub.Name = subject.Name;
            oldsub.Doc_Id = subject.Doc_Id;
       
            TempData["Action"] = "User Update Successfully";
            context.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}
