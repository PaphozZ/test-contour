using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows;
using XsltEmployeeWpf.Services;

namespace XsltEmployeeWpf.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly IXmlTransformService _transformService;
        private readonly XmlDataService _dataService;

        [ObservableProperty]
        private ObservableCollection<EmployeeViewModel> _employees;

        [ObservableProperty]
        private decimal _grandTotal;

        [ObservableProperty]
        private ObservableCollection<string> _monthColumns;

        public MainViewModel(IXmlTransformService transformService, XmlDataService dataService)
        {
            _transformService = transformService;
            _dataService = dataService;
            ExecuteFullProcess();
        }

        [RelayCommand]
        private void Refresh()
        {

            ExecuteFullProcess();
        }

        [RelayCommand]
        private void AddItem()
        {
            var dialog = new Views.AddItemDialog();
            var dialogVM = new AddItemViewModel();
            dialog.DataContext = dialogVM;

            if (dialog.ShowDialog() == true)
            {
                _dataService.AddItemToDataFile(
                    dialogVM.Surname,
                    dialogVM.Name,
                    dialogVM.Month,
                    dialogVM.Amount);

                ExecuteFullProcess();
            }
        }

        private void ExecuteFullProcess()
        {
            try
            {
                _transformService.Transform("Resources/Data1.xml", "Resources/Transform.xslt", "Resources/Employees.xml");
                _dataService.AddTotalToEmployeesFile();
                _dataService.AddTotalToDataFile();
                LoadDataToViewModel();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadDataToViewModel()
        {
            var employees = _dataService.LoadEmployees();
            var months = _dataService.GetAllMonths().ToList();

            MonthColumns = new ObservableCollection<string>(months);
            var employeeVMs = employees.Select(e => new EmployeeViewModel(e, months)).ToList();
            Employees = new ObservableCollection<EmployeeViewModel>(employeeVMs);
            GrandTotal = employees.Sum(e => e.TotalAmount);
        }
    }
}