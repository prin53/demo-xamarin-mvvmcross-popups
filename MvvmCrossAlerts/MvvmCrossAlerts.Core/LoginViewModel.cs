using System;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using SByteDev.MvvmCross.Extensions;

namespace MvvmCrossAlerts.Core
{
    public class LoginViewModel : MvxNavigationViewModelResult<string>
    {
        private IDisposable _subscription;

        public string Username { get; set; }
        public string Password { get; set; }

        public ICommand ValidateUsernameCommand { get; }
        public ICommand ValidatePasswordCommand { get; }

        public string UsernameValidationError { get; private set; }
        public string PasswordValidationError { get; private set; }

        public bool IsValid { get; private set; }

        public ICommand LoginCommand { get; }
        public ICommand CancelCommand { get; }

        public LoginViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
            : base(logProvider, navigationService)
        {
            ValidateUsernameCommand = new MvxCommand(ValidateUsername);
            ValidatePasswordCommand = new MvxCommand(ValidatePassword);

            LoginCommand = new MvxAsyncCommand(LoginAsync, () => IsValid);
            CancelCommand = new MvxAsyncCommand(CancelAsync);
        }

        private void UpdateValidationStatus()
        {
            IsValid = GetUsernameValidationError() == null && GetPasswordValidationError() == null;
        }

        private string GetUsernameValidationError()
        {
            return string.IsNullOrWhiteSpace(Username) ? "Invalid Username" : null;
        }

        private string GetPasswordValidationError()
        {
            return string.IsNullOrWhiteSpace(Password) ? "Invalid Password" : null;
        }

        private void ValidateUsername()
        {
            UsernameValidationError = GetUsernameValidationError();
            UpdateValidationStatus();
        }

        private void ValidatePassword()
        {
            PasswordValidationError = GetPasswordValidationError();
            UpdateValidationStatus();
        }

        // ReSharper disable once UnusedMember.Local
        private void OnUsernameChanged()
        {
            if (GetUsernameValidationError() == null)
            {
                UsernameValidationError = null;
            }

            UpdateValidationStatus();
        }

        // ReSharper disable once UnusedMember.Local
        private void OnPasswordChanged()
        {
            if (GetPasswordValidationError() == null)
            {
                PasswordValidationError = null;
            }

            UpdateValidationStatus();
        }

        private Task LoginAsync()
        {
            return NavigationService.Close(this, Username);
        }

        private Task CancelAsync()
        {
            return NavigationService.Close(this, null);
        }

        public override void ViewCreated()
        {
            base.ViewCreated();

            _subscription = LoginCommand.RelayOn(this, () => IsValid);
        }

        public override void ViewDestroy(bool viewFinishing = true)
        {
            base.ViewDestroy(viewFinishing);

            if (viewFinishing)
            {
                _subscription?.Dispose();
            }
        }
    }
}