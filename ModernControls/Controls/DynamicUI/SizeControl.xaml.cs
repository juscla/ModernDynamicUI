namespace ModernControls.Controls.DynamicUI
{
    using System;
    using System.Windows;

    /// <summary>
    /// Interaction logic for SizeControl
    /// </summary>
    public partial class SizeControl
    {
        /// <summary>
        /// The size property.
        /// </summary>
        public static readonly DependencyProperty SizeProperty = DependencyProperty.Register(
            "Size",
            typeof(Size),
            typeof(SizeControl),
            new PropertyMetadata(new Size(0, 0)));

        /// <summary>
        /// The minimum width property.
        /// </summary>
        public static readonly DependencyProperty MinimumWidthProperty = DependencyProperty.Register(
            "MinimumWidth",
            typeof(double),
            typeof(SizeControl),
            new PropertyMetadata(0D));

        /// <summary>
        /// The maximum width property.
        /// </summary>
        public static readonly DependencyProperty MaximumWidthProperty = DependencyProperty.Register(
            "MaximumWidth",
            typeof(double),
            typeof(SizeControl),
            new PropertyMetadata(2000D));

        /// <summary>
        /// The minimum height property.
        /// </summary>
        public static readonly DependencyProperty MinimumHeightProperty = DependencyProperty.Register(
            "MinimumHeight",
            typeof(double),
            typeof(SizeControl),
            new PropertyMetadata(0D));

        /// <summary>
        /// The maximum height property.
        /// </summary>
        public static readonly DependencyProperty MaximumHeightProperty = DependencyProperty.Register(
            "MaximumHeight",
            typeof(double),
            typeof(SizeControl),
            new PropertyMetadata(2000D));

        /// <summary>
        /// The step property.
        /// </summary>
        public static readonly DependencyProperty StepProperty = DependencyProperty.Register(
            "Step",
            typeof(double),
            typeof(SizeControl),
            new PropertyMetadata(1D));


        /// <summary>
        /// Initializes a new instance of the <see cref="SizeControl"/> class.
        /// </summary>
        public SizeControl()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// The value changed.
        /// </summary>
        public event EventHandler ValueChanged;

        /// <summary>
        /// Gets or sets the size.
        /// </summary>
        public Size Size
        {
            get => (Size)this.GetValue(SizeProperty);
            set => this.SetValue(SizeProperty, value);
        }

        /// <summary>
        /// Gets or sets the minimum width.
        /// </summary>
        public double MinimumWidth
        {
            get => (double)this.GetValue(MinimumWidthProperty);
            set => this.SetValue(MinimumWidthProperty, value);
        }

        /// <summary>
        /// Gets or sets the maximum width.
        /// </summary>
        public double MaximumWidth
        {
            get => (double)this.GetValue(MaximumWidthProperty);
            set => this.SetValue(MaximumWidthProperty, value);
        }

        /// <summary>
        /// Gets or sets the minimum height.
        /// </summary>
        public double MinimumHeight
        {
            get => (double)this.GetValue(MinimumHeightProperty);
            set => this.SetValue(MinimumHeightProperty, value);
        }

        /// <summary>
        /// Gets or sets the maximum height.
        /// </summary>
        public double MaximumHeight
        {
            get => (double)this.GetValue(MaximumHeightProperty);
            set => this.SetValue(MaximumHeightProperty, value);
        }

        /// <summary>
        /// Gets or sets the step.
        /// </summary>
        public double Step
        {
            get => (double)this.GetValue(StepProperty);
            set => this.SetValue(StepProperty, value);
        }

        /// <summary>
        /// The on value changed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void OnValueChanged(object sender, object e)
        {
            if (double.IsInfinity(this.Size.Width) || double.IsInfinity(this.Size.Height))
            {
                this.Size = new Size(0, 0);
            }

            this.Size = (sender as FrameworkElement).Uid == "H" ?
                new Size(this.Size.Width, Convert.ToDouble(e)) :
                new Size(Convert.ToDouble(e), this.Size.Height);

            this.ValueChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
