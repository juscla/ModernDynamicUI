namespace ModernControls.Controls
{
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;

    /// <summary>
    /// The hamburger modern window.
    /// </summary>
    /// <seealso cref="ModernControls.Controls.ModernWindow" />
    public class HamburgerModernWindow : ModernWindow
    {
        /// <summary>
        /// The menu size property.
        /// </summary>
        public static readonly DependencyProperty MenuSizeProperty = DependencyProperty.Register(
            "MenuSize",
            typeof(double),
            typeof(HamburgerModernWindow),
            new PropertyMetadata(300.0));

        /// <summary>
        /// The content.
        /// </summary>
        private ContentPresenter content;

        /// <summary>
        /// Initializes a new instance of the <see cref="HamburgerModernWindow" /> class.
        /// </summary>
        public HamburgerModernWindow()
        {
            this.Options = new ObservableCollection<SideMenuItem>();
            this.Options.CollectionChanged += this.OptionsOnCollectionChanged;
            this.Style = (Style)Application.Current.Resources["HamburgerWindowStyle"];
        }

        /// <summary>
        /// Gets or sets the options.
        /// </summary>
        /// <value>
        /// The options.
        /// </value>
        public ObservableCollection<SideMenuItem> Options { get; set; }

        /// <summary>
        /// Gets the menu.
        /// </summary>
        /// <value>
        /// The menu.
        /// </value>
        public SideMenu Menu { get; private set; }

        /// <summary>
        /// Gets or sets the menu size.
        /// </summary>
        /// <value>
        /// The size of the menu.
        /// </value>
        public double MenuSize
        {
            get => (double)this.GetValue(MenuSizeProperty);
            set
            {
                this.SetValue(MenuSizeProperty, value);

                if (this.Menu != null)
                {
                    this.Menu.MenuSize = value;
                }
            }
        }

        /// <summary>
        /// The on menu item selected.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="e">The e.</param>
        public virtual void OnMenuItemSelected(object s, Page e)
        {
            this.content.Content = e;
        }

        /// <summary>
        /// The on apply template.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var button = (ButtonBase)this.Template.FindName("menuButton", this);
            this.Menu = (SideMenu)this.Template.FindName("Menu", this);
            this.content = (ContentPresenter)this.Template.FindName("Presenter", this);

            if (this.Menu == null)
            {
                return;
            }

            this.Menu.MenuSize = this.MenuSize;
            this.Menu.ItemsSource = this.Options;
            button.Click += (s, e) => this.Menu.Show = !this.Menu.Show;
        }

        /// <summary>
        /// The options on collection changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void OptionsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (!(e.NewItems[0] is SideMenuItem item))
            {
                return;
            }

            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    item.Click += this.OnMenuItemSelected;
                    break;

                case NotifyCollectionChangedAction.Remove:
                    item.Click -= this.OnMenuItemSelected;
                    break;
            }
        }
    }
}