namespace ModernControls.Controls.DynamicUi
{
    using System;

    /// <summary>
    /// The numeric property attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class NumericPropertyAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the maximum numeric value.
        /// </summary>
        public double Max { get; set; }

        /// <summary>
        /// Gets or sets the minimum numeric value.
        /// </summary>
        public double Min { get; set; }

        /// <summary>
        /// Gets or sets the step size.
        /// </summary>
        public double Step { get; set; }
    }
}