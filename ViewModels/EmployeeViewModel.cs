using CommunityToolkit.Mvvm.ComponentModel;
using XsltEmployeeWpf.Models;

namespace XsltEmployeeWpf.ViewModels
{
    public partial class EmployeeViewModel : ObservableObject
    {
        private readonly Employee _employee;
        private readonly List<string> _monthColumns;

        public string Surname => _employee.Surname;
        public string Name => _employee.Name;
        public decimal TotalAmount => _employee.TotalAmount;

        public decimal this[string month]
        {
            get
            {
                var salary = _employee.Salaries.FirstOrDefault(s => s.Month == month);
                return salary?.Amount ?? 0;
            }
        }

        public EmployeeViewModel(Employee employee, List<string> monthColumns)
        {
            _employee = employee;
            _monthColumns = monthColumns;
        }
    }
}