namespace ModernControls.Controls.DynamicUI
{
    using System;

    /// <summary>
    /// Size Control Options
    /// </summary>
    /// <seealso cref="System.Attribute" />
    public class SizeOptionAttribute : Attribute
    {
        /// <summary>
        /// The minimum height
        /// </summary>
        private double minimumHeight;

        /// <summary>
        /// The minimum width
        /// </summary>
        private double minimumWidth;

        /// <summary>
        /// The step
        /// </summary>
        private double step;

        /// <summary>
        /// Initializes a new instance of the <see cref="SizeOptionAttribute"/> class.
        /// </summary>
        public SizeOptionAttribute()
        {
            this.Step = 1;
        }

        /// <summary>
        /// Gets or sets the maximum height.
        /// </summary>
        /// <value>
        /// The maximum height.
        /// </value>
        public double MaximumHeight { get; set; }

        /// <summary>
        /// Gets or sets the minimum height.
        /// </summary>
        /// <value>
        /// The minimum height.
        /// </value>
        public double MinimumHeight
        {
            get => this.minimumHeight;
            set => this.minimumHeight = value < 0 ? 0 : value;
        }

        /// <summary>
        /// Gets or sets the maximum width.
        /// </summary>
        /// <value>
        /// The maximum width.
        /// </value>
        public double MaximumWidth { get; set; }

        /// <summary>
        /// Gets or sets the minimum width.
        /// </summary>
        /// <value>
        /// The minimum width.
        /// </value>
        public double MinimumWidth
        {
            get => this.minimumWidth;
            set => this.minimumWidth = value < 0 ? 0 : value;
        }

        /// <summary>
        /// Gets or sets the step.
        /// </summary>
        /// <value>
        /// The step.
        /// </value>
        public double Step
        {
            get => this.step;

            set => this.step = value < 0 ? 1 : value;
        }
    }
}