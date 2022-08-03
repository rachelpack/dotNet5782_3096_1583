using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Model
{
    class BaseStationModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        BaseStation baseStation;
        public BaseStation BaseStation
        {
            get => baseStation;
            set
            {
                baseStation = value;
                DronesInChargeListView = new ListCollectionView(baseStation.DronesInCharge);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BaseStation)));
            }
        }

        public BaseStationModel(BaseStation station)
        {
            BaseStation = station;
        }


        ListCollectionView dronesInChargeView;
        public ListCollectionView DronesInChargeListView
        {
            get => dronesInChargeView;
            set
            {
                dronesInChargeView = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DronesInChargeListView)));
            }
        }



    }
}
