namespace WindowsParty.Ui
{
    using WindowsParty.Ui.Services;
    using WindowsParty.Ui.ViewModels;
    using WindowsParty.Ui.Views;
    using Microsoft.Extensions.DependencyInjection;

    public static class UiModule
    {
        public static void AddUiModule(this IServiceCollection services)
        {
            // Register all ViewModels.
            services.AddSingleton<LogInViewModel>();
            services.AddTransient<ServersViewModel>();

            // Register all the Windows of the applications.
            services.AddTransient<LogInView>();
            services.AddTransient<ServersView>();

            services.AddTransient<IWindowNavigationService, WindowNavigationService>();
        }
    }
}