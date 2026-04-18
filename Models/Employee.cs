namespace XsltEmployeeWpf.Models
{
    public class Employee
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public decimal TotalAmount { get; set; }
        public List<SalaryRecord> Salaries { get; set; } = new List<SalaryRecord>();
    }

    public class SalaryRecord
    {
        public string Month { get; set; }
        public decimal Amount { get; set; }
    }
}