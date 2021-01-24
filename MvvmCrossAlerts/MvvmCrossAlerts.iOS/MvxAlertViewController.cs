using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using CoreGraphics;
using Foundation;
using MvvmCross.Platforms.Ios.Presenters;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using MvvmCross.ViewModels;
using MvvmCross.WeakSubscription;
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
        private NSLayoutConstraint _heightConstraint;

        private readonly IDictionary<UIAlertAction, ICommand> _commands;
        private readonly IList<IDisposable> _disposables;

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

        protected MvxAlertViewController()
        {
            _commands = new Dictionary<UIAlertAction, ICommand>();
            _disposables = new List<IDisposable>();
        }

        private void OnCanExecuteChanged(object sender, EventArgs args)
        {
            if (!(sender is ICommand command))
            {
                return;
            }

            var pair = _commands.FirstOrDefault(item => item.Value == command);

            var alertAction = pair.Key;

            if (alertAction == null)
            {
                return;
            }

            alertAction.Enabled = command.SafeCanExecute();
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

            _commands[alertAction] = command;

            alertAction.Enabled = command.SafeCanExecute();

            _disposables.Add(command.WeakSubscribe(OnCanExecuteChanged));

            return alertAction;
        }

        protected void LayoutIfNeeded()
        {
            if (View.Frame.Width == 0)
            {
                return;
            }

            _heightConstraint.Constant = View.SizeThatFits(new CGSize(View.Frame.Width, 0)).Height;
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

            var preferredContentSize = new CGSize(PreferredContentSize.Width, 1);

            PreferredContentSize = preferredContentSize;
            _alertController.PreferredContentSize = preferredContentSize;

            return _alertController;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            _heightConstraint = View.HeightAnchor.ConstraintEqualTo(0);

            View.AddConstraint(_heightConstraint);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (var disposable in _disposables)
                {
                    disposable?.Dispose();
                }
            }

            base.Dispose(disposing);
        }
    }
}