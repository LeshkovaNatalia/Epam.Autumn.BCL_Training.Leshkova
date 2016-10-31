using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace UserServiceLibrary
{
    public class XmlUserListStorage : IUserServiceStorage
    {
        #region Fields
        private string fileName;
        #endregion

        #region Property
        public string FileName
        {
            get
            {
                return fileName;
            }

            private set
            {
                if (value != null)
                {
                    fileName = value;
                }
                else
                {
                    throw new ArgumentNullException(nameof(value));
                }
            }
        }
        #endregion

        #region Ctors
        /// <summary>
        /// Initializes a new instance of the <see cref="XmlUserListStorage"/> class.
        /// </summary>
        public XmlUserListStorage()
        {
        }

        public XmlUserListStorage(string path)
        {
            FileName = path;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Method SaveUsers save list of users to xml document.
        /// </summary>
        /// <param name="users">List of books.</param>
        public void SaveUsers(List<User> users)
        {
            XDocument xDoc = new XDocument(new XElement("Users"));
            foreach (var user in users)
            {
                xDoc.Element("Users").Add(
                    new XElement(
                        "User", 
                        new XElement("Id", user.Id),
                        new XElement("FirstName", user.FirstName),
                        new XElement("LastName", user.LastName),
                        new XElement("DateOfBirth", user.DateOfBirth.ToShortDateString())));
            }

            if (FileName != null)
            {
                xDoc.Save(FileName);
            }
            else
            {
                xDoc.Save("Users.xml");
            }
        }
        #endregion
    }
}
