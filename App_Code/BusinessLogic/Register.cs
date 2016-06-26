using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace BusinessLogic
{
    /// <summary>
    /// Summary description for RegisterBL
    /// </summary>
    public class RegisterBL
    {
        #region Constructors
        public RegisterBL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #endregion

        /// <summary>
        /// RegisterBL user.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        #region Static methods
        public void RegisterUser(string username, string password)
        {
           // Remove trailing and bailing empty spaces if any.
           username = username.Trim();
            UserDL user = new UserDL();
            user.Username = username;
            user.Password = password;

            // RegisterBL user.
            user.RegisterUser();
        }

        /// <summary>
        /// Returns the number of duplicate usernames if any.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static int DuplicateUser(string username)
        {
            return UserDL.DuplicateUser(username);
        }
        #endregion
    }
}