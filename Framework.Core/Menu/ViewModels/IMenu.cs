using System.Collections.Generic;

namespace Framework.Core.Menu.ViewModels
{
    public interface IMenu : IEnumerable<MenuItemBase>
    {
        IEnumerable<MenuItemBase> All { get; }

        void Add(params MenuItemBase[] items);
    }
}
