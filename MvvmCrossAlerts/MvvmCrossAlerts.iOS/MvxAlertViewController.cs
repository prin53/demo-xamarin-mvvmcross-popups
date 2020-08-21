using System.Windows.Input;
using CoreGraphics;
using Foundation;
using MvvmCross.Platforms.Ios.Presenters;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using MvvmCross.ViewModels;
using SByteDev.Common.Extensions;
using UIKit;

namespace MvvmCrossAlerts.iOS
{
    public abstract class MvxAlertViewController<TViewModel> : MvxViewController<TViewModel>, IMvxAlertViewController
        where TViewModel : class, IMvxViewModel
    {
        private const string ContentViewControllerKey = "contentViewController";

        private MvxIosViewPresenter _viewPresenter;
        private UIAlertController _alertController;

        public override string Title
        {
            get => _alertController.Title;
            set => _alertController.Title = value;
        }

        public string Message
        {
            get => _alertController.Message;
            set => _alertController.Message = value;
        }

        protected UITextField AddTextField()
        {
            if (_alertController.PreferredStyle != UIAlertControllerStyle.Alert)
            {
                return null;
            }

            var textField = default(UITextField);

            _alertController.AddTextField(view => textField = view);

            return textField;
        }

        protected UIAlertAction AddAction(ICommand command, string title, UIAlertActionStyle style, bool isPreferred)
        {
            var alertAction = UIAlertAction.Create(title, style, async _ =>
            {
                command.SafeExecute();

                await _viewPresenter.CloseModalViewController(_alertController, new MvxModalPresentationAttribute())
                    .ConfigureAwait(false);
            });

            _alertController.AddAction(alertAction);

            if (isPreferred)
            {
                _alertController.PreferredAction = alertAction;
            }

            return alertAction;
        }

        public UIAlertController Wrap(MvxAlertPresentationAttribute attribute, MvxIosViewPresenter viewPresenter)
        {
            _viewPresenter = viewPresenter;

            _alertController = UIAlertController.Create(
                attribute.Title,
                attribute.Message,
                attribute.PreferredStyle
            );

            _alertController.SetValueForKey(this, new NSString(ContentViewControllerKey));

            var preferredContentSize = new CGSize(1, 1);

            PreferredContentSize = preferredContentSize;
            _alertController.PreferredContentSize = preferredContentSize;

            return _alertController;
        }
    }
}