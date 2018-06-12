namespace ModernControls.Controls.DynamicUi
{
    /// <summary>
    /// The dynamic user interface factory.
    /// </summary>
    public static class DynamicUserInterfaceFactory
    {
        /// <summary>
        /// The create.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="element">The element.</param>
        /// <returns>
        /// The <see cref="DynamicUserInterface" />.
        /// </returns>
        public static DynamicUserInterface<T> Create<T>(T element) where T : new()
        {
            return new DynamicUserInterface<T>(element);
        }
    }
}