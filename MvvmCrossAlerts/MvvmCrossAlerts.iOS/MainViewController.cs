using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using MvvmCrossAlerts.Core;

namespace MvvmCrossAlerts.iOS
{
    [MvxRootPresentation]
    public partial class MainViewController : MvxViewController<MainViewModel>
    {
        public MainViewController() : base(nameof(MainViewController), null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var set = this.CreateBindingSet<MainViewController, MainViewModel>();
            set.Bind(_userLabel).To(vm => vm.User);
            set.Bind(_loginButton).To(vm => vm.LoginCommand);
            set.Apply();
        }
    }
}