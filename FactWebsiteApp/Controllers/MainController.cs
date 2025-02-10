using Microsoft.AspNetCore.Mvc;

namespace FactWebsiteApp.Controllers
{
    public class MainController : Controller
    {
        [HttpGet]
        public IActionResult Main()
        {
            return View();
        }


       
    }
}
