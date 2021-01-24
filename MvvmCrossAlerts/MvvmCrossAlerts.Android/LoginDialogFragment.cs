using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using MvvmCrossAlerts.Core;
using SByteDev.Common.Extensions;

namespace MvvmCrossAlerts.Android
{
    [MvxDialogFragmentPresentation]
    [Register(nameof(LoginDialogFragment))]
    public class LoginDialogFragment : MvxAlertDialogFragment<LoginViewModel>
    {
        private EditText _usernameEditText;
        private EditText _passwordEditText;

        protected override int LayoutResourceId => Resource.Layout.Login;

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            _usernameEditText = view.FindViewById<EditText>(Resource.Id.UsernameEditText);
            _passwordEditText = view.FindViewById<EditText>(Resource.Id.PasswordEditText);

            _usernameEditText!.FocusChange += OnUsernameEditTextOnFocusChange;
            _passwordEditText!.FocusChange += OnPasswordEditTextOnFocusChange;

            Title = "Login";
            Message = "Enter your username and login in the fields below:";

            SetButton(DialogButtonType.Positive, "Login", ViewModel.LoginCommand);
            SetButton(DialogButtonType.Negative, "Cancel", ViewModel.CancelCommand);
        }

        private void OnUsernameEditTextOnFocusChange(object _, View.FocusChangeEventArgs args)
        {
            if (args.HasFocus)
            {
                return;
            }

            ViewModel.ValidateUsernameCommand.SafeExecute();
        }

        private void OnPasswordEditTextOnFocusChange(object _, View.FocusChangeEventArgs args)
        {
            if (args.HasFocus)
            {
                return;
            }

            ViewModel.ValidatePasswordCommand.SafeExecute();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                var usernameEditText = _usernameEditText;
                if (usernameEditText != null)
                {
                    usernameEditText.FocusChange -= OnUsernameEditTextOnFocusChange;
                }

                var passwordEditText = _passwordEditText;
                if (passwordEditText != null)
                {
                    passwordEditText.FocusChange -= OnPasswordEditTextOnFocusChange;
                }
            }

            base.Dispose(disposing);
        }
    }
}