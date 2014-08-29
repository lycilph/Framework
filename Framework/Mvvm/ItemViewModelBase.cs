using ReactiveUI;

namespace Framework.Mvvm
{
    public class ItemViewModelBase<T> : ReactiveObject
    {
        public T AssociatedObject { get; private set; }

        public ItemViewModelBase(T obj)
        {
            AssociatedObject = obj;
        }
    }
}
