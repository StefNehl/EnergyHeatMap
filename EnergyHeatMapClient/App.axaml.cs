using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using EnergyHeatMap.Infrastructure;
using EnergyHeatMapClient.ViewModels;
using EnergyHeatMapClient.Views;
using Microsoft.Extensions.DependencyInjection;


namespace EnergyHeatMapClient
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddInfrastructure();

            var appSettingsSection = builder.Configuration.GetSection("SecuritySettings");
            serviceCollection.Configure<SecuritySettings>(appSettingsSection);

            var dataPathSettings = builder.Configuration.GetSection("DataPaths");
            serviceCollection.Configure<DataPathSettings>(dataPathSettings);

            var mainVm = new MainWindowViewModel();
            
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowViewModel(),
                };
            }            



            base.OnFrameworkInitializationCompleted();
        }
    }
}
