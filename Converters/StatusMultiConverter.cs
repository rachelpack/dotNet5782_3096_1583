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
    class StatusMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return values[0] != null && values[0] is DeliveryInTransfer deliveryInTransfer && values[1] is DroneStatuses status
                ? status == DroneStatuses.Delivery && !deliveryInTransfer.Status && !(bool)values[2] ? Visibility.Visible : Visibility.Collapsed
                : Visibility.Collapsed;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
