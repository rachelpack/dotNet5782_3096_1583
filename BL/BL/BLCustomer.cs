using BO;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    partial class BL
    {

        /// <summary>
        /// A function that receives data checks their correctness and creates an instance of a new customer and add it to the list of customers
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="phone"></param>
        /// <param name="lo"></param>
        /// <param name="la"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddCustomer(int id, string name, int phone, double lo, double la)
        {
            lock (dal)
                if (dal.GetCustomersList().Any(item => item.Id == id))
                {
                    throw new TheObjectIdAlreadyExist("The customer already exist in the system.");
                }

            if (lo is > 90 or < (-90))
            {
                throw new TheValueOutOfRange();
            }

            if (la is < (-180) or > 180)
            {
                throw new TheValueOutOfRange();
            }

            if (phone is < 1000000 or > 999999999)
            {
                throw new TheValueOutOfRange("The phone out of range");
            }

            Customer customer = new()
            {
                Id = id,
                Name = name,
                Phone = phone,
                Location = new Location() { Latitude = la, Longitude = lo },
                ListOfParcelsYouReceived = null,
                ListOfParcelsYouSent = null
            };
            try
            {
                lock (dal)
                    dal.AddCustomer(id, name, phone, lo, la);
            }
            catch (DO.TheObjectIdAlreadyExist ex)
            {
                throw new TheObjectIdAlreadyExist("The customer already exist in the system.", ex);
            }
        }

        /// <summary>
        /// Delete the customer.
        /// </summary>
        /// <param name="id">The id of the customer.</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteCustomer(int id)
        {
            try
            {
                lock (dal)
                    dal.DeleteCustomer(id);
            }
            catch (DO.TheObjectIDDoesNotExist ex)
            {
                throw new TheObjectIDDoesNotExist("The customer does not exist in the system.", ex);
            }

        }

        /// <summary>
        /// A function that create a new cudtomer list and over the original customer list.
        /// </summary>
        /// <returns>The new customer list</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<CustomerToList> GetCustomerList()
        {
            lock (dal)
                foreach (DO.Customer item in dal.GetCustomersList().Where(c => c.IsAvailable))
                {
                    IEnumerable<DO.Parcel> parcels = dal.GetParcelsList().Where(p => p.IsAvailable);
                    yield return new CustomerToList()
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Phone = item.Phone,
                        NumberOfParcelHeReceived = parcels.Count(p => p.TargetId == item.Id),
                        NumberOfParselSentAndDelivered = parcels.Count(p => p.SenderId == item.Id && !p.Delivered.Equals(default)),
                        SeveralPacelOnTheWayToTheCustomer = parcels.Count(p => p.SenderId == item.Id && !p.PickedUp.Equals(default)),
                        NumberOfParcelSentButNotYetDelivered = parcels.Count(p => p.SenderId == item.Id && !p.Scheduled.Equals(default))
                    };
                }
        }

        /// <summary>
        /// The function gets a customer ID, searches for it and returns it.
        /// </summary>
        /// <param name="id">customer id</param>
        /// <returns>The requested customer</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Customer GetCustomer(int id)
        {
            //Finding the desired station
            DO.Customer dalCustomer = new();
            try
            {
                lock (dal)
                {
                    dalCustomer = dal.GetCustomer(id);
                }

                if (!dalCustomer.IsAvailable)
                {
                    throw new TheObjectIDDoesNotExist();
                }
            }
            catch (DO.TheObjectIDDoesNotExist ex)
            {
                throw new TheObjectIDDoesNotExist("The customer does not exist in the system.", ex);
            }
            return ConvertDalCustomerToBlCustomer(dalCustomer);
        }

        /// <summary>
        /// Convert Dal customer to Bl customer
        /// </summary>
        /// <param name="customer">The DO customer</param>
        /// <returns>The Bl customer</returns>
        internal Customer ConvertDalCustomerToBlCustomer(DO.Customer customer)
        {
            IEnumerable<DeliveryInCustomer>[] arr = GetDeliveryInCustomersList(customer.Id);
            return new Customer()
            {
                Id = customer.Id,
                Name = customer.Name,
                Location = new Location() { Latitude = customer.Latitude, Longitude = customer.Longitude },
                Phone = customer.Phone,
                ListOfParcelsYouSent = arr[0],
                ListOfParcelsYouReceived = arr[1]
            };

        }

        /// <summary>
        /// The function updates the customer's name and / or his new phone number
        /// </summary>
        /// <param name="id">The customer's id.</param>
        /// <param name="name">The new customer's name.</param>
        /// <param name="phone">The new customer's phone.</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdatCustomer(int id, string name, int phone)
        {
            try
            {
                lock (dal)
                    dal.ChangeCustomerNameAndPhone(id, name, phone);

            }
            catch (DO.TheObjectIDDoesNotExist ex)
            {
                throw new TheObjectIDDoesNotExist("The customer does not exist in the system.", ex);
            }
        }

        /// <summary>
        /// The fuction get id of customer and return his name.
        /// </summary>
        /// <param name="id">customer id</param>
        /// <returns>customer name</returns>
        private string GetNameOfCustomer(int id)
        {
            lock (dal)
            {
                return dal.GetCustomersList().FirstOrDefault(item => item.Id == id).Name;
            }
        }

    }
}
