using System.Threading.Tasks;

namespace Framework.Core.Dialogs
{
    public interface IHaveDoneTask
    {
        Task Done { get; }
    }
}
