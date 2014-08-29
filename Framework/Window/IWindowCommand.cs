using Caliburn.Micro;

namespace Framework.Window
{
    public interface IWindowCommand : IHaveDisplayName
    {
        void Execute();
    }
}
