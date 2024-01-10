using EmployeeManagement.Models.Domain;
using EmployeeManagement.Models.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    public class HRController : Controller
    {
        private readonly IHRService _HRService;
        public HRController(IHRService hRService)
        {
            _HRService=hRService;
        }
  
        public ActionResult Index()
        {
            return View(_HRService.GetAllEmployees());
        }
        public ActionResult Create()
        {
           
            return View();
        }
        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            _HRService.AddEmp(employee);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public ActionResult Edit(int id) 
        {
            
            return View(_HRService.GetById(id));
        }

        [HttpPost]
       
        public ActionResult Edit(Employee employee)
        {
            _HRService.UpdateEmp(employee);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            return View(_HRService.GetById((int)id));
        }

        [HttpPost]    
        public ActionResult Delete(int id)
        {
            _HRService.DeleteEmp(id);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public ActionResult GetAllPendingLeave()
        {
            return View(_HRService.AllPendingLeaves());
        }

        public ActionResult ApproveLeave(int id)
        {
           string val= _HRService.AcceptLeave(id);
            return RedirectToAction("GetAllPendingLeave");
        }
        public ActionResult RejectLeave(int id)
        {
            string val = _HRService.RejectLeave(id);
            return RedirectToAction("GetAllPendingLeave");
        }
        [HttpGet]
        public IActionResult CreateSalary(int id)
        {
            Salary salary = new Salary();
            salary.EmpId = id;
            return View(salary);
        }
        [HttpPost]
        public IActionResult CreateSalary(Salary salary)
        {
            _HRService.GenerateSalary(salary);
            return RedirectToAction(nameof(Index));
        }


    }
}
