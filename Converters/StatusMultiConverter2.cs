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
    class StatusMultiConverter2 : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] == null)
            {
                return Visibility.Collapsed;
            }

            bool IsCollect = (values[0] as DeliveryInTransfer).Status;
            DroneStatuses status = (DroneStatuses)values[1];
            return status == DroneStatuses.Delivery && IsCollect && !(bool)values[2] ? Visibility.Visible : Visibility.Collapsed;
        }


        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
