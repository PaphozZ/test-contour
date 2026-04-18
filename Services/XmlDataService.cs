using System.Globalization;
using System.Xml;
using XsltEmployeeWpf.Models;

namespace XsltEmployeeWpf.Services
{
    public class XmlDataService
    {
        private const string DataFilePath = "Resources/Data1.xml";
        private const string EmployeesFilePath = "Resources/Employees.xml";

        public void AddTotalToDataFile()
        {
            var doc = new XmlDocument();
            doc.Load(DataFilePath);

            decimal total = 0;
            foreach (XmlElement item in doc.SelectNodes("//item"))
            {
                string amountStr = item.GetAttribute("amount");
                total += ParseAmount(amountStr);
            }

            XmlElement pay = doc.DocumentElement;
            pay?.SetAttribute("total", total.ToString(CultureInfo.InvariantCulture));

            doc.Save(DataFilePath);
        }

        public void AddTotalToEmployeesFile()
        {
            var doc = new XmlDocument();
            doc.Load(EmployeesFilePath);

            foreach (XmlElement employee in doc.SelectNodes("//Employee"))
            {
                decimal sum = 0;
                foreach (XmlElement salary in employee.SelectNodes("salary"))
                {
                    string amountStr = salary.GetAttribute("amount");
                    sum += ParseAmount(amountStr);
                }
                employee.SetAttribute("totalAmount", sum.ToString(CultureInfo.InvariantCulture));
            }

            doc.Save(EmployeesFilePath);
        }

        public List<Employee> LoadEmployees()
        {
            var employees = new List<Employee>();
            var doc = new XmlDocument();
            doc.Load(EmployeesFilePath);

            foreach (XmlElement empNode in doc.SelectNodes("//Employee"))
            {
                var emp = new Employee
                {
                    Surname = empNode.GetAttribute("surname"),
                    Name = empNode.GetAttribute("name"),
                    TotalAmount = decimal.Parse(empNode.GetAttribute("totalAmount"), CultureInfo.InvariantCulture)
                };

                foreach (XmlElement salNode in empNode.SelectNodes("salary"))
                {
                    emp.Salaries.Add(new SalaryRecord
                    {
                        Month = salNode.GetAttribute("mount"),
                        Amount = ParseAmount(salNode.GetAttribute("amount"))
                    });
                }

                employees.Add(emp);
            }

            return employees;
        }

        public void AddItemToDataFile(string surname, string name, string month, string amount)
        {
            var doc = new XmlDocument();
            doc.Load(DataFilePath);

            XmlElement newItem = doc.CreateElement("item");
            newItem.SetAttribute("mount", month);
            newItem.SetAttribute("amount", amount);
            newItem.SetAttribute("surname", surname);
            newItem.SetAttribute("name", name);

            doc.DocumentElement?.AppendChild(newItem);
            doc.Save(DataFilePath);
        }

        private decimal ParseAmount(string amountStr)
        {
            if (string.IsNullOrEmpty(amountStr)) return 0;
            string normalized = amountStr.Replace(',', '.');
            return decimal.Parse(normalized, CultureInfo.InvariantCulture);
        }

        public IEnumerable<string> GetAllMonths()
        {
            var doc = new XmlDocument();
            doc.Load(EmployeesFilePath);
            var months = new HashSet<string>();
            foreach (XmlElement salary in doc.SelectNodes("//salary"))
                months.Add(salary.GetAttribute("mount"));
            return months.OrderBy(m => m);
        }
    }
}