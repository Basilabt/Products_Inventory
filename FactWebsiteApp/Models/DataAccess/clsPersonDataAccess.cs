using Microsoft.Data.SqlClient;

namespace FactWebsiteApp.Models.DataAccess
{
    public class clsPersonDataAccess
    {
        public static int addNewPerson(string firstName , string secondName , string thirdName , string lastName , string email , string phoneNumber , bool isMale )
        {
            int insertedPersonID = -1;

            try
            {
                using(SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();

                    string cmd = "SP_AddNewPerson";
                    using(SqlCommand command = new SqlCommand(cmd, connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@FirstName",firstName);
                        command.Parameters.AddWithValue("@SecondName", secondName);
                        command.Parameters.AddWithValue("@ThirdName", thirdName);
                        command.Parameters.AddWithValue("@LastName", lastName);
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@PhoneNumber",phoneNumber);
                        command.Parameters.AddWithValue("@Gender", isMale);

                        object result = command.ExecuteScalar();

                        if(result != null && int.TryParse(result.ToString(),out int id))
                        {
                            insertedPersonID = id;
                        }


                    }

                }

            } catch( Exception exception) 
            {
                Console.WriteLine($"DEBUG: {exception.Message}");
            }

            return insertedPersonID;
        }

        public static bool getPersonByPersonID(int personID , ref string firstName, ref string secondName, ref string thirdName, ref string lastName, ref string email, ref string phoneNumber, ref bool gender)
        {
            bool isFound = false;

            try
            {
               using(SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
               {
                    connection.Open();

                    string cmd = "SP_GetPersonByPersonID";
                    using (SqlCommand command = new SqlCommand(cmd,connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@PersonID", personID);

                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            if(reader.Read())
                            {
                                isFound = true;

                                firstName = (string)reader["FirstName"];
                                secondName = (string)reader["SecondName"];
                                thirdName = (string)reader["ThirdName"];
                                lastName = (string)reader["LastName"];
                                email = (string)reader["Email"];
                                phoneNumber = (string)reader["PhoneNumber"];
                                gender = (bool)reader["Gender"];
                                
                            }

                        }

                    }
                }


            }
            catch( Exception exception )
            {
                Console.WriteLine($"DEBUG: {exception.Message}");
            }



            return isFound;
        }

        public static bool deletePersonByPersonID(int personID)
        {
            int numberOfAffectedRows = 0;

            try
            {
                using(SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();

                    string cmd = "SP_DeletePersonByPersonID";
                    using(SqlCommand command = new SqlCommand(cmd,connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PersonID", personID);

                        numberOfAffectedRows = command.ExecuteNonQuery();                        
                    }
                }

            } catch( Exception exception )
            {
                Console.WriteLine($"DEBUG: {exception.Message}");
            }

            return numberOfAffectedRows >= 1;
        }

        public static bool updatePersonByPersonID(int personID, string firstName, string secondName, string thirdName, string lastName , string email , string phoneNumber , bool isMale)
        {
            int numberOfAffectedRows = 0;

            try
            {
                using(SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();

                    string cmd = "SP_UpdatePersonByPersonID";
                    using(SqlCommand command = new SqlCommand(cmd,connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PersonID",personID);
                        command.Parameters.AddWithValue("@FirstName",firstName);
                        command.Parameters.AddWithValue("@SecondName",secondName);
                        command.Parameters.AddWithValue("@ThirdName",thirdName);
                        command.Parameters.AddWithValue("@LastName", lastName);
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                        command.Parameters.AddWithValue("@Gender", isMale);
                        
                        numberOfAffectedRows = command.ExecuteNonQuery();

                    }

                }


            } catch( Exception exception )
            {
                Console.WriteLine($"DEBUG: {exception.Message}");
            }

            return numberOfAffectedRows >= 1;
        }
    }
}
