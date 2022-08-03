using BlApi;
using BO;
using System;
using System.Collections.Generic;
using System.Linq;

//using IDAL.DO;

namespace BO
{

    sealed partial class BL : IBL
    {
        //prop that return the instance of DalObject
        public static IBL Instance { get; } = new BL();
        private Random rand;
        private readonly List<DroneToList> dronesList = new();
        public DlApi.IDal dal;
        private readonly double PowerDroneAvailable;
        private readonly double PowerDroneEasy;
        private readonly double PowerDroneMedium;
        private readonly double PowerDroneHeavy;
        private readonly double DroneLoadingRate;
        private const int DRONESTATUSESLENGTH = 2;
        public const int MAXINITBATTARY = 20;
        public const int MININITBATTARY = 0;
        public const int FULLBATTRY = 100;

        private BL()//private c'tor
        {
            rand = new Random();
            double[] powers;
            dal = DlApi.DalFactory.GetDAL();
            lock (dal)
            {
                powers = dal.GetData();
            }

            PowerDroneAvailable = powers[0];
            PowerDroneEasy = powers[1];
            PowerDroneMedium = powers[2];
            PowerDroneHeavy = powers[3];
            DroneLoadingRate = powers[4];
            Initialize();
        }

        /// <summary>
        /// Receiving the location of all customers who received parcels
        /// </summary>
        /// <param name="exsitParcelRecived">A predicate that checks whether the customer has sent at least one package.</param>
        /// <returns></returns>
        private IEnumerable<Location> GetLocationsCustomersGotParcels(Predicate<int> exsitParcelRecived)
        {
            lock (dal)
            {
                return GetCustomerList().Where(customer => exsitParcelRecived(customer.NumberOfParcelHeReceived))
                         .Select(Customer => new Location()
                         {
                             Latitude = dal.GetCustomer(Customer.Id).Latitude,
                             Longitude = dal.GetCustomer(Customer.Id).Longitude
                         });
            }
        }

        /// <summary>
        /// Calculate how much battery the drone should send to the customer.
        /// </summary>
        /// <param name="senderId">The Id of the sender</param>
        /// <param name="receiveId">The Id of the recive</param>
        /// <param name="weight">The weight of the package</param>
        /// <returns></returns>
        private double GetBatteryDroneNeedsToSendTheParcel(int senderId, int receiveId, int weight)
        {
            double batteryConsumption;
            Location senderLocation = LocationOfSomeone(senderId);
            Location receiveLocation = LocationOfSomeone(receiveId);
            lock (dal)
            {
                batteryConsumption = dal.GetData()[weight + 1] * Distance(senderLocation, receiveLocation);
            }

            batteryConsumption += Distance(receiveLocation, NearStationWithAvailableChargeSlots(receiveLocation).Location) * PowerDroneAvailable;
            return batteryConsumption;
        }

        /// <summary>
        /// Initialize the dtone list
        /// </summary>
        private void Initialize()
        {
            IEnumerable<DO.Drone> tmpDrones;
            IEnumerable<DO.Parcel> parcels;
            IEnumerable<Location> locationOfStation;
            IEnumerable<DO.DroneCharge> droneCharges;
            IEnumerable<DO.BaseStation> baseStations;
            lock (dal)
            {
                tmpDrones = dal.GetDronesList();
                parcels = dal.GetParcelsList();
                droneCharges = dal.GetDroneCharges().Where(drone => drone.IsAvailable);
                baseStations = dal.GetStationsList();
                // create list of stations' location
                locationOfStation = baseStations.Select(Station => new Location() { Latitude = Station.Latitude, Longitude = Station.Longitude });
            }
            IEnumerable<Location> customersGotParcelLocation = GetLocationsCustomersGotParcels((int recivedparcels) => recivedparcels > 0);
            foreach (DO.Drone drone in tmpDrones)
            {
                DO.Parcel parcel = parcels.FirstOrDefault(parcel => parcel.DroneId == drone.Id && parcel.Delivered == null);
                double BatteryStatus = default;
                double tmpBatteryStatus = default;
                Location Location = default;
                DroneStatuses state = default;
                //Check if the drone is charging
                if (droneCharges.Any(d => d.DroneId == drone.Id))
                {
                    state = DroneStatuses.MAINTENANCE;
                    DO.BaseStation station;
                    lock (dal)
                    {
                        station = dal.GetStation(droneCharges.First(d => d.DroneId == drone.Id).StationId);
                    }

                    Location = new Location() { Longitude = station.Longitude, Latitude = station.Latitude };
                }
                //Check if the drone is in the middle of sending a package
                if (parcel.DroneId != 0)
                {
                    state = DroneStatuses.DALIVERY;
                    tmpBatteryStatus = GetBatteryDroneNeedsToSendTheParcel(parcel.SenderId, parcel.TargetId, (int)parcel.Weight) + 0.2;
                    if (tmpBatteryStatus >= 99)
                    {
                        lock (dal)
                        {
                            dal.DeleteParcel(parcel.Id);
                        }

                        AddParcel(parcel.SenderId, parcel.TargetId, (int)parcel.Weight, (int)parcel.Priority);
                        state = default;
                    };

                }
                else if (state == default)
                {
                    state = customersGotParcelLocation.Any() ? (DroneStatuses)rand.Next(0, DRONESTATUSESLENGTH) : DroneStatuses.MAINTENANCE;
                }
                //Battery update Location according to skimmer status
                switch (state)
                {
                    case DroneStatuses.AVAILABLE:
                        Location = customersGotParcelLocation.ElementAt(rand.Next(0, customersGotParcelLocation.Count()));
                        try
                        {
                            BatteryStatus = rand.Next((int)MinBatteryForAvailAble(Location) + 1, FULLBATTRY);
                        }
                        catch (DroneCanNotBeSent)
                        {
                            BatteryStatus = 100;
                        }
                        break;
                    case DroneStatuses.MAINTENANCE:
                        if (Location == default)
                        {
                            int indexOfTheStation = rand.Next(0, locationOfStation.Count());
                            Location = locationOfStation.ElementAt(indexOfTheStation);
                            lock (dal)
                            {
                                lock (dal)
                                    dal.SendingADroneForChargingAtABaseStation(drone.Id, baseStations.ElementAt(indexOfTheStation).Id);
                            }
                        }
                        BatteryStatus = rand.NextDouble() + rand.Next(MININITBATTARY, MAXINITBATTARY);
                        break;
                    case DroneStatuses.DALIVERY:
                        Location = LocationOfParcel(parcel.SenderId);
                        BatteryStatus = tmpBatteryStatus;
                        break;
                    default:
                        break;
                }
                dronesList.Add(new DroneToList()
                {
                    Id = drone.Id,
                    Weight = (WeightCategory)drone.MaxWeight,
                    Model = drone.Model,
                    Statuses = state,
                    Location = Location,
                    ParcelId = parcel.DroneId == 0 ? 0 : parcel.Id,
                    Battery = BatteryStatus
                });

            }
        }

    }
}

