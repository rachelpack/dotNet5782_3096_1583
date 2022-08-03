using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using DO;

namespace BO
{
    internal partial class BL
    {
        public void AddParcel(int senderId, int recieverId, int weight, int priorities)
        {
            lock (dal)
            {
                if (!dal.GetCustomersList().Any(item => item.Id == senderId))
                {
                    throw new TheObjectIDDoesNotExist("The sender customer doesnt exist in the system.");
                }
            }

            lock (dal)
            {
                if (!dal.GetCustomersList().Any(item => item.Id == recieverId))
                {
                    throw new TheObjectIDDoesNotExist("The receives customer doesnt exist in the system.");
                }
            }

            if (weight is > 3 or < 0)
            {
                throw new ThereIsWorngDetails("Wrong Weight");
            }

            if (priorities is < 0 or > 2)
            {
                throw new ThereIsWorngDetails("Wrong Priority");
            }

            Parcel parcel = new()
            {

                Priority = (Priorities)priorities

            };

            try
            {
                lock (dal)
                    parcel.Id = dal.AddAParcel(recieverId, senderId, weight, priorities);
            }
            catch (TheObjectIdAlreadyExist)
            {
                parcel = null;
                throw;
            }
        }
        public void DeleteParcel(int id)
        {
            try
            {
                lock (dal)
                    dal.GetParcel(id);
                if (dronesList.Exists(d => d.ParcelId == id))
                {
                    throw new TheObjectCanBeDeleted("There parcel can be deleted");
                }
                lock (dal)
                    dal.DeleteParcel(id);
            }
            catch (DO.TheObjectIDDoesNotExist ex)
            {
                throw new TheObjectIDDoesNotExist("The sender customer doesnt exist in the system.", ex);
            }
        }

        /// <summary>
        /// A function that receives data checks their correctness and creates an instance of a new parcel and add it to the list of parcels
        /// </summary>
        /// <param name="sId">The sender id</param>
        /// <param name="rId">The receives id</param>
        /// <param name="weight">The weight of the parcel</param>
        /// <param name="priority">The priority of the parcel</param>
        public void ReceivingAPackageForDelivery(int sId, int rId, int weight, int priority)
        {
            lock (dal)
            {
                if (!dal.GetCustomersList().Any(item => item.Id == sId))
                {
                    throw new TheObjectIDDoesNotExist("The sender customer doesnt exist in the system.");
                }
            }

            lock (dal)
            {
                if (!dal.GetCustomersList().Any(item => item.Id == rId))
                {
                    throw new TheObjectIDDoesNotExist("The receives customer doesnt exist in the system.");
                }
            }

            if (weight is > 3 or < 0)
            {
                throw new ThereIsWorngDetails("The weight out of range.");
            }

            if (priority is > 3 or < 0)
            {
                throw new ThereIsWorngDetails("The priority out of range.");
            }

            Parcel parcel = new()
            {
                Sender = new CustomerInDelivery() { Id = sId, Name = GetNameOfCustomer(sId) },
                Receives = new CustomerInDelivery() { Id = rId, Name = GetNameOfCustomer(rId) },
                Weight = (WeightCategory)weight,
                Priority = (Priorities)priority,
                TimeOfCreateDelivery = DateTime.Now,
                AssignmentTime = null,
                SupplyTime = null,
                CollectingTime = null,
                Drone = null,
            };


            lock (dal)
            {
                parcel.Id = dal.AddAParcel(rId, sId, weight, priority);
            }
        }


        /// <summary>
        /// The function gets a parcel ID, searches for it and returns it.
        /// </summary>
        /// <param name="id">The parce id</param>
        /// <returns>The requested parcel</returns>
        public Parcel GetParcel(int id)
        {
            //Finding the desired station
            DO.Parcel dalParcel = new();
            try
            {
                lock (dal)
                {
                    dalParcel = dal.GetParcel(id);
                }
            }
            catch (DO.TheObjectIDDoesNotExist ex)
            {
                throw new TheObjectIDDoesNotExist("The parcel doesnt exist in the system.", ex);
            }
            //convert IDAL parcel to BL parcel.
            return ConvertIDALParcelToIBLParcel(dalParcel);
        }

