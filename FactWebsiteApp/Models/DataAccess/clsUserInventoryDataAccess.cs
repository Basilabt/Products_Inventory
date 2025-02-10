using Microsoft.Data.SqlClient;
using System.Data;

namespace FactWebsiteApp.Models.DataAccess
{
    public class clsUserInventoryDataAccess
    {
        public static DataTable getUserInventoriesByUserID(int userID)
        {
            DataTable dataTable = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();

                    string cmd = "SP_GetUserInventoriesByUserID";
                    using (SqlCommand command = new SqlCommand(cmd, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserID", userID);

                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            if(reader.HasRows)
                            {
                                dataTable.Load(reader);
                            }
                        }
                    }
                }

            } catch (Exception exception)
            {
                Console.WriteLine($"DEBUG: {exception.Message}");
            }



            return dataTable;
        }

        public static int addToUserInventory(int userID , int productID,int quantity) 
        {
            int newInventoryID = -1;

            try
            {
                using(SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();

                    string cmd = "SP_AddToUserInventory";
                    using(SqlCommand command = new SqlCommand(cmd, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserID", userID);
                        command.Parameters.AddWithValue("@ProductID", productID);
                        command.Parameters.AddWithValue("@Quantity", quantity);

                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int id))
                        {
                            newInventoryID = id;
                        }


                    }



                }

            } catch (Exception exception)
            {
                Console.WriteLine($"DEBUG: {exception.Message}");
            }



            return newInventoryID;
        }

        public static bool deleteUserInventoryByInventoryID(int inventoryID)
        {
            int numberOfAffectedRows = 0;

            try
            {
                using(SqlConnection connection = new SqlConnection (clsDataAccessSettings.connectionString))
                {
                    connection.Open();


                    string cmd = "SP_DeleteUserInventoryByInventoryID";
                    using(SqlCommand command = new SqlCommand (cmd, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@InventoryID",inventoryID);

                        numberOfAffectedRows = command.ExecuteNonQuery();

                    }

                }


            } catch (Exception exception)
            {
                Console.WriteLine($"DEBUG: {exception.Message}");
            }


            return numberOfAffectedRows >= 1;
        }
    }
}
