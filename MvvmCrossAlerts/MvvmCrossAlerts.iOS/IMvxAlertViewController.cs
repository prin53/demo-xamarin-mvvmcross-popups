using MvvmCross.Platforms.Ios.Presenters;
using UIKit;

namespace MvvmCrossAlerts.iOS
{
    public interface IMvxAlertViewController
    {
        UIAlertController Wrap(MvxAlertPresentationAttribute attribute, MvxIosViewPresenter viewPresenter);
    }
}