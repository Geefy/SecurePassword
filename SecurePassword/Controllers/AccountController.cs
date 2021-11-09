using System;
using System.Collections.Generic;
using System.Text;

namespace SecurePassword
{
    public class AccountController
    {
        DatabaseRepository db = new DatabaseRepository();
        PBKDF2 pBKDF2 = new PBKDF2();
        int numberOfRounds = 10000;

        /// <summary>
        /// Calls the hashing password method to hash the users password and afterwards calls the database create account method to insert the user into the db
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public string CreateAccount(string username, string password)
        {
            try
            {
                string salt = Convert.ToBase64String(pBKDF2.CreateSalt());
                string hashedPassword = Convert.ToBase64String(pBKDF2.PasswordHasher(password, Encoding.UTF8.GetBytes(salt), numberOfRounds));

                db.CreateAccount(username, hashedPassword, salt, numberOfRounds);
                return "account succefully created";

            }
            catch (Exception ex)
            {
                return "something went wrong in account creation " + ex.ToString();
                throw ex;
            }
        }
        /// <summary>
        /// Tries to log in the user out from password and username
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public string Login(string username, string password)
        {
            try
            {
                UserModel userObj = db.FindAccountByUsername(username);
                if (userObj.NumberOfLogin < 5)
                {
                //Hashes the parameter password and checks if its the same as the one in the database
                string testHashedPass = Convert.ToBase64String(pBKDF2.PasswordHasher(password, Encoding.UTF8.GetBytes(userObj.Salt), userObj.NumberOfRounds));
                    if (testHashedPass == userObj.HashedPassword)
                    {
                        //Calls the database succeful login method
                        db.SuccesfulLogin(userObj.Username);
                        return "You are succefully logged in";
                    }
                    else
                    {
                        //calls the database failed login method
                        db.FailedLogin(userObj.Username, userObj.NumberOfLogin + 1);
                        return "Password was incorrect";
                    }
                }
                else
                {
                    //Code to notify user would be called here

                    return "You have tried to login more than 5 times, user will be notified";
                }
            }
            catch (Exception)
            {
                return "No user with that name found";
                throw;
            }
        }
    }
}
