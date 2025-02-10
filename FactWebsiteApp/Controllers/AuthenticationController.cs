using FactWebsiteApp.Models.Business;
using Microsoft.AspNetCore.Mvc;

using FactWebsiteApp.Models;
using FactWebsiteApp.Models.ViewModel;

namespace FactWebsiteApp.Controllers
{
    public class AuthenticationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(clsLoginViewModel model)
        {            
            if(!ModelState.IsValid)
            {
                return View(model);
            }
 
            if(!clsUser.doesUserExist(model.username))
            {
                ModelState.AddModelError("username", "Username does not exist !");
                return View(model);
            } 
            else if(!clsUser.isUserActive(model.username))
            {
                ModelState.AddModelError("username", "User account is not active !");
                return View(model);
            }
            else if(!clsUser.login(model.username, model.password))
            {
                ModelState.AddModelError("username", "Incorrect username or password");
                return View(model);
            }


            return RedirectToAction("Main", "Main");
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Register(clsRegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (clsUser.doesUserExist(model.username))
            {
                ModelState.AddModelError("username", "username already exists !");
                return View(model);
            }


            clsPerson person = new clsPerson();
            
            person.mode = clsPerson.enMode.AddNew;
            person.firstName = model.firstname;
            person.secondName = model.secondname;
            person.thirdName = model.thirdname;
            person.lastName = model.lastname;
            person.email = model.email;
            person.phoneNumber = model.phonenumber;
            person.gender = (model.isMale) ? clsPerson.enGender.Male : clsPerson.enGender.Female;

            if(!person.save())
            {
                ModelState.AddModelError("save_error", "Failed to create new user");
                return View();
            }

            clsUser user = new clsUser();

            user.mode = clsUser.enMode.AddNew;
            user.personID = person.personID;
            user.username = model.username;
            user.password = model.password;
            user.isActive = true;

            if(!user.save())
            {
                ModelState.AddModelError("save_error", "Failed to create new user");
                return View();
            }

            TempData["Message"] = "New account Created successfully!";

            return RedirectToAction("Login", "Authentication");
        }

        [HttpGet]
        public IActionResult Logout()
        {
            clsGlobal.loggedUser = null;

            return RedirectToAction("Login", "Authentication");
        }

    }
}
