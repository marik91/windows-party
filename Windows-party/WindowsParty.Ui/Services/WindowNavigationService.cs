namespace WindowsParty.Ui.Services
{
    using System;
    using System.Threading.Tasks;
    using System.Windows;
    using WindowsParty.Ui.ViewModels;
    using Microsoft.Extensions.DependencyInjection;

    internal class WindowNavigationService : IWindowNavigationService
    {
        private readonly IServiceProvider _serviceProvider;

        public WindowNavigationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task ShowAsync<T>(object parameter = null) where T : Window
        {
            var window = await GetAndActivateWindowAsync<T>(parameter);
            window.Show();
        }

        private async Task<Window> GetAndActivateWindowAsync<T>(object parameter = null) where T : Window
        {
            var window = _serviceProvider.GetRequiredService(typeof(T)) as Window;

            if (window?.DataContext is IViewModel viewModel)
            {
                await viewModel.ActivateAsync(parameter);
            }

            return window;
        }
    }
}