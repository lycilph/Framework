using Framework.Core;

namespace Framework.Module
{
    public class ModuleBase : IModule
    {
        public virtual void Create(IShell shell) { }

        public virtual void Initialize() { }
    }
}