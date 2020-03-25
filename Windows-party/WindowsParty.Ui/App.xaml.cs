namespace WindowsParty.Ui
{
    using System;
    using System.Windows;
    using WindowsParty.Ui.ViewModels;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder() // Use default settings
                //new HostBuilder()          // Initialize an empty HostBuilder
               .ConfigureAppConfiguration(
                    (context, builder) =>
                    {
                        // Add other configuration files...
                        builder.AddJsonFile("appsettings.local.json", optional: true);
                    })
               .ConfigureServices((context, services) => { ConfigureServices(context.Configuration, services); })
               .ConfigureLogging(
                    logging =>
                    {
                        // Add other loggers...
                    })
               .Build();

            ServiceProvider = _host.Services;
        }

        public static IServiceProvider ServiceProvider { get; private set; }

        private void ConfigureServices(IConfiguration configuration, IServiceCollection services)
        {
            // Register all ViewModels.
            services.AddSingleton<MainViewModel>();

            // Register all the Windows of the applications.
            services.AddTransient<MainWindow>();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await _host.StartAsync();

            var window = ServiceProvider.GetRequiredService<MainWindow>();
            window.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            using (_host)
            {
                await _host.StopAsync(TimeSpan.FromSeconds(5));
            }

            base.OnExit(e);
        }
    }
}