        /// <summary>
        /// A function that create a new parcels list and over the original parcels list. 
        /// </summary>
        /// <returns>The new parcel list</returns>
        public IEnumerable<ParcelToList> GetParcelList()
        {
            lock (dal)
            {
                foreach (DO.Parcel parcel in dal.GetParcelsList().Where(p => p.IsAvailable))
                {
                    yield return ConvertDALParcelToParcelToList(parcel);
                }
            }
        }

        /// <summary>
        /// Convert DO parcel to BO ParcelToList
        /// </summary>
        /// <param name="parcel">The DO parcel.</param>
        /// <returns>The BO parcel.</returns>
        private static ParcelToList ConvertDALParcelToParcelToList(DO.Parcel parcel)
        {
            ParcelToList parcel1 = new()
            {
                Id = parcel.Id,
                Priority = (Priorities)parcel.Priority,
                ReceivesId = parcel.TargetId,
                SenderId = parcel.SenderId,
                Status = ReturnTheStatusOfTheParcel(parcel)
            };
            return parcel1;
        }


        /// <summary>
        /// Creation a list of parcels that have not yet been delivered to the customer based on the base data.
        /// </summary>
        /// <returns>The new parcel list</returns>
        public IEnumerable<ParcelToList> GetParcelsNotRelated()
        {
            return GetParcelsToList((ParcelToList p) => p.Status != ParcelMode.ASSOCIATED);
        }


        /// <summary>
        /// Acceptance of all parcels that meet the condition/s.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<ParcelToList> GetParcelsToList(Predicate<ParcelToList> predicate)
        {
            lock (dal)
            {
                foreach (DO.Parcel parcel in dal.GetParcelsList().Where(p => p.IsAvailable && predicate(ConvertDALParcelToParcelToList(p))))
                {
                    yield return ConvertDALParcelToParcelToList(parcel);
                }
            }
        }

        /// <summary>
        /// Acceptance of all parcels that meet the condition/s.
        /// </summary>
        /// <param name="predicate">The condition/s</param>
        /// <returns>The list (type of parcels) for all the parcels that meet the condition/s.</returns>
        public IEnumerable<Parcel> GetParcelsList(Predicate<DO.Parcel> predicate)
        {
            lock (dal)
            {
                foreach (DO.Parcel parcel in dal.GetParcelsList(predicate).Where(p => p.IsAvailable))
                {
                    yield return ConvertIDALParcelToIBLParcel(parcel);
                }
            }
        }


        /// <summary>
        /// A help function that return the location of parcel
        /// </summary>
        /// <param name="parcel">The parcel that we need to know it location</param>
        /// <returns>The location of the parcel</returns>
        internal Location LocationOfParcel(int senderId)
        {
            return LocationOfSomeone(senderId);
        }

        /// <summary>
        /// A method that connect between a drone to a suit parcel
        /// </summary>
        /// <param name="droneId">The id of the desired drone</param>
        public int ParcelToDrone(int droneId)
        {
            DroneToList d = dronesList.FirstOrDefault(d => d.Id == droneId);
            if (d == default(DroneToList))
            {
                throw new TheObjectIDDoesNotExist("The drone does not exist in the system.");
            }

            if (d.Statuses != DroneStatuses.AVAILABLE)
            {
                throw new DroneCanNotBeSent("Drone can not be sent because the drone status is: " + d.Statuses);
            }

            IEnumerable<Parcel> parcels = GetParcelsList((DO.Parcel p) => (int)p.Weight <= (int)d.Weight && p.DroneId == 0);
            if (!parcels.Any())
            {
                throw new DroneCanNotBeSent("There isnt good parcel.");
            }

            parcels = parcels.OrderBy(p => (int)p.Priority).ThenBy(p => (int)p.Weight).ThenBy(p => Distance(LocationOfParcel(p.Sender.Id), d.Location));
            d.Statuses = DroneStatuses.DALIVERY;
            Parcel p = parcels.LastOrDefault(p => IsEnoughBattery(d, p.Sender.Id, p.Receives.Id, p.Weight));
            d.Statuses = DroneStatuses.AVAILABLE;
            if (p != null)
            {
                d.Statuses = DroneStatuses.DALIVERY;
                d.ParcelId = p.Id;
                try
                {
                    lock (dal)
                    {
                        dal.AssigningAParcelToADrone(p.Id, droneId);
                    }
                }
                catch (DO.TheObjectIDDoesNotExist ex)
                {
                    throw new TheObjectIDDoesNotExist("The parcel id does not exist in the system", ex);
                }
            }
            else
            {
                throw new ThereIsNotEnoughBattery("There is not enough battery");
            }

            return p.Id;
        }

