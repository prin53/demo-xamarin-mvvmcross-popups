using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding;
using MvvmCrossAlerts.Core;
using UIKit;

namespace MvvmCrossAlerts.iOS
{
    [MvxAlertPresentation(PreferredStyle = UIAlertControllerStyle.Alert)]
    public class LoginViewController : MvxAlertViewController<LoginViewModel>
    {
        private UILabel _validationErrorLabel;

        public string ValidationError
        {
            get => _validationErrorLabel.Text;
            set
            {
                _validationErrorLabel.Text = value;

                LayoutIfNeeded();
            }
        }

        public override void LoadView()
        {
            View = _validationErrorLabel = new UILabel
            {
                Font = UIFont.PreferredFootnote,
                TextColor = UIColor.Red,
                Lines = 0,
                LineBreakMode = UILineBreakMode.WordWrap,
                TextAlignment = UITextAlignment.Center
            };
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Title = "Login";
            Message = "Enter your username and login in the fields below:";

            var usernameTextField = AddTextField();
            usernameTextField.Placeholder = "username";

            var passwordTextField = AddTextField();
            passwordTextField.Placeholder = "password";
            passwordTextField.SecureTextEntry = true;

            AddAction(ViewModel.CancelCommand, "Cancel", UIAlertActionStyle.Cancel, false);
            AddAction(ViewModel.LoginCommand, "Login", UIAlertActionStyle.Default, true);

            var set = this.CreateBindingSet<LoginViewController, LoginViewModel>();
            set.Bind(this).For(v => v.ValidationError).ByCombining(new StringValueCombiner(), vm => vm.UsernameValidationError, vm => vm.PasswordValidationError);
            set.Bind(usernameTextField).To(vm => vm.Username);
            set.Bind(passwordTextField).To(vm => vm.Password);
            set.Bind(usernameTextField).For(v => v.BindEditingDidEnd()).To(vm => vm.ValidateUsernameCommand);
            set.Bind(passwordTextField).For(v => v.BindEditingDidEnd()).To(vm => vm.ValidatePasswordCommand);
            set.Apply();
        }
    }
}