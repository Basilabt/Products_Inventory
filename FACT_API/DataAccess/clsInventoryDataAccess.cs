using System.Data;
using FACT_API.Model;
using Microsoft.Data.SqlClient;


namespace FACT_API.DataAccess
{
    public class clsInventoryDataAccess
    {

        public static DataTable getUserInventoriesByUserIDAsDataTable(int userID)
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

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                dataTable.Load(reader);
                            }
                        }
                    }
                }

            }
            catch (Exception exception)
            {
                Console.WriteLine($"DEBUG: {exception.Message}");
            }



            return dataTable;
        }

        public static List<clsUserInventory> getUserInventoriesByUserID(int userID)
        {
            DataTable dataTable = getUserInventoriesByUserIDAsDataTable(userID);

            List<clsUserInventory> list = new List<clsUserInventory>();

            if(dataTable == null)
            {
                return new List<clsUserInventory>();
            }

            foreach(DataRow row in dataTable.Rows)
            {

                clsUserInventory inventory = new clsUserInventory
                {
                    inventoryID = Convert.ToInt32(row["inventoryID"]),
                    serialNumber = row["serialNumber"].ToString(),
                    name = row["name"].ToString(),
                    description = row["description"].ToString(),
                    unitPrice = Convert.ToDecimal(row["unitPrice"]),
                    quantity = Convert.ToInt32(row["quantity"]),
                    total = Convert.ToDecimal(row["total"])
                };

                list.Add(inventory);

            }


            return list;
        }
     

    }
}
