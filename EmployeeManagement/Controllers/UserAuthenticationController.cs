using EmployeeManagement.Models.Domain;
using EmployeeManagement.Models.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    public class UserAuthenticationController : Controller
    {
        private readonly IUserAuthentication _authentication;

        public UserAuthenticationController(IUserAuthentication authentication)
        {
            _authentication = authentication;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login() 
        {
            return View();

        }
        [HttpPost]
        public IActionResult Login(string email,string password) 
        {
            var  newuser = _authentication.Login(email, password,out Employee user);
            if(user != null)
            {
                if(user.RoleId == 1)
                {
                    return RedirectToAction("GetAllSalary", "Admin");
                }
                else if (user.RoleId == 2)
                {
                    return RedirectToAction("Index", "HR");
                }
                else
                {
                    return RedirectToAction("ViewProfile", "Employee", new {id=user.EmpId});
                }
            }
            else
            {
                TempData["msg"] = "Login Unsuccessfull";
                return View();
            }

        }
    }
}