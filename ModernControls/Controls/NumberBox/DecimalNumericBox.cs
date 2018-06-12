namespace ModernControls.Controls
{
    /// <summary>
    /// The decimal numeric box.
    /// </summary>
    public class DecimalNumericBox : NumberBox.NumericBox<decimal>
    {
        /// <summary>
        /// The minimum step size.
        /// </summary>
        public override decimal MinimumStepSize => .0001m;

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