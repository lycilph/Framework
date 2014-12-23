using Caliburn.Micro;

namespace Framework.Core.Shell
{
    public interface IWindowCommand : IHaveDisplayName
    {
        bool IsEnabled { get; set; }
        bool IsVisible { get; set; }
        void Execute();
    }
}
