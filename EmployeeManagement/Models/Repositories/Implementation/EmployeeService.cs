using Dapper;
using EmployeeManagement.Models.Domain;
using EmployeeManagement.Models.Repositories.Abstract;
using Microsoft.Data.SqlClient;
using System.Data;

namespace EmployeeManagement.Models.Repositories.Implementation
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IConfiguration _configuration;
        private readonly string DBConnectionstring;
        public EmployeeService(IConfiguration configuration)
        {
            _configuration= configuration;
            DBConnectionstring = _configuration["ConnectionStrings:DBConn"]??"";

        }
        public string ApplyLeave(Leaves obj)
        {
            using(var conn = new SqlConnection(DBConnectionstring))
            {
                var result = conn.Execute("Insert into Leaves values(@LeaveType,@LeaveDate,@LeaveStatus,@EmpId);",new {LeaveType=obj.LeaveType,LeaveDate=obj.LeaveDate.ToString(),LeaveStatus="Pending",EmpId=obj.EmpId });
                if(result == 1)
                {
                    return "Leave apply succesfully";
                }
                else
                {
                    return "Something went wrong";
                }
            }
        }

        public IEnumerable<Leaves> GetAllLeaveHistory(int id)
        {
            using( var conn = new SqlConnection( DBConnectionstring))
            {
                List<Leaves> result = (List<Leaves>)conn.Query<Leaves>("select * from Leaves where EmpId=@id;", new { id });
                return result;
            }
        }

        public Employee ViewProfile(int empId)
        {
            using (var con = new SqlConnection(DBConnectionstring))
            {
                Employee Employee =con.QueryFirstOrDefault<Employee>("select * from Employee where EmpId=@Empid;", new {empId});
                return Employee;
            }
            
        }

        public Salary ViewSalarySlip(int id)
        {
            using (var con = new SqlConnection(DBConnectionstring))
            {
                Salary salary = con.QueryFirstOrDefault<Salary>("select * from Salary where EmpId=@Empid;", new { Empid= id });
                return salary;
            }

        }
    }
}
