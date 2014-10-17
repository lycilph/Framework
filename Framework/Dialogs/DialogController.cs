﻿using System;
using System.Threading.Tasks;
using System.Windows;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using ReactiveUI;

namespace Framework.Dialogs
{
    public static class DialogController
    {
        private static MetroWindow GetMainWindow()
        {
            var window = Application.Current.MainWindow as MetroWindow;
            if (window == null)
                throw new InvalidOperationException("Main window must be a MetroWindow");
            return window;
        }

        public static Task<MessageDialogResult> ShowAsync(IReactiveObject view_model, DialogButtonOptions button_options = DialogButtonOptions.OkAndCancel)
        {
            var window = GetMainWindow();
            var dialog = new HostDialog(button_options) {DataContext = view_model};

            if (view_model is ICanOk)
            {
                var vm = view_model as ICanOk;
                vm.WhenAnyValue(x => x.CanOk)
                  .Subscribe(x => dialog.ok_button.IsEnabled = x);
            }

            if (view_model is IHaveCloseAction)
            {
                var vm = view_model as IHaveCloseAction;
                vm.CloseCallback = dialog.CloseCallback;
            }

            return window.ShowMetroDialogAsync(dialog)
                         .ContinueWith(async _ =>
                         {
                             var result = await dialog.Task;
                             await window.HideMetroDialogAsync(dialog);
                             return result;
                         }, TaskScheduler.FromCurrentSynchronizationContext()).Unwrap();
        }

        public static Task<MessageDialogResult> ShowMessage(string title, string message, MessageDialogStyle style = MessageDialogStyle.Affirmative, MetroDialogSettings settings = null)
        {
            var window = GetMainWindow();
            return window.ShowMessageAsync(title, message, style, settings);
        }

        public static Task<string> ShowInput(string title, string message)
        {
            var window = GetMainWindow();
            return window.ShowInputAsync(title, message);
        }

        public static Task<ProgressDialogController> ShowBusyDialog(string title, string message)
        {
            var window = GetMainWindow();
            return window.ShowProgressAsync(title, message, false, new MetroDialogSettings {AnimateShow = false, AnimateHide = true});
        }
    }
}
