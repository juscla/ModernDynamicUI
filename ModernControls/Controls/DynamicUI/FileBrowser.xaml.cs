namespace ModernControls.Controls.DynamicUI
{
    using System;
    using System.Windows;

    using Microsoft.Win32;

    /// <summary>
    /// Interaction logic for FileBrowser
    /// </summary>
    public partial class FileBrowser
    {
        /// <summary>
        /// The extension property.
        /// </summary>
        public static readonly DependencyProperty ExtensionProperty =
            DependencyProperty.Register("Extension", typeof(string), typeof(FileBrowser), new PropertyMetadata(null));

        /// <summary>
        /// The file name property.
        /// </summary>
        public static readonly DependencyProperty FileNameProperty = DependencyProperty.Register(
            "FileName",
            typeof(string),
            typeof(FileBrowser),
            new PropertyMetadata(default(string)));

        /// <summary>
        /// The is save file property.
        /// </summary>
        public static readonly DependencyProperty IsSaveFileProperty =
            DependencyProperty.Register("IsSaveFile", typeof(bool), typeof(FileBrowser), new PropertyMetadata(false));

        /// <summary>
        /// Initializes a new instance of the <see cref="FileBrowser"/> class.
        /// </summary>
        public FileBrowser()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// The file name changed.
        /// </summary>
        public event EventHandler<string> FileNameChanged;

        /// <summary>
        /// Gets or sets the file name.
        /// </summary>
        public string FileName
        {
            get => (string)this.GetValue(FileNameProperty);
            set
            {
                this.SetValue(FileNameProperty, value);
                this.FileNameChanged?.Invoke(this, value);
            }
        }

        /// <summary>
        /// Gets or sets the extension.
        /// </summary>
        public string Extension
        {
            get => (string)this.GetValue(ExtensionProperty);
            set => this.SetValue(ExtensionProperty, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether is save file.
        /// </summary>
        public bool IsSaveFile
        {
            get => (bool)this.GetValue(IsSaveFileProperty);
            set => this.SetValue(IsSaveFileProperty, value);
        }

        /// <summary>
        /// The update.
        /// </summary>
        /// <param name="options">
        /// The options.
        /// </param>
        public void Update(FileBrowseAttribute options)
        {
            this.IsSaveFile = options.Save;
            this.Extension = options.Extension;
        }

        /// <summary>
        /// The browse clicked.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void BrowseClicked(object sender, RoutedEventArgs e)
        {
            FileDialog fo;

            if (this.IsSaveFile)
            {
                fo = new SaveFileDialog();
            }
            else
            {
                fo = new OpenFileDialog();
            }

            fo.Filter = this.Extension;

            if (fo.ShowDialog() == true)
            {
                this.FileName = fo.FileName;
            }
        }
    }
}
