using System.Threading.Tasks;
using MahApps.Metro.Controls.Dialogs;

namespace Framework.Core.Dialogs
{
    public interface IHaveDoneTask
    {
        Task<MessageDialogResult> Done { get; }
    }
}
