using Caliburn.Micro.ReactiveUI;
using Framework.Core.Menu.ViewModels;
using ReactiveUI;

namespace Framework.Core.Shell
{
    public class ShellBase : ReactiveScreen, IShell
    {
        private readonly ReactiveList<IWindowCommand> left_shell_commands = new ReactiveList<IWindowCommand>();
        public ReactiveList<IWindowCommand> LeftShellCommands 
        {
            get { return left_shell_commands; }
        }

        private readonly ReactiveList<IWindowCommand> right_shell_commands = new ReactiveList<IWindowCommand>();
        public ReactiveList<IWindowCommand> RightShellCommands
        {
            get { return right_shell_commands; }
        }

        private readonly ReactiveList<IFlyout> shell_flyouts = new ReactiveList<IFlyout>();

        public ReactiveList<IFlyout> ShellFlyouts
        {
            get { return shell_flyouts; }
        }

        private readonly IMenu menu = new MenuViewModel();
        public IMenu Menu
        {
            get { return menu; }
        }
    }
}
