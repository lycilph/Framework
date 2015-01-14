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
        private static MetroWindow GetMetroWindow()
        {
            var window = Application.Current.MainWindow as MetroWindow;
            if (window == null)
                throw new InvalidOperationException("Main window must be a MetroWindow");
            return window;
        }

        public static Task<MessageDialogResult> ShowMessageAsync(string title, string message, MessageDialogStyle style = MessageDialogStyle.Affirmative)
        {
            var window = GetMetroWindow();
            return window.ShowMessageAsync(title, message, style);
        }

        public static Task<MessageDialogResult> ShowAsync(IScreen view_model, DialogButtons buttons = DialogButtons.OkAndCancel)
        {
            var window = GetMetroWindow();
            var dialog = new HostDialog { ViewModel = view_model, Buttons = buttons };

            return window.ShowMetroDialogAsync(dialog)
                         .ContinueWith(async _ =>
                             {
                                 var result = await dialog.Task;
                                 await window.HideMetroDialogAsync(dialog);
                                 return result;
                             }, TaskScheduler.FromCurrentSynchronizationContext()).Unwrap();
        }

        public static async Task ShowViewModel(IHaveDoneTask screen)
        {
            var window = GetMetroWindow();
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

        public static async Task ShowView(UserControl view)
        {
            var window = GetMetroWindow();
            var container_field = window.GetType().GetField("metroDialogContainer", BindingFlags.Instance | BindingFlags.NonPublic);
            if (container_field == null) return;
            var container = container_field.GetValue(window) as Grid;
            if (container == null) return;

            // ReSharper disable once SuspiciousTypeConversion.Global
            var screen = view as IHaveDoneTask;
            if (screen == null) return;

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
