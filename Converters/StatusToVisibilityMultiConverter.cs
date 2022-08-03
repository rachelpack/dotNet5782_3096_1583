using Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace PL.Converters
{
    class StatusToVisibilityMultiConverter : IMultiValueConverter
    {
        //public object Convert(object values, Type targetType[], object parameter, CultureInfo culture)
        //{
        //    DroneStatuses status = (DroneStatuses)value;
        //    return status == DroneStatuses.Available ? Visibility.Visible : Visibility.Collapsed;
        //}

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            DroneStatuses status = (DroneStatuses)values[0];
            return values.Length == 3 ?
              status == DroneStatuses.Available && !(bool)values[1] && (double)values[2] < 100 ? Visibility.Visible : Visibility.Collapsed :
              status == DroneStatuses.Available && !(bool)values[1] ? Visibility.Visible : Visibility.Collapsed;
        }

        //public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        //{
        //    throw new NotImplementedException();
        //}

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
