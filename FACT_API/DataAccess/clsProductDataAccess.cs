using System.Data;
using FACT_API.Model;
using Microsoft.Data.SqlClient;

namespace FACT_API.DataAccess
{
    public class clsProductDataAccess
    {

        public static DataTable getProductsAsDataTable ()
        {
            DataTable dataTable = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();

                    string cmd = "SP_GetProducts";
                    using (SqlCommand command = new SqlCommand(cmd, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

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

        public static List<clsProduct> getProducts()
        {

            DataTable dataTable = getProductsAsDataTable();

            List<clsProduct> list = new List<clsProduct>();

            if (dataTable == null)
            {
                return new List<clsProduct>();
            }

            foreach (DataRow row in dataTable.Rows)
            {

                clsProduct product = new clsProduct
                {
                    productID = Convert.ToInt32(row["ProductID"]),
                    serialNumber = row["SerialNumber"].ToString(),
                    name = row["Name"].ToString(),
                    description = row["Description"].ToString(),
                    price = Convert.ToDecimal(row["Price"])

                };

                list.Add(product);

            }


            return list;

        }
    }
}
