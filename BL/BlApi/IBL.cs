using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
namespace BlApi
{
    public interface IBL
    {
        public void AddStation(int id, string name, double longitude, double latitudes, int ChargeSlots);
        public void AddDrone(int id, string model, int weight, int stationNumber);
        public void AddCustomer(int id, string name, int phone, double lo, double la);
        public void AddParcel( int senderId, int recieverId,int weight, int priority);
        public void DeleteDrone(int id);
        public void DeleteCustomer(int id);
        public void DeleteParcel(int id);
        public void DeleteBaseStation(int id);
        public void ReceivingAPackageForDelivery(int sId, int rId, int weight, int priority);
        IEnumerable<BaseStationForList> ChargingStationsWhithEmptyChargeSlots();
        IEnumerable<DroneToList> GetDroneList();
        IEnumerable<ParcelToList> GetParcelsNotRelated();
        IEnumerable<ParcelToList> GetParcelList();
        IEnumerable<CustomerToList> GetCustomerList();
        IEnumerable<DroneInCharge> GetDroneChargingList();
        IEnumerable<BaseStationForList> GetBaseStationList();
        IEnumerable<BaseStationForList> GetBaseStationList(Predicate<BaseStationForList> predicate);
        IEnumerable<ParcelToList> GetParcelsToList(Predicate<ParcelToList> predicate);
        Parcel GetParcel(int id);
        Customer GetCustomer(int id);
        Drone GetDrone(int id);
        BaseStation GetBaseStation(int id);
        void CollectingPackage(int id);
        int ParcelToDrone(int id);
        void SendDroneToCharging(int id);
        void DroneFromCharging(int id);
        void UpdatCustomer(int id, string name, int phone);
        void UpdateStation(int id, int numOfCa, string name);
        void DroneUpdating(int id, string model);
        void ShippingPackage(int id);
        public IEnumerable<DroneToList> GetDroneToLists(Predicate<DroneToList> predicate);


        void StartDroneSimulator(int id, Action update, Func<bool> checkStop);

        public IEnumerable<User> GetAllTheUsers();
        public User GetManager();
        public void AddUser(User user);
        public void DeleteUser(User user);
        public void UpDateUser(User oldUser, User newUser);
        public bool IsThisTheManager(User user);
        public bool DoesUserExistInTheSystem(User user);
    }
}
