using System.Threading.Tasks;

namespace Framework.Docking
{
    public class ContentConductor<T> : LayoutItemConductor<T>, IContent where T : class
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
