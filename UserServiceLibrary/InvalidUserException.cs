using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserServiceLibrary
{
    public class InvalidUserException : Exception
    {
        #region Ctors
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidUserException"/> class.
        /// </summary>
        public InvalidUserException() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidUserException"/> class.
        /// </summary>
        /// <param name="message">Description of exception.</param>
        public InvalidUserException(string message) : base(message)
        {
        }
        #endregion
    }
}
