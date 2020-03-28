namespace WindowsParty.Ui.ViewModels
{
    using System;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;

    public class LoginViewModel : ViewModelBase
    {
        private string _password;
        private string _userName;

        public LoginViewModel()
        {
            LogInCommand = new RelayCommand(LogIn, CanExecuteLogin);
        }

        public RelayCommand LogInCommand { get; }

        public string Password
        {
            get => _password;
            set => Set(ref _password, value);
        }

        public string UserName
        {
            get => _userName;
            set => Set(ref _userName, value);
        }

        private bool CanExecuteLogin()
        {
            return !(string.IsNullOrWhiteSpace(_userName) && string.IsNullOrWhiteSpace(_password));
        }

        private void LogIn()
        {
            throw new NotImplementedException();
        }
    }
}