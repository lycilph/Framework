using System.Collections.Generic;
using Caliburn.Micro.ReactiveUI;
using Framework.Core;
using Framework.Mvvm;
using Framework.Window;
using ReactiveUI;

namespace Framework.Starter
{
    public class ShellViewModelBase : ReactiveConductor<IViewModel>, IShell
    {
        protected readonly Stack<IViewModel> items = new Stack<IViewModel>();

        private readonly ReactiveList<IWindowCommand> _LeftShellCommands = new ReactiveList<IWindowCommand>();
        public ReactiveList<IWindowCommand> LeftShellCommands
        {
            get { return _LeftShellCommands; }
        }

        private readonly ReactiveList<IWindowCommand> _RightShellCommands = new ReactiveList<IWindowCommand>();
        public ReactiveList<IWindowCommand> RightShellCommands
        {
            get { return _RightShellCommands; }
        }

        private readonly ReactiveList<IFlyout> _ShellFlyouts = new ReactiveList<IFlyout>();
        public ReactiveList<IFlyout> ShellFlyouts
        {
            get { return _ShellFlyouts; }
        }

        protected void Back()
        {
            items.Pop();
            ActivateItem(items.Peek());
        }

        protected void Show(IViewModel view_model)
        {
            items.Push(view_model);
            ActivateItem(view_model);
        }
    }
}
