namespace ModernControls.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Media;

    /// <summary>
    /// The modern dialog message.
    /// </summary>
    public class ModernDialogMessage : ModernWindow
    {
        /// <summary>
        /// The message property.
        /// </summary>
        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register("Message", typeof(string), typeof(ModernDialogMessage), new PropertyMetadata(string.Empty));

        /// <summary>
        /// The options.
        /// </summary>
        private readonly StackPanel options = new StackPanel { Orientation = Orientation.Horizontal };

        /// <summary>
        /// Prevents a default instance of the <see cref="ModernDialogMessage"/> class from being created.
        /// </summary>
        private ModernDialogMessage()
        {
            this.Name = "root";
            this.DataContext = this;

            this.SizeToContent = SizeToContent.WidthAndHeight;
            this.WindowButton = WindowButtons.None;
            this.ShowHeaderColors = true;
            this.ResizeMode = ResizeMode.NoResize;
            var content = new StackPanel();
            this.Content = content;


            var msg = new TextBlock
            {
                Padding = new Thickness(50),
                FontSize = 40,
                TextWrapping = TextWrapping.Wrap,
                MaxWidth = 300
            };

            BindingOperations.SetBinding(msg, TextBlock.TextProperty, new Binding("Message"));

            content.Children.Add(msg);
            content.Children.Add(this.options);
        }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        public string Message
        {
            get => (string)this.GetValue(MessageProperty);
            set => this.SetValue(MessageProperty, value);
        }

        /// <summary>
        /// The show.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="caption">
        /// The caption.
        /// </param>
        /// <param name="buttons">
        /// The buttons.
        /// </param>
        /// <returns>
        /// The <see cref="MessageBoxResult"/>.
        /// </returns>
        public static MessageBoxResult Show(
            string message,
            string caption = null,
            MessageBoxButton buttons = MessageBoxButton.OKCancel)
        {
            var win = new ModernDialogMessage { Message = message, Title = caption, Topmost = true };

            try
            {
                win.Owner = Application.Current.MainWindow;
                win.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            }
            catch
            {
                // ignored
            }

            var result = MessageBoxResult.OK;

            var options = new List<Button>();

            switch (buttons)
            {
                case MessageBoxButton.OK:
                    options.Add(new Button
                    {
                        Content = "OK",
                        Uid = ((int)MessageBoxResult.OK).ToString(),
                        Margin = new Thickness(10, 0, 10, 5),
                        HorizontalAlignment = HorizontalAlignment.Stretch
                    });
                    break;

                case MessageBoxButton.OKCancel:
                    options.Add(new Button
                    {
                        Content = "OK",
                        Uid = ((int)MessageBoxResult.OK).ToString(),
                        Margin = new Thickness(10, 0, 10, 5),
                        HorizontalAlignment = HorizontalAlignment.Stretch
                    });
                    options.Add(new Button
                    {
                        Content = "Cancel",
                        Uid = ((int)MessageBoxResult.Cancel).ToString(),
                        Margin = new Thickness(10, 0, 10, 5),
                        HorizontalAlignment = HorizontalAlignment.Stretch
                    });
                    break;

                case MessageBoxButton.YesNoCancel:
                    options.Add(new Button
                    {
                        Content = "Yes",
                        Uid = ((int)MessageBoxResult.Yes).ToString(),
                        Margin = new Thickness(10, 0, 10, 5),
                        HorizontalAlignment = HorizontalAlignment.Stretch
                    });
                    options.Add(new Button
                    {
                        Content = "No",
                        Uid = ((int)MessageBoxResult.No).ToString(),
                        Margin = new Thickness(10, 0, 10, 5),
                        HorizontalAlignment = HorizontalAlignment.Stretch
                    });
                    options.Add(new Button
                    {
                        Content = "Cancel",
                        Uid = ((int)MessageBoxResult.Cancel).ToString(),
                        Margin = new Thickness(10, 0, 10, 5),
                        HorizontalAlignment = HorizontalAlignment.Stretch
                    });
                    break;

                case MessageBoxButton.YesNo:
                    options.Add(new Button
                    {
                        Content = "Yes",
                        Uid = ((int)MessageBoxResult.Yes).ToString(),
                        Margin = new Thickness(10, 0, 10, 5),
                        HorizontalAlignment = HorizontalAlignment.Stretch
                    });
                    options.Add(new Button
                    {
                        Content = "No",
                        Uid = ((int)MessageBoxResult.No).ToString(),
                        Margin = new Thickness(10, 0, 10, 5),
                        HorizontalAlignment = HorizontalAlignment.Stretch
                    });
                    break;
            }

            var width = 205.0 / options.Count;

            foreach (var b in options)
            {
                b.Width = width;
                b.Style = (Style)Application.Current.Resources["ModernButton"];
                b.BorderBrush = Brushes.Black;
                b.BorderThickness = new Thickness(2);

                b.Click += (s, e) =>
                {
                    if (!(s is Button button) || button.Uid == string.Empty)
                    {
                        return;
                    }

                    result = (MessageBoxResult)Enum.Parse(typeof(MessageBoxResult), button.Uid);
                    win.Close();
                };

                win.options.Children.Add(b);
            }

            win.ShowDialog();
            return result;
        }
    }
}
