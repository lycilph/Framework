using ReactiveUI;

namespace Framework.Docking
{
    public abstract class ToolScreen : LayoutItemScreen, ITool
    {
        public virtual PaneLocation DefaultLocation { get { return PaneLocation.Bottom; }  }
        public virtual double DefaultWidth { get { return 0; } }
        public virtual double DefaultHeight { get { return 0; } }
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
