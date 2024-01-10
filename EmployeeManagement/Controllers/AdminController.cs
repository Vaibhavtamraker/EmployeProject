using EmployeeManagement.Models.Domain;
using EmployeeManagement.Models.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminService _AdminService;
        public AdminController(IAdminService adminService)
        {
            _AdminService = adminService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAllSalary()
        {
            return View(_AdminService.ViewAllSalary());
        }

        public IActionResult ApproveSalary(int id)
        {
            string value=_AdminService.ApproveSalarySlip(id);
            return RedirectToAction(nameof(GetAllSalary));
        }
    }
}
