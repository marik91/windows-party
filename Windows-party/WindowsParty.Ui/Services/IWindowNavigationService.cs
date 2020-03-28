using System.Threading.Tasks;
using System.Windows;

namespace WindowsParty.Ui.Services
{
    public interface IWindowNavigationService
    {
        Task ShowAsync<T>(object parameter = null) where T : Window;
    }
}