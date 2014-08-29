namespace Framework.Docking
{
    public interface ITool : ILayoutItem
    {
        PaneLocation DefaultLocation { get; }
        double DefaultWidth { get; }
        double DefaultHeight { get; }
        bool StartAutoHidden { get; }
        bool StartDockedAsDocument { get; }
        bool IsVisible { get; set; }
    }
}
