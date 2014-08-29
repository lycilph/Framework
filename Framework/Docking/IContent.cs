using System.Threading.Tasks;

namespace Framework.Docking
{
    public interface IContent : ILayoutItem
    {
        Task Finish();
    }
}
