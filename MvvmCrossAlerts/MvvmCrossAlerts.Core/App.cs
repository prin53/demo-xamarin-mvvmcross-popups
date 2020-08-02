using MvvmCross.ViewModels;

namespace MvvmCrossAlerts.Core
{
    public sealed class App : MvxApplication
    {
        public override void Initialize()
        {
            base.Initialize();

            RegisterAppStart<MainViewModel>();
        }
    }
}