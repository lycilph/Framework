using Framework.Core.Menu.ViewModels;
using ReactiveUI;

namespace Framework.Core.Shell
{
    public interface IShell
    {
        ReactiveList<IWindowCommand> LeftShellCommands { get; }
        ReactiveList<IWindowCommand> RightShellCommands { get; }
        ReactiveList<IFlyout> ShellFlyouts { get; }
        IMenu Menu { get; }
    }
}
