using MvvmCross.Binding.BindingContext;
using MvvmCrossAlerts.Core;
using UIKit;

namespace MvvmCrossAlerts.iOS
{
    [MvxAlertPresentation(PreferredStyle = UIAlertControllerStyle.Alert)]
    public class LoginViewController : MvxAlertViewController<LoginViewModel>
    {
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
            set.Bind(usernameTextField).To(vm => vm.Username);
            set.Bind(passwordTextField).To(vm => vm.Password);
            set.Apply();
        }
    }
}