using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DO;
namespace Dal
{
    partial class DalXml
    {

        /// <summary>
        /// convert user object to XElement object.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private XElement GetUserElement(User user)
        {
            XElement element = new("User");
            XAttribute attribute = new("Password", user.Password);
            XAttribute attribute1 = new("UserName", user.UserName);
            element.Add(attribute, attribute1);
            return element;
        }

        /// <summary>
        /// Get all the users in the system.
        /// </summary>
        /// <returns>All the users</returns>
        public IEnumerable<User> GetAllTheUsers()
        {
            XElement root;
            try
            {
                root = XMLTools.LoadListFromXmlElement(USERSPATH);
            }
            catch (DirectoryNotFoundException)
            {
                throw;
            }
            return root.Element("AllTheUsers")
                .Elements("User")
                .Select(e => new User() { Password = e.Attribute("Password").Value, UserName = e.Attribute("UserName").Value });
        }

        /// <summary>
        /// Get the manager of the system.
        /// </summary>
        /// <returns>The manager.</returns>
        public User GetManager()
        {
            XElement root;
            try
            {
                root = XMLTools.LoadListFromXmlElement(USERSPATH);
            }
            catch (DirectoryNotFoundException)
            {
                throw;
            }
            XElement xElement = root.Element("Manager");
            User user = new() { Password = xElement.Attribute("Password").Value, UserName = xElement.Attribute("UserName").Value };
            return user;
        }


        /// <summary>
        /// Add a new user to the system.
        /// </summary>
        /// <param name="user">The user to adding.</param>
        public void AddUser(User user)
        {
            XElement root;
            List<Customer> customers;
            try
            {
                customers = XMLTools.LoadListFromXmlSerializer<Customer>(CUSTOMERPATH);
                root = XMLTools.LoadListFromXmlElement(USERSPATH);
            }
            catch (DirectoryNotFoundException)
            {
                throw;
            }
            if (!customers.Any(c => c.Id.ToString() == user.UserName))
            {
                throw new TheObjectIDDoesNotExist("The user doesnt exist in the system");
            }

            if (root.Element("AllTheUsers")
                .Elements("User").Any(u => u.Attribute("UserName").Value == user.UserName))
            {
                throw new TheObjectIdAlreadyExist("The user already exist in the system");
            }


            root.Element("AllTheUsers").Add(GetUserElement(user));
            XMLTools.SaveListToXmlElement(root, USERSPATH);
        }

        /// <summary>
        /// Delete a user from the system.
        /// </summary>
        /// <param name="user">The user to deleting</param>
        public void DeleteUser(User user)
        {
            XElement root;
            try
            {
                root = XMLTools.LoadListFromXmlElement(USERSPATH);
            }
            catch (DirectoryNotFoundException)
            {
                throw;
            }
            try
            {
                XMLTools.SaveListToXmlElement(root, USERSPATH);
            }
            catch (DirectoryNotFoundException)
            {
                throw;
            }
        }
        /// <summary>
        /// Update user information
        /// </summary>
        /// <param name="oldUser">The old details of the user</param>
        /// <param name="newUser">The new details of the user</param>
        public void UpDateUser(User oldUser, User newUser)
        {
            XElement root;
            try
            {
                root = XMLTools.LoadListFromXmlElement(USERSPATH);
            }
            catch (DirectoryNotFoundException)
            {
                throw;
            }
            root.Element("AllTheUsers").Elements().Single(e => e.Attribute("UserName").Value == oldUser.UserName).Remove();
            root.Element("AllTheUsers").Add(GetUserElement(newUser));
            XMLTools.SaveListToXmlElement(root, USERSPATH);
        }

    }
}
