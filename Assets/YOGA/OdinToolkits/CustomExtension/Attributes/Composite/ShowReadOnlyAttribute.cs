﻿using Sirenix.OdinInspector;
using System;

namespace YOGA.Modules.OdinToolkits.Attributes.Composite
{
    /// <summary>
    /// [ShowInInspector] [EnableGUI] [ReadOnly] 组合而成。
    /// </summary>
    [IncludeMyAttributes]
    [ShowInInspector]
    [ReadOnly]
    [EnableGUI]
    public class ShowReadOnlyAttribute : Attribute { }
}