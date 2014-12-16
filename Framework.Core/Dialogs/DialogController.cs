using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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

        public static async Task ShowContent(IHaveDoneTask screen)
        {
            var window = Application.Current.MainWindow as MetroWindow;
            if (window == null)
                throw new InvalidOperationException("Main window must be a MetroWindow");

            var container_field = window.GetType().GetField("metroDialogContainer", BindingFlags.Instance | BindingFlags.NonPublic);
            if (container_field == null) return;
            var container = container_field.GetValue(window) as Grid;
            if (container == null) return;

            // Find and bind the view for the model
            var view = ViewLocator.LocateForModel(screen, null, null);
            ViewModelBinder.Bind(screen, view, null);

            // Show overlay and content
            await window.ShowOverlayAsync();
            container.Children.Add(view);

            // Wait for content to signal it is done
            await screen.Done;

            // Remove view and hide overlay
            container.Children.Remove(view);
            await window.HideOverlayAsync();
        }
    }
}
