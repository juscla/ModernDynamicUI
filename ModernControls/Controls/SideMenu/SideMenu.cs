namespace ModernControls.Controls
{
    using System;
    using System.Collections.ObjectModel;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media.Animation;

    /// <summary>
    /// The side menu.
    /// </summary>
    public class SideMenu : ItemsControl
    {
        /// <summary>
        /// The show menu property.
        /// </summary>
        public static readonly DependencyProperty ShowProperty =
            DependencyProperty.Register("Show", typeof(bool), typeof(SideMenu), new PropertyMetadata(false));

        /// <summary>
        /// The menu size property.
        /// </summary>
        public static readonly DependencyProperty MenuSizeProperty = DependencyProperty.Register(
            "MenuSize",
            typeof(double),
            typeof(SideMenu),
            new PropertyMetadata(300.0, PropertyChangedCallback));

        /// <summary>
        /// The corner radius property.
        /// </summary>
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(
            "CornerRadius",
            typeof(CornerRadius),
            typeof(SideMenu),
            new PropertyMetadata(new CornerRadius(0, 5, 5, 0)));

        /// <summary>
        /// The options property.
        /// </summary>
        public static readonly DependencyProperty OptionsProperty = DependencyProperty.Register(
            "Options",
            typeof(ObservableCollection<SideMenuItem>),
            typeof(SideMenu),
            new PropertyMetadata(new ObservableCollection<SideMenuItem>()));

        /// <summary>
        /// The open animation.
        /// </summary>
        private readonly DoubleAnimation openAnimation;

        /// <summary>
        /// The close animation.
        /// </summary>
        private readonly DoubleAnimation closeAnimation;

        /// <summary>
        /// The scroll Viewer.
        /// </summary>
        private ScrollViewer scroller;

        /// <summary>
        /// The over.
        /// </summary>
        private bool over;

        /// <summary>
        /// Initializes a new instance of the <see cref="SideMenu"/> class.
        /// </summary>
        public SideMenu()
        {
            this.openAnimation = new DoubleAnimation(0.0, this.MenuSize, TimeSpan.FromSeconds(.25), FillBehavior.Stop);
            this.closeAnimation = new DoubleAnimation(this.MenuSize, 0.0, TimeSpan.FromSeconds(.15), FillBehavior.Stop);
            this.closeAnimation.Completed += (s, e) => this.Visibility = Visibility.Collapsed;
            this.FocusVisualStyle = null;
            this.DataContext = this;
            this.ItemsSource = this.Options;

            this.Width = this.MenuSize;
            this.Visibility = Visibility.Collapsed;

            this.Style = (Style)Application.Current.Resources["SideMenuStyle"];
        }

        /// <summary>
        /// Gets or sets the menu size.
        /// </summary>
        public double MenuSize
        {
            get => (double)this.GetValue(MenuSizeProperty);
            set => this.SetValue(MenuSizeProperty, value);
        }

        /// <summary>
        /// Gets or sets the corner radius.
        /// </summary>
        public CornerRadius CornerRadius
        {
            get => (CornerRadius)this.GetValue(CornerRadiusProperty);
            set => this.SetValue(CornerRadiusProperty, value);
        }

        /// <summary>
        /// Gets a value indicating whether show menu.
        /// </summary>
        public bool Show
        {
            get => (bool)this.GetValue(ShowProperty);

            internal set
            {
                this.SetValue(ShowProperty, value);

                if (value)
                {
                    this.Visibility = Visibility.Visible;
                    this.Focus();
                    this.BeginAnimation(SideMenu.WidthProperty, this.openAnimation);
                }
                else
                {
                    this.closeAnimation.From = this.ActualWidth;
                    this.BeginAnimation(SideMenu.WidthProperty, this.closeAnimation);
                }
            }
        }

        /// <summary>
        /// Gets or sets the options.
        /// </summary>
        public ObservableCollection<SideMenuItem> Options
        {
            get => (ObservableCollection<SideMenuItem>)this.GetValue(OptionsProperty);
            set => this.SetValue(OptionsProperty, value);
        }

        /// <summary>
        /// The on apply template.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.scroller = (ScrollViewer)this.Template.FindName("scroller", this);

            this.scroller.MouseEnter += (s, ex) => this.over = true;

            this.MouseLeave += this.MenuChanged;

            this.PreviewMouseUp += this.MenuChanged;

            this.PreviewKeyUp += (s, e) =>
                {
                    if (this.Show && e.Key == Key.Escape)
                    {
                        this.over = this.Show = false;
                    }
                };
        }

        /// <summary>
        /// The menu changed.
        /// </summary>
        /// <param name="s">
        /// The s.
        /// </param>
        /// <param name="ex">
        /// The ex.
        /// </param>
        public virtual void MenuChanged(object s, EventArgs ex)
        {
            if (!this.over)
            {
                return;
            }

            this.over = this.Show = false;
        }

        /// <summary>
        /// The property changed callback.
        /// </summary>
        /// <param name="o">
        /// The o.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private static void PropertyChangedCallback(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var menu = o as SideMenu;
            if (menu == null)
            {
                return;
            }

            menu.openAnimation.To = menu.MenuSize = menu.Width = Convert.ToDouble(e.NewValue);
        }
    }
}
