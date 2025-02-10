using FactWebsiteApp.Models.DataAccess;
using System.Data;

namespace FactWebsiteApp.Models.Business
{
    public class clsUserInventory
    {
        public struct stInventoryItem
        {
            public int inventoryID { get; set; }


            public string serialNumber { get; set; }
            public string name { get; set; }
            public string description { get; set; }
            public decimal unitPrice { get; set; }
            public int quantity { get; set; }
            public decimal total { get; set; }
        }

        public static DataTable getUserInventoriesByUserIDAsDataTable(int userID)
        {
            return clsUserInventoryDataAccess.getUserInventoriesByUserID(userID);
        }

        public static List<stInventoryItem> getUserInventoriesByUserIDAsList(int userID)
        {
            List<stInventoryItem> inventoryList = new List<stInventoryItem>();

            DataTable dataTable = getUserInventoriesByUserIDAsDataTable(userID);

            foreach(DataRow row in  dataTable.Rows)
            {
                stInventoryItem item = new stInventoryItem();
                item.inventoryID = Convert.ToInt32(row["InventoryID"]);
                item.serialNumber = row["SerialNumber"].ToString();
                item.name = row["Name"].ToString();
                item.description = row["Description"].ToString();
                item.unitPrice = Convert.ToDecimal(row["UnitPrice"]);
                item.quantity = Convert.ToInt32(row["Quantity"]);
                item.total = Convert.ToDecimal(row["Total"]);

                inventoryList.Add(item);
            }


            return inventoryList;
        }
         

        public static int addToUserInventory(int userID , int productID,int quantity)
        {
            return clsUserInventoryDataAccess.addToUserInventory(userID, productID,quantity);
        }

        public static bool deleteUserInventoryByInventoryID(int inventoryID)
        {
            return clsUserInventoryDataAccess.deleteUserInventoryByInventoryID(inventoryID);
        }

    }
}
