using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DlApi
{
    public interface IDal
    {
        double[] GetData();
        public IEnumerable<Drone> GetDronesList();
        public IEnumerable<Parcel> GetParcelsList();
        public IEnumerable<Parcel> GetParcelsList(Predicate<Parcel> predicate);
        public IEnumerable<BaseStation> GetStationsList();
        public IEnumerable<BaseStation> GetStationsList(Predicate<BaseStation> predicate);
        public IEnumerable<Customer> GetCustomersList();
        public IEnumerable<DroneCharge> GetDroneCharges();
        public IEnumerable<DroneCharge> GetDroneCharges(Predicate<DroneCharge> predicate);
        public void AddCustomer(int id, string name, int phone, double longitude, double latitude);
        public void ChangeDronesName(int id, string modal);
        public int AddAParcel( int idGiv, int idSend, int weight, int priority);
        public void AddStation(int id, string name, double longitude, double latitude, int chargeSlots = 7);
        public void AddDrone(int id, string model, int weight, int stationID);
        public void DeleteDrone(int id);
        public void DeleteCustomer(int id);
        public void DeleteParcel(int id);
        public void DeleteBaseStation(int id);
        public void ReleaseDroneFromChargingAtBaseStation(int id);
        public void SendingADroneForChargingAtABaseStation(int droneId, int stationId);
        public void DeliveryOfAParcelToTheDestination(int id);
        public void CollectAParcelByADrone(int parcelId, int droneId);
        public void AssigningAParcelToADrone(int parcelId, int droneId);
        public void ChangeStationNameAndChargeSlots(int id, string name, int num);
        public IEnumerable<BaseStation> GetBaseStationsWithAvailableChargingStations();
        public IEnumerable<Parcel> GetTheListOfParcelsNotYetBeenAssignedToTheDrone();
        public void ChangeCustomerNameAndPhone(int id, string name, int phone);
        public Drone GetDrone(int id);
        public BaseStation GetStation(int id);
        public Customer GetCustomer(int senderId);
        public Parcel GetParcel(int parcelId);



        public IEnumerable<User> GetAllTheUsers();
        public User GetManager();
        public void AddUser(User user);
        public void DeleteUser(User user);
        public void UpDateUser(User olsUser, User newUser);




    }
}
