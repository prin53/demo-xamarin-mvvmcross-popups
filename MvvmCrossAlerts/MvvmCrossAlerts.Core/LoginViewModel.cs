using System.Threading.Tasks;
using System.Windows.Input;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace MvvmCrossAlerts.Core
{
    public class LoginViewModel : MvxNavigationViewModelResult<string>
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public ICommand LoginCommand { get; }
        public ICommand CancelCommand { get; }

        public LoginViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
            : base(logProvider, navigationService)
        {
            LoginCommand = new MvxAsyncCommand(LoginAsync);
            CancelCommand = new MvxAsyncCommand(CancelAsync);
        }

        private Task LoginAsync()
        {
            CloseCompletionSource?.TrySetResult(Username);

            return NavigationService.Close(this);
        }

        private Task CancelAsync()
        {
            CloseCompletionSource?.TrySetCanceled();

            return NavigationService.Close(this);
        }
    }
}