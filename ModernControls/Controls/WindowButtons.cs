namespace ModernControls.Controls
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// The window buttons.
    /// </summary>
    [Flags]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1602:EnumerationItemsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
    public enum WindowButtons
    {
        Maximize = 1,
        Minimize = 2,
        Close = 4,
        MaximizeMinimize = Maximize | Minimize,
        MaximizeClose = Close | Maximize,
        MinimizeClose = Close | Minimize,
        All = Maximize | MinimizeClose,
        None = 128
    }
}