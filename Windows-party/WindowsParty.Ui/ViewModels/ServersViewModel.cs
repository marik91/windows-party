namespace WindowsParty.Ui.ViewModels
{
    using System;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using WindowsParty.Domain.Entities;
    using WindowsParty.Domain.Models;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;

    public class ServersViewModel : ViewModelBase, IViewModel
    {
        private TokenResult _tokenResult;

        public ServersViewModel()
        {
            LogOffCommand = new RelayCommand(async () => await LogOff());
        }

        public RelayCommand LogOffCommand { get; }

        public BindingList<Server> Servers { get; } = new BindingList<Server>();

        public Task ActivateAsync(object parameter)
        {
            if (parameter is TokenResult tokenResult)
            {
                _tokenResult = tokenResult;
            }

            return Task.CompletedTask;
        }

        private async Task LogOff()
        {
            throw new NotImplementedException();
        }
    }
}