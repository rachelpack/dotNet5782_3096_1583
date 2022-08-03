using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using Model;
namespace PL.Converters
{

    class ReleaseDroneFromChargingConverter : IMultiValueConverter
    {
    
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            DroneStatuses status = (DroneStatuses)values[0];
              return status == DroneStatuses.Maintenance && !(bool)values[1] ? Visibility.Visible : Visibility.Collapsed;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

