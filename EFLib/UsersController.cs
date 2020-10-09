using EFProject;
using System;
using System.Linq;

namespace EFLib
{
    public class UsersController
    {

        private readonly prsContext _context;
        public UsersController(prsContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Returns a user is username and password are found in the 
        /// users table of the database
        /// </summary>
        /// <param name="username">usename as a string</param>
        /// <param name="password">password as a string</param>
        /// <returns>
        /// a user instance if the username and password combination 
        /// is found. Else return null
        /// </returns>
        public Users Login(string username, string password)
        {
           return _context.Users
                .SingleOrDefault(u => u.UserName == username && u.Password == password);
        }//SingleOrDefault says it will only find one instance of that property
    }
}
