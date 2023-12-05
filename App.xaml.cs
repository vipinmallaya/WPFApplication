using AssessmentVipin.Core.DataExporters;
using AssessmentVipin.Core.DataExporters.Abstraction;
using AssessmentVipin.Core.Helpers;
using AssessmentVipin.Core.Helpers.Abstraction;
using AssessmentVipin.Core.Services;
using AssessmentVipin.Core.Services.Abstractions;
using AssessmentVipin.Core.ViewModels;
using AssessmentVipin.Helpers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace AssessmentVipin
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Services = ConfigureServices();
        }
        public new static App Current => (App)Application.Current;
        public IServiceProvider Services { get; }

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();
            var httpClient = new System.Net.Http.HttpClient();
            services.AddSingleton<IConfiguration, Helpers.Configuration>();

            _ = services.AddTransient<IEmployeeQueryServiceClient>((serviceCollection) =>
            {
                return new EmployeeQueryServiceClient(httpClient, serviceCollection.GetService<IConfiguration>());
            });
            services.AddTransient<IEmployeeManagementService>((serviceCollection) =>
            {
                return new EmployeeManagementService(httpClient, serviceCollection.GetService<IConfiguration>());
            });

            services.AddSingleton<IDialogService, DialogService>();

            services.AddTransient<IDataExporter, CsvDataExporter>();
            services.AddTransient<IFileProxy, FileProxy>();


            services.AddTransient<NewEmployeeViewModel>();
            services.AddTransient<MainViewModel>();

            return services.BuildServiceProvider();
        }
    }
}
