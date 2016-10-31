using System;
using System.Collections.Generic;
using System.Linq;
using UserServiceLibrary;

namespace ServiceApplication
{
    public class UserServiceUI
    {
        public static void Main(string[] args)
        {
            List<User> users = new List<User>
            {
                new User { FirstName = "Ann", LastName = "Smith", DateOfBirth = new DateTime(2001, 8, 15) },
                new User { FirstName = "John", LastName = "Jonson", DateOfBirth = new DateTime(1985, 4, 10) }
            };
            var service = new UserService(users);
            User newUser = new User
            {
                FirstName = "Peter",
                LastName = "Jonson",
                DateOfBirth = new DateTime(1995, 3, 14)
            };
            Console.WriteLine(Environment.NewLine + "---- List of users not modified ----" + Environment.NewLine);
            foreach (var u in users)
            {
                Console.WriteLine(u);
            }

            // 1. Add a new user to the storage.
            service.AddUser(newUser);
            Console.WriteLine(Environment.NewLine + "---- List of users: add new user Peter Jonson ----" + Environment.NewLine);
            foreach (var u in service.UsersList)
            {
                Console.WriteLine(u);
            }

            // 2. Remove an user from the storage.
            service.DeleteUser(users.First(usr => usr.LastName == "Orlova"));
            Console.WriteLine(Environment.NewLine + "---- List of users: delete user Orlova ----" + Environment.NewLine);
            foreach (var u in service.UsersList)
            {
                Console.WriteLine(u);
            }

            // 3. Search for an user by the first name.
            Console.WriteLine(Environment.NewLine + "---- Find user by the first name John ----" + Environment.NewLine);
            var user = service.FindUser(usr => usr.FirstName == "John");
            Console.WriteLine(user);

            // 4. Search for an user by the last name.
            Console.WriteLine(Environment.NewLine + "---- Find users by the last name Jonson ----" + Environment.NewLine);
            var usrs = service.FindUsers(usr => usr.LastName == "Jonson");
            foreach (var u in usrs)
            {
                Console.WriteLine(u);
            }

            // XmlUserListStorage
            UserService.StoreUsers(new XmlUserListStorage("users.xml"), service.UsersList);
            Console.WriteLine(Environment.NewLine + "---- List of users stored in file users.xml ----" + Environment.NewLine);

            Console.ReadLine();
        }
    }
}
