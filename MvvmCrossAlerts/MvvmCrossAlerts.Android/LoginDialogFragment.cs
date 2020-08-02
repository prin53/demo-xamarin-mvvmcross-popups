using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using MvvmCrossAlerts.Core;
using SByteDev.Common.Extensions;
using AlertDialog = Android.Support.V7.App.AlertDialog;

namespace MvvmCrossAlerts.Android
{
    [MvxDialogFragmentPresentation]
    [Register(nameof(LoginDialogFragment))]
    public class LoginDialogFragment : MvxDialogFragment<LoginViewModel>
    {
        private void OnPositiveButtonClick(object sender, DialogClickEventArgs e)
        {
            ViewModel.LoginCommand.SafeExecute();
        }

        private void OnNegativeButtonClick(object sender, DialogClickEventArgs e)
        {
            ViewModel.CancelCommand.SafeExecute();
        }

        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            return new AlertDialog.Builder(Activity)
                .SetTitle("Login")
                .SetPositiveButton("Login", OnPositiveButtonClick)
                .SetNegativeButton("Cancel", OnNegativeButtonClick)
                .Create();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            return this.BindingInflate(Resource.Layout.Login, null);
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);

            if (Dialog is AlertDialog alertDialog)
            {
                alertDialog.SetView(View);
            }
        }
    }
}