﻿using System.Windows;
using System.Windows.Controls;

namespace NewEva.StyleHelper
{
    /// <summary>
    /// Displays an icon image/path, according to the specified <see cref="Type"/> name.
    /// </summary>
    /// <remarks>
    /// All icons sourced from Material Design Icons Font - https://materialdesignicons.com/ - in accordance of 
    /// https://github.com/Templarian/MaterialDesign/blob/master/LICENSE.
    /// </remarks>
    public class Icon : Control
    {
        public static readonly DependencyProperty TypeProperty = DependencyProperty.Register(
            nameof(Type), typeof(IconType), typeof(Icon), new PropertyMetadata(default(IconType)));

        /// <summary>
        /// Gets or sets the name of icon being displayed.
        /// </summary>
        public IconType Type
        {
            get { return (IconType)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }
    }
}
