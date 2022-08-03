using DO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Dal
{
    partial class DalXml
    {

        /// <summary>
        /// The function converts droneInCharge XElement object to dal droneCharge object.
        /// </summary>
        /// <param name="element">The XElement you wont to convert.</param>
        /// <returns></returns>
        private DroneCharge ConvertXElementToDroneChargeObject(XElement element)
        {
            try
            {
                return new DroneCharge()
                {
                    DroneId = int.Parse(element.Element("DroneId").Value),
                    StationId = int.Parse(element.Element("StationId").Value),
                    IsAvailable = int.Parse(element.Element("IsAvailable").Value) == 1,
                    DronentryTimeForCharging = (DateTime?)element.Element("DronentryTimeForCharging")
                };
            }
            catch (Exception e)
            {
                throw new XMLElementDidNotFound("The element didnt found.", e);
            }
        }

        /// <summary>
        /// The function converts dal droneCharge object to XElement object.
        /// </summary>
        /// <param name="drone">The droneCharge you wont to convert.</param>
        /// <returns></returns>
        private XElement GetDroneChargeElement(DroneCharge drone)
        {
            XElement element = new("DroneInCharges");
            XElement droneIdElement = new("DroneId", drone.DroneId);
            XElement stationIdElement = new("StationId", drone.StationId);
            XElement isAvelabeElement = new("IsAvailable", drone.IsAvailable ? "1" : "0");
            XElement timeForChargingElement = new("DronentryTimeForCharging", drone.DronentryTimeForCharging);
            element.Add(droneIdElement, stationIdElement, isAvelabeElement, timeForChargingElement);
            return element;
        }

        /// <summary>
        /// The function adds a new drone in charge to the system.
        /// </summary>
        /// <param name="droneId">The id of the drone.</param>
        /// <param name="stationId">The id of the base station the drone charges in it.</param>
        private void AddDroneCharge(int droneId, int stationId)
        {
            DroneCharge drone = new() { DroneId = droneId, StationId = stationId, IsAvailable = true, DronentryTimeForCharging = DateTime.Now };
            XElement root;
            try
            {
                root = XMLTools.LoadListFromXmlElement(DRONECHARGESPATH);
            }
            catch (DirectoryNotFoundException)
            {
                throw;
            }
            root.Add(GetDroneChargeElement(drone));
            try
            {
                XMLTools.SaveListToXmlElement(root, DRONECHARGESPATH);
            }
            catch (DirectoryNotFoundException)
            {
                throw;
            }
        }

        /// <summary>
        /// The function returns all the dronesCharge list.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DroneCharge> GetDroneCharges()
        {
            XElement root;
            try
            {
                root = XMLTools.LoadListFromXmlElement(DRONECHARGESPATH);
            }
            catch (DirectoryNotFoundException)
            {
                throw;
            }
            try
            {
                return root.Elements("DroneInCharges")
                    .Select(drone => ConvertXElementToDroneChargeObject(drone));
            }
            catch (Exception e)
            {
                throw new XMLElementDidNotFound("The element didnt found.", e);
            }
        }

        /// <summary>
        /// The function deletes a droneCharge.
        /// </summary>
        /// <param name="id">The droneCharge's you wont to delete.</param>
        public void DeleteDroneCharge(int id)
        {
            XElement root;
            try
            {
                root = XMLTools.LoadListFromXmlElement(DRONECHARGESPATH);
            }
            catch (DirectoryNotFoundException)
            {
                throw;
            }
            try
            {
                root.Elements()
                    .Single(drone => int.Parse(drone.Element("DroneId").Value) == id && drone.Element("IsAvailable").Value == "1")
                    .SetElementValue("IsAvailable", "0");
                XMLTools.SaveListToXmlElement(root, DRONECHARGESPATH);
            }
            catch (Exception e)
            {
                throw new XMLElementDidNotFound("The element didnt found.", e);
            }
        }

        /// <summary>
        /// The function returns all the drones in charge list.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DroneCharge> GetDroneCharges(Predicate<DroneCharge> predicate)
        {
            return GetDroneCharges().Where(drone => predicate(drone));
        }

        /// <summary>
        /// The functiom sending a drone for charging at a baseStation.
        /// </summary>
        /// <param name="droneId">The drone's id.</param>
        /// <param name="stationId">The base staion's id.</param>
        public void SendingADroneForChargingAtABaseStation(int droneId, int stationId)
        {
            RemoveChargeSlots(stationId);
            AddDroneCharge(droneId, stationId);
        }

        /// <summary>
        /// The function release drone from charging at base station.
        /// </summary>
        /// <param name="id">The drone's id.</param>
        public void ReleaseDroneFromChargingAtBaseStation(int id)
        {
            XElement root;
            try
            {
                root = XMLTools.LoadListFromXmlElement(DRONECHARGESPATH);
            }
            catch (DirectoryNotFoundException)
            {
                throw;
            }
            try
            {
                XElement drone = root.Elements()
                    .Single(drone => int.Parse(drone.Element("DroneId").Value) == id && drone.Element("IsAvailable").Value == "1");
                int stationId = ConvertXElementToDroneChargeObject(drone).StationId;
                AddChargeSlots(stationId);
                DeleteDroneCharge(id);
            }
            catch (Exception e)
            {
                throw new XMLElementDidNotFound("The element didnt found.", e);
            }
        }


    }
}
