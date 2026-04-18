using System.Windows;
using XsltEmployeeWpf.ViewModels;

namespace XsltEmployeeWpf.Views
{
    public partial class AddItemDialog : Window
    {
        public AddItemDialog()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is AddItemViewModel vm)
            {
                if (string.IsNullOrWhiteSpace(vm.Surname) ||
                    string.IsNullOrWhiteSpace(vm.Name) ||
                    string.IsNullOrWhiteSpace(vm.Month) ||
                    string.IsNullOrWhiteSpace(vm.Amount) || !decimal.TryParse(vm.Amount, out _))
                {
                    MessageBox.Show("Все поля обязательны для заполнения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                DialogResult = true;
                Close();
            }
        }
    }
}