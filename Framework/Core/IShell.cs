using Framework.MainMenu.ViewModels;
using Framework.Window;
using ReactiveUI;

namespace Framework.Core
{
    public interface IShell
    {
        ReactiveList<IWindowCommand> LeftShellCommands { get; }
        ReactiveList<IWindowCommand> RightShellCommands { get; }
        ReactiveList<IFlyout> ShellFlyouts { get; }
        IMenu Menu { get; }
    }
}
