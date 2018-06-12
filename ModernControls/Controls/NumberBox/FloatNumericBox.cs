namespace ModernControls.Controls
{
    /// <summary>
    /// The float numeric box.
    /// </summary>
    public class FloatNumericBox : NumberBox.NumericBox<float>
    {
        /// <summary>
        /// The minimum step size.
        /// </summary>
        public override float MinimumStepSize => .0001F;

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