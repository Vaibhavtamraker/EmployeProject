using EmployeeManagement.Models.Domain;

namespace EmployeeManagement.Models.Repositories.Abstract
{
    public interface IAdminService:IHRService
    {
        string ApproveSalarySlip(int id);
        string RejectSalarySlip();
        List<Salary> ViewAllSalary();
    }
}
