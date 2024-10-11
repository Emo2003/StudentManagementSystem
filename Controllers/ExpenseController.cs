using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentProject.Models;

namespace StudentProject.Controllers
{
    public class ExpenseController : Controller
    {
        private StudentContext context;
        public ExpenseController(){
            context = new() ;
            }
        public IActionResult Index()
        {
            List<Expense> expense = context.expenses.Include(e=>e.student).ToList();

            return View(expense);
        }
        public IActionResult Details(int id)
        {
            if (id <= 0) return View("Error");
            Expense? expense = context.expenses.Include(e=>e.student ).Where(e => e.ID == id).SingleOrDefault();
            if (expense == null) return View("Error");
            TempData["Action"] = "Users Details Successfully";
            return View(expense);
        }
        public IActionResult AddNew()
        {
            List<Student>? students = context.students.ToList();
            ViewBag.students = students;

            return View();
        }
        public IActionResult Update(int id)
        {
            Expense? expense = context.expenses.Where(e => e.ID == id).SingleOrDefault();
            if (expense == null) return View("Error");
            ViewBag.students = context.students.ToList();

            return View(expense);
        }
        public IActionResult Delete(int id)
        {
            Expense? expense = context.expenses.Where(e => e.ID == id).SingleOrDefault();
            if (expense == null) return View("Error");
            TempData["Action"] = "User Deleted Successfully";
            context.expenses.Remove(expense);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult AddDB(Expense expense)
        {
            context.expenses.Add(expense);
            TempData["Action"] = "User Added Successfully";

            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult UpdateDB(int id, Expense expense)
        {
            Expense? oldexpen = context.expenses.SingleOrDefault(e => e.ID == id);
            if (oldexpen == null) return View("Error");
            oldexpen.Amount = expense.Amount;
            oldexpen.Date_Of_Payment = expense.Date_Of_Payment;
            oldexpen.Stu_Id = expense.Stu_Id;

            TempData["Action"] = "User Update Successfully";
            context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
