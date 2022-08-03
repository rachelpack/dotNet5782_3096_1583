using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;

namespace Model
{
    class DroneListModel : INotifyPropertyChanged
    {
        private GroupDescription groupingSelected;
        private ListCollectionView droneListView;
        private DroneStatuses? droneStatusesSelected;
        private WeightCategory? weightCategorySelected;
        public string[] Statuses { get; } = Enum.GetNames(typeof(DroneStatuses));
        public string[] Weights { get; } = Enum.GetNames(typeof(WeightCategory));
        public List<GroupDescription> GroupDescriptions { get; set; }

        public DroneListModel()
        {
            DroneListView = new ListCollectionView(ListsModel.DronesList);
            DroneListView.Filter = DroneFilter;
            GroupDescriptions = new List<GroupDescription>();
            foreach (System.Reflection.PropertyInfo propertyInfo in typeof(DroneToList).GetProperties())
            {
                GroupDescriptions.Add(new PropertyGroupDescription(propertyInfo.Name));
            }
        }

        public ListCollectionView DroneListView
        {
            get => droneListView;
            set
            {
                droneListView = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DroneListView)));
            }
        }

        public GroupDescription GroupingSelected
        {
            get => groupingSelected;
            set
            {
                DroneListView.GroupDescriptions.Remove(groupingSelected);
                groupingSelected = value;
                DroneListView.GroupDescriptions.Add(value);
                DroneListView.Refresh();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(GroupingSelected)));
            }
        }

        private bool DroneFilter(object obj)
        {
            if (obj is DroneToList drone)
            {
                return (!DroneStatusesSelected.HasValue || drone.Status == DroneStatusesSelected) && (!WeightCategorySelected.HasValue || drone.Weight == weightCategorySelected);
            }
            return false;
        }


        public DroneStatuses? DroneStatusesSelected
        {
            get => droneStatusesSelected;
            set
            {
                droneStatusesSelected = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DroneStatusesSelected)));
                DroneListView.Refresh();
            }
        }

        public WeightCategory? WeightCategorySelected
        {
            get => weightCategorySelected;
            set
            {
                weightCategorySelected = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(WeightCategorySelected)));
                DroneListView.Refresh();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}
