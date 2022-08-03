using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    partial class DalXml
    {
        /// <summary>
        /// The function adds a new station to the system.
        /// </summary>
        /// <param name="id">The new station's id.</param>
        /// <param name="name">The new station's name.</param>
        /// <param name="longitude">The new station's longitude.</param>
        /// <param name="latitude">The new station's latitude</param>
        /// <param name="chargeSlots">The new station's chargeSlots.</param>
        public void AddStation(int id, string name, double longitude, double latitude, int chargeSlots = 7)
        {
            BaseStation station = new()
            {
                Id = id,
                Name = name,
                Latitude = latitude,
                Longitude = longitude,
                ChargeSlots = chargeSlots,
                IsAvailable = true
            };
            List<BaseStation> stations = XMLTools.LoadListFromXmlSerializer<BaseStation>(BASESTATIONPATH);
            stations.Add(station);
            XMLTools.SaveListToXmlSerializer(stations, BASESTATIONPATH);
        }

        /// <summary>
        /// The function deletes a station from the system.
        /// </summary>
        /// <param name="id">he station's id you wont to delete.</param>
        public void DeleteBaseStation(int id)
        {
            List<BaseStation> stations = XMLTools.LoadListFromXmlSerializer<BaseStation>(BASESTATIONPATH);
            BaseStation station = stations.SingleOrDefault(s => s.Id == id);
            stations.Remove(station);
            station.IsAvailable = false;
            stations.Add(station);
            XMLTools.SaveListToXmlSerializer(stations, BASESTATIONPATH);
        }

        /// <summary>
        /// The function returns all the stations list with available charging stations.
        /// </summary>
        /// <returns>The stations' list with available charging stations.</returns>
        public IEnumerable<BaseStation> GetBaseStationsWithAvailableChargingStations()
        {
            return XMLTools.LoadListFromXmlSerializer<BaseStation>(BASESTATIONPATH).Where(s => s.ChargeSlots > 0);
        }

        /// <summary>
        /// The function returns a station.
        /// </summary>
        /// <param name="id">The station's id you wont.</param>
        /// <returns>The station.</returns>
        public BaseStation GetStation(int id)
        {
            return XMLTools.LoadListFromXmlSerializer<BaseStation>(BASESTATIONPATH).SingleOrDefault(s => s.Id == id);
        }

        /// <summary>
        /// The function returns all the stations list.
        /// </summary>
        /// <returns>The stations' list.</returns>
        public IEnumerable<BaseStation> GetStationsList()
        {
            return XMLTools.LoadListFromXmlSerializer<BaseStation>(BASESTATIONPATH);
        }

        public IEnumerable<BaseStation> GetStationsList(Predicate<BaseStation> predicate)
        {
            return XMLTools.LoadListFromXmlSerializer<BaseStation>(BASESTATIONPATH).Where(s => predicate(s));
        }


        /// <summary>
        /// update the stations details.
        /// </summary>
        /// <param name="id">the station id</param>
        /// <param name="name">The station name</param>
        /// <param name="num">The number of slots in the station.</param>
        public void ChangeStationNameAndChargeSlots(int id, string name = "0", int num = 0)
        {
            List<BaseStation> stations = XMLTools.LoadListFromXmlSerializer<BaseStation>(BASESTATIONPATH);
            BaseStation station = stations.SingleOrDefault(s => s.Id == id && s.IsAvailable);
            stations.Remove(station);
            if (name != "0")
            {
                station.Name = name;
            }
            if (num != 0) station.ChargeSlots = num;
            stations.Add(station);
            XMLTools.SaveListToXmlSerializer(stations, BASESTATIONPATH);
        }

        /// <summary>
        /// The function adds charge slot in a station.
        /// </summary>
        /// <param name="id">The station id.</param>
        private void AddChargeSlots(int id)
        {
            List<BaseStation> stations = XMLTools.LoadListFromXmlSerializer<BaseStation>(BASESTATIONPATH);
            BaseStation station = stations.SingleOrDefault(s => s.Id == id);
            stations.Remove(station);
            ++station.ChargeSlots;
            stations.Add(station);
            XMLTools.SaveListToXmlSerializer(stations, BASESTATIONPATH);
        }

        /// <summary>
        /// The function removes charge slot in a station.
        /// </summary>
        /// <param name="id">The station id.</param>
        private void RemoveChargeSlots(int id)
        {
            List<BaseStation> stations = XMLTools.LoadListFromXmlSerializer<BaseStation>(BASESTATIONPATH);
            List<BaseStation> temp = stations.FindAll(s => s.Id == id && s.IsAvailable);
            BaseStation station = stations.SingleOrDefault(s => s.Id == id && s.IsAvailable);
            stations.Remove(station);
            --station.ChargeSlots;
            stations.Add(station);
            XMLTools.SaveListToXmlSerializer(stations, BASESTATIONPATH);
        }
    }
}
