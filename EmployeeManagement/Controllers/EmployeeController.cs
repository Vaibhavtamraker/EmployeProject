using EmployeeManagement.Models.Domain;
using EmployeeManagement.Models.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _EmpService;
        public EmployeeController(IEmployeeService EmpService)
        {
            _EmpService = EmpService;
        }

        public IActionResult ViewProfile(int id)
        {
            
            return View(_EmpService.ViewProfile(id));
        }
        [HttpGet]
        public IActionResult ApplyLeave(int id)
        {
            Leaves obj = new Leaves();
            obj.EmpId= id;
            return View(obj);
        }
        [HttpPost]
        public IActionResult ApplyLeave(Leaves obj) 
        {
            string mess= _EmpService.ApplyLeave(obj);
            TempData["message"]= mess;
            return RedirectToAction("ViewProfile", new {id=obj.EmpId});
        }
        [HttpGet]
        public IActionResult GetLeaveHistory(int id)
        {
            return View(_EmpService.GetAllLeaveHistory(id));
        }
        [HttpGet]
        public IActionResult ViewSlip(int id)
        {
            return View(_EmpService.ViewSalarySlip(id));

        }
    }
}
