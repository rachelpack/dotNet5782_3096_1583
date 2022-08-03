using Dal;
using DO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    partial class DalObject
    {

        /// <summary>
        /// A function that receives data checks their correctness and creates an instance of a new drone and add it to the list of drone
        /// </summary>
        /// <param name="id">The id of the new drone</param>
        /// <param name="model">The model of the new drone</param>
        /// <param name="maxWeight">The maximum weight the new drone can carry</param>
        /// <param name="battery">The battery status of the new drone</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddDrone(int id, string model, int maxWeight, int stationID)
        {
            if (DataSource.Drones.Exists((item) => item.Id == id && item.IsAvailable))
                throw new TheObjectIdAlreadyExist("The drone already exist in the system.");
            Drone drone = new()
            {
                Id = id,
                Model = model,
                MaxWeight = (WeightCategory)maxWeight,
                IsAvailable = true
            };
            DataSource.Drones.Add(drone);
            AddDroneChargesObject(id, stationID);
        }

        /// <summary>
        /// Delete a drone.
        /// </summary>
        /// <param name="id">The drone id to delete.</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteDrone(int id)
        {
            try
            {
                Drone drone = DataSource.Drones.Find(d => d.Id == id && d.IsAvailable);
                DataSource.Drones.Remove(drone);
                drone.IsAvailable = false;
                DataSource.Drones.Add(drone);
            }
            catch (ArgumentNullException)
            {
                throw new TheObjectIDDoesNotExist("The drone does not exist in the system.");
            }
        }

        /// <summary>
        /// The function of pulling a drone to a charging station
        /// </summary>
        /// <param name="droneId">The id of the drone you wont to loading</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void SendingADroneForChargingAtABaseStation(int droneId, int stationId)
        {
            try
            {
                BaseStation station = DataSource.BaseStations.First(s => s.Id == stationId && s.IsAvailable);
                DataSource.BaseStations.Remove(station);
                station.ChargeSlots -= 1;
                DataSource.BaseStations.Add(station);
                AddDroneChargesObject(droneId, stationId);
            }
            catch (ArgumentNullException ex)
            {
                throw new TheObjectIDDoesNotExist("The drone does not exist in the system.", ex);
            }
            catch (InvalidOperationException ex)
            {
                throw new TheObjectIDDoesNotExist("The drone does not exist in the system.", ex);
            }
        }

        /// <summary>
        /// The function add a new object to the droneCharges list 
        /// </summary>
        /// <param name="droneId">id of the drone that charging</param>
        /// <param name="stationId">id of the station the drone charging on it</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        private void AddDroneChargesObject(int droneId, int stationId)
        {
            DroneCharge tempDroneCharge = new() { DroneId = droneId, StationId = stationId, IsAvailable = true, DronentryTimeForCharging = DateTime.Now };
            DataSource.DroneCharges.Add(tempDroneCharge);
        }

        /// <summary>
        /// The function releases a drone from a charger
        /// </summary>
        /// <param name="droneId">The id of the drone you wont to release</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void ReleaseDroneFromChargingAtBaseStation(int droneId)
        {
            DroneCharge droneCharge = DataSource.DroneCharges.FirstOrDefault((item) => item.DroneId == droneId && item.IsAvailable);
            if (droneCharge.Equals(default(DroneCharge)))
            {
                throw new TheObjectIDDoesNotExist("This drone doesnt in charging now.");
            }
            try
            {
            RemoveDroneFromCharging(droneCharge.StationId);
            }
            catch (TheObjectIDDoesNotExist)
            {
                throw;
            }
            DataSource.DroneCharges.Remove(droneCharge);
            droneCharge.IsAvailable = false;
            DataSource.DroneCharges.Add(droneCharge);
        }

        /// <summary>
        /// The function change the drone model to a new model
        /// </summary>
        /// <param name="id">The drone id</param>
        /// <param name="modal">The new model</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void ChangeDronesName(int id, string modal)
        {
            Drone drone = DataSource.Drones.FirstOrDefault(item => item.Id == id && item.IsAvailable);
            if (drone.Equals(default(Drone)))
            {
                throw new TheObjectIDDoesNotExist("The drone does not exist in the system.");
            }
            DataSource.Drones.Remove(drone);
            drone.Model = modal;
            DataSource.Drones.Add(drone);
        }

        /// <summary>
        /// The function gets a drone ID, searches for it and returns it.
        /// </summary>
        /// <param name="id">drone id</param>
        /// <returns>The drone</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Drone GetDrone(int id)
        {
            try
            {
                return DataSource.Drones.First(drone => drone.Id == id && drone.IsAvailable);
            }
            catch (ArgumentNullException ex)
            {
                throw new TheObjectIDDoesNotExist("The drone does not exist in the system.", ex);
            }
            catch (InvalidOperationException ex)
            {
                throw new TheObjectIDDoesNotExist("The drone does not exist in the system.", ex);
            }
        }

        /// <summary>
        /// Returns all drones
        /// </summary>
        /// <returns>All the drones</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Drone> GetDronesList()
        {
            return DataSource.Drones.Where(drone => drone.IsAvailable);
        }

        /// <summary>
        /// Returns all DroneCharge
        /// </summary>
        /// <returns>All the DroneCharge</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneCharge> GetDroneCharges()
        {
            return DataSource.DroneCharges;
        }

        /// <summary>
        /// Returns all DroneCharge that meet the condition/s
        /// </summary>
        /// <param name="predicate">The condition</param>
        /// <returns>All the DroneCharge that meet the condition/s</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneCharge> GetDroneCharges(Predicate<DroneCharge> predicate)
        {
            return DataSource.DroneCharges.Where(drone => predicate(drone));
        }


    }
}
