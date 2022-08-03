using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    static public class ListsModel /*: Singleton.Singleton<ListsModel>*/
    {
        private static readonly BlApi.IBL bl;
        static public ObservableCollection<DroneToList> DronesList { get; set; }
        static public ObservableCollection<BaseStationForList> StationsList { get; set; }
        static public ObservableCollection<CustomerToList> CustomersList { get; set; }
        static public ObservableCollection<ParcelToList> ParcelsList { get; set; }
       static private int customerId;
        public static int CustomerId
        {
            get => customerId;
            set
            {
                customerId = value;
                RefreshParcels(customerId);
            }
        }
       
        static ListsModel()
        {
            bl = BlApi.BlFactory.GetBl();
            DronesList = DroneToList.ConvertBOToPO(bl.GetDroneList());
            StationsList = BaseStationForList.ConvertBoToPo(bl.GetBaseStationList());
            CustomersList = CustomerToList.ConvertBOToPO(bl.GetCustomerList());
            ParcelsList = ParcelToList.ConvertBoToPo(bl.GetParcelList());
            //CustomerId != 0
            //    ? Kind == "sender"
            //        ? ParcelToList.ConvertBoToPo(bl.GetParcelsToList(p=>p.SenderId == CustomerId))
            //        : ParcelToList.ConvertBoToPo(bl.GetParcelsToList(p => p.ReceivesId == CustomerId))
            //    : ParcelToList.ConvertBoToPo(bl.GetParcelList());
        }


        public static void RefreshDrones()
        {
            DronesList.Clear();
            foreach (var drone in DroneToList.ConvertBOToPO(bl.GetDroneList()))
            {
                DronesList.Add(drone);
            }
        }

        public static void RefreshStations()
        {
            StationsList.Clear();
            foreach (var station in BaseStationForList.ConvertBoToPo(bl.GetBaseStationList()))
            {
                StationsList.Add(station);
            }
        }
        public static void RefreshCustomers()
        {
            CustomersList.Clear();
            foreach (var customer in CustomerToList.ConvertBOToPO(bl.GetCustomerList()))
            {
                CustomersList.Add(customer);
            }
        }
        public static void RefreshParcels()
        {
            ParcelsList.Clear();
            foreach (var parcel in ParcelToList.ConvertBoToPo(bl.GetParcelList()))
            {
                ParcelsList.Add(parcel);
            }
        }
        public static void RefreshParcels(int id)
        {
            ParcelsList.Clear();
            ObservableCollection<ParcelToList> parcels = ParcelToList.ConvertBoToPo(bl.GetParcelsToList(p => p.SenderId == id || p.ReceivesId == id));
            foreach (ParcelToList parcel in parcels)
            {
                ParcelsList.Add(parcel);
            }
        }

        //static public void RefreshDronesCharging()
        //{
        //    dronesCharging.Clear();
        //    foreach (var item in ConvertFunctions.BODroneInChargingTOPO(bl.GetDronesInCharging()))
        //        dronesCharging.Add(item);
        //}
    }
}
