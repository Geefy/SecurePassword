using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SecurePassword
{
    public class DatabaseRepository
    {
        static string connectionString = "server=127.0.0.1;uid=root;pwd=root;database=SecurePassword";
        private MySqlConnection conn = new MySqlConnection(connectionString);
        private MySqlCommand cmd;


        /// <summary>
        /// Creates a user account in the database
        /// </summary>
        /// <param name="username"></param>
        /// <param name="hashedPassword"></param>
        /// <param name="salt"></param>
        /// <param name="numberOfRounds"></param>
        public void CreateAccount(string username, string hashedPassword, string salt, int numberOfRounds)
        {
            string sqlQuery = "INSERT INTO Password(Username, HashedPassword, Salt, NumberOfRounds, NumberOfLogin) VALUES (@Username, @HashedPassword, @Salt, @NumberOfRounds, @NumberOfLogin);";
            try
            {
                conn.Open();
                cmd = new MySqlCommand(sqlQuery, conn);
                cmd.Parameters.AddWithValue("@UserName", username);
                cmd.Parameters.AddWithValue("@HashedPassword", hashedPassword);
                cmd.Parameters.AddWithValue("@Salt", salt);
                cmd.Parameters.AddWithValue("@NumberOfRounds", numberOfRounds);
                cmd.Parameters.AddWithValue("@NumberOfLogin", 0);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Fins a user from a username
        /// </summary>
        /// <param name="username"></param>
        /// <returns>A user model</returns>
        public UserModel FindAccountByUsername(string username)
        {
            string sqlQuery = "Select * from Password WHERE Username = @Username;";
            UserModel userObj = new UserModel();
            try
            {
                conn.Open();
                cmd = new MySqlCommand(sqlQuery, conn);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.ExecuteNonQuery();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow row in dt.Rows)
                {
                    userObj.Username = row["Username"].ToString();
                    userObj.HashedPassword = row["HashedPassword"].ToString();
                    userObj.Salt = row["Salt"].ToString();
                    userObj.NumberOfRounds = (int)row["NumberOfRounds"];
                    userObj.NumberOfLogin = (int)row["NumberOfLogin"];
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return userObj;
        }

        /// <summary>
        /// Updates the number of login attempts
        /// </summary>
        /// <param name="username"></param>
        /// <param name="newNumberOfLogin"></param>
        public void FailedLogin(string username, int newNumberOfLogin)
        {
            string sqlQuery = "UPDATE Password SET NumberOfLogin = @NewNumberOfLogin WHERE Username = @UserName;";
            try
            {
                conn.Open();
                cmd = new MySqlCommand(sqlQuery, conn);
                cmd.Parameters.AddWithValue("@NewNumberOfLogin", newNumberOfLogin);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Resets the numbers of login tries to 0
        /// </summary>
        /// <param name="username"></param>
        public void SuccesfulLogin(string username)
        {
            string sqlQuery = "UPDATE Password SET NumberOfLogin = @NewNumberOfLogin WHERE Username = @UserName;";
            try
            {
                conn.Open();
                cmd = new MySqlCommand(sqlQuery, conn);
                cmd.Parameters.AddWithValue("@NewNumberOfLogin", 0);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
