using FactWebsiteApp.Models.Business;
using FactWebsiteApp.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace FactWebsiteApp.Controllers
{
    public class ProductController : Controller
    {

        [HttpGet]
        public IActionResult Main()
        {
            clsProductViewModel model = new clsProductViewModel();

            return View(model);
        }


        [HttpGet]
        public IActionResult Update(int productID) 
        {
            clsProductViewModel model = new clsProductViewModel();

         
            clsProduct product = clsProduct.getProductByProductID(productID);
         
            model.productID = productID;
            model.serialNumber = product.serialNumber;
            model.name = product.name;
            model.description = product.description;
            model.price = product.price;



            return View(model);
        }

        [HttpPost]
        public IActionResult UpdateInfo(clsProductViewModel model)
        {
          
            if(!ModelState.IsValid)
            {
                return View("Update",model);
            }

           

            clsProduct product = clsProduct.getProductByProductID(model.productID);

            product.serialNumber = model.serialNumber;
            product.name = model.name;
            product.description = model.description;
            product.price = model.price;
            product.mode = clsProduct.enMode.Update;

            if(product.save())
            {
                TempData["Message"] = "Product updated successfully!";
                model.updateProductsList();
                return RedirectToAction("Main");
            }


            return View("Update",model);
        }

        [HttpPost]
        public IActionResult Delete(clsProductViewModel model)
        {
            Console.WriteLine($"ProductID: {model.productID}");

            clsProduct product = clsProduct.getProductByProductID(model.productID);
            product.mode = clsProduct.enMode.Delete;

            

            if (product.save())
            {
                TempData["Message"] = "Product deleted successfully!";
                model.updateProductsList();
                return RedirectToAction("Main");
            }

            return View("Update", model);
        }
        
  
    }
}
