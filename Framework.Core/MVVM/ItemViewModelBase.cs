using ReactiveUI;

namespace Framework.Core.MVVM
{
    public class ItemViewModelBase<T> : ReactiveObject
    {
        public T AssociatedObject { get; protected set; }

        public ItemViewModelBase(T obj)
        {
            AssociatedObject = obj;

            if (AssociatedObject is IReactiveObject)
            {
                var temp = AssociatedObject as IReactiveObject;
                temp.PropertyChanging += (sender, args) => this.RaisePropertyChanging(args.PropertyName);
                temp.PropertyChanged += (sender, args) => this.RaisePropertyChanged(args.PropertyName);
            }
        }
    }
}