        /// <summary>
        /// A method that the drone collect the parcel that associated it
        /// </summary>
        /// <param name="droneId">The id of desired drone</param>
        public void CollectingPackage(int droneId)
        {
            DroneToList drone = dronesList.FirstOrDefault(d => d.Id == droneId);
            if (drone == default(DroneToList))
            {
                throw new TheObjectIDDoesNotExist("The drone does not exist in the system.");
            }

            Parcel p;
            lock (dal)
            {
                p = ConvertIDALParcelToIBLParcel(dal.GetParcel(drone.ParcelId));
            }

            p.Id = drone.ParcelId;
            lock (dal)
            {
                if (drone.ParcelId == 0 || dal.GetParcel(drone.ParcelId).Scheduled == null)
                {
                    throw new ThisActionIsNotPossible("ItIsNotPossibleToCollectTheParcel");//שויכה??
                }
            }

            Location locationOfParcel = LocationOfSomeone(p.Sender.Id);
            double batteryPower = BatteryConsumption(drone, Distance(drone.Location, locationOfParcel), 0);
            if (batteryPower > drone.Battery)
            {
                throw new ThereIsNotEnoughBattery("ThereIsNotEnoughBattery");
            }

            drone.Battery -= batteryPower;
            drone.Location = locationOfParcel;
            p.CollectingTime = DateTime.Now;
            try
            {
                lock (dal)
                {
                    dal.CollectAParcelByADrone(p.Id, drone.Id);
                }
            }
            catch (DO.TheObjectIDDoesNotExist ex)
            {
                throw new TheObjectIDDoesNotExist("The parcel id does not exist in the system", ex);
            }
        }

        /// <summary>
        /// A method that Delivery of a parcel by drone
        /// </summary>
        /// <param name="droneId">The id of desired drone</param>
        public void ShippingPackage(int droneId)
        {
            DroneToList d = dronesList.FirstOrDefault(d => d.Id == droneId);
            lock (dal)
            {
                if (d == null || d.Statuses != DroneStatuses.DALIVERY || dal.GetParcel(d.ParcelId).PickedUp == null || dal.GetParcel(d.ParcelId).Delivered != null)
                {
                    throw new ThisActionIsNotPossible("It is not possible to shipping this parcel");
                }
            }

            Parcel parcel;
            lock (dal)
            {
                parcel = ConvertIDALParcelToIBLParcel(dal.GetParcel(d.ParcelId));
            }

            Location locationOfParcel = LocationOfSomeone(parcel.Receives.Id);
            double batteryPower = BatteryConsumption(d, Distance(d.Location, locationOfParcel), parcel.Weight);
            if (batteryPower > d.Battery)
            {
                throw new ThereIsNotEnoughBattery("ThereIsNotEnoughBattery");
            }

            d.Battery -= batteryPower;
            d.Location = locationOfParcel;
            d.Statuses = DroneStatuses.AVAILABLE;
            try
            {
                lock (dal)
                {
                    dal.DeliveryOfAParcelToTheDestination(dal.GetParcel(d.ParcelId).Id);
                }
            }
            catch (DO.TheObjectIDDoesNotExist ex)
            {
                throw new TheObjectIDDoesNotExist("The parcel id does not exist in the system", ex);
            }
        }

