using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PL.Converters
{
    class TextInTextBoxMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return values.Length == 4
                ? (string)values[0] != string.Empty && (string)values[1] != string.Empty && (string)values[2] != string.Empty && (string)values[3] != string.Empty
                : (string)values[0] != string.Empty && (string)values[1] != string.Empty && (string)values[2] != string.Empty && (string)values[3] != string.Empty && (string)values[4] != string.Empty;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
