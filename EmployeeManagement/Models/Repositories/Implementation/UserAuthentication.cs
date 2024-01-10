using Dapper;
using EmployeeManagement.Models.Domain;
using EmployeeManagement.Models.Repositories.Abstract;
using Microsoft.Data.SqlClient;

namespace EmployeeManagement.Models.Repositories.Implementation
{
    public class UserAuthentication : IUserAuthentication
    {
        private readonly IConfiguration _configuration;
        private readonly string DBConnectionstring;
        public UserAuthentication(IConfiguration configuration)
        {
            _configuration = configuration;
            DBConnectionstring = _configuration["ConnectionStrings:DBConn"] ?? "";

        }
        public bool Login(string email, string password,out Employee? user)
        {
            var result = false;
            using(var con= new SqlConnection(DBConnectionstring)) 
            {
                result = con.ExecuteScalar<bool>("select count(1) from Employee where Email=@email and Password=@password;", new { email, password });
                if (result)
                {
                    user = con.QueryFirst<Employee>("select * from Employee where Email=@email; ", new { email });
                }
                else
                {
                    user = null;
                }
     
            }
            return result;
        }
    }
}
