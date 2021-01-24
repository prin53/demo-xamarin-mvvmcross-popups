using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Views.Fragments;
using MvvmCross.ViewModels;
using MvvmCross.WeakSubscription;
using SByteDev.Common.Extensions;

namespace MvvmCrossAlerts.Android
{
    public abstract class MvxAlertDialogFragment<TViewModel> : MvxDialogFragment<TViewModel>
        where TViewModel : class, IMvxViewModel
    {
        private readonly IDictionary<DialogButtonType, ICommand> _commands;
        private readonly IList<IDisposable> _disposables;

        private string _title;
        private string _message;

        private AlertDialog _alertDialog;

        protected virtual int LayoutResourceId => View.NoId;

        public string Title
        {
            get => _title;
            set
            {
                _title = value;

                _alertDialog.SetTitle(_title);
            }
        }

        public string Message
        {
            get => _message;
            set
            {
                _message = value;

                _alertDialog.SetMessage(_message);
            }
        }

        protected MvxAlertDialogFragment()
        {
            _commands = new Dictionary<DialogButtonType, ICommand>();
            _disposables = new List<IDisposable>();
        }

        protected MvxAlertDialogFragment(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
            /* Required constructor */
        }

        private void UpdateButton(DialogButtonType type, ICommand command)
        {
            var button = _alertDialog.GetButton((int) type);

            if (button == null)
            {
                return;
            }

            button.Enabled = command.SafeCanExecute();
        }

        private void OnCanExecuteChanged(object sender, EventArgs args)
        {
            if (!(sender is ICommand command))
            {
                return;
            }

            var type = _commands.First(item => item.Value == command).Key;

            UpdateButton(type, command);
        }

        private void OnButtonClicked(object _, DialogClickEventArgs args)
        {
            if (!_commands.TryGetValue((DialogButtonType) args.Which, out var command))
            {
                return;
            }

            command.SafeExecute();
        }

        protected void SetButton(DialogButtonType type, string title, ICommand command)
        {
            _commands[type] = command ?? throw new ArgumentNullException(nameof(command));

            _alertDialog.SetButton((int) type, title, OnButtonClicked);

            _disposables.Add(command.WeakSubscribe(OnCanExecuteChanged));
        }

        public sealed override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            return _alertDialog = new AlertDialog.Builder(Activity).Create();
        }

        public sealed override View OnCreateView(
            LayoutInflater inflater,
            ViewGroup container,
            Bundle savedInstanceState
        )
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            return LayoutResourceId != View.NoId
                ? this.BindingInflate(LayoutResourceId, null)
                : new LinearLayout(Context)
                {
                    LayoutParameters = new ViewGroup.LayoutParams(
                        ViewGroup.LayoutParams.MatchParent,
                        ViewGroup.LayoutParams.WrapContent
                    )
                };
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);

            _alertDialog.SetView(View);
        }

        public override void OnResume()
        {
            base.OnResume();

            foreach (var (dialogButtonType, command) in _commands)
            {
                UpdateButton(dialogButtonType, command);
            }
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