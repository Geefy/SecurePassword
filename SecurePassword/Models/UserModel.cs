using System;
using System.Collections.Generic;
using System.Text;

namespace SecurePassword
{
    /// <summary>
    /// This class is just for data transfering that mirrors the user in the database.
    /// </summary>
    public class UserModel
    {
        public string Username;
        public string HashedPassword;
        public string Salt;
        public int NumberOfRounds;
        public int NumberOfLogin;
    }
}
