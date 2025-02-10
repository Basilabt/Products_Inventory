using Microsoft.Data.SqlClient;
using System.Data;

namespace FactWebsiteApp.Models.DataAccess
{
    public class clsProductDataAccess
    {
       public static DataTable getProducts()
        {
            DataTable dataTable = new DataTable();

            try
            {
                using(SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();

                    string cmd = "SP_GetProducts";
                    using(SqlCommand command = new SqlCommand(cmd, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
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

       public static bool updateProduct(int productID , string serialNumber , string name , string description , Decimal price)
        {
            int numberOfAffectedRows = 0;


            try
            {

                using(SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString)) 
                {
                    connection.Open();

                    string cmd = "SP_EditProductByProductID";
                    using(SqlCommand command = new SqlCommand(cmd,connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ProductID",productID);
                        command.Parameters.AddWithValue("@SerialNumber",serialNumber);
                        command.Parameters.AddWithValue("@Name",name);
                        command.Parameters.AddWithValue("@Description",description);
                        command.Parameters.AddWithValue("@Price",price);

                        numberOfAffectedRows = command.ExecuteNonQuery();

                    }
                
                
                }



            } catch (Exception exception)
            {
                Console.WriteLine($"DEBUG: {exception.Message}");
            }




            return numberOfAffectedRows >= 1;
        }

       public static bool getProductByProductID(int productID , ref string serialNumber , ref string name , ref string description , ref Decimal price)
       {
            bool isFound = false;

            try
            {

                using(SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();

                    string cmd = "SP_GetProductByProductID";
                    using(SqlCommand command=new SqlCommand(cmd,connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ProductID", productID);


                        using(SqlDataReader reader = command.ExecuteReader())
                        {

                            if(reader.Read())
                            {
                                isFound = true; 

                                serialNumber = (string)reader["SerialNumber"];
                                name = (string)reader["Name"];
                                description = (string)reader["Description"];
                                price = (decimal)reader["Price"];
                            }

                        }

                    }

                }

            } catch (Exception exception)
            {
                Console.WriteLine($"DEBUG: {exception.Message}");
            }


            return isFound;

        }

       public static bool deleteProductByProductID(int productID)
        {
            int numberOfAffectedRows = 0;

            try
            {
                using(SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();

                    string cmd = "SP_DeleteProductByProductID";
                    using(SqlCommand command = new SqlCommand(cmd,connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ProductID", productID);

                        numberOfAffectedRows = command.ExecuteNonQuery();   
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Exception: {exception.Message}");
            }



            return numberOfAffectedRows >= 1;
        }
    }
}
