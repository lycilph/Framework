using System;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace Framework.Core.Dialogs
{
    public static class DialogController
    {
        public static Task<MessageDialogResult> ShowAsync(IScreen view_model, DialogButtons buttons = DialogButtons.OkAndCancel)
        {
            var window = Application.Current.MainWindow as MetroWindow;
            if (window == null)
                throw new InvalidOperationException("Main window must be a MetroWindow");

            var dialog = new HostDialog { ViewModel = view_model, Buttons = buttons };

            return window.ShowMetroDialogAsync(dialog)
                         .ContinueWith(async _ =>
                             {
                                 var result = await dialog.Task;
                                 await window.HideMetroDialogAsync(dialog);
                                 return result;
                             }, TaskScheduler.FromCurrentSynchronizationContext()).Unwrap();
        }
    }
}
