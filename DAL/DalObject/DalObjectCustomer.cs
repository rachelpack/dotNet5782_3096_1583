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
        /// A function that receives data checks their correctness and creates an instance of a new customer and add it to the list of customer
        /// </summary>
        /// <param name="id">The id of the new customer</param>
        /// <param name="name">The name of the new customer</param>
        /// <param name="phone">The phone number of the new customer</param>
        /// <param name="longitude">The longitude of the new customer</param>
        /// <param name="latitude">The latitude of the new customer</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddCustomer(int id, string name, int phone, double longitude, double latitude)
        {
            if (DataSource.Customers.Exists((item) => item.Id == id))
                throw new TheObjectIdAlreadyExist("The customer already exist in the system.");
            Customer customer = new()
            {
                Id = id,
                Name = name,
                Phone = phone,
                Latitude = latitude,
                Longitude = longitude,
                IsAvailable = true
            };
            DataSource.Customers.Add(customer);
        }

        /// <summary>
        /// Delete a customer.
        /// </summary>
        /// <param name="id">The customer id to delete.</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteCustomer(int id)
        {
            Customer customer = DataSource.Customers.FirstOrDefault(c => c.Id == id && c.IsAvailable);
            if (customer.Equals(default(Customer)))
            {
                throw new TheObjectIDDoesNotExist("The customer does not exist in the system.");
            }

            DataSource.Customers.Remove(customer);
            customer.IsAvailable = false;
            DataSource.Customers.Add(customer);
        }



        /// <summary>
        /// Returns all customers
        /// </summary>
        /// <returns>All the customers.</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Customer> GetCustomersList()
        {
            return DataSource.Customers.Where(customer => customer.IsAvailable);
        }

        /// <summary>
        /// The function gets a customer ID, searches for it and returns it.
        /// </summary>
        /// <param name="id">customer id</param>
        /// <returns>The customer</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Customer GetCustomer(int id)
        {
            IEnumerable<Customer> customers = GetCustomersList();
            Customer customer = customers.FirstOrDefault(d => d.Id == id);
            return customer.Equals(default(Customer))
                ? throw new TheObjectIDDoesNotExist("The customer does not exist in the system.")
                : customer;
        }


        /// <summary>
        /// The function updates the customer's name and / or his new phone number
        /// </summary>
        /// <param name="id">The customer's id.</param>
        /// <param name="name">The new customer's name.</param>
        /// <param name="phone">The new customer's phone.</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void ChangeCustomerNameAndPhone(int id, string name, int phone)
        {
            Customer customer;
            try
            {
                customer = DataSource.Customers.First((item) => item.Id == id && item.IsAvailable);
            }
            catch (ArgumentNullException)
            {
                throw new TheObjectIDDoesNotExist("The customer does not exist in the system.");
            }

            DataSource.Customers.Remove(customer);
            if (name != "0")
            {
                customer.Name = name;
            }

            if (phone != 0)
            {
                customer.Phone = phone;
            }

            DataSource.Customers.Add(customer);
        }

    }
}
