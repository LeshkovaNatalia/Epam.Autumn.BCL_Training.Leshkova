using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserServiceLibrary
{
    public class UserService
    {
        #region Fields
        private List<User> usersList;
        #endregion

        #region Properties
        public List<User> UsersList
        {
            get
            {
                return usersList;
            }

            private set
            {
                if (value != null)
                {
                    usersList = new List<User>(value);
                }
                else
                {
                    throw new InvalidUserException("List of users can not be null.");
                }
            }
        }

        #endregion

        #region Ctors
        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        public UserService()
        {
            UsersList = new List<User>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="users">List of users.</param>
        public UserService(List<User> users)
        {
            UsersList = users;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Method add new user to users.
        /// </summary>
        /// <param name="user">Added user.</param>
        /// <exception cref="InvalidUserException">Thrown when added user is null.</exception>
        /// <exception cref="System.ArgumentNullException">Thrown when added user FirstName and/or 
        /// LastName is null and/or DateOfBirth is default date.</exception>
        public void AddUser(User user)
        {
            CheckUserData(user);

            if (!UserExist(user))
            {
                UsersList.Add(user);
            }   
        }

        /// <summary>
        /// Method delete user from users.
        /// </summary>
        /// <param name="user">Deleted user.</param>
        /// <exception cref="InvalidUserException">Thrown when deleted user is null.</exception>
        /// <exception cref="System.ArgumentNullException">Thrown when deleted user FirstName, 
        /// LastName is null and DateOfBirth is default date.</exception>
        public void DeleteUser(User user)
        {
            CheckUserData(user);

            if (UserExist(user))
            {
                UsersList.Remove(user);
            }
        }

        /// <summary>
        /// Method find user by predicate. 
        /// </summary>
        /// <param name="p">Condition of searched user.</param>
        /// <returns>Array of find users.</returns>
        public User[] FindUsers(Predicate<User> p)
        {
            if (p == null)
            {
                throw new ArgumentNullException(nameof(p));
            }

            var findUsers = UsersList.FindAll(p);
            return findUsers.ToArray();
        }

        /// <summary>
        /// Method find user by predicate. 
        /// </summary>
        /// <param name="p">Condition of searched user.</param>
        /// <returns>Find user.</returns>
        public User FindUser(Predicate<User> p)
        {
            if (p == null)
            {
                throw new ArgumentNullException(nameof(p));
            }

            var findUser = UsersList.Find(p);
            return findUser;
        }

        /// <summary>
        /// Method StoreUsers for storing list of users.
        /// </summary>
        /// <param name="userStorage">Storage parameter.</param>
        /// <param name="users">List of users.</param>
        public static void StoreUsers(IUserServiceStorage userStorage, List<User> users)
        {
            if (users == null)
            {
                throw new ArgumentNullException();
            }

            userStorage.SaveUsers(users);
        }

        #endregion

        #region Private Methods
        /// <summary>
        /// Method checks the validity of user data.
        /// </summary>
        /// <param name="user">Checked user.</param>
        private void CheckUserData(User user)
        {
            if (user == null)
            {
                throw new InvalidUserException("User can not be null.");
            }

            if (string.IsNullOrEmpty(user.FirstName))
            {
                throw new ArgumentNullException(nameof(user.FirstName));
            }

            if (string.IsNullOrEmpty(user.LastName))
            {
                throw new ArgumentNullException(nameof(user.LastName));
            }

            if (user.DateOfBirth == default(DateTime))
            {
                throw new ArgumentNullException(nameof(user.DateOfBirth));
            }
        }

        /// <summary>
        /// Method check if user exist in users list.
        /// </summary>
        /// <param name="user">Checked user.</param>
        /// <returns>True if user exist and false if not.</returns>
        private bool UserExist(User user)
        {
            if (FindUser(usr => usr.FirstName == user.FirstName && usr.LastName == user.LastName && usr.DateOfBirth == user.DateOfBirth) != null)
            {
                return true;
            }

            return false;
        }
        #endregion
    }
}
