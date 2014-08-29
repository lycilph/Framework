﻿using Caliburn.Micro;
using MahApps.Metro.Controls;

namespace Framework.Window
{
    public interface IFlyout : IHaveDisplayName
    {
        bool IsOpen { get; set; }
        bool IsPinned { get; set; }
        Position Position { get; set; }
        void Toggle();
        void Show();
        void Hide();
    }
}
