using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    internal sealed partial class DalObject
    {

        /// <summary>
        /// Add a new user to the system.
        /// </summary>
        /// <param name="user">The user to adding.</param>
        public void AddUser(User user)
        {
            if (DataSource.Users.Exists(u => u.UserName == user.UserName))
            {
                throw new TheObjectIdAlreadyExist("The user already exist in the system");
            }
            if (!DataSource.Customers.Exists(c => c.IsAvailable && c.Id == int.Parse(user.UserName)))
            {
                throw new TheObjectIDDoesNotExist("The user doesnt exist in the system");
            }

            DataSource.Users.Add(user);
        }


        /// <summary>
        /// Delete a user from the system.
        /// </summary>
        /// <param name="user">The user to deleting</param>
        public void DeleteUser(User user)
        {
            if (!DataSource.Users.Remove(user))
            {
                throw new TheObjectIDDoesNotExist("The user doesnt exist in the system");
            }
        }

        /// <summary>
        /// Get all the users in the system.
        /// </summary>
        /// <returns>All the users</returns>
        public IEnumerable<User> GetAllTheUsers()
        {
            return DataSource.Users;
        }

        /// <summary>
        /// Get the manager of the system.
        /// </summary>
        /// <returns>The manager.</returns>
        public User GetManager()
        {
            return DataSource.Manager;
        }


        /// <summary>
        /// Update user information
        /// </summary>
        /// <param name="oldUser">The old details of the user</param>
        /// <param name="newUser">The new details of the user</param>
        public void UpDateUser(User oldUser, User newUser)
        {
            if (!DataSource.Users.Exists(u => u.UserName == oldUser.UserName && u.Password == oldUser.Password))
            {
                throw new TheObjectIDDoesNotExist("The user doesnt exist in the system");
            }
            DataSource.Users.Add(newUser);
        }


    }
}
