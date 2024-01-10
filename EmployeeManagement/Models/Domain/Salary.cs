namespace EmployeeManagement.Models.Domain
{
    public class Salary
    {
        public int SalaryId { get; set; }
        public int EmpId { get; set; }
        public string Month { get; set; }
        public bool HRGenerated { get; set; }
        public bool AdminApproved { get; set; }
        public decimal BasicSalary { get; set; }
        public decimal HRA { get; set; }
        public decimal TA { get; set;}
        public decimal DA { get; set; }
        public decimal OtherAllowence { get; set; }
        public decimal TDS { get; set; }
        public decimal TotalSalary { get; set; }
        public int TotalMonthDays { get; set; }
        public int TotalLeaves { get; set;}
        public decimal FinalSalary { get; set; }
    }
}
