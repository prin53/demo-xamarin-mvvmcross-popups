using Android.App;
using MvvmCross.Platforms.Android.Core;
using MvvmCross.Platforms.Android.Views;
using MvvmCrossAlerts.Core;

namespace MvvmCrossAlerts.Android
{
    [Activity(MainLauncher = true, NoHistory = true)]
    public class SplashScreenActivity : MvxSplashScreenActivity<MvxAndroidSetup<App>, App>
    {
        public SplashScreenActivity() : base(Resource.Layout.SplashScreen)
        {
            /* Required constructor */
        }
    }
}