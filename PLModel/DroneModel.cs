using System;
using System.ComponentModel;

namespace Model
{
   public class DroneModel : INotifyPropertyChanged
    {
        public DroneModel()
        {
            drone = new();
            drone.DeliveryInTransfer = null;
        }
        private Drone drone;

        public Drone Drone
        {
            get => drone;
            set { drone = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Drone))); }
        }


        private int stationId;
        public int StationId
        {
            get => stationId;
            set { stationId = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(StationId))); }
        }

        private int battery;
        public int Battery
        {
            get => battery;
            set { battery = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Battery))); }
        }

        public string[] Weights { get; } = Enum.GetNames(typeof(WeightCategory));

        bool auto;
        public bool Auto
        {
            get => auto;
            set
            {
                auto = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Auto)));
            }
        }

        //bool doesDroneWork;
        //public bool DoesDroneWork
        //{
        //    get => doesDroneWork;
        //    set
        //    {
        //        doesDroneWork = value;
        //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DoesDroneWork)));
        //    }
        //}
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
