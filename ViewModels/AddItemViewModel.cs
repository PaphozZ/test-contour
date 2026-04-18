using CommunityToolkit.Mvvm.ComponentModel;

namespace XsltEmployeeWpf.ViewModels
{
    public partial class AddItemViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _surname;

        [ObservableProperty]
        private string _name;

        [ObservableProperty]
        private string _month;

        [ObservableProperty]
        private string _amount;
    }
}