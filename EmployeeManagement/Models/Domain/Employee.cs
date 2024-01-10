namespace EmployeeManagement.Models.Domain
{
    public class Employee
    {
        public int EmpId { get; set; }
        public string EName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string EmpCode { get; set; }
        public decimal BasicSalary { get; set; }
        public int RoleId { get; set; }
    }
}
