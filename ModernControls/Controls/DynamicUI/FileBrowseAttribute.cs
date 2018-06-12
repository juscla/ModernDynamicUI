namespace ModernControls.Controls
{
    using System;

    /// <summary>
    /// The file browse attribute.
    /// </summary>
    public class FileBrowseAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileBrowseAttribute" /> class.
        /// </summary>
        /// <param name="isSaveFile">To show a Save or Open file Dialog.</param>
        public FileBrowseAttribute(bool isSaveFile)
        {
            this.Save = isSaveFile;
            this.Extension = "All|*.*";
        }

        /// <summary>
        /// Gets a value indicating whether is save dialog expected.
        /// </summary>
        /// <value>
        ///   <c>true</c> if save; otherwise, <c>false</c>.
        /// </value>
        public bool Save { get;  }

        /// <summary>
        /// Gets or sets the extension.
        /// </summary>
        public string Extension { get; set; }
    }
}