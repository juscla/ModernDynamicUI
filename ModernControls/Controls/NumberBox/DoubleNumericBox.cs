namespace ModernControls.Controls
{
    /// <summary>
    /// The double numeric box.
    /// </summary>
    public class DoubleNumericBox : NumberBox.NumericBox<double>
    {
        /// <summary>
        /// The minimum step size.
        /// </summary>
        public override double MinimumStepSize => .00001D;

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
