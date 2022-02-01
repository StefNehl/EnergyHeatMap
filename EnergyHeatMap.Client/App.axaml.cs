using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using EnergyHeatMap.Contracts.Repositories;
using EnergyHeatMap.Infrastructure;
using EnergyHeatMap.Infrastructure.Queries;
using EnergyHeatMap.Infrastructure.Services;
using EnergyHeatMap.Client.ViewModels;
using EnergyHeatMap.Client.Views;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MediatR;
using System;
using System.Reflection;

namespace EnergyHeatMap.Client
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public IHost IoC { get; private set; } 

        public override void OnFrameworkInitializationCompleted()
        {
            IoC = Host.CreateDefaultBuilder().ConfigureServices(services=>
            {
                ConfigureServices(services);
            }).Start();           


            var mainVm = new MainWindowViewModel();
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    DataContext = mainVm,
                };
            }            

            base.OnFrameworkInitializationCompleted();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddInfrastructure();
            services.AddLogging();
            services.AddSingleton<ICountryEnergyStateServices, CountryEnergyStateService>();
            services.AddSingleton<ICryptoCoinStateService, CryptoCoinStateService>();
            services.AddSingleton<IUsersService, UsersService>();

            var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            var appSettingsSection = config.GetSection("SecuritySettings");
            services.Configure<SecuritySettings>(Options.DefaultName, appSettingsSection);

            var dataPathSettings = config.GetSection("DataPaths");
            services.Configure<DataPathSettings>(dataPathSettings);
        }
    }
}
