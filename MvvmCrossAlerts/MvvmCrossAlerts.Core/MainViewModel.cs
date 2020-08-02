using System.Threading.Tasks;
using System.Windows.Input;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace MvvmCrossAlerts.Core
{
    public class MainViewModel : MvxNavigationViewModel
    {
        public string User { get; private set; }

        public ICommand LoginCommand { get; }

        public MainViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
            : base(logProvider, navigationService)
        {
            User = "Anonymous";
            LoginCommand = new MvxAsyncCommand(LoginAsync);
        }

        private async Task LoginAsync()
        {
            User = await NavigationService.Navigate<LoginViewModel, string>() ?? User;
        }
    }
}