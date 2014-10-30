using Caliburn.Micro;

namespace Framework.Core
{
    public interface IWindowCommand : IHaveDisplayName
    {
        void Execute();
    }
}
