namespace ModernControls.Controls
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;

    /// <summary>
    /// The side menu item.
    /// </summary>
    public class SideMenuItem : MenuItem
    {
        /// <summary>
        /// The highlight color brush property.
        /// </summary>
        public static readonly DependencyProperty HighlightColorProperty = DependencyProperty.Register(
            "HighlightColor",
            typeof(SolidColorBrush),
            typeof(SideMenuItem),
            new FrameworkPropertyMetadata(Brushes.Aqua, FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// The item color property.
        /// </summary>
        public static readonly DependencyProperty ItemColorProperty = DependencyProperty.Register(
            "ItemColor",
            typeof(SolidColorBrush),
            typeof(SideMenuItem),
            new FrameworkPropertyMetadata(Brushes.White, FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// The page property.
        /// </summary>
        public static readonly DependencyProperty PageProperty = DependencyProperty.Register(
            "Page",
            typeof(Page),
            typeof(SideMenuItem),
            new PropertyMetadata(null));

        /// <summary>
        /// The default brush.
        /// </summary>
        private SolidColorBrush defaultBrush;

        /// <summary>
        /// Initializes a new instance of the <see cref="SideMenuItem"/> class.
        /// </summary>
        public SideMenuItem()
        {
            this.Style = (Style)Application.Current.Resources["SideMenuItemStyle"];
        }

        /// <summary>
        /// The click.
        /// </summary>
        public new event EventHandler<Page> Click;

        /// <summary>
        /// Gets or sets the highlight color.
        /// </summary>
        public SolidColorBrush HighlightColor
        {
            get => (SolidColorBrush)this.GetValue(HighlightColorProperty);
            set => this.SetValue(HighlightColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the item color.
        /// </summary>
        public SolidColorBrush ItemColor
        {
            get => (SolidColorBrush)this.GetValue(ItemColorProperty);
            set => this.SetValue(ItemColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the page.
        /// </summary>
        public Page Page
        {
            get => (Page)this.GetValue(PageProperty);
            set => this.SetValue(PageProperty, value);
        }

        /// <summary>
        /// The on initialized.
        /// </summary>
        /// <param name="e">
        /// The ex.
        /// </param>
        protected override void OnInitialized(EventArgs e)
        {
            this.DataContext = this;
            base.OnInitialized(e);
        }

        /// <summary>
        /// The on mouse enter.
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        protected override void OnMouseEnter(MouseEventArgs e)
        {
            this.defaultBrush = this.ItemColor;
            this.ItemColor = this.HighlightColor;
            base.OnMouseEnter(e);
        }

        /// <summary>
        /// The on mouse leave.
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        protected override void OnMouseLeave(MouseEventArgs e)
        {
            this.ItemColor = this.defaultBrush;
            base.OnMouseLeave(e);
        }

        /// <summary>
        /// The on click.
        /// </summary>
        protected override void OnClick()
        {
            this.ItemColor = this.defaultBrush;
            this.Click?.Invoke(this, this.Page);
        }
    }
}