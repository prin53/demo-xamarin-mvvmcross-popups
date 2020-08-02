using Android.App;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCrossAlerts.Core;

namespace MvvmCrossAlerts.Android
{
    [Activity(MainLauncher = true, NoHistory = true)]
    public class SplashScreenActivity : MvxSplashScreenAppCompatActivity<MvxAppCompatSetup<App>, App>
    {
        public SplashScreenActivity() : base(Resource.Layout.SplashScreen)
        {
            /* Required constructor */
        }
    }
}