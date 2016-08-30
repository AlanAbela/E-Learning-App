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
        #region Properties
        UserDL userDL { get; set;}
        #endregion

        #region Constructors
        public RegisterBL()
        {
            userDL = new UserDL();
        }
        #endregion

        #region Methods
        /// <summary>
        /// RegisterBL user.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public void RegisterUser(string username, string password)
        {
           // Remove trailing and bailing empty spaces if any.
           username = username.Trim();
            userDL.Username = username;
            userDL.Password = password;

            // RegisterBL user.
            userDL.RegisterUser();
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