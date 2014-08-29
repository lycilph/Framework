using System.ComponentModel;

namespace Framework.Module
{
    public interface IOrderMetadata
    {
        [DefaultValue(int.MaxValue)]
        int Order { get; }
    }
}
