using Microsoft.UI.Xaml.Data;
using System;

namespace LoquatDocs.Converter {
  public class DateTimeOffsetToDateTimeConverter : IValueConverter {
    public object Convert(object value, Type targetType, object parameter, string language) {
      return new DateTimeOffset((DateTime) value);
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language) {
      return ((DateTimeOffset)value).DateTime;
    }
  }
}
