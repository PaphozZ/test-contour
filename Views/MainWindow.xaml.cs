using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using XsltEmployeeWpf.Services;
using XsltEmployeeWpf.ViewModels;

namespace XsltEmployeeWpf.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContextChanged += OnDataContextChanged;

            var transformService = new XmlTransformService();
            var dataService = new XmlDataService();
            DataContext = new MainViewModel(transformService, dataService);
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue is MainViewModel oldVm)
                oldVm.MonthColumns.CollectionChanged -= OnMonthColumnsChanged;

            if (e.NewValue is MainViewModel newVm)
            {
                newVm.MonthColumns.CollectionChanged += OnMonthColumnsChanged;
                RebuildMonthColumns(newVm);
            }
        }

        private void OnMonthColumnsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (DataContext is MainViewModel vm)
                RebuildMonthColumns(vm);
        }

        private void RebuildMonthColumns(MainViewModel vm)
        {
            for (int i = EmployeeGrid.Columns.Count - 1; i >= 2; i--)
            {
                if (EmployeeGrid.Columns[i].Header.ToString() != "Итого")
                    EmployeeGrid.Columns.RemoveAt(i);
            }

            int insertIndex = 2;
            foreach (string month in vm.MonthColumns)
            {
                var column = new DataGridTextColumn
                {
                    Header = month,
                    Binding = new Binding($"[{month}]") { StringFormat = "F2" }
                };
                EmployeeGrid.Columns.Insert(insertIndex++, column);
            }
        }
    }
}