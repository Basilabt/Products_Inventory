using FactWebsiteApp.Models.Business;
using FactWebsiteApp.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace FactWebsiteApp.Controllers
{
    public class UserController : Controller
    {

        [HttpGet]
        public IActionResult UserInfo()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Delete()
        {
            return View();
        }

        [HttpGet] 
        public IActionResult Update()
        {

            clsRegisterViewModel model = new clsRegisterViewModel
            {
                username = clsGlobal.loggedUser.username,
                password = clsGlobal.loggedUser.password,
                firstname = clsGlobal.loggedUser.person.firstName,
                secondname = clsGlobal.loggedUser.person.secondName,
                thirdname = clsGlobal.loggedUser.person.thirdName,
                lastname = clsGlobal.loggedUser.person.lastName,
                email = clsGlobal.loggedUser.person.email,
                phonenumber = clsGlobal.loggedUser.person.phoneNumber,
                isMale = clsGlobal.loggedUser.person.isMale
            };

            return View(model);
        }



        [HttpPost]
        public IActionResult DeleteUser()
        {

            clsUser user = clsGlobal.loggedUser;
            clsPerson person = clsPerson.getPersonByPersonID(user.personID);

            user.mode = clsUser.enMode.Delete;
            person.mode = clsPerson.enMode.Delete;

            if(user.save() &&  person.save())
            {
                TempData["Message"] = "Account deleted successfully!";
                return RedirectToAction("Login", "Authentication");
            }

            return Delete();
        }

        [HttpPost]
        public IActionResult Update(clsRegisterViewModel model) 
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string currentUsername = clsGlobal.loggedUser.username;

            if (currentUsername != model.username && clsUser.doesUserExist(model.username))
            {
                ModelState.AddModelError("username", "username already exists !");
                return View(model);
            }

 
            clsGlobal.loggedUser.username = model.username;
            clsGlobal.loggedUser.password = model.password;

            clsGlobal.loggedUser.person.firstName = model.firstname;
            clsGlobal.loggedUser.person.secondName = model.secondname;
            clsGlobal.loggedUser.person.thirdName = model.thirdname;
            clsGlobal.loggedUser.person.lastName = model.lastname;
            clsGlobal.loggedUser.person.email = model.email;
            clsGlobal.loggedUser.person.phoneNumber = model.phonenumber;
            clsGlobal.loggedUser.person.gender = (model.isMale) ? clsPerson.enGender.Male : clsPerson.enGender.Female;


            if(clsGlobal.loggedUser.save() && clsGlobal.loggedUser.person.save())
            {
                TempData["Message"] = "Account updated successfully!";
            }


            return View(model);            
        }

    }
}
