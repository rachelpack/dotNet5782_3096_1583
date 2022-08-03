using BO;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
namespace BO
{
    partial class BL
    {

        /// <summary>
        /// A function that receives data checks their correctness and creates an instance of a new drone and add it to the list of drone
        /// </summary>
        /// <param name="id">Drone id</param>
        /// <param name="model">Drone model</param>
        /// <param name="weight">Drone max weight</param>
        /// <param name="stationNumber">station id for charging</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddDrone(int id, string model, int weight, int stationNumber)
        {
            if (dronesList.Exists(item => item.Id == id))
                throw new TheObjectIdAlreadyExist("The drone already exist in the system.");
            if (weight > 3 || weight < 0)
                throw new ThereIsWorngDetails("The weight out of range.");
            DO.BaseStation station;
            lock (dal)
                station = dal.GetStationsList().FirstOrDefault(item => item.Id == stationNumber);
            if (station.Equals(default(DO.BaseStation)))
                throw new TheObjectIDDoesNotExist("The station doesnt exist in the system.");
            DroneToList drone = new()
            {
                Battery = rand.NextDouble() + rand.Next(20, 40),
                Id = id,
                Location = new Location() { Latitude = station.Latitude, Longitude = station.Longitude },
                Model = model,
                Statuses = DroneStatuses.MAINTENANCE,
                Weight = (WeightCategory)weight
            };
            try
            {
                lock (dal)
                    dal.AddDrone(id, model, weight, stationNumber);
            }
            catch (DO.TheObjectIdAlreadyExist ex)
            {
                drone = null;
                throw new TheObjectIdAlreadyExist("The drone already exist in the system.", ex);
            }
            dronesList.Add(drone);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteDrone(int id)
        {

            if (!dronesList.Exists(d => d.Id == id))
            {
                throw new TheObjectIDDoesNotExist("The drone doesnt exist in the system.");
            }
            try
            {
                lock (dal)
                    dal.DeleteDrone(id);
                dronesList.RemoveAll(d => d.Id == id);
            }
            catch (DO.TheObjectIDDoesNotExist ex)
            {
                throw new TheObjectIDDoesNotExist("The drone does not exist in the system.", ex);
            }
        }


        /// <summary>
        /// A function that create a new drone list and over the original drone list.
        /// </summary>
        /// <returns>The new drone list</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneToList> GetDroneList()
        {
            return dronesList;
        }

        /// <summary>
        /// Update drone model
        /// </summary>
        /// <param name="id">Drone id</param>
        /// <param name="modal">The new model of the drone</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DroneUpdating(int id, string modal)
        {
            DroneToList drone = dronesList.FirstOrDefault(item => item.Id == id);
            if (drone == null)
            {
                throw new TheObjectIDDoesNotExist("The drone does not exist in the system.");
            }

            dronesList.Remove(drone);
            drone.Model = modal;
            dronesList.Add(drone);
            try
            {
                lock (dal)
                {
                    dal.ChangeDronesName(id, modal);
                }
            }
            catch (DO.TheObjectIDDoesNotExist ex)
            {
                throw new TheObjectIDDoesNotExist("The drone does not exist in the system.", ex);
            }
        }

        internal double AmountOfBatteryDroneNeedsToBeShippedForCharging(DroneToList drone)
        {
            if (drone == default(DroneToList))
            {
                throw new TheObjectIDDoesNotExist("The drone does not exist in the system.");
            }

            if (drone.Statuses != DroneStatuses.AVAILABLE)
            {
                throw new DroneCanNotBeSent("Drone can not be sent because the drone status is: " + drone.Statuses);
            }

            BaseStation station = NearStationWithAvailableChargeSlots(drone.Location);
            double batteryNeeded = BatteryConsumption(drone, Distance(drone.Location, station.Location), 0);
            if (drone.Battery < batteryNeeded)
            {
                throw new ThereIsNotEnoughBattery("There is not enough battery");
            }
            try
            {
                lock (dal)
                    dal.SendingADroneForChargingAtABaseStation(drone.Id, station.Id);
            }
            catch (DO.TheObjectIDDoesNotExist ex)
            {
                throw new TheObjectIDDoesNotExist("The drone does not exist in the system.", ex);
            }
            drone.Location = station.Location;
            drone.Statuses = DroneStatuses.MAINTENANCE;
            return batteryNeeded;
        }
        /// <summary>
        /// This function send the desired drone to charging in near station
        /// </summary>
        /// <param name="droneId">The id of desired drone</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void SendDroneToCharging(int droneId)
        {
            DroneToList d = dronesList.FirstOrDefault(d => d.Id == droneId);
            d.Battery -= AmountOfBatteryDroneNeedsToBeShippedForCharging(d);
        }


        /// <summary>
        /// A method that releases a drone from a charge if it exists and is in charge
        /// </summary>
        /// <param name="droneId">The id of desired drone</param>
        /// <param name="timeOfCharging"> The time the drone was charging</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DroneFromCharging(int droneId)
        {
            DroneToList d = dronesList.FirstOrDefault(d => d.Id == droneId);
            if (d == default(DroneToList))
            {
                throw new TheObjectIDDoesNotExist("The drone does not exist in the system.");
            }

            if (d.Statuses != DroneStatuses.MAINTENANCE)
            {
                throw new DroneCanNotBeSent("Drone can not be sent because the drone status is: " + d.Statuses);
            }
            TimeSpan? timeOfCharging;
            lock (dal)
                timeOfCharging = DateTime.Now - dal.GetDroneCharges().FirstOrDefault(drone => drone.DroneId == d.Id).DronentryTimeForCharging;
            d.Battery = Math.Min(d.Battery + DroneLoadingRate * (double)timeOfCharging?.Milliseconds, 100.0);//צריך שעות??
            d.Statuses = DroneStatuses.AVAILABLE;
            try
            {
                lock (dal)
                    dal.ReleaseDroneFromChargingAtBaseStation(droneId);
            }
            catch (DO.TheObjectIDDoesNotExist ex)
            {
                throw new TheObjectIDDoesNotExist("This drone doesnt in charging now.", ex);
            }
        }

        /// <summary>
        /// Receipt of the drone to which the parcel is associated
        /// </summary>
        /// <param name="id">parcel id</param>
        /// <returns>The drone to which the parcel is associated</returns>
        private DroneInParcel ReceiptOfTheDroneToWhichTheParcelIsAssociated(int id)
        {
            DroneToList droneToList = dronesList.Find(item => item.ParcelId == id);
            if (droneToList == null)
            {
                return null;
            }

            DroneInParcel drone = new()
            {
                Id = droneToList.Id,
                Status = droneToList.Statuses,
                Location = new Location() { Latitude = droneToList.Location.Latitude, Longitude = droneToList.Location.Longitude }
            };
            return drone;
        }

        /// <summary>
        /// The function gets a drone ID, searches for it and returns it.
        /// </summary>
        /// <param name="id">The drone id</param>
        /// <returns>The drone</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Drone GetDrone(int id)
        {
            DroneToList droneToConvert = dronesList.FirstOrDefault(item => item.Id == id);
            if (droneToConvert.Equals(default(DroneToList)))
            {
                throw new TheObjectIDDoesNotExist("The drone does not exist in the system.");
            }
            //Convert DroneToList to Drone
            Drone drone = new()
            {
                Id = droneToConvert.Id,
                Modal = droneToConvert.Model,
                Status = droneToConvert.Statuses,
                Location = droneToConvert.Location,
                Weight = droneToConvert.Weight,
                Battery = droneToConvert.Battery,
                DeliveryInTransfer = null
            };
            //Check if the drone is in delivery mode
            if (droneToConvert.Statuses == DroneStatuses.DALIVERY)
            {
                DO.Parcel parcel;
                //Looking for the package associated with the droen
                lock (dal)
                    parcel = dal.GetParcelsList().FirstOrDefault(item => item.Id == droneToConvert.ParcelId);
                //Convert parcel to IBL DeliveryInTransfer
                drone.DeliveryInTransfer = ConvertParcelToIBLDeliveryInTransfer(parcel);
            }
            return drone;
        }

        /// <summary>
        ///Find the drones that are loaded at station
        /// </summary>
        /// <param name="stationId"> The station id</param>
        /// <returns>The list Of the drones that are loaded at station</returns>
        private IEnumerable<DroneInCharge> FindDronesThatAreLoadedAtStation(int stationId)
        {
            lock (dal)
            {
                foreach (DroneCharge drone in dal.GetDroneCharges((DroneCharge d) => d.StationId == stationId && d.IsAvailable))
                {
                    yield return new DroneInCharge()
                    {
                        Id = drone.DroneId,
                        StationId = stationId,
                        DronentryTimeForCharging = drone.DronentryTimeForCharging
                    };
                }
            }
        }

        public IEnumerable<DroneInCharge> GetDroneChargingList()
        {
            lock (dal)
            {
                foreach (DroneCharge item in dal.GetDroneCharges().Where(d => d.IsAvailable))
                {
                    yield return new DroneInCharge()
                    {
                        StationId = item.StationId,
                        Id = item.DroneId,
                        DronentryTimeForCharging = item.DronentryTimeForCharging
                    };
                }
            }
        }


        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneToList> GetDroneToLists(Predicate<DroneToList> predicate)
        {
            return dronesList.Where(drone => predicate(drone));
        }

        /// <summary>
        /// Starting the simulator.
        /// </summary>
        /// <param name="id">The drone's id</param>
        /// <param name="update">The function that upDate the display</param>
        /// <param name="checkStop">The </param>
        public void StartDroneSimulator(int id, Action update, Func<bool> checkStop) => new DroneSimulator(this, id, update, checkStop);

    }
}
