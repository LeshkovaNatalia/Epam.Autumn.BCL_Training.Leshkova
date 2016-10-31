using System;
using System.Globalization;
using System.Configuration;

namespace UserServiceLibrary
{
    public class User
    {
        #region Fields
        private static int stepId;
        private static int counterId;
        private readonly int instanceId;
        #endregion

        #region Properties
        public int Id
        {
            get { return instanceId; }
        }

        public string FirstName { get; set; }

        public string LastName { get; set; } 
               
        public DateTime DateOfBirth { get; set; }
        #endregion

        #region Ctors
        /// <summary>
        /// Initializes static members of the <see cref="User"/> class.
        /// </summary>
        static User()
        {
            try
            {
                stepId = int.Parse(ConfigurationManager.AppSettings["step"], CultureInfo.InvariantCulture);
            }
            catch (ConfigurationErrorsException)
            {
                throw new ArgumentNullException(nameof(stepId));
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        public User()
        {
            instanceId = (stepId * counterId) + 1; 
            counterId++;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        /// <param name="name">First name of user.</param>
        /// <param name="surname">Last name of user.</param>
        /// <param name="birthday">Date of birth.</param>
        public User(string name, string surname, DateTime birthday)
        {
            FirstName = name;
            LastName = surname;
            DateOfBirth = birthday;            
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Override Object method ToString()
        /// </summary>
        /// <returns>Data of user in string format.</returns>
        public override string ToString()
        {
            return $"User id: {Id}. {FirstName} {LastName}. Date of birth: {DateOfBirth.ToShortDateString()}";
        }
        #endregion
    }
}
