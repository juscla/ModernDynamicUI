namespace ModernControls.Controls
{
    using System;
    using System.Windows;
    using System.Windows.Input;

    /// <summary>
    /// The modern window.
    /// </summary>
    public class ModernWindow : Window
    {
        /// <summary>
        /// The sub title property.
        /// </summary>
        public static readonly DependencyProperty SubTitleProperty =
            DependencyProperty.Register("SubTitle", typeof(string), typeof(ModernWindow), new PropertyMetadata(string.Empty));

        /// <summary>
        /// The show header colors property.
        /// </summary>
        public static readonly DependencyProperty ShowHeaderColorsProperty =
            DependencyProperty.Register("ShowHeaderColors", typeof(bool), typeof(ModernWindow), new PropertyMetadata(false));

        /// <summary>
        /// The window button property.
        /// </summary>
        public static readonly DependencyProperty WindowButtonProperty =
            DependencyProperty.Register("WindowButton", typeof(WindowButtons), typeof(ModernWindow), new PropertyMetadata(WindowButtons.All));

        /// <summary>
        /// Initializes a new instance of the <see cref="ModernWindow"/> class.
        /// </summary>
        public ModernWindow()
        {
            this.Style = (Style)Application.Current.Resources["ModernWindowStyle"];

            this.StateChanged += (s, e) =>
                {
                    if (this.WindowState == WindowState.Maximized && !this.WindowButton.HasFlag(WindowButtons.Maximize))
                    {
                        this.WindowState = WindowState.Normal;
                    }
                    else if (this.WindowState == WindowState.Minimized
                             && !this.WindowButton.HasFlag(WindowButtons.Minimize))
                    {
                        this.WindowState = WindowState.Normal;
                    }
                };

            this.CommandBindings.Add(new CommandBinding(SystemCommands.CloseWindowCommand, this.OnCloseWindow));
            this.CommandBindings.Add(new CommandBinding(SystemCommands.MaximizeWindowCommand, this.OnMaximizeWindow, this.OnCanResizeWindow));
            this.CommandBindings.Add(new CommandBinding(SystemCommands.MinimizeWindowCommand, this.OnMinimizeWindow, this.OnCanMinimizeWindow));
            this.CommandBindings.Add(new CommandBinding(SystemCommands.RestoreWindowCommand, this.OnRestoreWindow, this.OnCanResizeWindow));
        }

        /// <summary>
        /// The navigated back.
        /// </summary>
        public event EventHandler<FrameworkElement> NavigatedBack;

        /// <summary>
        /// Gets or sets a value indicating whether show header colors.
        /// </summary>
        public bool ShowHeaderColors
        {
            get => (bool)this.GetValue(ShowHeaderColorsProperty);
            set => this.SetValue(ShowHeaderColorsProperty, value);
        }

        /// <summary>
        /// Gets or sets the window button.
        /// </summary>
        public WindowButtons WindowButton
        {
            get => (WindowButtons)this.GetValue(WindowButtonProperty);
            set => this.SetValue(WindowButtonProperty, value);
        }

        /// <summary>
        /// Gets or sets the sub title.
        /// </summary>
        public string SubTitle
        {
            get => (string)this.GetValue(SubTitleProperty);

            set => this.SetValue(SubTitleProperty, value);
        }

        /// <summary>
        /// Gets or sets the can go back command.
        /// </summary>
        public ICommand CanGoBackCommand
        {
            get;
            set;
        }
      
        /// <summary>
        /// The on can resize window.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void OnCanResizeWindow(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.ResizeMode == ResizeMode.CanResize || this.ResizeMode == ResizeMode.CanResizeWithGrip;
        }

        /// <summary>
        /// The on can minimize window.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void OnCanMinimizeWindow(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.ResizeMode != ResizeMode.NoResize;
        }

        private void OnCloseWindow(object target, ExecutedRoutedEventArgs e)
        {
            SystemCommands.CloseWindow(this);
        }

        private void OnMaximizeWindow(object target, ExecutedRoutedEventArgs e)
        {
            SystemCommands.MaximizeWindow(this);
        }

        private void OnMinimizeWindow(object target, ExecutedRoutedEventArgs e)
        {
            SystemCommands.MinimizeWindow(this);
        }

        private void OnRestoreWindow(object target, ExecutedRoutedEventArgs e)
        {
            SystemCommands.RestoreWindow(this);
        }
    }
}