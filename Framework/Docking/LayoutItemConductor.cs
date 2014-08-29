using System;
using System.Windows.Input;
using Caliburn.Micro.ReactiveUI;
using Framework.Utils;
using ReactiveUI;

namespace Framework.Docking
{
    public class LayoutItemConductor<T> : ReactiveConductor<T>, ILayoutItem where T : class
    {
        private readonly Guid id = Guid.NewGuid();

        public Guid Id
        {
            get { return id; }
        }

        public string ContentId
        {
            get { return id.ToString(); }
        }

        private bool _IsSelected;
        public bool IsSelected
        {
            get { return _IsSelected; }
            set { this.RaiseAndSetIfChanged(ref _IsSelected, value); }
        }

        public ICommand CloseCommand { get; private set; }

        public LayoutItemConductor()
        {
            CloseCommand = new RelayCommand(_ => OnClose());
        }

        protected virtual void OnClose() { }
    }
}
