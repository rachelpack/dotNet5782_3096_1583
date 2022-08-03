using DO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dal
{
    partial class DalXml
    {

        /// <summary>
        /// The function converts dal drone object to XElement object.
        /// </summary>
        /// <param name="drone">The drone you wont to convert.</param>
        /// <returns></returns>
        private XElement GetDroneElement(Drone drone)
        {
            XElement element = new("Drone");
            XElement idElement = new("Id", drone.Id);
            XElement ModelElement = new("Model", drone.Model);
            XElement weightElement = new("MaxWeight", (int)drone.MaxWeight);
            XElement IsAvelableElement = new("IsAvailable", drone.IsAvailable ? "1" : "0");
            element.Add(idElement, ModelElement, weightElement, IsAvelableElement);
            return element;
        }

        /// <summary>
        /// The function converts drone XElement object to dal drone object.
        /// </summary>
        /// <param name="element">The XElement you wont to convert.</param>
        /// <returns></returns>
        private Drone ConvertXElementToDroneObject(XElement element)
        {
            return new Drone()
            {
                Id = int.Parse(element.Element("Id").Value),
                Model = element.Element("Model").Value,
                IsAvailable = int.Parse(element.Element("IsAvailable").Value) == 1,
                MaxWeight = (WeightCategory)int.Parse(element.Element("MaxWeight").Value)
            };
        }

        /// <summary>
        /// The function adds a new drone to the system.
        /// </summary>
        /// <param name="id">The id of the new drone.</param>
        /// <param name="model">The model of the new drone.</param>
        /// <param name="weight">The weight of the new drone.</param>
        /// <param name="stationID">The baseStation ID that the new drone charges on it.</param>
        public void AddDrone(int id, string model, int weight, int stationID)
        {
            Drone drone = new() { Id = id, IsAvailable = true, MaxWeight = (WeightCategory)weight, Model = model };
            XElement root;
            try
            {
                root = XMLTools.LoadListFromXmlElement(DRONECHARGESPATH);
            }
            catch (DirectoryNotFoundException)
            {
                throw;
            }
            try { root.Add(GetDroneElement(drone)); }
            catch (Exception e)
            {
                throw new XMLElementDidNotFound("The element didnt found.", e);
            }
            try
            {
                XMLTools.SaveListToXmlElement(root, DRONEPATH);
            }
            catch (DirectoryNotFoundException)
            {
                throw;
            }
        }

        /// <summary>
        /// The function deletes a drone from the system.
        /// </summary>
        /// <param name="id">The drone's id you wont to delete.</param>
        public void DeleteDrone(int id)
        {
            XElement root;
            try
            {
                root = XMLTools.LoadListFromXmlElement(DRONEPATH);
            }
            catch (DirectoryNotFoundException)
            {
                throw;
            }
            try
            {
                root.Elements()
                    .SingleOrDefault(drone => int.Parse(drone.Element("Id").Value) == id && drone.Element("IsAvailable").Value == "1")
                    .SetElementValue("IsAvailable", "0");
            }
            catch (Exception e)
            {
                throw new XMLElementDidNotFound("The element didnt found.", e);
            }
            try
            {
                XMLTools.SaveListToXmlElement(root, DRONEPATH);
            }
            catch (DirectoryNotFoundException)
            {
                throw;
            }
        }

        /// <summary>
        /// The function returns a drone.
        /// </summary>
        /// <param name="id">The drone's id you wont.</param>
        /// <returns>The drone.</returns>
        public Drone GetDrone(int id)
        {
            XElement root;
            try
            {
                root = XMLTools.LoadListFromXmlElement(DRONEPATH);
            }
            catch (DirectoryNotFoundException)
            {
                throw;
            }
            try
            {
                XElement droneElement = root.Elements("Drone")
                            .SingleOrDefault(drone => int.Parse(drone.Element("Id").Value) == id);
                return ConvertXElementToDroneObject(droneElement);
            }
            catch (Exception e)
            {
                throw new XMLElementDidNotFound("The element didnt found.", e);
            }
        }

        /// <summary>
        /// The function returns all the drones list.
        /// </summary>
        /// <returns>The drone list.</returns>
        public IEnumerable<Drone> GetDronesList()
        {
            XElement root;
            try
            {
                root = XMLTools.LoadListFromXmlElement(DRONEPATH);
            }
            catch (DirectoryNotFoundException)
            {
                throw;
            }
            try
            {
                return root.Elements()
                             .Select(drone => ConvertXElementToDroneObject(drone));
            }
            catch (Exception e)
            {
                throw new XMLElementDidNotFound("The element didnt found.", e);
            }
        }

        /// <summary>
        /// The function changes the model of the drone.
        /// </summary>
        /// <param name="id">The drone's id you wont to change.</param>
        /// <param name="modal">The new model.</param>
        public void ChangeDronesName(int id, string modal)
        {
            XElement root;
            try
            {
                root = XMLTools.LoadListFromXmlElement(DRONEPATH);
            }
            catch (DirectoryNotFoundException)
            {
                throw;
            }
            try
            {
                root.Elements()
                           .SingleOrDefault(drone => int.Parse(drone.Element("Id").Value) == id && drone.Element("IsAvailable").Value == "1")
                           .SetElementValue("Model", modal);
                try
                {
                    XMLTools.SaveListToXmlElement(root, DRONEPATH);
                }
                catch (DirectoryNotFoundException)
                {
                    throw;
                }
            }
            catch (Exception e)
            {
                throw new XMLElementDidNotFound("The element didnt found.", e);
            }
        }

    }
}
