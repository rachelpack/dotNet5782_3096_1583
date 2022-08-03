using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Dal
{
    partial class DalObject
    {

        /// <summary>
        /// A function that receives data checks their correctness and creates an instance of a new station and add it to the list of stations
        /// </summary>
        /// <param name="id">id of the new station </param>
        /// <param name="name">name of the new station</param>
        /// <param name="lo">longitude of the new station </param>
        /// <param name="la">Latitude of the new staion</param>
        /// <param name="chargeSlots">Number of the charge slots</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddStation(int id, string name, double lo, double la, int chargeSlots = 7)
        {
            if (DataSource.BaseStations.Exists(item=>item.Id == id))
            {
                throw new TheObjectIdAlreadyExist("This station is already exist in the system.");
            }

            BaseStation baseStation = new()
            {
                Id = id,
                Name = name,
                Longitude = lo,
                Latitude = la,
                ChargeSlots = chargeSlots,
                IsAvailable = true
            };
            DataSource.BaseStations.Add(baseStation);
        }

        /// <summary>
        /// Delete a station
        /// </summary>
        /// <param name="id">The station's id the delete.</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteBaseStation(int id)
        {
            try
            {
                BaseStation baseStation = DataSource.BaseStations.Find(b => b.Id == id && b.IsAvailable);
                DataSource.BaseStations.Remove(baseStation);
                baseStation.IsAvailable = false;
                DataSource.BaseStations.Add(baseStation);
            }
            catch (ArgumentNullException ex)
            {
                throw new TheObjectIDDoesNotExist("The station does not exist in the system.", ex);
            }

        }


        /// <summary>
        /// A function that create a new BaseStation list and over the original baseStation list and active
        /// the DisplayStation function on each limb in the list.
        /// </summary>
        /// <returns> the new baseStation list.</returns>
         [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<BaseStation> GetBaseStationsWithAvailableChargingStations()
        {
            return GetStationsList(s => s.ChargeSlots > 0);
        }

        /// <summary>
        /// The function get station id and increas the charge slots.
        /// </summary>
        /// <param name="StationId"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        private void RemoveDroneFromCharging(int StationId)
        {
            BaseStation station = DataSource.BaseStations.FirstOrDefault((item) => item.Id == StationId);
            if (station.Equals(default(BaseStation)))
            {
                throw new TheObjectIDDoesNotExist("The station does not exist in the system.");
            }
            DataSource.BaseStations.Remove(station);
            station.ChargeSlots++;
            DataSource.BaseStations.Add(station);
        }

        /// <summary>
        /// The function changes the name of the station and / or the number of charging stations in the station
        /// </summary>
        /// <param name="id">The station id.</param>
        /// <param name="num">The new number of the charging stations.</param>
        /// <param name="name">The new name.</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void ChangeStationNameAndChargeSlots(int id, string name, int num)
        {
            BaseStation station = DataSource.BaseStations.FirstOrDefault(item => item.Id == id && item.IsAvailable);
            if (station.Equals(default(BaseStation)))
            {
                throw new TheObjectIDDoesNotExist("The station does not exist in the system.");
            }
            DataSource.BaseStations.Remove(station);
            if (num != 0)
            {
                if (num < station.ChargeSlots)
                {
                    throw new OutOfRangeValue("The number of the charge slots too small.");
                }
                station.ChargeSlots = num;
            }
            if (name != "0")
            {
                station.Name = name;
            }

            DataSource.BaseStations.Add(station);
        }

        /// <summary>
        /// Returns all stations that meet the condition/s
        /// </summary>
        /// <param name="predicate">The condition</param>
        /// <returns>All the station that meet the condition/s</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<BaseStation> GetStationsList(Predicate<BaseStation> predicate)
        {
            return DataSource.BaseStations.Where(p => predicate(p));
        }
        /// <summary>
        /// Returns all stations
        /// </summary>
        /// <returns>All the stations</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<BaseStation> GetStationsList()
        {
            return DataSource.BaseStations.Where(station => station.IsAvailable);
        }

        /// <summary>
        /// The function gets a station ID, searches for it and returns it.
        /// </summary>
        /// <param name="id">Station id</param>
        /// <returns>The station</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public BaseStation GetStation(int id)
        {
            BaseStation station = GetStationsList().FirstOrDefault(d => d.Id == id);
            return station.Equals(default(BaseStation))
                ? throw new TheObjectIDDoesNotExist("The station does not exist in the system.")
                : station;
        }

    }
}

