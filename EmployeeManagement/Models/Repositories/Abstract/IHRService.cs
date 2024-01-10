using EmployeeManagement.Models.Domain;

namespace EmployeeManagement.Models.Repositories.Abstract
{
    public interface IHRService
    {
       bool AddEmp(Employee employee);
       bool UpdateEmp(Employee employee);
       bool DeleteEmp(int id);
       IEnumerable<Employee> GetAllEmployees();
        IEnumerable<Leaves> AllPendingLeaves();
        Employee GetById(int id);

        string AcceptLeave(int id);
        string RejectLeave(int id);

        string GenerateSalary(Salary salary);
       
        

    }
}
