namespace ModernControls.Controls
{
    using System.Windows;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;

    /// <summary>
    /// The modern toggle button.
    /// </summary>
    public class ModernToggleButton : ToggleButton
    {
        /// <summary>
        /// The deselected string property.
        /// </summary>
        public static readonly DependencyProperty DeselectedStringProperty = DependencyProperty.Register(
            "DeselectedString",
            typeof(string),
            typeof(ModernToggleButton),
            new PropertyMetadata("Off"));

        /// <summary>
        /// The selected string property.
        /// </summary>
        public static readonly DependencyProperty SelectedStringProperty = DependencyProperty.Register(
            "SelectedString",
            typeof(string),
            typeof(ModernToggleButton),
            new PropertyMetadata("On"));

        /// <summary>
        /// The selected brush property.
        /// </summary>
        public static readonly DependencyProperty SelectedBrushProperty = DependencyProperty.Register(
            "SelectedBrush",
            typeof(SolidColorBrush),
            typeof(ModernToggleButton),
            new PropertyMetadata(Brushes.Green));

        /// <summary>
        /// The deselected brush property.
        /// </summary>
        public static readonly DependencyProperty DeselectedBrushProperty = DependencyProperty.Register(
            "DeselectedBrush",
            typeof(SolidColorBrush),
            typeof(ModernToggleButton),
            new PropertyMetadata(Brushes.Transparent));

        public ModernToggleButton()
        {
            this.Style = (Style)Application.Current.Resources[this.GetType().Name + "Style"];
        }

        /// <summary>
        /// Gets or sets the selected string.
        /// </summary>
        public string SelectedString
        {
            get => (string)GetValue(SelectedStringProperty);
            set => this.SetValue(SelectedStringProperty, value);
        }

        /// <summary>
        /// Gets or sets the deselected string.
        /// </summary>
        public string DeselectedString
        {
            get => (string)this.GetValue(DeselectedStringProperty);
            set => this.SetValue(DeselectedStringProperty, value);
        }

        /// <summary>
        /// Gets or sets the deselected brush.
        /// </summary>
        public SolidColorBrush DeselectedBrush
        {
            get => (SolidColorBrush)this.GetValue(DeselectedBrushProperty);
            set => this.SetValue(DeselectedBrushProperty, value);
        }

        /// <summary>
        /// Gets or sets the selected brush.
        /// </summary>
        public SolidColorBrush SelectedBrush
        {
            get => (SolidColorBrush)this.GetValue(SelectedBrushProperty);
            set => this.SetValue(SelectedBrushProperty, value);
        }
    }
}
