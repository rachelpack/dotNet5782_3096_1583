using DlApi;
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
        /// The function adds a new customer to the system.
        /// </summary>
        /// <param name="id">The new customer's id.</param>
        /// <param name="name">The new customer's name.</param>
        /// <param name="phone">The new customer's phone.</param>
        /// <param name="longitude">The new customer's longitude.</param>
        /// <param name="latitude">The new customer's latitude.</param>
        public void AddCustomer(int id, string name, int phone, double longitude, double latitude)
        {
            Customer customer = new Customer()
            {
                Id = id,
                Name = name,
                Phone = phone,
                Longitude = longitude,
                Latitude = latitude,
                IsAvailable = true
            };
            List<Customer> customers = XMLTools.LoadListFromXmlSerializer<Customer>(CUSTOMERPATH);
            customers.Add(customer);
            XMLTools.SaveListToXmlSerializer(customers, CUSTOMERPATH);
        }

        /// <summary>
        /// The function deletes a drone from the system.
        /// </summary>
        /// <param name="id">he customer's id you wont to delete.</param>
        public void DeleteCustomer(int id)
        {
            try
            {
                List<Customer> customers = XMLTools.LoadListFromXmlSerializer<Customer>(CUSTOMERPATH);
                // Customer customer = GetCustomer(id);
                Customer customer = customers.SingleOrDefault(c => c.Id == id && c.IsAvailable);
                customers.Remove(customer);
                customer.IsAvailable = false;
                customers.Add(customer);
                XMLTools.SaveListToXmlSerializer(customers, CUSTOMERPATH);
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// The function returns a customer.
        /// </summary>
        /// <param name="id">The customer's id you wont.</param>
        /// <returns>The customer.</returns>
        public Customer GetCustomer(int id)
        {
            try
            {
                return XMLTools.LoadListFromXmlSerializer<Customer>(CUSTOMERPATH).SingleOrDefault(customer => customer.Id == id);
            }
            catch (InvalidOperationException ex)
            {
                throw new TheObjectIDDoesNotExist("The customer doesnt exist in the system or their is no customers in the system", ex);
            }
            catch (ArgumentNullException)
            {
                throw;
            }
        }

        /// <summary>
        /// The function returns all the customers list.
        /// </summary>
        /// <returns>The customers' list.</returns>
        public IEnumerable<Customer> GetCustomersList()
        {
            return XMLTools.LoadListFromXmlSerializer<Customer>(CUSTOMERPATH);
        }

        /// <summary>
        /// The function changes the name ond/or the phone of the customer.
        /// </summary>
        /// <param name="id">The customer's id.</param>
        /// <param name="name">The new customer's name.</param>
        /// <param name="phone">The new customer's phone.</param>
        public void ChangeCustomerNameAndPhone(int id, string name, int phone)
        {
            try
            {
                List<Customer> customers = XMLTools.LoadListFromXmlSerializer<Customer>(CUSTOMERPATH);
                //Customer customer = GetCustomer(id);
                Customer customer = customers.SingleOrDefault(c => c.Id == id);
                customers.Remove(customer);
                if (name != "0") customer.Name = name;
                if (phone != 0) customer.Phone = phone;
                customers.Add(customer);
                XMLTools.SaveListToXmlSerializer<Customer>(customers, CUSTOMERPATH);
            }
            catch (Exception)
            {

                throw;
            }

        }

    }
}
