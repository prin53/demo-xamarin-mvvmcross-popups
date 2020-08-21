using System;
using System.Threading.Tasks;
using MvvmCross.Platforms.Ios.Presenters;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using MvvmCross.Presenters;
using MvvmCross.ViewModels;
using UIKit;

namespace MvvmCrossAlerts.iOS
{
    public sealed class MvxAlertIosViewPresenter : MvxIosViewPresenter
    {
        public MvxAlertIosViewPresenter(IUIApplicationDelegate applicationDelegate, UIWindow window)
            : base(applicationDelegate, window)
        {
            AttributeTypesToActionsDictionary.Register<MvxAlertPresentationAttribute>(
                ShowAlertView,
                CloseAlertView
            );
        }

        private Task<bool> ShowAlertView(
            Type viewType,
            MvxAlertPresentationAttribute attribute,
            MvxViewModelRequest request
        )
        {
            var viewController = this.CreateViewControllerFor(request);

            if (!(viewController is IMvxAlertViewController alertViewController))
            {
                return Task.FromResult(false);
            }

            var alertController = alertViewController.Wrap(attribute, this);

            var modalPresentationAttribute = new MvxModalPresentationAttribute
            {
                WrapInNavigationController = false
            };

            return ShowModalViewController(alertController, modalPresentationAttribute, request);
        }

        private Task<bool> CloseAlertView(IMvxViewModel viewModel, MvxAlertPresentationAttribute attribute)
        {
            return CloseModalViewController(viewModel, new MvxModalPresentationAttribute());
        }
    }
}