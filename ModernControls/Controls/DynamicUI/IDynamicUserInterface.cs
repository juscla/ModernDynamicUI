namespace ModernControls.Controls.DynamicUi
{
    using System.Windows;

    /// <summary>
    /// The DynamicUserInterface interface.
    /// </summary>
    public interface IDynamicUserInterface
    {
        /// <summary>
        /// The build view.
        /// </summary>
        /// <param name="hideReadonly">The hide Readonly.</param>
        /// <returns>
        /// The <see cref="FrameworkElement" />.
        /// </returns>
        FrameworkElement BuildView(bool hideReadonly = true);
    }
}