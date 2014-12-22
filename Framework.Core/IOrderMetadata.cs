﻿using System;
using System.ComponentModel;

namespace Framework.Core
{
    public interface IOrderMetadata
    {
        [DefaultValue(Int32.MaxValue)]
        int Order { get; }
    }
}
