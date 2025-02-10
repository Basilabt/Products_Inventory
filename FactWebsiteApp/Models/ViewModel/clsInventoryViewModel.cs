using FactWebsiteApp.Models.Business;
using System.ComponentModel.DataAnnotations;

namespace FactWebsiteApp.Models.ViewModel
{
    public class clsInventoryViewModel
    {
        public int inventoryID { get; set; }


        [Required(ErrorMessage = "Product is required")]
        public int productID { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        public int productQuantity { get; set; }
       
        public  List<clsProduct> productList = clsProduct.getProductsAsList();

        public List<clsUserInventory.stInventoryItem> userInventories = clsUserInventory.getUserInventoriesByUserIDAsList(clsGlobal.loggedUser.userID);
        public void updateInventoryList()
        {
           this.userInventories = clsUserInventory.getUserInventoriesByUserIDAsList(clsGlobal.loggedUser.userID);
        }

    }
}
