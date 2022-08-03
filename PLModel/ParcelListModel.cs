using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;

namespace Model
{
    class ParcelListModel : INotifyPropertyChanged
    {
        ListCollectionView parcelListview;
        ParcelMode? parcelModeSelected;
        public string[] Statuses { get; } = Enum.GetNames(typeof(ParcelMode));
        public string[] Priorities { get; } = Enum.GetNames(typeof(Priorities));
        Priorities? prioritySelected;
        GroupDescription groupingSelected;
        public List<GroupDescription> GroupDescriptions { get; set; }
        public ParcelListModel()
        {
            ParcelListView = new ListCollectionView(ListsModel.ParcelsList);
            ParcelListView.Filter = ParcelFilter;
            GroupDescriptions = new List<GroupDescription>();
            foreach (System.Reflection.PropertyInfo propertyInfo in typeof(ParcelToList).GetProperties())
            {
                GroupDescriptions.Add(new PropertyGroupDescription(propertyInfo.Name));
            }
        }

        private bool ParcelFilter(object obj)
        {
            if (obj is ParcelToList parcel)
            {

                if ((!ParcelModeSelected.HasValue || parcelModeSelected == null || parcel.Status == ParcelModeSelected) && (!PrioritySelected.HasValue || prioritySelected == null || parcel.Priority == PrioritySelected))
                {
                    return true;
                }
            }
            return false;
        }

        public ListCollectionView ParcelListView
        {
            get => parcelListview;
            set
            {
                parcelListview = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ParcelListView)));
            }
        }
        public ParcelMode? ParcelModeSelected
        {
            get => parcelModeSelected;
            set
            {
                parcelModeSelected = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ParcelModeSelected)));
                ParcelListView.Refresh();
            }
        }
        public Priorities? PrioritySelected
        {
            get => prioritySelected;
            set
            {
                prioritySelected = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PrioritySelected)));
                ParcelListView.Refresh();
            }
        }

        public GroupDescription GroupingSelected
        {
            get => groupingSelected;
            set
            {
                ParcelListView.GroupDescriptions.Remove(groupingSelected);
                groupingSelected = value;
                ParcelListView.GroupDescriptions.Add(value);
                ParcelListView.Refresh();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(GroupingSelected)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

