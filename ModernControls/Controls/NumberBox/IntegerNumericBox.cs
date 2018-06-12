namespace ModernControls.Controls
{
    /// <summary>
    /// The integer numeric box.
    /// </summary>
    public class IntegerNumericBox : NumberBox.NumericBox<int>
    {
        /// <summary>
        /// The minimum step size.
        /// </summary>
        public override int MinimumStepSize => 1;

        /// <summary>
        /// The increment.
        /// </summary>
        protected override void Increment()
        {
            this.Value = this.Value + this.Step;
        }

        /// <summary>
        /// The decrement.
        /// </summary>
        protected override void Decrement()
        {
            this.Value = this.Value - this.Step;
        }
    }
}