namespace WindowsParty.Ui.ViewModels
{
    using Microsoft.Extensions.DependencyInjection;

    public class ViewModelLocator
    {
        public MainViewModel MainViewModel => App.ServiceProvider.GetRequiredService<MainViewModel>();
    }
}