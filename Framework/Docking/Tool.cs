using ReactiveUI;

namespace Framework.Docking
{
    public abstract class Tool : LayoutItemScreen, ITool
    {
        public abstract PaneLocation DefaultLocation { get;  }
        public abstract double DefaultWidth { get; }
        public abstract double DefaultHeight { get; }
        public virtual bool StartAutoHidden { get { return false; } }
        public virtual bool StartDockedAsDocument { get { return false; } }

        private bool _IsVisible = true;
        public bool IsVisible
        {
            get { return _IsVisible; }
            set { this.RaiseAndSetIfChanged(ref _IsVisible, value); }
        }

        protected override void OnClose()
        {
            IsVisible = false;
        }
    }
}
