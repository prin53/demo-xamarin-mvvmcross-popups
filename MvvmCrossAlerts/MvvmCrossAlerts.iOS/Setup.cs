using MvvmCross.Platforms.Ios.Core;
using MvvmCross.Platforms.Ios.Presenters;
using MvvmCrossAlerts.Core;

namespace MvvmCrossAlerts.iOS
{
    public class Setup : MvxIosSetup<App>
    {
        protected override IMvxIosViewPresenter CreateViewPresenter()
        {
            return new MvxAlertIosViewPresenter(ApplicationDelegate, Window);
        }
    }
}