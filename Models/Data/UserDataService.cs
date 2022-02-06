using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PositionPortal.Models.Data
{
    public class UserDataService
    {
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=PositionPortal;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        /// <summary>
        /// Check if user exists in USER db table
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public User Authenticate(User user)
        {
            User res = null;

            string sqlStatement = "SELECT * FROM dbo.[USER] WHERE EMAIL = @email and PASSWORD = @password";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                command.Parameters.Add("@email", System.Data.SqlDbType.NVarChar, 45).Value = user.Email;
                command.Parameters.Add("@password", System.Data.SqlDbType.NVarChar, 45).Value = user.Password;

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        res = new User((int)reader["ID"], (string)reader["EMAIL"], (string)reader["PASSWORD"], (string)reader["FIRST_NAME"]);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
            return res;
        }

        public bool DeleteUser(int id)
        {
            bool success = false;

            string sqlStatement = "DELETE FROM dbo.[USER] WHERE ID = @id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;

                try
                {
                    connection.Open();
                    if (command.ExecuteNonQuery() > 0)
                        success = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
            return success;
        }

        /// <summary>
        /// Insert record into db table
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool Insert(User user)
        {
            bool success = false;
            string sqlStatement = "INSERT INTO dbo.[USER] (EMAIL, PASSWORD, FIRSt_NAME) VALUES (@email, @password, @firstname)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                command.Parameters.Add("@email", System.Data.SqlDbType.VarChar, 45).Value = user.Email.Trim();
                command.Parameters.Add("@password", System.Data.SqlDbType.VarChar, 45).Value = user.Password.Trim();
                command.Parameters.Add("@firstname", System.Data.SqlDbType.VarChar, 45).Value = user.FirstName.Trim();
                try
                {
                    connection.Open();
                    if (command.ExecuteNonQuery() > 0)
                        success = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
            return success;
        }

        /// <summary>
        /// Find and return User object using their id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public User FindById(int Id)
        {
            User foundUser = null;

            string sqlStatement = "SELECT * FROM dbo.[USER] WHERE ID=@id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                command.Parameters.Add("@id", System.Data.SqlDbType.Int, 11).Value = Id;

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        foundUser = new User((int)reader["ID"], (string)reader["EMAIL"], (string)reader["PASSWORD"], (string)reader["FIRST_NAME"]);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
            return foundUser;
        }

        /// <summary>
        /// Return all records from USER db table
        /// </summary>
        /// <returns></returns>
        public List<User> FindAll()
        {
            List<User> res = new List<User>();

            string sqlStatement = "SELECT * FROM dbo.[USER]";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        User u1 = new User((int)reader["ID"], (string)reader["EMAIL"], (string)reader["PASSWORD"], (string)reader["FIRST_NAME"]);
                        res.Add(u1);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }

            return res;
        }

        /// <summary>
        /// Update user info in USER db
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool UpdateUser(User user)
        {
            bool success = false;

            string sqlStatement = "UPDATE dbo.[USER] SET FIRST_NAME = @firstname, EMAIL = @email, PASSWORD = @password WHERE ID = @id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = user.Id;
                command.Parameters.Add("@firstname", System.Data.SqlDbType.VarChar, 10).Value = user.FirstName;
                command.Parameters.Add("@email", System.Data.SqlDbType.VarChar, 45).Value = user.Email;
                command.Parameters.Add("@password", System.Data.SqlDbType.VarChar, 45).Value = user.Password;

                try
                {
                    connection.Open();
                    if (command.ExecuteNonQuery() > 0)
                        success = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
            return success;
        }
    }
}
