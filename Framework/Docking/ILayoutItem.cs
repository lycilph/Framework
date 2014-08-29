using System;
using Caliburn.Micro;

namespace Framework.Docking
{
    public interface ILayoutItem : IScreen
    {
        Guid Id { get; }
        string ContentId { get; }
        bool IsSelected { get; set; }
    }
}