        /// <summary>
        /// The function Gets id of parcel and convert it to IB DeliveryInTransfer
        /// </summary>
        /// <param name="parcel">parcel id</param>
        /// <returns>the new parcel</returns>
        private DeliveryInTransfer ConvertParcelToIBLDeliveryInTransfer(DO.Parcel parcel)
        {
            Location[] locations = GetLocationOf2Customers(parcel.SenderId, parcel.TargetId);
            DeliveryInTransfer delivery = new()
            {
                Id = parcel.Id,
                Priority = (Priorities)parcel.Priority,
                Weight = (WeightCategory)parcel.Weight,
                CollectLocation = locations[0],
                DeliveryDestinationLocation = locations[1],
                Status = parcel.PickedUp != null,
                TheRecipient = new CustomerInDelivery() { Id = parcel.TargetId, Name = GetNameOfCustomer(parcel.TargetId) },
                TheSender = new CustomerInDelivery() { Id = parcel.SenderId, Name = GetNameOfCustomer(parcel.SenderId) },
            };
            return delivery;
        }

        /// <summary>
        /// The function Gets id of parcel and convert it to IB DeliveryInCustomer
        /// </summary>
        /// <param name="parcel">parcel id</param>
        /// <returns>the new parcel</returns>
        private DeliveryInCustomer ConvertParcelToIBLDeliveryInCustomer(Parcel parcel)
        {
            DeliveryInCustomer delivery = new()
            {
                Id = parcel.Id,
                Priority = parcel.Priority,
                Weight = parcel.Weight,
                CustomerOf = new CustomerInDelivery() { Id = parcel.Receives.Id, Name = GetNameOfCustomer(parcel.Receives.Id) },
            };
            return delivery;
        }

        /// <summary>
        /// Convert IDAL Parcel To IBL Parcel
        /// </summary>
        /// <param name="p">The IDAL Parcel</param>
        /// <returns>The IBL Parcel</returns>
        private Parcel ConvertIDALParcelToIBLParcel(DO.Parcel p)
        {
            Parcel parcel = new()
            {
                Id = p.Id,
                Priority = (Priorities)p.Priority,
                TimeOfCreateDelivery = p.Requested,
                AssignmentTime = p.Scheduled,
                CollectingTime = p.PickedUp,
                SupplyTime = p.Delivered,
                Weight = (WeightCategory)p.Weight,
                Receives = new CustomerInDelivery() { Id = p.TargetId, Name = GetNameOfCustomer(p.TargetId) },
                Sender = new CustomerInDelivery() { Id = p.SenderId, Name = GetNameOfCustomer(p.SenderId) },
                Drone = ReceiptOfTheDroneToWhichTheParcelIsAssociated(p.Id)
            };
            return parcel;
        }

        /// <summary>
        /// The function gets customer id, looks for the parcels he send and the parcels he will get/got
        /// </summary>
        /// <param name="id">customer id</param>
        /// <returns>Arry of the customer's parcels</returns>
        private IEnumerable<DeliveryInCustomer>[] GetDeliveryInCustomersList(int id)
        {
            IEnumerable<DeliveryInCustomer> deliveriesSend = GetParcelsList((DO.Parcel p) => p.SenderId == id).Select((p) => ConvertParcelToIBLDeliveryInCustomer(p));
            IEnumerable<DeliveryInCustomer> deliveriesTarget = GetParcelsList((DO.Parcel p) => p.TargetId == id).Select((p) => ConvertParcelToIBLDeliveryInCustomer(p));
            IEnumerable<DO.Parcel> dalParcel;
            lock (dal)
            {
                dalParcel = dal.GetParcelsList();
            }

            IEnumerable<DeliveryInCustomer>[] deliveries = new IEnumerable<DeliveryInCustomer>[2] { deliveriesSend, deliveriesTarget };
            return deliveries;
        }

        /// <summary>
        /// The function get a parcel and return its status.
        /// </summary>
        /// <param name="parcel">parcel</param>
        /// <returns>THe parcel's status.</returns>
        private static ParcelMode ReturnTheStatusOfTheParcel(DO.Parcel parcel)
        {
            return !parcel.Delivered.Equals(default)
                ? ParcelMode.PROVIDED
                : !parcel.PickedUp.Equals(default)
                ? ParcelMode.COLLECTED
                : !parcel.Scheduled.Equals(default) ? ParcelMode.ASSOCIATED : ParcelMode.DEFINED;
        }
    }
}