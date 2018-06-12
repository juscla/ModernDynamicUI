namespace ModernControls.Controls.DynamicUi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;

    using ModernControls.Controls.DynamicUI;

    /// <summary>
    /// The dynamic user interface defines.
    /// </summary>
    public static class DynamicUserInterfaceDefines
    {
        /// <summary>
        /// The controls.
        /// </summary>
        public static readonly Dictionary<Type, Type> Controls = new Dictionary<Type, Type>
                                                                     {
                                                                         {
                                                                             typeof(bool),
                                                                             typeof(CheckBox)
                                                                         },
                                                                         {
                                                                             typeof(string),
                                                                             typeof(TextBox)
                                                                         },
                                                                         {
                                                                             typeof(Enum),
                                                                             typeof(Selector)
                                                                         },
                                                                         {
                                                                             typeof(double),
                                                                             typeof(DoubleNumericBox)
                                                                         },
                                                                         {
                                                                             typeof(int),
                                                                             typeof(IntegerNumericBox)
                                                                         },
                                                                         {
                                                                             typeof(float),
                                                                             typeof(FloatNumericBox)
                                                                         },
                                                                         {
                                                                             typeof(decimal),
                                                                             typeof(DecimalNumericBox)
                                                                         },
                                                                         {
                                                                             typeof(IDynamicUserInterface),
                                                                             typeof(IDynamicUserInterface)
                                                                         },
                                                                         {
                                                                             typeof(Size),
                                                                             typeof(SizeControl)
                                                                         }
                                                                     };

        /// <summary>
        /// The get type from property.
        /// </summary>
        /// <param name="property">
        /// The property.
        /// </param>
        /// <returns>
        /// The <see cref="Type"/>.
        /// </returns>
        public static Type GetTypeFromProperty(this PropertyInfo property)
        {
            if (!property.CanWrite)
            {
                return typeof(Label);
            }

            Type type = null;

            if (property.PropertyType.IsEnum)
            {
                type = Controls[typeof(Enum)];
            }
            else if (property.PropertyType.IsGenericType)
            {
                type = property.PropertyType.GetGenericTypeDefinition();
            }
            else
            {
                type = Controls[property.PropertyType];
            }

            if (type == typeof(Selector))
            {
                type = property.PropertyType.GetCustomAttributes()
                                  .Any(x => x.GetType() == typeof(FlagsAttribute)) ? typeof(ListBox) : typeof(ComboBox);
            }
            else if (type == typeof(TextBox))
            {
                type = property.GetCustomAttributes<FileBrowseAttribute>().Any() ? typeof(FileBrowser) : typeof(TextBox);
            }

            return type;
        }

        /// <summary>
        /// The get object from property.
        /// </summary>
        /// <param name="property">
        /// The property.
        /// </param>
        /// <returns>
        /// The <see cref="FrameworkElement"/>.
        /// </returns>
        public static FrameworkElement GetObjectFromProperty(this PropertyInfo property)
        {
            FrameworkElement response = null;
            try
            {
                var type = property.GetTypeFromProperty();
                if (type.IsGenericType)
                {
                    response = Activator.CreateInstance(type, type.GetGenericArguments()) as FrameworkElement;
                }
                else
                {
                    response = Activator.CreateInstance(type) as FrameworkElement;
                }
            }
            catch
            {
                // ignored. 
            }

            return response;
        }
    }
}