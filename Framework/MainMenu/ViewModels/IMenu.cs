using System.Collections.Generic;

namespace Framework.MainMenu.ViewModels
{
    public interface IMenu : IEnumerable<MenuItemBase>
    {
        IEnumerable<MenuItemBase> All { get; }

        void Add(params MenuItemBase[] items);
    }
}
