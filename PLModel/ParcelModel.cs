using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Model
{
    class ParcelModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string[] Statuses { get; } = Enum.GetNames(typeof(ParcelMode));
        public string[] Priorities { get; } = Enum.GetNames(typeof(Priorities));
        public string[] ParcelsWeight { get; } = Enum.GetNames(typeof(WeightCategory));
        Parcel parcel = new();
        public Parcel Parcel
        {
            get => parcel;
            set
            {
                parcel = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Parcel)));
            }
        }

    }
}
