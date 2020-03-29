namespace WindowsParty.Ui.Services
{
    using System.Windows.Controls;

    public interface IPageNavigationService
    {
        void NavigateTo<T>(object parameter = null) where T : Page;
    }
}