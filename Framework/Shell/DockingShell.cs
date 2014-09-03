using Caliburn.Micro.ReactiveUI;
using Framework.Core;
using Framework.Docking;
using Framework.MainMenu.ViewModels;
using Framework.Window;
using ReactiveUI;

namespace Framework.Shell
{
    public class DockingShell : ReactiveConductor<ILayoutItem>.Collection.OneActive, IShell
    {
        private IReactiveDerivedList<ITool> _Tools;
        public IReactiveDerivedList<ITool> Tools
        {
            get { return _Tools; }
            private set { this.RaiseAndSetIfChanged(ref _Tools, value); }
        }

        private IReactiveDerivedList<IContent> _Content;
        public IReactiveDerivedList<IContent> Content
        {
            get { return _Content; }
            private set { this.RaiseAndSetIfChanged(ref _Content, value); }
        }

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

        private IMenu _Menu;
        public IMenu Menu
        {
            get { return _Menu; }
            set { this.RaiseAndSetIfChanged(ref _Menu, value); }
        }

        public DockingShell()
        {
            Tools = Items.CreateDerivedCollection(t => t as ITool, t => t is ITool);
            Content = Items.CreateDerivedCollection(c => c as IContent, c => c is IContent);
        }
    }
}
