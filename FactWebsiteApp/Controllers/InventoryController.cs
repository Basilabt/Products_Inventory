using FactWebsiteApp.Models.Business;
using FactWebsiteApp.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace FactWebsiteApp.Controllers
{
    public class InventoryController : Controller
    {
        [HttpGet]
        public IActionResult Main()
        {
            clsInventoryViewModel model = new clsInventoryViewModel();

            return View(model);
        }


        [HttpPost]
        public IActionResult Add(clsInventoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Main",model);
            }

            clsUserInventory.addToUserInventory(clsGlobal.loggedUser.userID, model.productID, model.productQuantity);

          
            model.updateInventoryList();

            return View("Main",model);
        }


        [HttpPost]
        public  IActionResult Delete(clsInventoryViewModel model)
        {
            clsUserInventory.deleteUserInventoryByInventoryID(model.inventoryID);
           
            model.updateInventoryList();

            return View("Main", model);
        }       


    }
}
