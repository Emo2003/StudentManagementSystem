using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentProject.Models;

namespace StudentProject.Controllers
{
    public class DoctorController : Controller
    {
        private StudentContext context;
        public DoctorController()
        {
            context = new();
        }

        public IActionResult Index()
        {
            List<Doctor> doctor = context.doctors.ToList();
            return View(doctor);
           
        }
        public IActionResult Details(int id)
        {
            if (id <= 0) return View("Error");
            Doctor? doctor = context.doctors.Where(d => d.ID == id).SingleOrDefault();
            if (doctor == null) return View("Error");
            TempData["Action"] = "Users Details Successfully";
            return View(doctor);

        }
        public IActionResult AddNew()
        {
            return View();
        }
        public IActionResult Update(int id)
        {
            Doctor? doctor = context.doctors.Where(d => d.ID == id).SingleOrDefault();
            if (doctor == null) return View("Error");
          
            return View(doctor);
        }
        public IActionResult Delete(int id)
        {
            Doctor? doctor = context.doctors.Where(d=> d.ID == id).SingleOrDefault();
            if (doctor == null) return View("Error");
            TempData["Action"] = "User Deleted Successfully";
            context.doctors.Remove(doctor);
            context.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult AddDB(Doctor doctor)
        {
            context.doctors.Add(doctor);
            TempData["Action"] = "User Added Successfully";
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult UpdateDB(int id, Doctor doctor)
        {
            Doctor? olddoc = context.doctors.SingleOrDefault
                (d => d.ID == id);
            if (olddoc == null) return View("Error");
            olddoc.Name = doctor.Name;
            olddoc.DateOfBirth = doctor.DateOfBirth;
            olddoc.Address = doctor.Address;
            olddoc.Phone = doctor.Phone;
            TempData["Action"] = "User Update Successfully";
            context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
