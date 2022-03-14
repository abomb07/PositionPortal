/**
 * Adam Bender
 * CST452 Mark Reha
 * 2/6/22
 * Position Data service
 **/

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PositionPortal.Models.Data
{
    public class PositionDataService
    {
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=PositionPortal;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        /// <summary>
        /// Insert record into POSITION db table
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public bool Insert(Position position)
        {
            bool success = false;
            string sqlStatement = "INSERT INTO dbo.[POSITION] (TYPE, NAME, QUANTITY, PRICE, DATE, USER_ID) VALUES (@type, @name, @quantity, @price, @date, @user_id)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                command.Parameters.Add("@type", System.Data.SqlDbType.Int).Value = position.Type;
                command.Parameters.Add("@name", System.Data.SqlDbType.NVarChar, 11).Value = position.Name;
                command.Parameters.Add("@quantity", System.Data.SqlDbType.Float).Value = position.Quantity;
                command.Parameters.Add("@price", System.Data.SqlDbType.Float).Value = position.Price;
                command.Parameters.Add("@date", System.Data.SqlDbType.Date).Value = position.Date;
                command.Parameters.Add("@user_id", System.Data.SqlDbType.Int).Value = position.UserId;


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
        /// Find and return list of position for a user
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public List<Position> FindByUserId(int UserId)
        {
            List<Position> res = new List<Position>();
            Position pos = null;

            string sqlStatement = "SELECT * FROM dbo.[POSITION] WHERE USER_ID=@user_id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                command.Parameters.Add("@user_id", System.Data.SqlDbType.Int).Value = UserId;

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        pos = new Position((int)reader[0], (int)reader[1], (string)reader[2], (double)reader[3], (double)reader[4], (DateTime)reader[5], (int)reader[6]);
                        res.Add(pos);
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
        /// Update position info in POSITION db
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public bool Update(Position position)
        {
            bool success = false;

            string sqlStatement = "UPDATE dbo.[POSITION] SET TYPE = @type, NAME = @name, QUANTITY = @quantity, PRICE = @price, DATE = @date, USER_ID = @user_id WHERE ID = @id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = position.Id;
                command.Parameters.Add("@type", System.Data.SqlDbType.Int).Value = position.Type;
                command.Parameters.Add("@name", System.Data.SqlDbType.NVarChar, 11).Value = position.Name;
                command.Parameters.Add("@quantity", System.Data.SqlDbType.Float).Value = position.Quantity;
                command.Parameters.Add("@price", System.Data.SqlDbType.Float).Value = position.Price;
                command.Parameters.Add("@date", System.Data.SqlDbType.Date).Value = position.Date;
                command.Parameters.Add("@user_id", System.Data.SqlDbType.Int).Value = position.UserId;

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
        /// Delete position by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            bool success = false;

            string sqlStatement = "DELETE FROM dbo.[POSITION] WHERE ID = @id";

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
    }
}
