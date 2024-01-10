namespace EmployeeManagement.Models.Domain
{
    public class Leaves
    {
        public int LeaveId { get; set; }
        public string LeaveType { get; set;}
        public string LeaveDate { get; set; }
        public string LeaveStatus { get; set; }
        public int  EmpId { get; set; }
    }
}
