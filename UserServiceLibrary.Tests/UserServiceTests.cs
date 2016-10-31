using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace UserServiceLibrary.Tests
{
    [TestFixture]
    public class UserServiceTests
    {
        #region Data for testing
        private User newUser = new User
        {
            FirstName = "Peter",
            LastName = "Jonson",
            DateOfBirth = new DateTime(1995, 3, 14)
        };

        private List<User> users = new List<User>
        {
            new User { FirstName = "Ann", LastName = "Smith", DateOfBirth = new DateTime(2001, 8, 15) },
            new User { FirstName = "John", LastName = "Jonson", DateOfBirth = new DateTime(1985, 4, 10) }
        };

        private UserService service = new UserService();
        #endregion

        [Test]
        [ExpectedException(typeof(InvalidUserException), ExpectedMessage = "User can not be null.")]
        public void Add_NullUser_ExceptionThrown()
        {
            service.AddUser(default(User));
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Add_NullFirstNameUser_ExceptionThrown()
        {
            service.AddUser(new User { FirstName = null });
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Add_NullLastNameUser_ExceptionThrown()
        {
            service.AddUser(new User { LastName = null });
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Add_DefaultDateOfBirthUser_ExceptionThrown()
        {
            service.AddUser(new User { DateOfBirth = default(DateTime) });
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Add_NullFirstAndLastNameUser_ExceptionThrown()
        {
            service.AddUser(new User { FirstName = null, LastName = null });
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Add_NullFirstNameAndDateOfBirthUser_ExceptionThrown()
        {
            service.AddUser(new User { FirstName = null, DateOfBirth = default(DateTime) });
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Add_NullLastNameAndDateOfBirthUser_ExceptionThrown()
        {
            service.AddUser(new User { LastName = null, DateOfBirth = default(DateTime) });
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Add_NullDFirstAndLastNameAndDateOfBirthUser_ExceptionThrown()
        {
            service.AddUser(new User
            {
                FirstName = string.Empty,
                LastName = string.Empty,
                DateOfBirth = default(DateTime)
            });
        }

        [Test]
        public void Add_ValidUser_NoException()
        {
            service.AddUser(newUser);
            Assert.AreEqual("Peter", service.FindUser(usr => usr.LastName == newUser.LastName && usr.DateOfBirth == newUser.DateOfBirth).FirstName);
        }
        
        [Test]
        [ExpectedException(typeof(InvalidUserException), ExpectedMessage = "User can not be null.")]
        public void Delete_NullUser_ExceptionThrown()
        {
            service.DeleteUser(default(User));
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Delete_NullFirstNameUser_ExceptionThrown()
        {
            service.DeleteUser(new User { FirstName = null });
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Delete_NullLastNameUser_ExceptionThrown()
        {
            service.DeleteUser(new User { LastName = null });
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Delete_DefaultDateOfBirthUser_ExceptionThrown()
        {
            service.DeleteUser(new User { DateOfBirth = default(DateTime) });
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Delete_NullFirstAndLastNameUser_ExceptionThrown()
        {
            service.DeleteUser(new User { FirstName = null, LastName = null });
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Delete_NullFirstNameAndDateOfBirthUser_ExceptionThrown()
        {
            service.DeleteUser(new User { FirstName = null, DateOfBirth = default(DateTime) });
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Delete_NullLastNameAndDateOfBirthUser_ExceptionThrown()
        {
            service.DeleteUser(new User { LastName = null, DateOfBirth = default(DateTime) });
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Delete_NullDFirstAndLastNameAndDateOfBirthUser_ExceptionThrown()
        {
            service.DeleteUser(new User
            {
                FirstName = string.Empty,
                LastName = string.Empty,
                DateOfBirth = default(DateTime)
            });
        }

        [Test]
        public void Delete_ValidUser_Expected0()
        {
            service.AddUser(newUser);
            service.DeleteUser(newUser);
            int count = service.FindUsers(usr => usr.LastName == newUser.LastName).Length;
            Assert.AreEqual(count, 0);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FindUsers_Null_ExceptionThrown()
        {
            service = new UserService(users);
            service.FindUsers(null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FindUser_Null_ExceptionThrown()
        {
            service = new UserService(users);
            service.FindUser(null);
        }

        [Test]
        public void FindUsers_ByFirstName_ListOfUsers()
        {
            service = new UserService(users);
            int count = service.FindUsers(usr => usr.FirstName == "Ann").Length;
            Assert.AreEqual(count, 1);
        }

        [Test]
        public void FindUser_ByFirstName_User()
        {
            service = new UserService(users);
            var result = service.FindUser(usr => usr.FirstName == "Ann");
            Assert.AreEqual("Smith", result.LastName);
        }

        [Test]
        public void FindUsers_ByLastName_ListOfUsers()
        {
            int count = service.FindUsers(usr => usr.FirstName == "Ann").Length;
            Assert.AreEqual(count, 1);
        }

        [Test]
        public void SearchUser_ByLastName_User()
        {
            service = new UserService(users);
            var result = service.FindUser(usr => usr.LastName == "Jonson");
            Assert.AreEqual("John", result.FirstName);
        }

        [Test]
        public void SearchAllUsers_ByLastName_Users()
        {
            service = new UserService(users);
            service.AddUser(newUser);
            int count = service.FindUsers(usr => usr.LastName == "Jonson").Length;
            Assert.AreEqual(count, 2);
        }

        [Test]
        public void SearchUser_ByFirstAndLastName_User()
        {
            service.AddUser(newUser);
            int count = service.FindUsers(usr => usr.FirstName == newUser.FirstName && usr.LastName == newUser.LastName).Length;
            Assert.AreEqual(count, 1);
        }

        [Test]
        public void SearchUser_ByFirstLastNameAndDateOfBirth_User()
        {
            service.AddUser(newUser);
            var result = service.FindUser(usr => usr.FirstName == newUser.FirstName && usr.LastName == newUser.LastName && usr.DateOfBirth == newUser.DateOfBirth);
            Assert.AreEqual(result, newUser);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void StoreUsers_NoUsers_ExceptionThrown()
        {
            var storage = Mock.Of<XmlUserListStorage>();
            service.AddUser(newUser);
            UserService.StoreUsers(storage, null);
        }

        [Test]
        public void StoreUsers_OneUser_ExceptionThrown()
        {
            var storage = Mock.Of<XmlUserListStorage>();
            service.AddUser(newUser);
            UserService.StoreUsers(storage, service.UsersList);
        }
    }
}
