﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Markup;

namespace PushBindingInStyleDemo.Converters
{
    public class PushBindingStringFormatConverter : MarkupExtension, IValueConverter
    {
        public string StringFormat { get; set; }

        public PushBindingStringFormatConverter() { }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) => value;

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) => value != null ? string.Format(StringFormat, value.ToString()) : value;

        public override object ProvideValue(IServiceProvider serviceProvider) => this;
    }
}
