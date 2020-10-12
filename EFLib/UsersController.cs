using EFProject;
using System;
using System.Linq;

namespace EFLib
{
    public class UsersController
    {
        //property type prsContext class with property_context
        private readonly prsContext _context;
        //construct pass in type/parameter
        public UsersController(prsContext context)
        {
            //set property = parameter
            _context = context;//or can use this._context = context. 
            //The only time you need this. is if property and parameter have same name
        }

        //METHOD
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
            //entity framework alwas has _context.tables (whatever table you wanted in your database)
            //when we want to read or update data u is an instance of a user
            //we can then reference that user later in code by saying u.xxx
           return _context.Users
                .SingleOrDefault(u => u.UserName == username && u.Password == password);
        }//SingleOrDefault says it will only find one instance of that property
        //can't use .Find() b/c it only searches for PK
    }
}
