using Dapper;
using EmployeeManagement.Models.Domain;
using EmployeeManagement.Models.Repositories.Abstract;
using Microsoft.Data.SqlClient;

namespace EmployeeManagement.Models.Repositories.Implementation
{
    public class AdminService : IAdminService
    {
        private readonly IConfiguration _configuration;
        private readonly string DBConnectionstring;
        public AdminService(IConfiguration configuration)
        {
            _configuration = configuration;
            DBConnectionstring = _configuration["ConnectionStrings:DBConn"] ?? "";

        }
     

        public string AcceptLeave(int id)
        {
            throw new NotImplementedException();
        }

        public bool AddEmp(Employee employee)
        {
            using (var conn = new SqlConnection(DBConnectionstring))
            {
                employee.EmpCode = Guid.NewGuid().ToString();
                var result = conn.ExecuteScalar<Employee>("Insert into Employee(EName,Address,Phone,Email,EmpCode,BasicSalary,Password,RoleId) Values(@EName,@Address,@Phone,@Email,@EmpCode,@BasicSalary,@Password,@RoleId)", new { employee });

                if (result != null)
                {
                    return true;
                }
                else
                    return false;
            }
        }

        public IEnumerable<Leaves> AllPendingLeaves()
        {
            throw new NotImplementedException();
        }

        public string ApproveSalarySlip(int id)
        {
            using (var conn = new SqlConnection(DBConnectionstring))
            {
                
                var result = conn.Execute("update Salary set AdminApproved=@AdminApproved where SalaryId=@SalaryId;", new { AdminApproved = true ,SalaryId=id });
                if (result == 0)
                {
                    return "Not Approved";
                }
                else
                {
                    return "Approved";
                }
            }
        }



        public bool DeleteEmp(int id)
        {
            var result = false;
            using (var conn = new SqlConnection(DBConnectionstring))
            {
                result = conn.ExecuteScalar<bool>("select count(1) from Employee where EmpId=@EmpId;", id);
                if (result)
                {

                    conn.Execute("Delete From Employee where EmpId=@EmpId", id);

                    return result;
                }
                else
                    return result;
            }
        }

        

        public string GenerateSalary()
        {
            throw new NotImplementedException();
        }

        public string GenerateSalary(Salary salary)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            IEnumerable<Employee> result = new List<Employee>();
            using (var conn = new SqlConnection(DBConnectionstring))
            {
                result = (IEnumerable<Employee>)conn.Query("Select * from Employee");
                return result;
            }
        }

        public Employee GetById(int id)
        {
            using (var conn = new SqlConnection(DBConnectionstring))
            {
                Employee emp = conn.QueryFirst<Employee>("Select * from Employee where EmpId=@EmpId", id);
                if (emp != null)
                { return emp; }
                else
                    return null;
            }
        }

        public string RejectLeave(int id)
        {
            throw new NotImplementedException();
        }

        public string RejectSalarySlip()
        {
            throw new NotImplementedException();
        }

        public bool UpdateEmp(Employee employee)
        {
            throw new NotImplementedException();
        }

        public List<Salary> ViewAllSalary()
        {
            using (var conn = new SqlConnection(DBConnectionstring))
            {
                var salaries = conn.Query<Salary>("Select * from Salary where AdminApproved='false';");
                if (salaries != null)
                { return salaries.ToList(); }
                else
                    return null;
            }
        }
    }
}
