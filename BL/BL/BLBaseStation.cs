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
        /// A function that receives data checks their correctness and creates an instance of a new staion and add it to the list of station
        /// </summary>
        /// <param name="id">Station id</param>
        /// <param name="name">Station modal</param>
        /// <param name="longitude">Station longitude</param>
        /// <param name="latitudes">Station latitudes</param>
        /// <param name="ChargeSlots">Number if  available charge slots in the staion</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddStation(int id, string name, double longitude, double latitudes, int ChargeSlots)
        {
            lock (dal)
            {
                if (dal.GetStationsList().Any(item => item.Id == id))
                {
                    throw new TheObjectIdAlreadyExist("The station already exist in the system.");
                }
            }

            if (longitude is < 0 or > 180)
            {
                throw new TheValueOutOfRange("The longitude out of value");
            }

            if (latitudes is < 0 or > 90)
            {
                throw new TheValueOutOfRange("The latitudes out of value");
            }

            BaseStation baseStation = new()
            {
                Id = id,
                DronesInCharge = new List<DroneInCharge>(),
                Name = name,
                NumOfChargeSlots = ChargeSlots,
                Location = new Location() { Latitude = latitudes, Longitude = longitude }
            };
            try
            {
                lock (dal)
                {
                    dal.AddStation(id, name, longitude, latitudes, ChargeSlots);
                }
            }
            catch (DO.TheObjectIdAlreadyExist ex)
            {
                baseStation = null;
                throw new TheObjectIdAlreadyExist("The base station aleady exist in the system.", ex);
            }

            catch (ArgumentNullException ex)
            {
                baseStation = null;
                throw new TheObjectIdAlreadyExist("The base station aleady exist in the system.", ex);
            }
        }

        /// <summary>
        /// Delete the base station.
        /// </summary>
        /// <param name="id">The id of the base station.</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteBaseStation(int id)
        {
            try
            {
                lock (dal)
                {
                    dal.DeleteBaseStation(id);
                }

            }
            catch (DO.TheObjectIDDoesNotExist ex)
            {
                throw new TheObjectIDDoesNotExist("You are trying to delete a station that does not appear in the system", ex);
            }
        }

        /// <summary>
        /// A function that create a new BaseStation list and over the original baseStation list.
        /// </summary>
        /// <returns>The new Station list</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<BaseStationForList> GetBaseStationList()
        {
            IEnumerable<DroneCharge> droneCharges;
            IEnumerable<DO.BaseStation> stations;
            lock (dal)
            {
                droneCharges = dal.GetDroneCharges().Where(d => d.IsAvailable);
                stations = dal.GetStationsList().Where(b => b.IsAvailable);
                foreach (DO.BaseStation item in stations)
                {
                    yield return new BaseStationForList()
                    {
                        Id = item.Id,
                        Name = item.Name,
                        SeveralChargingStationsAreAvailable = item.ChargeSlots,
                        SeveralChargingStationsAreOccupied = droneCharges.Count(dc => dc.StationId == item.Id)
                    };
                }
            }
        }


        /// <summary>
        /// The function return list with the station that have an empty charge slotes.
        /// </summary>
        /// <returns>List with the station that have an empty charge slotes</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<BaseStationForList> ChargingStationsWhithEmptyChargeSlots()
        {
            return GetBaseStationList((BaseStationForList b) => b.SeveralChargingStationsAreAvailable > 0);
        }

        /// <summary>
        /// Returns all stations that meet the condition/s
        /// </summary>
        /// <param name="predicate">The condition</param>
        /// <returns>All the station that meet the condition/s</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<BaseStationForList> GetBaseStationList(Predicate<BaseStationForList> predicate)
        {

            List<BaseStationForList> stationForLists = new();
            lock (dal)
            {
                foreach (DO.BaseStation item in dal.GetStationsList().Where(s => s.IsAvailable))
                {
                    stationForLists.Add(ConvertDalStationToBLStaionToList(item));
                }
                return stationForLists.Where(b => predicate(b));
            }
        }

        /// <summary>
        /// Convert dal station to BL staionToList
        /// </summary>
        /// <param name="station">The DO station</param>
        /// <returns>The BO staionToList</returns>
        private BaseStationForList ConvertDalStationToBLStaionToList(DO.BaseStation station)
        {
            lock (dal)
            {
                return new BaseStationForList()
                {
                    Id = station.Id,
                    Name = station.Name,
                    SeveralChargingStationsAreAvailable = station.ChargeSlots,
                    SeveralChargingStationsAreOccupied = dal.GetDroneCharges().Count(dc => dc.StationId == station.Id)
                };
            }
        }

        /// <summary>
        /// Convert Dal station toBL staion
        /// </summary>
        /// <param name="station">The DO station</param>
        /// <returns>The new BO station</returns>
        private static BaseStation ConvertDalStationToBLStaion(DO.BaseStation station)
        {
            return new BaseStation()
            {
                Id = station.Id,
                Name = station.Name,
                Location = new() { Latitude = station.Latitude, Longitude = station.Longitude },
                NumOfChargeSlots = station.ChargeSlots,

            };
        }
        /// <summary>
        /// The function gets a station ID, searches for it and returns it.
        /// </summary>
        /// <param name="id">Station id</param>
        /// <returns>The station</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public BaseStation GetBaseStation(int id)
        {
            DO.BaseStation dalStation = new();
            //Finding the desired station
            try
            {
                lock (dal)
                {
                    dalStation = dal.GetStation(id);
                }

                if (!dalStation.IsAvailable)
                {
                    throw new TheObjectIDDoesNotExist("The station doesnt exist in the system.");
                }
            }
            catch (DO.TheObjectIDDoesNotExist ex)
            {
                throw new TheObjectIDDoesNotExist("The station doesnt exist in the system.", ex);
            }
            //Convert station to UBL station
            return new()
            {
                Id = dalStation.Id,
                Name = dalStation.Name,
                Location = new Location() { Longitude = dalStation.Longitude, Latitude = dalStation.Latitude },
                NumOfChargeSlots = dalStation.ChargeSlots,
                DronesInCharge = FindDronesThatAreLoadedAtStation(id)
            };

        }


        /// <summary>
        /// The function changes the name of the station and / or the number of charging stations in the station.
        /// </summary>
        /// <param name="id">The station id.</param>
        /// <param name="num">The new number of the charging stations.</param>
        /// <param name="name">The new name.</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateStation(int id, int num, string name)
        {
            try
            {
                lock (dal)
                {
                    dal.ChangeStationNameAndChargeSlots(id, name, num);
                }
            }
            catch (DO.TheObjectIDDoesNotExist ex)
            {
                throw new TheObjectIDDoesNotExist("The station doesnt exist in the system.", ex);
            }
            catch (OutOfRangeValue ex)
            {
                throw new TheValueOutOfRange("The number of the charge slots too small.", ex);
            }
        }
    }
}
