using Framework.Core;

namespace Framework.Module
{
    public interface IModule
    {
        void Create(IShell shell);
        void Initialize();
    }
}
