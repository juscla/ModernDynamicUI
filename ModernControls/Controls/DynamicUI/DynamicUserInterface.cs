namespace ModernControls.Controls.DynamicUi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Data;

    using ModernControls.Controls.DynamicUI;

    /// <summary>
    /// The dynamic user interface.
    /// </summary>
    /// <typeparam name="T">The type of object to generate.</typeparam>
    /// <seealso cref="ModernControls.Controls.DynamicUi.IDynamicUserInterface" />
    public sealed class DynamicUserInterface<T> : IDynamicUserInterface where T : new()
    {
        /// <summary>
        /// The properties.
        /// </summary>
        private PropertyInfo[] properties;

        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicUserInterface{T}" /> class.
        /// </summary>
        public DynamicUserInterface() : this(new T())
        {
            // no default object sent to the constructor so generate a new instance. 
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicUserInterface{T}" /> class.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public DynamicUserInterface(T instance)
        {
            this.Instance = instance;
        }

        /// <summary>
        /// The properties.
        /// </summary>
        public PropertyInfo[] Properties => this.properties
                                            ?? (this.properties = this.Instance.GetType().GetProperties().ToArray());

        /// <summary>
        /// Gets the instance.
        /// </summary>
        public T Instance { get; }

        /// <summary>
        /// The build view.
        /// </summary>
        /// <param name="hideReadOnly">The hide Read Only.</param>
        /// <returns>
        /// The <see cref="FrameworkElement" />.
        /// </returns>
        FrameworkElement IDynamicUserInterface.BuildView(bool hideReadOnly)
        {
            return this.BuildView(hideReadOnly);
        }

        /// <summary>
        /// The build view.
        /// </summary>
        /// <param name="ordered">if set to <c>true</c> [ordered].</param>
        /// <param name="hideReadOnly">The hide Read Only.</param>
        /// <returns>
        /// The <see cref="Grid" />.
        /// </returns>
        public Grid BuildView(bool ordered = true, bool hideReadOnly = true)
        {
            var grid = new Grid();
            grid.Loaded += (s, e) => ResizeLabelColumns(grid);

            while (grid.RowDefinitions.Count < this.Properties.Length)
            {
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
            }

            IEnumerable<PropertyInfo> items = this.properties;
            if (ordered)
            {
                items = this.properties.OrderBy(x => x.Name);
            }
          
            var row = 0;
            foreach (var property in items)
            {
                if (hideReadOnly && !property.CanWrite)
                {
                    continue;
                }

                var parent = new Grid
                {
                    Margin = new Thickness(5, 10, 5, 10),
                    VerticalAlignment = VerticalAlignment.Center,
                };

                parent.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                parent.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                parent.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

                FrameworkElement control;

                bool isDynamic = typeof(IDynamicUserInterface).IsAssignableFrom(property.PropertyType);
                if (isDynamic)
                {
                    var temp = (property.GetValue(this.Instance) ??
                        Activator.CreateInstance(property.PropertyType)) as IDynamicUserInterface;

                    property.SetValue(this.Instance, temp);
                    control = temp?.BuildView();
                }
                else
                {
                    control = property.GetObjectFromProperty();
                    this.HandleControl(control, property, parent);
                }

                if (control == null)
                {
                    continue;
                }

                try
                {
                    var label = new TextBlock { Text = property.Name, Padding = new Thickness(10, 0, 10, 0), FontSize = 20 };

                    parent.Children.Add(label);
                    parent.Children.Add(control);
                    grid.Children.Add(parent);

                    Grid.SetColumn(label, 0);
                    Grid.SetColumn(control, 1);
                    Grid.SetRow(parent, row++);
                }
                catch
                {
                    // ignored
                }
            }

            return grid;
        }

        /// <summary>
        /// Builds the view scrollable.
        /// </summary>
        /// <param name="ordered">if set to <c>true</c> [ordered].</param>
        /// <param name="hideReadOnly">if set to <c>true</c> [hide read only].</param>
        /// <returns>
        /// The scroller viewer with all the elements.
        /// </returns>
        public ScrollViewer BuildViewScrollable(bool ordered = true, bool hideReadOnly = true)
        {
            return new ScrollViewer
            {
                Content = this.BuildView(ordered, hideReadOnly),
                HorizontalScrollBarVisibility = ScrollBarVisibility.Auto,
                VerticalScrollBarVisibility = ScrollBarVisibility.Auto
            };
        }

        /// <summary>
        /// The resize label columns.
        /// </summary>
        /// <param name="parent">The parent.</param>
        private static void ResizeLabelColumns(Panel parent)
        {
            var width = parent.Children.OfType<Grid>().Max(x => x.ColumnDefinitions.First().ActualWidth);

            foreach (var column in parent.Children.OfType<Grid>().Select(x => x.ColumnDefinitions.First()))
            {
                if (!column.ActualWidth.Equals(width))
                {
                    column.Width = new GridLength(width);
                }
            }
        }

        /// <summary>
        /// The handle control.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="info">The info.</param>
        /// <param name="parent">The parent.</param>
        private void HandleControl(FrameworkElement control, PropertyInfo info, Grid parent)
        {
            control.VerticalAlignment = VerticalAlignment.Center;

            var binding = new Binding(info.Name)
            {
                Source = this.Instance,
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };

            switch (control)
            {
                case CheckBox checkBox:
                    checkBox.SetBinding(ToggleButton.IsCheckedProperty, binding);
                    break;

                case Label label:
                    label.SetBinding(ContentControl.ContentProperty, binding);
                    break;

                case TextBox textBox:
                    textBox.SetBinding(TextBox.TextProperty, binding);
                    break;

                case FileBrowser fb:
                    fb.SetBinding(FileBrowser.FileNameProperty, binding);
                    fb.Update(info.GetCustomAttribute<FileBrowseAttribute>());
                    break;

                case SizeControl sc:
                    sc.SetBinding(SizeControl.SizeProperty, binding);

                    var option = info.GetCustomAttributes<SizeOptionAttribute>().FirstOrDefault();

                    if (option != null)
                    {
                        sc.SetValue(SizeControl.MinimumHeightProperty, option.MinimumHeight);
                        sc.SetValue(SizeControl.MinimumWidthProperty, option.MinimumWidth);
                        sc.SetValue(SizeControl.MaximumHeightProperty, option.MaximumHeight);
                        sc.SetValue(SizeControl.MaximumWidthProperty, option.MaximumWidth);
                        sc.SetValue(SizeControl.StepProperty, option.Step);
                    }

                    break;

                case NumberBox box:

                    if (!(box.GetType().BaseType?.GetField("ValueProperty").GetValue(box) is DependencyProperty valueProperty))
                    {
                        return;
                    }

                    box.SetBinding(valueProperty, binding);

                    var bounds = info.GetCustomAttributes<NumericPropertyAttribute>().FirstOrDefault();
                    if (bounds != null)
                    {
                        try
                        {
                            var max =
                                box.GetType().BaseType?.GetField("MaximumProperty").GetValue(box) as DependencyProperty;

                            var min =
                                box.GetType().BaseType?.GetField("MinimumProperty").GetValue(box) as DependencyProperty;

                            var step =
                                box.GetType().BaseType?.GetField("StepProperty").GetValue(box) as DependencyProperty;

                            if (max == null || min == null || step == null)
                            {
                                return;
                            }

                            box.SetValue(max, Convert.ChangeType(bounds.Max, max.PropertyType));
                            box.SetValue(min, Convert.ChangeType(bounds.Min, min.PropertyType));
                            box.SetValue(step, Convert.ChangeType(bounds.Step, step.PropertyType));
                        }
                        catch
                        {
                            // ignored
                        }
                    }
                    break;

                case Selector sel:
                    foreach (var value in Enum.GetValues(info.PropertyType))
                    {
                        sel.Items.Add(value);
                    }

                    sel.MaxHeight = 100;

                    if (sel is ListBox listBox)
                    {
                        listBox.SelectionMode = SelectionMode.Multiple;

                        var block = new TextBlock
                        {
                            HorizontalAlignment = HorizontalAlignment.Center,
                            Text = Convert.ToString(info.GetValue(this.Instance)) + "\t[0x" + Convert.ToUInt32(info.GetValue(this.Instance)).ToString("X2") + "]"
                        };

                        listBox.SelectionChanged += (s, e) =>
                            {
                                info.SetValue(this.Instance, listBox.SelectedItems.Cast<int>().Aggregate(0, (x, y) => x | y));
                                block.Text = Convert.ToString(info.GetValue(this.Instance)) + "\t[0x"
                                                                                            + Convert.ToUInt32(info.GetValue(this.Instance)).ToString("X2") + "]";
                            };

                        parent.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                        parent.Children.Add(block);
                        Grid.SetRow(block, 1);
                        Grid.SetColumnSpan(block, 2);

                        return;
                    }

                    sel.SetBinding(Selector.SelectedItemProperty, binding);
                    break;
            }
        }
    }
}
