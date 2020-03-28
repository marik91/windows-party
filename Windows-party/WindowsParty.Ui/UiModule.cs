namespace WindowsParty.Ui
{
    using Microsoft.Extensions.DependencyInjection;
    using WindowsParty.Ui.ViewModels;

    public static class UiModule
    {
        public static void AddUiModule(this IServiceCollection services)
        {
            // Register all ViewModels.
            services.AddSingleton<MainViewModel>();

            // Register all the Windows of the applications.
            services.AddTransient<MainWindow>();
        }
    }
}