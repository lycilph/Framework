using Caliburn.Micro;

namespace Framework.Core.Shell
{
    public interface IWindowCommand : IHaveDisplayName
    {
        void Execute();
    }
}
