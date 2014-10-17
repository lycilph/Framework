using System;

namespace Framework.Dialogs
{
    public interface IHaveCloseAction
    {
        Action CloseCallback { get; set; }
    }
}
