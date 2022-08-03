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
        /// A function that receives data checks their correctness and creates an instance of a new parcel
        /// and add it to the list of parcels.
        /// </summary>
        /// <param name="idGiv">The id of the parcel's recipient</param>
        /// <param name="idSend">The id of the parcel's sender</param>
        /// <param name="weight">The weight of the new parcel</param>
        /// <param name="priority"></param>
        /// <returns>return the id of the new parcel</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public int AddAParcel(int idGiv, int idSend, int weight, int priority)
        {
            Parcel parcel = new()
            {
                Id = ++DataSource.Config.createIdParcel,
                SenderId = idSend,
                TargetId = idGiv,
                Weight = (WeightCategory)weight,
                Priority = (Priorities)priority,
                DroneId = 0,
                Requested = DateTime.Now,
                IsAvailable = true,
                Scheduled = null,
                Delivered = null,
                PickedUp = null
            };
            DataSource.Parcels.Add(parcel);
            return parcel.Id;
        }

        /// <summary>
        /// Delete a parcel.
        /// </summary>
        /// <param name="id">The parcel id to delete.</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteParcel(int id)
        {
            try
            {
                Parcel parcel = DataSource.Parcels.Find(p => p.Id == id && p.IsAvailable);
                DataSource.Parcels.Remove(parcel);
                parcel.IsAvailable = false;
                DataSource.Parcels.Add(parcel);
            }
            catch (ArgumentNullException ex)
            {
                throw new TheObjectIDDoesNotExist("The parcel does not exist in the system.", ex);
            }
        }


        /// <summary>
        /// A function that create new parcel array and over the original parcel array and active the DisplayParcel
        /// function co each limb in the array that not yet been assigned to a drone.
        /// </summary>
        /// <returns>The new parcel array.</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Parcel> GetTheListOfParcelsNotYetBeenAssignedToTheDrone()
        {
            return GetParcelsList((Parcel p) => p.DroneId == 0 && p.IsAvailable);
        }


        /// <summary>
        /// A method that assigning between the parcel and a suit drone
        /// </summary>
        /// <param name = "parcel">The desired parcel</param>
        /// <param name="droneId">The index of the suit drone</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AssigningAParcelToADrone(int parcelId, int droneId)
        {
            Parcel parcel = DataSource.Parcels.FirstOrDefault(p => p.Id == parcelId && p.IsAvailable);
            if (parcel.Equals(default(Parcel)))
            {
                throw new TheObjectIDDoesNotExist("The parcel id does not exist in the system");
            }

            DataSource.Parcels.Remove(parcel);
            parcel.DroneId = droneId;
            parcel.Scheduled = DateTime.Now;
            DataSource.Parcels.Add(parcel);
        }

        /// <summary>
        /// A function to collect a parcel by a drone, if the parcel exist, it update the time of collect.
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void CollectAParcelByADrone(int parcelId, int droneId)
        {
            Parcel parcel = DataSource.Parcels.FirstOrDefault(p => p.Id == parcelId && p.IsAvailable);
            if (parcel.Equals(default(Parcel)))
                throw new TheObjectIDDoesNotExist("The parcel id does not exist in the system");
            DataSource.Parcels.Remove(parcel);
            parcel.PickedUp = DateTime.Now;
            parcel.DroneId = droneId;
            DataSource.Parcels.Add(parcel);
        }

        /// <summary>
        /// A function to delivery of a parcel to the destination, if the parcel exist it update the time of the delivery.
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeliveryOfAParcelToTheDestination(int parcelId)
        {
            Parcel parcel = DataSource.Parcels.FirstOrDefault(p => p.Id == parcelId && p.IsAvailable);
            if (parcel.Equals(default(Parcel)))
                throw new TheObjectIDDoesNotExist("The parcel id does not exist in the system");
            DataSource.Parcels.Remove(parcel);
            parcel.Delivered = DateTime.Now;//אספקה??
            DataSource.Parcels.Add(parcel);
        }


        /// <summary>
        /// Returns all parcels that meet the condition/s
        /// </summary>
        /// <param name="predicate">The condition</param>
        /// <returns>All the parcels that meet the condition/s</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Parcel> GetParcelsList(Predicate<Parcel> predicate)
        {
            return DataSource.Parcels.Where(p => predicate(p) && p.IsAvailable);
        }

        /// <summary>
        /// Returns all parcels
        /// </summary>
        /// <returns>All the parcels</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Parcel> GetParcelsList()
        {
            return DataSource.Parcels.Where(parcel => parcel.IsAvailable);
        }

        /// <summary>
        /// The function gets a parcel ID, searches for it and returns it.
        /// </summary>
        /// <param name="id">parcel id</param>
        /// <returns>The parcel</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Parcel GetParcel(int parcelId)
        {
            Parcel parcel = GetParcelsList().FirstOrDefault(parcel => parcel.Id == parcelId);
            return parcel.Equals(default(Parcel)) ? throw new TheObjectIDDoesNotExist("The parcel does not exist in the system.") : parcel;
        }


    }
}
