namespace ModernControls.Controls
{
    /// <summary>
    /// The long numeric box.
    /// </summary>
    public class LongNumericBox : NumberBox.NumericBox<long>
    {
        /// <summary>
        /// The minimum step size.
        /// </summary>
        public override long MinimumStepSize => 1L;

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