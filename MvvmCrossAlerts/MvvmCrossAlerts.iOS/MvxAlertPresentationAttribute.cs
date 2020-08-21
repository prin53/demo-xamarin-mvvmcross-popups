using MvvmCross.Presenters.Attributes;
using UIKit;

namespace MvvmCrossAlerts.iOS
{
    public class MvxAlertPresentationAttribute : MvxBasePresentationAttribute
    {
        public static UIAlertControllerStyle DefaultPreferredStyle = UIAlertControllerStyle.ActionSheet;

        public UIAlertControllerStyle PreferredStyle { get; set; } = DefaultPreferredStyle;

        public string Title { get; set; }
        public string Message { get; set; }
    }
}