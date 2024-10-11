using StudentProject.Models;
using StudentProject.Requests;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace StudentProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly StudentContext context;

        public AccountController()
        {
            context = new();
        }

        public IActionResult Basic()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(Registerval request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            var admin = new Admin
            {
                Email = request.Email,
                Password = request.Password,
                Name = request.Name,
                Address = request.Address,
                Phone = request.Phone,
                Role = request.Is_Admin ? Role.Admin : Role.Student
            };

            context.admins.Add(admin);
            context.SaveChanges();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Loginval request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            var admin = context.admins.FirstOrDefault(x => x.Email == request.Email && x.Password == request.Password);
            var student = context.students.FirstOrDefault(s => s.Email == request.Email && s.Password == request.Password);

            if (admin != null)
            {
                // Create the claims list for the user
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, admin.Name),
            new Claim(ClaimTypes.Email, admin.Email),
            new Claim(ClaimTypes.Role, admin.Role.ToString())
        };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                // Sign in the user using Authentication
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                // Check the user's role and redirect accordingly
                return RedirectToAction("Index", "Home");
            }
            else if (student != null)
            {
                // Create the claims list for the user
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, student.Name),
            new Claim(ClaimTypes.Email, student.Email),
            new Claim(ClaimTypes.Role, "Student")
        };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                // Sign in the user using Authentication
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                // Check the user's role and redirect accordingly
                return RedirectToAction("Profile", "Student");
            }
            else
            {
                ModelState.AddModelError("", "Email or Password is incorrect. Please try again!");
                return View(request);
            }
        }

        // Logout
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Profile()
        {
            var admin = context.admins.FirstOrDefault(x => x.Email == User.FindFirstValue(ClaimTypes.Email));

            if (admin != null)
            {
                return View(admin);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        public IActionResult Show()
        {
            return View();
        }
    }
}
