namespace ModernControls.Controls
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;

    /// <summary>
    /// The number box.
    /// </summary>
    public abstract class NumberBox : UserControl
    {
        /// <summary>
        /// The value changed.
        /// </summary>
        public event EventHandler<object> ValueChanged;

        /// <summary>
        /// The numeric box.
        /// </summary>
        /// <typeparam name="T">
        /// the type of numeric box to create
        /// </typeparam>
        public abstract class NumericBox<T> : NumberBox where T : struct,
        IComparable,
        IComparable<T>,
        IConvertible,
        IEquatable<T>,
        IFormattable
        {
            /// <summary>
            /// The maximum property.
            /// </summary>
            public static readonly DependencyProperty MaximumProperty = DependencyProperty.Register(
                "Maximum",
                typeof(T),
                typeof(NumericBox<T>),
                new UIPropertyMetadata());

            /// <summary>
            /// The minimum property.
            /// </summary>
            public static readonly DependencyProperty MinimumProperty = DependencyProperty.Register(
                "Minimum",
                typeof(T),
                typeof(NumericBox<T>),
                new UIPropertyMetadata());

            /// <summary>
            /// The value property.
            /// </summary>
            public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
                "Value",
                typeof(T),
                typeof(NumericBox<T>),
                new FrameworkPropertyMetadata(
                    default(T),
                    (o, e) =>
                        {
                            var box = (NumericBox<T>)o;
                            box.ValueChanged?.Invoke(o, (T)e.NewValue);
                        },
                    CoerceValueCallback));

            /// <summary>
            /// The step property.
            /// </summary>
            public static readonly DependencyProperty StepProperty = DependencyProperty.Register(
                "Step", typeof(T), typeof(NumericBox<T>), new UIPropertyMetadata(default(T), CheckStepSize));

            /// <summary>
            /// Initializes a new instance of the <see cref="NumberBox.NumericBox{T}"/> class. 
            /// </summary>
            protected NumericBox()
            {
                this.Maximum = (T)Convert.ChangeType(100, typeof(T));
                this.Minimum = (T)Convert.ChangeType(0, typeof(T));
                this.Step = (T)Convert.ChangeType(1, typeof(T));

                this.KeyUp += (s, e) =>
                {
                    if (!this.IsKeyboardFocusWithin)
                    {
                        return;
                    }

                    switch (e.Key)
                    {
                        case Key.Up:
                            this.Increment();
                            break;

                        case Key.Down:
                            this.Decrement();
                            break;
                    }
                };

                this.Style = (Style)Application.Current.Resources["NumericBoxStyle"];
            }

            /// <summary>
            /// Gets or sets the maximum.
            /// </summary>
            public T Maximum
            {
                get => (T)this.GetValue(MaximumProperty);
                set
                {
                    this.SetValue(MaximumProperty, value);
                    this.Value = this.Value;
                }
            }

            /// <summary>
            /// Gets or sets the minimum.
            /// </summary>
            public T Minimum
            {
                get => (T)this.GetValue(MinimumProperty);
                set
                {
                    this.SetValue(MinimumProperty, value);
                    this.Value = this.Value;
                }
            }

            /// <summary>
            /// Gets or sets the value.
            /// </summary>
            public T Value
            {
                get => (T)this.GetValue(ValueProperty);
                set
                {
                    if (value.CompareTo(this.Minimum) == -1)
                    {
                        value = this.Minimum;
                    }
                    else if (value.CompareTo(this.Maximum) == 1)
                    {
                        value = this.Maximum;
                    }

                    this.SetCurrentValue(ValueProperty, value);
                }
            }

            /// <summary>
            /// Gets or sets the step.
            /// </summary>
            public T Step
            {
                get => (T)this.GetValue(StepProperty);
                set => this.SetValue(StepProperty, value);
            }

            /// <summary>
            /// Gets the minimum step size.
            /// </summary>
            public abstract T MinimumStepSize { get; }

            /// <summary>
            /// The on apply template.
            /// </summary>
            public override void OnApplyTemplate()
            {
                base.OnApplyTemplate();

                var up = this.Template.FindName("PART_UpButton", this) as RepeatButton;
                var down = this.Template.FindName("PART_DownButton", this) as RepeatButton;

                if (up == null || down == null)
                {
                    return;
                }

                this.Value = this.Value;
                up.Click += (s, e) => this.Increment();
                down.Click += (s, e) => this.Decrement();

                this.PreviewKeyUp += (s, e) =>
                    {
                        if (e.Key == Key.Enter)
                        {
                            Keyboard.ClearFocus();
                        }
                    };
            }

            /// <summary>
            /// The increment.
            /// </summary>
            protected abstract void Increment();

            /// <summary>
            /// The decrement.
            /// </summary>
            protected abstract void Decrement();

            /// <summary>
            /// The coerce value callback.
            /// </summary>
            /// <param name="o">
            /// The o.
            /// </param>
            /// <param name="value">
            /// The value.
            /// </param>
            /// <returns>
            /// The <see cref="object"/>.
            /// </returns>
            private static object CoerceValueCallback(DependencyObject o, object value)
            {
                var box = (NumericBox<T>)o;
                var temp = (T)value;

                if (temp.CompareTo(box.Minimum) == -1)
                {
                    return box.Minimum;
                }

                return temp.CompareTo(box.Maximum) == 1 ? box.Maximum : temp;
            }

            /// <summary>
            /// The check step size.
            /// </summary>
            /// <param name="o">
            /// The o.
            /// </param>
            /// <param name="e">
            /// The e.
            /// </param>
            private static void CheckStepSize(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                var box = (NumericBox<T>)o;
                if (box.Step.CompareTo(box.MinimumStepSize) == -1)
                {
                    box.Step = box.MinimumStepSize;
                }
            }
        }
    }
}