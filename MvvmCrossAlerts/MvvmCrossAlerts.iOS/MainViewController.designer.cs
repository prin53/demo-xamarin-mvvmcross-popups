// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace MvvmCrossAlerts.iOS
{
    [Register("MainViewController")]
    partial class MainViewController
    {
        [Outlet]
        UIKit.UILabel _userLabel { get; set; }

        [Outlet]
        UIKit.UIButton _loginButton { get; set; }

        void ReleaseDesignerOutlets()
        {
            if (_userLabel != null)
            {
                _userLabel.Dispose();
                _userLabel = null;
            }

            if (_loginButton != null)
            {
                _loginButton.Dispose();
                _loginButton = null;
            }
        }
    }
}
