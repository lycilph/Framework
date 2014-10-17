using System.Threading.Tasks;

namespace Framework.Docking
{
    public class ContentScreen : LayoutItemScreen, IContent
    {
        protected override void OnClose()
        {
            TryClose();
        }

        public virtual Task Finish()
        {
            return Task.FromResult(0);
        }
    }
}
