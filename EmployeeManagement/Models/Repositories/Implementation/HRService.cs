using Dapper;
using EmployeeManagement.Models.Domain;
using EmployeeManagement.Models.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models.Repositories.Implementation
{
    public class HRService : IHRService
    {
        private readonly IConfiguration _configuration;
        private readonly string DBConnectionstring;
        public HRService(IConfiguration configuration)
        {
            _configuration = configuration;
            DBConnectionstring = _configuration["ConnectionStrings:DBConn"] ?? "";

        }

        [HttpGet]
        public string AcceptLeave( int id)
        {
            using(var conn = new SqlConnection(DBConnectionstring))
            {
                var result = conn.Execute("update Leaves set LeaveStatus='Approved' where LeaveId=@id;", new {id});
                if(result == 0)
                {
                    return "Not Approved";
                }
                else
                {
                    return "Approved";
                }
            }
        }

        public bool AddEmp(Employee employee)
        {
            
            using(var conn=new SqlConnection(DBConnectionstring))
            {
                 employee.EmpCode= Guid.NewGuid().ToString();
             var result  =  conn.ExecuteScalar<Employee>("Insert into Employee(EName,Address,Phone,Email,EmpCode,BasicSalary,Password,RoleId) Values(@EName,@Address,@Phone,@Email,@EmpCode,@BasicSalary,@Password,@RoleId)", new { EName = employee.EName, Phone = employee.Phone, Address = employee.Address, Password = employee.Password, BasicSalary = employee.BasicSalary, Email = employee.Email, RoleId = employee.RoleId, EmpCode = employee.EmpCode });

                if(result!=null)
                {
                    return true;
                }
                else
                    return false;
            }
        }

        public IEnumerable<Leaves> AllPendingLeaves()
        {
            using(SqlConnection conn=new SqlConnection(DBConnectionstring))
            {
                List<Leaves> AllLeave = (List<Leaves>)conn.Query<Leaves>("Select * from Leaves where LeaveStatus='Pending';");
                return AllLeave;
            }
        }

        public bool DeleteEmp(int id)
        {
            var result = false;
            using(var conn= new SqlConnection(DBConnectionstring))
            {
                result = conn.ExecuteScalar<bool>("select count(1) from Employee where EmpId=@EmpId;", new { EmpId = id } );
                if(result)
                {

                    conn.Execute("Delete From Employee where EmpId=@EmpId",new {EmpId=id});

                    return result;
                }
                else
                    return result;
            }
        }


            public string GenerateSalary(Salary salary)
            {
                using (var con = new SqlConnection(DBConnectionstring))
                {
                    
                    var result = con.Execute("Insert into Salary(EmpId,Month,HRGenerated,AdminApproved,BasicSalary,HRA,TA,DA,OtherAllowence,TDS,TotalSalary,TotalMonthDays,TotalLeaves,FinalSalary) Values(@EmpId,@Month,@HRGenerated,@AdminApproved,@BasicSalary,@HRA,@TA,@DA,@OtherAllowence,@TDS,@TotalSalary,@TotalMonthDays,@TotalLeaves,@FinalSalary) ;",
                        new { EmpId = salary.EmpId, Month = salary.Month, HRGenerated = true, AdminApproved = false, BasicSalary = salary.BasicSalary, HRA = salary.HRA, TA = salary.TA, DA = salary.DA, OtherAllowence = salary.OtherAllowence, TDS = salary.TDS, TotalSalary = salary.TotalSalary, TotalMonthDays = salary.TotalMonthDays, TotalLeaves = salary.TotalLeaves, FinalSalary = salary.FinalSalary });
                    if (result == 1)
                        return "Salary Slip generated ";
                    else
                        return "Salary slip not created";
                }
            }
        

        public IEnumerable<Employee> GetAllEmployees()
        {
            IEnumerable<Employee> result = new List<Employee>();
            using(var conn= new SqlConnection(DBConnectionstring))
            {
                result = conn.Query<Employee>("Select * from Employee");
                return result.ToList();
            }
        }

        public Employee GetById(int id)
        {
            using(var conn= new SqlConnection(DBConnectionstring))
            {
                Employee emp = conn.QueryFirst<Employee>("Select * from Employee where EmpId=@EmpId", new { EmpId = id });
                if(emp!=null)
                { return emp; }
                else 
                    return null;
            }
        }

        public string RejectLeave(int id)
        {
            using (var conn = new SqlConnection(DBConnectionstring))
            {
                var result = conn.Execute("update Leaves set LeaveStatus='Rejected' where LeaveId=@id;", new { id });
                if (result == 0)
                {
                    return "Not Rejected";
                }
                else
                {
                    return "Rejected";
                }
            }
        }

        public bool UpdateEmp(Employee employee)
        {
            using (var conn= new SqlConnection(DBConnectionstring))
            {
                bool result = conn.ExecuteScalar<bool>("select count(1) from Employee where EmpId=@EmpId;", new { EmpId = employee.EmpId });
                if (result)
                {
                    conn.Execute("Update Employee SET EName=@EName,Address=@Address,Phone=@Phone,Email=@Email,EmpCode=@EmpCode,BasicSalary=@BasicSalary,Password=@Password,RoleId=@RoleId where EmpId=@EmpId", new {EmpId=employee.EmpId, EName =employee.EName,Phone=employee.Phone,Address=employee.Address,Password=employee.Password, BasicSalary =employee.BasicSalary,Email=employee.Email,RoleId=employee.RoleId,EmpCode=employee.EmpCode});
                    return result;
                }
                else
                    return result;
            }
        }
    }
}
