using Android.App;
using Android.OS;
using MvvmCross.Platforms.Android.Views;
using MvvmCrossAlerts.Core;

namespace MvvmCrossAlerts.Android
{
    [Activity]
    public class MainActivity : MvxActivity<MainViewModel>
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);
        }
    }
}