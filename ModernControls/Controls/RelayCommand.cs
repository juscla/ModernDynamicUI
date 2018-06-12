namespace ModernControls.Controls
{
    using System;
    using System.Windows.Input;

    /// <summary>
    /// The relay command.
    /// </summary>
    public class RelayCommand : ICommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand"/> class.
        /// </summary>
        /// <param name="action">
        /// The action to perform
        /// </param>
        public RelayCommand(Action action)
        {
            this.ToExecute = action;
        }

        /// <summary>
        /// The can execute changed.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Gets the to execute.
        /// </summary>
        public Action ToExecute
        {
            get;
            private set;
        }

        /// <summary>
        /// The can execute.
        /// </summary>
        /// <param name="parameter">
        /// The parameter.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// The execute.
        /// </summary>
        /// <param name="parameter">
        /// The parameter.
        /// </param>
        public void Execute(object parameter)
        {
            this.ToExecute();
        }
    }
}