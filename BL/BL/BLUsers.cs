using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    partial class BL
    {
        /// <summary>
        /// ConvertDO user to BO user.
        /// </summary>
        /// <param name="user">The DO user.</param>
        /// <returns>The BO user.</returns>
        private static User ConvertDALUserToBLUser(DO.User user)
        {
            return new User() { Password = user.Password, UserName = user.UserName };
        }

        /// <summary>
        /// Convert BO user to DO user.
        /// </summary>
        /// <param name="user">The BO user.</param>
        /// <returns>The DO user.</returns>
        private static DO.User ConvertBLUserToDALUser(User user)
        {
            return new DO.User() { Password = user.Password, UserName = user.UserName };
        }

        /// <summary>
        /// Get all the users in the system.
        /// </summary>
        /// <returns>All the users</returns>
        public IEnumerable<User> GetAllTheUsers()
        {
            lock (dal)
            {
                return dal.GetAllTheUsers().Select(u => ConvertDALUserToBLUser(u));
            }
        }

        /// <summary>
        /// Get the manager of the system.
        /// </summary>
        /// <returns>The manager.</returns>
        public User GetManager()
        {
            lock (dal)
            {
                return ConvertDALUserToBLUser(dal.GetManager());
            }
        }

        /// <summary>
        /// Add a new user to the system.
        /// </summary>
        /// <param name="user">The user to adding.</param>
        public void AddUser(User user)
        {
            IEnumerable<DO.User> users;
            User manager;
            lock (dal)
            {
                users = dal.GetAllTheUsers();
                manager = ConvertDALUserToBLUser(dal.GetManager());
            }
            if (users.Any(u => u.Password == user.Password && u.UserName == user.UserName) || (manager.Password == user.Password && manager.UserName == user.UserName))
            {
                throw new TheObjectIdAlreadyExist("The user already exist in the system");
            }
            try
            {
                lock (dal)
                {
                    dal.AddUser(ConvertBLUserToDALUser(user));
                }
            }
            catch (DO.TheObjectIdAlreadyExist ex)
            {
                throw new TheObjectIdAlreadyExist("The user already exist in the system", ex);
            }
            catch (DO.TheObjectIDDoesNotExist ex)
            {
                throw new TheObjectIDDoesNotExist("The user doesnt exist in the system.", ex);
            }
        }

        /// <summary>
        /// Delete a user from the system.
        /// </summary>
        /// <param name="user">The user to deleting</param>
        public void DeleteUser(User user)
        {
            try
            {
                lock (dal)
                {
                    dal.DeleteUser(ConvertBLUserToDALUser(user));
                }
            }
            catch (DO.TheObjectIDDoesNotExist ex)
            {
                throw new TheObjectIDDoesNotExist("The user doesnt exist in the system.", ex);
            }
        }
        /// <summary>
        /// Update user information
        /// </summary>
        /// <param name="oldUser">The old details of the user</param>
        /// <param name="newUser">The new details of the user</param>
        public void UpDateUser(User oldUser, User newUser)
        {
            lock (dal)
            {
                IEnumerable<DO.User> users = dal.GetAllTheUsers();
                if (!users.Any(u => u.Password == oldUser.Password && u.UserName == oldUser.UserName))
                {
                    throw new TheObjectIDDoesNotExist("The user doesnt exist in the system.");
                }
                if (users.Any(u => oldUser.UserName != newUser.UserName && u.UserName == newUser.UserName))
                {
                    throw new TheObjectIdAlreadyExist("The username already appears in the system");
                }
                try
                {
                    dal.UpDateUser(ConvertBLUserToDALUser(oldUser), ConvertBLUserToDALUser(newUser));
                }
                catch (DO.TheObjectIDDoesNotExist ex)
                {
                    throw new TheObjectIDDoesNotExist("The user doesnt exist in the system", ex);
                }
            }
        }

        /// <summary>
        /// Checks whether the user is an administrator
        /// </summary>
        /// <param name="user">The user to checking.</param>
        /// <returns>True if the user is the administrator and otherwise false</returns>
        public bool IsThisTheManager(User user)
        {
            User manager = ConvertDALUserToBLUser(dal.GetManager());
            return manager.Password == user.Password && manager.UserName == user.UserName;
        }
        /// <summary>
        /// Check if the user exists in the system
        /// </summary>
        /// <param name="user">The user to checking.</param>
        /// <returns>True if the user exists otherwise false.</returns>
        public bool DoesUserExistInTheSystem(User user)
        {
            IEnumerable<User> users = GetAllTheUsers();
            User managerUser = GetManager();
            return users.Any(tempUser => tempUser.Password == user.Password && tempUser.UserName == user.UserName) && !IsThisTheManager(user);
        }

    }
}
