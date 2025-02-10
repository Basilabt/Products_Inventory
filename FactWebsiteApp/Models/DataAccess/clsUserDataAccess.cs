

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using FactWebsiteApp.Models.Business;


namespace FactWebsiteApp.Models.DataAccess
{
    public class clsUserDataAccess
    {

        public static bool doesUserExist(string username)
        {
            bool isFound = false;
            
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();

                    string cmd = "SP_DoesUserExist";
                    using (SqlCommand command = new SqlCommand(cmd, connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Username", username);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            if (reader.Read())
                            {
                                isFound = true;
                            }

                        }
                    }

                }


            }
            catch (Exception exception)
            {
                Console.WriteLine($"DEBUG: {exception.Message}");
            }

            return isFound;
        }

        public static bool login(ref int userID, ref int personID, string username, string password, ref bool isActive)
        {
            bool isSuccesfull = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();

                    string cmd = "SP_Login";
                    using (SqlCommand command = new SqlCommand(cmd, connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Password", password);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {


                            if (reader.Read())
                            {
                                Console.WriteLine("DEBUG: User Validated");
                                userID = (int)reader["UserID"];
                                personID = (int)reader["PersonID"];
                                username = (string)reader["Username"];
                                password = (string)reader["Password"];
                                isActive = (bool)reader["IsActive"];

                                isSuccesfull = true;
                            }

                        }
                    }

                }


            }
            catch (Exception exception)
            {
                Console.WriteLine($"DEBUG: {exception.Message}");
            }

            return isSuccesfull;
        }

        public static bool isUserActive(string username)
        {
            bool isActive = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();

                    string cmd = "SP_IsUserActive";
                    using (SqlCommand command = new SqlCommand(cmd, connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Username", username);


                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            if (reader.Read())
                            {
                                isActive = true;
                            }

                        }
                    }

                }


            }
            catch (Exception exception)
            {
                Console.WriteLine($"DEBUG: {exception.Message}");
            }

            return isActive;
        }

        public static int addNewUser(int personID , string username , string password , bool isActive)
        {
            int insertedUserID = -1;


            try
            {
                using(SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();

                    string cmd = "SP_AddNewUser";
                    using(SqlCommand command = new SqlCommand(cmd,connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@PersonID", personID);
                        command.Parameters.AddWithValue("@Username",username);
                        command.Parameters.AddWithValue("@Password", password);
                        command.Parameters.AddWithValue("@IsActive", isActive);


                        object result = command.ExecuteScalar();

                        if(result != null && int.TryParse(result.ToString(),out int id))
                        {
                            insertedUserID = id; 
                        }

                    }

                }

            } catch (Exception exception)
            {
                Console.WriteLine($"DEBUG: {exception.Message}");
            }


            return insertedUserID;

        }

        public static bool deleteUserByUserID(int userID)
        {
            int numberOfAffectedRows = 0;

            try
            {
                using(SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();

                    string cmd = "SP_DeleteUserByUserID";
                    using(SqlCommand command = new SqlCommand(cmd,connection))
                    {

                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserID",userID);

                        numberOfAffectedRows = command.ExecuteNonQuery();

                    }

                }
 


            } catch (Exception exception)
            {
                Console.WriteLine($"DEBUG: {exception.Message}");
            }



            return numberOfAffectedRows >= 1;
        }

        public static bool deleteUserByUsername(string username)
        {
            int numberOfAffectedRows = 0;

            try
            {
                using(SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();


                    string cmd = "SP_DeleteUserByUsername";
                    using(SqlCommand command = new SqlCommand(cmd, connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Username", username);
                         

                        numberOfAffectedRows = command.ExecuteNonQuery();
                    }


                }


            } catch (Exception exception)
            {
                Console.WriteLine($"DEBUG: {exception.Message}");
            }


            return numberOfAffectedRows >= 1;
        }

        public static bool updateUserByUserID(int userID , int personID , string username , string password , bool isActive)
        {
            int numberOfAffectedRows = 0;

            try
            {
                using(SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();

                    string cmd = "SP_UpdateUserByUserID";
                    using(SqlCommand command = new SqlCommand(cmd,connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserID",userID);
                        command.Parameters.AddWithValue("@PersonID",personID);
                        command.Parameters.AddWithValue("@Username",username);
                        command.Parameters.AddWithValue("@Password",password);
                        command.Parameters.AddWithValue("@IsActive", isActive);

                        numberOfAffectedRows = command.ExecuteNonQuery();

                    }

                }



            }
            catch(Exception exception) 
            {
                Console.WriteLine($"DEBUG: {exception.Message}");
            }


            return numberOfAffectedRows >= 1;
        }
    }
}
