using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dal
{
    partial class DalXml
    {
        /// <summary>
        /// A function that receives data checks their correctness and creates an instance of a new parcel
        /// and add it to the list of parcels.
        /// </summary>
        /// <param name="idGiv">The id of the parcel's recipient</param>
        /// <param name="idSend">The id of the parcel's sender</param>
        /// <param name="weight">The weight of the new parcel</param>
        /// <param name="priority"></param>
        /// <returns>return the id of the new parcel</returns>
        public int AddAParcel(int idGiv, int idSend, int weight, int priority)
        {
            XElement dataConfig = XMLTools.LoadListFromXmlElement(CONFIGPATH);

            Parcel parcel = new()
            {
                Id = int.Parse(dataConfig.Element("data").Element("createIdParcel").Value) + 1,
                SenderId = idSend,
                TargetId = idGiv,
                Priority = (Priorities)priority,
                Weight = (WeightCategory)weight,
                Requested = DateTime.Now,
                IsAvailable = true
            };
            dataConfig.Element("data").Element("createIdParcel").Value = parcel.Id.ToString();
            dataConfig.Save(@"../../Data/" + CONFIGPATH);
            List<Parcel> parcels = XMLTools.LoadListFromXmlSerializer<Parcel>(PARCELPATH);
            parcels.Add(parcel);
            XMLTools.SaveListToXmlSerializer(parcels, PARCELPATH);
            return parcel.Id;
        }


        public void DeleteParcel(int id)
        {
            List<Parcel> parcels = XMLTools.LoadListFromXmlSerializer<Parcel>(PARCELPATH);
            Parcel parcel = GetParcel(id);
            parcels.Remove(parcel);
            parcel.IsAvailable = false;
            parcels.Add(parcel);
            XMLTools.SaveListToXmlSerializer(parcels, PARCELPATH);
        }
        /// <summary>
        /// A function that create new parcel array and over the original parcel array and active the DisplayParcel
        /// function co each limb in the array that not yet been assigned to a drone.
        /// </summary>
        /// <returns>The new parcel array.</returns>
        public IEnumerable<Parcel> GetTheListOfParcelsNotYetBeenAssignedToTheDrone()
        {
            return XMLTools.LoadListFromXmlSerializer<Parcel>(PARCELPATH).Where(parcel => parcel.DroneId == 0);
        }


        /// <summary>
        /// A function that receives id of desired parcel, checks if it exists, and return it details to display.
        /// </summary>
        /// <param name="parcelId">The id of desired parcel.</param>
        /// <returns>New customer with desired parcel details if it exists, else, throw exception.</returns>
        public Parcel GetParcel(int parcelId)
        {
            return XMLTools.LoadListFromXmlSerializer<Parcel>(PARCELPATH).SingleOrDefault(parcel => parcel.Id == parcelId);
        }

        public IEnumerable<Parcel> GetParcelsList()
        {
            return XMLTools.LoadListFromXmlSerializer<Parcel>(PARCELPATH).Where(parcel => parcel.IsAvailable);
        }

        public IEnumerable<Parcel> GetParcelsList(Predicate<Parcel> predicate)
        {
            return XMLTools.LoadListFromXmlSerializer<Parcel>(PARCELPATH).Where(parcel => predicate(parcel));
        }

        /// <summary>
        /// A function to collect a parcel by a drone, if the parcel exist, it update the time of collect.
        /// </summary>
        public void CollectAParcelByADrone(int parcelId, int droneId)
        {
            try
            {
                List<Parcel> parcels = XMLTools.LoadListFromXmlSerializer<Parcel>(PARCELPATH);
                Parcel parcel = GetParcel(parcelId);
                parcels.Remove(parcel);
                parcel.PickedUp = DateTime.Now;
                parcel.DroneId = droneId;
                parcels.Add(parcel);
                XMLTools.SaveListToXmlSerializer(parcels, PARCELPATH);

            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// A function to delivery of a parcel to the destination, if the parcel exist it update the time of the delivery.
        /// </summary>
        public void DeliveryOfAParcelToTheDestination(int parcelId)
        {
            try
            {
                List<Parcel> parcels = XMLTools.LoadListFromXmlSerializer<Parcel>(PARCELPATH);
                Parcel parcel = GetParcel(parcelId);
                parcels.Remove(parcel);
                parcel.Delivered = DateTime.Now;
                parcels.Add(parcel);
                XMLTools.SaveListToXmlSerializer(parcels, PARCELPATH);

            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// A method that assigning between the parcel and a suit drone
        /// </summary>
        /// <param name = "parcel">The desired parcel</param>
        /// <param name="droneId">The index of the suit drone</param>
        public void AssigningAParcelToADrone(int parcelId, int droneId)
        {
            try
            {
                List<Parcel> parcels = XMLTools.LoadListFromXmlSerializer<Parcel>(PARCELPATH);
                Parcel parcel = GetParcel(parcelId);
                parcels.Remove(parcel);
                parcel.Scheduled = DateTime.Now;
                parcel.DroneId = droneId;
                parcels.Add(parcel);
                XMLTools.SaveListToXmlSerializer(parcels, PARCELPATH);

            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
