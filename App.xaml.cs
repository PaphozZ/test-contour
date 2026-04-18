using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using XsltEmployeeWpf.Services;
using XsltEmployeeWpf.ViewModels;
using XsltEmployeeWpf.Views;

namespace XsltEmployeeWpf
{
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            ServiceProvider = services.BuildServiceProvider();

            var mainWindow = new MainWindow
            {
                DataContext = ServiceProvider.GetRequiredService<MainViewModel>()
            };
            mainWindow.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IXmlTransformService, XmlTransformService>();
            services.AddSingleton<XmlDataService>();
            services.AddTransient<MainViewModel>();
        }
    }
}