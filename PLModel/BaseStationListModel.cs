using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Model
{
    class BaseStationListModel : INotifyPropertyChanged
    {
        GroupDescription groupingSelected;
        ListCollectionView baseStationListView;
        bool isGroupByavailableSlots;

        public BaseStationListModel()
        {
           // BaseStations = stationForLists;
            groupingSelected = new PropertyGroupDescription(nameof(BaseStationForList.SeveralChargingStationsAreAvailable));
            BaseStationListView = new ListCollectionView(ListsModel.StationsList);
        }

        public ListCollectionView BaseStationListView
        {
            get => baseStationListView;
            set
            {
                baseStationListView = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BaseStationListView)));
            }
        }


        public bool IsGroupByavailableSlots
        {
            get => isGroupByavailableSlots;
            set
            {
                isGroupByavailableSlots = value;
                BaseStationListView.GroupDescriptions.Remove(groupingSelected);
                if (isGroupByavailableSlots)
                    BaseStationListView.GroupDescriptions.Add(groupingSelected);
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
