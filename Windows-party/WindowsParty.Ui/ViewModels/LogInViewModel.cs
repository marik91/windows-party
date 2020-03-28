namespace WindowsParty.Ui.ViewModels
{
    using System.Threading.Tasks;
    using WindowsParty.Domain.Contracts;
    using WindowsParty.Domain.Models;
    using WindowsParty.Ui.Services;
    using WindowsParty.Ui.Views;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;

    public class LogInViewModel : ViewModelBase, IViewModel
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IWindowNavigationService _windowNavigationService;
        private string _password;
        private string _userName;

        public LogInViewModel(
            IAuthenticationService authenticationService,
            IWindowNavigationService windowNavigationService)
        {
            _authenticationService = authenticationService;
            _windowNavigationService = windowNavigationService;
            LogInCommand = new RelayCommand(async () => await LogIn(), CanExecuteLogin);
        }

        public RelayCommand LogInCommand { get; }

        public string Password
        {
            get => _password;
            set
            {
                Set(ref _password, value);
                LogInCommand.RaiseCanExecuteChanged();
            }
        }

        public string UserName
        {
            get => _userName;
            set
            {
                Set(ref _userName, value);
                LogInCommand.RaiseCanExecuteChanged();
            }
        }

        public Task ActivateAsync(object parameter)
        {
            return Task.CompletedTask;
        }

        private bool CanExecuteLogin()
        {
            return !(string.IsNullOrWhiteSpace(_userName) && string.IsNullOrWhiteSpace(_password));
        }

        private async Task LogIn()
        {
            UiServices.SetBusyState();
            var result = await _authenticationService.LogInAsync(
                new Credentials
                {
                    Password = _password,
                    Username = _userName
                });

            if (result.IsSuccess)
            {
                await _windowNavigationService.ShowAsync<ServersView>(result);
            }
        }
    }
}