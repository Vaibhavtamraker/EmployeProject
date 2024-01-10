using EmployeeManagement.Models.Domain;

namespace EmployeeManagement.Models.Repositories.Abstract
{
    public interface IEmployeeService
    {
        Employee ViewProfile(int empId);
        string ApplyLeave(Leaves obj);
        Salary ViewSalarySlip(int id);
        IEnumerable<Leaves> GetAllLeaveHistory(int id);
    }
}
